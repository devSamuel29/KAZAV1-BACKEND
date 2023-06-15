namespace kazariobranco_backend.Models;

public class CartModel
{
    public int Id { get; set; }

    // FK

    public int UserId { get; set; }
    
    // RELATIONSHIPS
    public virtual UserModel User { get; set; }
    
    public virtual IList<OrderModel> Orders { get; set; }
}
