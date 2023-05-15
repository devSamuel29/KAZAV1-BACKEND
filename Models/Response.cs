namespace kazariobranco_backend.Models;

public class Response
{
    private int _code;

    private string _message;

    public int code { get { return _code; } }
    
    public string message { get { return _message; } }

    public Response(int code, string message)
    {
        _code = code;
        _message = message;
    }
}
