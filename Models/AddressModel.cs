namespace kazariobranco_backend.Models;

public class AddressModel
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Address { get; set; }

    public int Number { get; set; }

    public string District { get; set; }

    public string City { get; set; }

    public string State { get; set; }
    
    public int ZipCode { get; set; }

    //

    public UserModel User { get; set; }
}
