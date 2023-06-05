namespace kazariobranco_backend.Models;

public class UserModel
{
    public int Id { get; }

    public string Role { get; set; }

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Cpf { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // FK
    
    public CartModel Cart { get; set; }

    public IList<AddressModel> Addresses { get; set; }
}
