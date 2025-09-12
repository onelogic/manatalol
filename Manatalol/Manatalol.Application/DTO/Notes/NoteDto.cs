namespace Manatalol.Application.DTO.Notes
{
    public record NoteDto
    {
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
    }
}
