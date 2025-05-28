using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Usuarios;

public sealed class Usuario : BaseEntity<Usuario>, IAggregateRoot
{
    #region Ctor
    internal Usuario() { }

    public Usuario(string nome, Email email, string senha, Celular celular)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Celular = celular;
    }
    #endregion

    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public string Senha { get; private set; }
    public Celular Celular { get; private set; }

    public void AtualizarNome(string  nome) => Nome = nome;
    public void AtualizarEmail(Email email) => Email = email;
    public void AtualizarSenha(string senha) => Senha = senha;
    public void AtualizarCelular(Celular celular) => Celular = celular;
}
