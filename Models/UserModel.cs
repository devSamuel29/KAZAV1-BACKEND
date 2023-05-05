namespace kazariobranco_backend.Models;
public class UserModel 
{  
    public int id { get; set; }
    
    public string firstname { get; set; }
    
    public string  lastname { get; set; }
    
    public string cpf { get; set; }
    
    public string  phone { get; set; }
    
    public string email { get; set; }
    
    public string password { get; set; }
    
    public DateTime birthday { get; set; }
    
    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }
}
