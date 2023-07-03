namespace kazariobranco_backend.Response;

public class ReadAllContactsResponse
{
    public int Size { get; set; }

    public int From { get; set; }

    public int To { get; set; }

    public int Quantity { get; set; }
    
    public required IList<ContactResponse> Contacts { get; set; }
}
