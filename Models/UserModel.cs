namespace kazariobranco_backend.Models;

public class UserModel
{
    public int Id { get; }

    public string Role { get; set; } = "user";

    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Cpf { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // RELATIONSHIPS
    
    public virtual CartModel Cart { get; set; } = new CartModel();

    public IList<AddressModel> Addresses { get; set; } = new List<AddressModel>();
}
