namespace kazariobranco_backend.Models;

public class ForgottenPasswordModel
{
    public int Id { get; set; }

    public required string Email { get; set; }

    public int Attempts { get; set; }

    public DateTime IsValid { get; set; } = DateTime.Now.AddMinutes(5);
}