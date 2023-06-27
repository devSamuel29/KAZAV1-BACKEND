namespace kazariobranco_backend.Models;

public class ContactModel
{
    public int Id { get; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Reason { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime EndedAt { get; set; } = DateTime.Now;
}
