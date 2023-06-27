namespace kazariobranco_backend.Response;

public class UserResponse
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public CartResponse Cart { get; set; }

    public List<AddressResponse> Addresses { get; set; }
}
