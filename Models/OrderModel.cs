namespace kazariobranco_backend.Models;

public class OrderModel
{
    public int Id { get; set; }

    public int CartId { get; set; }

    // RELATIONSHIPS

    public CartModel Cart { get; set; } = null!;

    public IList<ProductModel> Products { get; set; } = null!;

    public IList<OrderProductModel> OrderProducts { get; set; } = null!;
}
