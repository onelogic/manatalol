namespace Manatalol.Application.DTO.Experiences
{
    public record ExperienceDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }

        public string? Description { get; set; }
    }
}
