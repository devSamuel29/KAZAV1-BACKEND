namespace kazariobranco_backend.Models;

public class OrderModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int CartId { get; set; }

    // RELATIONSHIPS

    public CartModel Cart { get; set; } = null!;

    public IList<ProductModel> Products { get; set; } = null!;

}
