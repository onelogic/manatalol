using Manatalol.Domain.Entities;
using System.Text.RegularExpressions;

namespace Manatalol.Application.Helpers
{
    public static class PdfParser
    {
        public static Candidate ExtractCandidate(string text)
        {
            var fullName = ExtractName(text);
            return new Candidate
            {
                Email = ExtractEmail(text),
                FirstName = fullName.Split(' ')?.FirstOrDefault() ?? "",
                LastName = fullName?.Split(' ')?.Skip(1)?.FirstOrDefault() ?? "",
                Skills = ExtractSkills(text),
                Educations = ExtractEducation(text),
                Experiences = ExtractExperiences(text),
            };

        }
        private static string ExtractEmail(string text)
        {
            var match = Regex.Match(text, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-z]{2,}");
            return match.Success ? match.Value : null;
        }

        private static string ExtractPhone(string text)
        {
            var match = Regex.Match(text, @"(\+?\d{1,3}[\s-]?)?(\(?\d{2,3}\)?[\s-]?)?\d{2,4}[\s-]?\d{2,4}[\s-]?\d{2,4}");
            return match.Success ? match.Value : null;
        }

        private static string ExtractName(string text)
        {
            var lines = text.Split('\n')
                            .Select(l => l.Trim())
                            .Where(l => !string.IsNullOrEmpty(l))
                            .ToList();

            if (lines.Count > 0)
            {
                var firstLine = lines[0];

                if (!firstLine.ToLower().Contains("cv"))
                    return firstLine;
            }

            return null;
        }

        private static List<Skill> ExtractSkills(string text)
        {
            var skills = new List<Skill>();
            if (text.Contains("Skills"))
            {
                var part = text.Split("Skills")[1];
                var lables = part.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(s => s.Trim())
                             .ToList();
                skills.AddRange(lables.Select(l => new Skill { Name = l }));
                    
            }
            return skills;
        }

        private static List<Experience> ExtractExperiences(string text)
        {
            var experiences = new List<Experience>();

            var regex = new Regex(@"(?i)(experience|expérience)(.*?)(education|formation|skills|compétences|$)", RegexOptions.Singleline);
            var match = regex.Match(text);

            if (match.Success)
            {
                var block = match.Groups[2].Value;

                var lines = block.Split('\n')
                                 .Select(l => l.Trim())
                                 .Where(l => l.Length > 3)
                                 .ToList();

                Experience current = null;

                foreach (var line in lines)
                {
                    var dateRegex = new Regex(@"((Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec|jan|fév|mar|avr|mai|juin|juil|août|sep|oct|nov|déc)[a-z]*\s*\d{4})|((19|20)\d{2})");
                    var dateMatches = dateRegex.Matches(line);

                    bool isDateLine = dateMatches.Count > 0;

                    if (!isDateLine && (line.Contains("–") || line.Contains("-") || line.Contains(" at ") || line.Contains("@")))
                    {
                        if (current != null)
                        {
                            experiences.Add(current);
                            current = null;
                        }

                        var parts = line.Split(new[] { '–', '-' }, 2);
                        var position = parts[0].Trim();
                        var company = parts.Length > 1 ? parts[1].Trim() : "";

                        current = new Experience
                        {
                            Position = position,
                            CompanyName = company
                        };
                    }
                    else if (isDateLine && current != null)
                    {
                        var years = dateMatches.Select(m => m.Value).ToList();
                        var start = years.FirstOrDefault();
                        var end = years.Count > 1 ? years.Last() : null;

                        current.StartDate = TryParseDate(start);
                        current.EndDate = TryParseDate(end);
                    }
                    else if (current != null)
                    {
                        if (string.IsNullOrWhiteSpace(current.Description))
                            current.Description = line;
                        else
                            current.Description += " " + line;
                    }
                }

                if (current != null)
                    experiences.Add(current);
            }

            return experiences;
        }

        private static DateTime? TryParseDate(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            if (DateTime.TryParse(input, out var date))
                return date;

            if (int.TryParse(input, out var year))
                return new DateTime(year, 1, 1);

            if (Regex.IsMatch(input, "(present|actuel|aujourd'hui)", RegexOptions.IgnoreCase))
                return null;

            return null;
        }


        private static List<Education> ExtractEducation(string text)
        {
            var educations = new List<Education>();

             var regex = new Regex(@"(?i)(education|formation)(.*?)(experience|skills|compétences|$)", RegexOptions.Singleline);
            var match = regex.Match(text);

            if (match.Success)
            {
                var block = match.Groups[2].Value;

                var lines = block.Split('\n')
                                 .Select(l => l.Trim())
                                 .Where(l => l.Length > 3)
                                 .ToList();

                foreach (var line in lines)
                {
                  
                    var yearRegex = new Regex(@"(19|20)\d{2}");
                    var years = yearRegex.Matches(line).Select(m => m.Value).ToList();

                    int? startYear = years.Count > 0 ? int.Parse(years.First()) : (int?)null;
                    int? endYear = years.Count > 1 ? int.Parse(years.Last()) : (int?)null;

                    string degree = null;
                    string school = null;

                    if (line.Contains("–") || line.Contains("-"))
                    {
                        var parts = line.Split(new[] { '–', '-' }, 2);
                        school = parts[0].Trim();
                        degree = parts.Length > 1 ? parts[1].Trim() : "";
                    }
                    else if (line.Contains(":"))
                    {
                        var parts = line.Split(':', 2);
                        degree = parts[1].Trim();
                        school = parts[0].Trim();
                    }
                    else
                    {
                        degree = line;
                    }

                    degree = Regex.Replace(degree, @"\d{4}", "").Trim(' ', '-', '–', ':', ',');
                    school = Regex.Replace(school ?? "", @"\d{4}", "").Trim(' ', '-', '–', ':', ',');

                    educations.Add(new Education
                    {
                        Degree = degree,
                        School = school,
                        StartDate = new DateTime(startYear ?? 1, 1, 1),
                        EndDate = new DateTime(endYear ?? 1, 1, 1)
                    });
                }
            }

            return educations;
        }
    }
}
