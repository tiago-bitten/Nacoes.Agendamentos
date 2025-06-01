namespace Nacoes.Agendamentos.Domain.Exceptions;

public class DomainException(string code, string mensagem) : Exception
{
    public string Mensagem => mensagem;
    public string Code => code;
}
