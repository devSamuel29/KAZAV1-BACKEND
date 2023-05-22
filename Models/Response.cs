namespace kazariobranco_backend.Models;

public class Response
{
    private int _code;

    private string _message;

    public int Code { get { return _code; } }

    public string Message { get { return _message; } }

    public Response(int code, string message)
    {
        _code = code;
        _message = message;
    }
}
