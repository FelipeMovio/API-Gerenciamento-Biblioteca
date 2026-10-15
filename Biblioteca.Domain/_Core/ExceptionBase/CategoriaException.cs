namespace Biblioteca.Domain._Core.ExceptionBase;

public class BadRequestException : ApplicationExceptionBase
{
    public BadRequestException(string message) : base(message)
    {
    }
}

public class UnauthorizedException : ApplicationExceptionBase
{
    public UnauthorizedException(string message) : base(message)
    {
    }
}

public  class ForbiddenException : ApplicationExceptionBase
{
    public ForbiddenException(string message) : base(message)
    {
    }
}

public class NotFoundException : ApplicationExceptionBase
{
    public NotFoundException(string message) : base(message)
    {
    }
}

public class MethodNotAllowedException : ApplicationExceptionBase
{
    public MethodNotAllowedException(string message) : base(message)
    {
    }
}

public class ConflictException : ApplicationExceptionBase
{
    public ConflictException(string message) : base(message)
    {
    }
}

public class GoneException : ApplicationExceptionBase
{
    public GoneException(string message) : base(message)
    {
    }
}

public class UnprocessableEntityException : ApplicationExceptionBase
{
    public UnprocessableEntityException(string message) : base(message)
    {
    }
}

public class TooManyRequestsException : ApplicationExceptionBase
{
    public TooManyRequestsException(string message) : base(message)
    {
    }
}
