namespace kazariobranco_backend.Response;

public class ContactResponse
{
    public int Id { get; set; }
    
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public required string Reason { get; set; }

    public required string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsFinished { get; set; }
}
