namespace kazariobranco_backend.Interfaces;

public interface IEmailService 
{ 
    Task SendEmail(string to, string subjetct, string message);
}
