namespace Final.Lab.Domain.Results.Errors;

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
