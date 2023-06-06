namespace kazariobranco_backend.Models;

public class OrderModel
{
    public int Id { get; set; }

    // RELATIONSHIPS

    public CartModel Cart { get; set; }

    public IList<ProductModel> Products { get; set; }

    public IList<OrderProductModel> OrderProducts { get; set; }
    // FK

    public int CartId { get; set; }
}
