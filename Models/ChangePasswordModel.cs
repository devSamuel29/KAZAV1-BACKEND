namespace kazariobranco_backend.Models;

public class ChangePasswordModel
{
    public int Id { get; set; }

    public int Code { get; set; }

    public required string Email { get; set; }

    public DateTime IsValid { get; set; } = DateTime.Now.AddMinutes(15);
    
    public bool IsFinished { get; set; } = false;
}
