namespace kazariobranco_backend.Models;

public class ProductModel 
{ 
    public int Id { get; set; }
    public IList<OrderModel> Orders { get; set; }
    public IList<OrderProductModel> OrderProducts { get; set; }
}
