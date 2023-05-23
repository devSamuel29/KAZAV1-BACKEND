namespace kazariobranco_backend.Models;

public class ContactModel
{
    public int Id { get; set; }
   
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Reason { get; set; }

    public string Description { get; set; }

    private DateTime _createdAt;

    public DateTime CreatedAt
    {
        get { return _createdAt.Date; }
        set { _createdAt = value; }
    }

    public DateTime EndedAt { get; set; }
}
