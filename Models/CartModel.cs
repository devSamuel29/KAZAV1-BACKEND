namespace kazariobranco_backend.Models;

public class CartModel
{
    public int Id { get; set; }

    // RELATIONSHIPS
    public IList<OrderModel> Orders { get; set; }

    // FK

    public int UserId { get; set; }

    public UserModel User { get; set; }
}
