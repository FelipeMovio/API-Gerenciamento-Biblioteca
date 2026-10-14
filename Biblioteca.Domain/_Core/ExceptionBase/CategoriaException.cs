namespace Biblioteca.Domain._Core.ExceptionBase;

public abstract class BadRequestException : ApplicationExceptionBase
{
    protected BadRequestException(string message) : base(message)
    {
    }
}

public abstract class UnauthorizedException : ApplicationExceptionBase
{
    protected UnauthorizedException(string message) : base(message)
    {
    }
}

public abstract class ForbiddenException : ApplicationExceptionBase
{
    protected ForbiddenException(string message) : base(message)
    {
    }
}

public abstract class NotFoundException : ApplicationExceptionBase
{
    protected NotFoundException(string message) : base(message)
    {
    }
}

public abstract class MethodNotAllowedException : ApplicationExceptionBase
{
    protected MethodNotAllowedException(string message) : base(message)
    {
    }
}

public abstract class ConflictException : ApplicationExceptionBase
{
    protected ConflictException(string message) : base(message)
    {
    }
}

public abstract class GoneException : ApplicationExceptionBase
{
    protected GoneException(string message) : base(message)
    {
    }
}

public abstract class UnprocessableEntityException : ApplicationExceptionBase
{
    protected UnprocessableEntityException(string message) : base(message)
    {
    }
}

public abstract class TooManyRequestsException : ApplicationExceptionBase
{
    protected TooManyRequestsException(string message) : base(message)
    {
    }
}
