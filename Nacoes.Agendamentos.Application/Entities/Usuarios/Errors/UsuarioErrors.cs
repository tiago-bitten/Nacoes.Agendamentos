
using Nacoes.Agendamentos.Domain.Common;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Errors;

public static class UsuarioErrors
{
    public static readonly Error UsuarioComEmailExistente = 
        new("Usuario.UsuarioComEmailExistente", "Já existe um usuário com o e-mail informado.");

    public static readonly Error UsuarioContaNaoEstaAprovada =
        new("Usuario.UsuarioComEmailExistente", "Sua conta não está aprovada. Aguarde uma avaliação");

}