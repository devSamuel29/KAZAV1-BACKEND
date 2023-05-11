using System.ComponentModel.DataAnnotations;

namespace kazariobranco_backend.Request;

public class ContactRequest
{
    [Required(ErrorMessage = "O campo nome é obrigatório!")]
    public string name { get; set; }

    [Required(ErrorMessage = "O campo email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Formato de email inválido!")]
    public string email { get; set; }

    [Required(ErrorMessage = "O campo telefone é obrigatório!")]
    public string phone { get; set; }

    [Required(ErrorMessage = "O campo motivo é obrigatório!")]
    public string reason { get; set; }
    
    [Required(ErrorMessage = "O campo descrição é obrigatório!")]
    public string description { get; set; }
}
