namespace kazariobranco_backend.Response;

public class CartResponse
{
    public int Id { get; set; }

    public IList<OrderResponse> Orders { get; set; } = null!;
}