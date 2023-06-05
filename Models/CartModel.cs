namespace kazariobranco_backend.Models;

public class CartModel
{
    public int Id { get; set; }


    // FK
    public int UserId { get; set; }

    public UserModel User { get; set; }
    
    public IList<ProductModel> Products { get; set; }
}
