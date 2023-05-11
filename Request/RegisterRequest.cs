using System.ComponentModel.DataAnnotations;

namespace kazariobranco_backend.Request;

public class RegisterRequest
{
    [Required]
    public string firstname { get; set; }

    [Required]
    public string lastname { get; set; }

    [Required]
    public string cpf { get; set; }

    [Required]
    public string phone { get; set; }

    [Required]
    public string email { get; set; }

    [Required]
    public string password { get; set; }
}
