using System.ComponentModel.DataAnnotations;

namespace kazariobranco_backend.Request;

public class RegisterRequest
{
    public string Firstname { get; set; }

    public string Lastname { get; set; }

    public string Cpf { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
