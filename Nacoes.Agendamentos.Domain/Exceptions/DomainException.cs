namespace Nacoes.Agendamentos.Domain.Exceptions;

public class DomainException(string code, string mensagem) : Exception(mensagem)
{
    public string Code => code;
}
