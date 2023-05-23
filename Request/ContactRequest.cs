using System.ComponentModel.DataAnnotations;

namespace kazariobranco_backend.Request;

public class ContactRequest
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Reason { get; set; }
    
    public string Description { get; set; }
}
