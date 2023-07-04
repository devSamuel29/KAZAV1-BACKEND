namespace kazariobranco_backend.Request.Contact;

public class ContactRequest
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public required string Reason { get; set; }
    
    public required string Description { get; set; }
}
