using System.Text.Json.Serialization;

namespace Manatalol.Application.DTO.Api
{
    public class LinkedinProfile
    {
        [JsonPropertyName("profile_pic_url")]
        public string ProfilePictureUrl { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("occupation")]
        public string Occupation { get; set; }

        [JsonPropertyName("headline")]
        public string Headline { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("location_str")]
        public string LocationStr { get; set; }

        [JsonPropertyName("experiences")]
        public List<LinkedinExperience> Experiences { get; set; }

        [JsonPropertyName("education")]
        public List<LinkedinEducation> Education { get; set; }

        [JsonPropertyName("languages")]
        public List<string> Languages { get; set; }

        [JsonPropertyName("skills")]
        public List<string> Skills { get; set; }
    }

    public class LinkedinExperience
    {
        [JsonPropertyName("starts_at")]
        public DateDetail StartsAt { get; set; }

        [JsonPropertyName("ends_at")]
        public DateDetail EndsAt { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }
    }

    public class LinkedinEducation
    {
        [JsonPropertyName("starts_at")]
        public DateDetail StartsAt { get; set; }

        [JsonPropertyName("ends_at")]
        public DateDetail EndsAt { get; set; }

        [JsonPropertyName("field_of_study")]
        public string FieldOfStudy { get; set; }

        [JsonPropertyName("degree_name")]
        public string DegreeName { get; set; }

        [JsonPropertyName("school")]
        public string School { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class DateDetail
    {
        [JsonPropertyName("day")]
        public int? Day { get; set; }

        [JsonPropertyName("month")]
        public int? Month { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }
    }
}
