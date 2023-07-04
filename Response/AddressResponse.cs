namespace kazariobranco_backend.Response;

public class AddressResponse 
{
    public required string Address { get; set; }

    public required int Number { get; set; }

    public required string District { get; set; }

    public required string State { get; set; }

    public required string City { get; set; }

    public int ZipCode { get; set; }
}