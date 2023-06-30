namespace kazariobranco_backend.Request;

public class AddNewAddressRequest
{
    public string Address { get; set; }

    public int Number { get; set; }

    public string District { get; set; }

    public string City { get; set; }

    public string State { get; set; }
    
    public int ZipCode { get; set; }
}
