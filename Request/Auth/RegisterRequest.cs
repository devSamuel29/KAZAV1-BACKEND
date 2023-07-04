namespace kazariobranco_backend.Request.Auth;

public class RegisterRequest
{
    public required string Firstname { get; set; }

    public required string Lastname { get; set; }

    public required string Cpf { get; set; }

    public required string Phone { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
