namespace Manatalol.Application.DTO.Educations
{
    public record EducationDto
    {
        public string? School { get; set; }
        public string? Degree { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}