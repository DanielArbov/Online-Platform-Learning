namespace Talent;


public abstract class BaseError
{
    public string Message { get; set; }

    protected BaseError(string message)
    {
        Message = message;
    }

}

public class RouteNotFoundError: BaseError //404
{
    public RouteNotFoundError(string method,string route): base($"Route {route} on method {method} not exists") { }
    
}

public class ResourceNotFoundError : BaseError //404
{
    public ResourceNotFoundError() : base($"id not found") { }

}

public class ValidationError : BaseError //400
{
    public ValidationError(string msg) : base(msg) { }

}

public class UnauthorizedError : BaseError //400
{
    public UnauthorizedError(string msg) : base(msg) { }

}


public class InternalServerError : BaseError //500
{
    public InternalServerError(string msg) : base(msg) { }
}
