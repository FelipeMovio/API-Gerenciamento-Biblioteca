namespace Biblioteca.Domain._Core.ExceptionBase;

public class BadRequestException : ApplicationExceptionBase
{
    public BadRequestException(string message = "A solicitação informada é inválida.")
        : base(message)
    {
    }
}

public class UnauthorizedException : ApplicationExceptionBase
{
    public UnauthorizedException(string message = "Não foi possível autenticar a solicitação.")
        : base(message)
    {
    }
}

public class ForbiddenException : ApplicationExceptionBase
{
    public ForbiddenException(string message = "Você não possui permissão para acessar este recurso.")
        : base(message)
    {
    }
}

public class NotFoundException : ApplicationExceptionBase
{
    public NotFoundException(string message = "O recurso solicitado não foi encontrado.")
        : base(message)
    {
    }
}

public class MethodNotAllowedException : ApplicationExceptionBase
{
    public MethodNotAllowedException(string message = "O método informado não é permitido para este recurso.")
        : base(message)
    {
    }
}

public class ConflictException : ApplicationExceptionBase
{
    public ConflictException(string message = "A solicitação gerou um conflito com o estado atual do recurso.")
        : base(message)
    {
    }
}

public class GoneException : ApplicationExceptionBase
{
    public GoneException(string message = "O recurso solicitado não está mais disponível.")
        : base(message)
    {
    }
}

public class UnprocessableEntityException : ApplicationExceptionBase
{
    public UnprocessableEntityException(string message = "Não foi possível processar a solicitação.")
        : base(message)
    {
    }
}

public class TooManyRequestsException : ApplicationExceptionBase
{
    public TooManyRequestsException(string message = "O limite de requisições foi excedido.")
        : base(message)
    {
    }
}
