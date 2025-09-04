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
            return new List<Experience>
        {
            new Experience { CompanyName = "Example Corp", StartDate = DateTime.Parse("2020-01-01"), EndDate = null }
        };
        }

        private static List<Education> ExtractEducation(string text)
        {
            return new List<Education> {};
        }
    }

}
