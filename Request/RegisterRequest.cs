using System.ComponentModel.DataAnnotations;

namespace kazariobranco_backend.Request;

public class RegisterRequest
{
    [Required(ErrorMessage = "O primeiro nome é obrigatório!")]
    public string firstname { get; set; }

    [Required(ErrorMessage = "O sobrenome é obrigatório!")]
    public string lastname { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório!")]
    public string cpf { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório!")]
    public string phone { get; set; }

    [Required(ErrorMessage = "O email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória!")]
    [RegularExpression(@"^(?=.*\d.*\d)(?=.*[A-Z])(a-zA-Z0-9){8,16}$")]
    [Login(MyProperty = 20)]
    public string password { get; set; }

    private class LoginAttribute : Attribute
    {
        public int MyProperty { get; set; }
    }
}
