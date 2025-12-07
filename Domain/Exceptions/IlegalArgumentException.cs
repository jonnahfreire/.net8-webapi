namespace WebApi.Domain.Exceptions;

public class IllegalArgumentException : DomainException
{
    public IllegalArgumentException(string message) : base(message) { }
}