using System.ComponentModel.DataAnnotations;

namespace kazariobranco_backend.Request;

public class ContactRequest
{
    public string name { get; set; }

    public string email { get; set; }

    public string phone { get; set; }

    public string reason { get; set; }
    
    public string description { get; set; }
}
