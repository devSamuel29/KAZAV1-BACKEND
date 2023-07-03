namespace kazariobranco_backend.Models;

public class OrderProductModel 
{ 
    public int Id { get; set; }

    public int OrderId { get; set; }

    public OrderModel Order { get; set; } = null!;

    public int ProductId { get; set; }

    public ProductModel Product { get; set; } = null!;
}
