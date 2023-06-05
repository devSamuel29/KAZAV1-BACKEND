namespace kazariobranco_backend.Models;

public class ProductModel 
{ 
    public int Id { get; set; }

    // RELATIONSHIPS
    public int CartId { get; set; }
    public CartModel Cart { get; set; }
}
