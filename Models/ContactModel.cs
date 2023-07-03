namespace kazariobranco_backend.Models;

public class ContactModel
{
    public int Id { get; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public required string Reason { get; set; }

    public required string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime EndedAt { get; set; } = DateTime.MinValue;
}
