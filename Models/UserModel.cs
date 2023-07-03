namespace kazariobranco_backend.Models;

public class UserModel
{
    public int Id { get; }

    public string Role { get; set; } = "user";

    public required string Firstname { get; set; }

    public required string Lastname { get; set; }

    public required string Cpf { get; set; }

    public required string Phone { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // RELATIONSHIPS
    
    public CartModel Cart { get; set; } = new CartModel();

    public IList<AddressModel> Addresses { get; set; } = new List<AddressModel>();
}
