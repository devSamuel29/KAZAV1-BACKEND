namespace kazariobranco_backend.Request.User;

public class CreteAddressRequest
{
    public required string Address { get; set; }

    public int Number { get; set; }

    public required string District { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }
    
    public int ZipCode { get; set; }
}
