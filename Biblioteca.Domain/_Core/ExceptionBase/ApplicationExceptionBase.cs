namespace Biblioteca.Domain._Core.ExceptionBase;

public abstract class ApplicationExceptionBase : Exception
{
    protected ApplicationExceptionBase(string?  message = null) : base(message)
    {}
}
