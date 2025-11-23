namespace API.Domain.Exceptions;

public class ArgumentNullException : DomainException
{
    public ArgumentNullException(string message) : base(message) { }
}