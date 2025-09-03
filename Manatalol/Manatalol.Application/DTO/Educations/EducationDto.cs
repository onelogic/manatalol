namespace Manatalol.Application.DTO.Educations
{
    public record EducationDto
    {
        public string School { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
