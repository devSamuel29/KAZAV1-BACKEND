namespace kazariobranco_backend.Models;

public class ContactModel
{
    public int Id { get; set; }
   
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Reason { get; set; }

    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool Ended { get; set; }
}
