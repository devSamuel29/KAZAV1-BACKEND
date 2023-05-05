namespace kazariobranco_backend.Request;

public record LoginRequest
{ 
    public string email { get; set; }
    public string password { get; set; }
}
