namespace kazariobranco_backend.Models;

public class CartModel
{
    public int Id { get; set; }

    public int UserId { get; set; }
    
    // RELATIONSHIPS
    public UserModel User { get; set; } = null!;
    
    public IList<ProductModel> Products { get; set; } = null!;
}
