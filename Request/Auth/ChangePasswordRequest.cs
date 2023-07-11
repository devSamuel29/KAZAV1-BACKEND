namespace kazariobranco_backend.Request.Auth;

public class ChangePasswordRequest
{
    public required string Email { get; set; }

    public int Code { get; set; }
    
    public required string NewPassword { get; set; }
}
