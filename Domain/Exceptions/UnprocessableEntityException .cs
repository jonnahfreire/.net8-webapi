namespace API.Domain.Exceptions;

public class UnprocessableEntityException : DomainException
{
    public UnprocessableEntityException(string message) : base(message) { }
}