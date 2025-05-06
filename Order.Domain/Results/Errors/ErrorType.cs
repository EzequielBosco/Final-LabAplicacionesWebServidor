namespace Order.Domain.Results.Errors;

public enum ErrorType
{
    None, 
    NotFound,
    Conflict,
    Validation,
    Authorization,
    Forbidden,
    Unexpected
}
