namespace kazariobranco_backend.Models;

public class AddressModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public required string Address { get; set; }

    public int Number { get; set; }

    public required string District { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }
    
    public int ZipCode { get; set; }

    //

    public UserModel User { get; set; } = null!;
}
