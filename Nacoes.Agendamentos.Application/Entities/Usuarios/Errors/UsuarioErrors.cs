using Nacoes.Agendamentos.Application.Common.Results;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Errors;

public static class UsuarioErrors
{
    public static readonly Error UsuarioComEmailExistente = 
        new("Usuarios.Adicionar.EmailExistente", "Já existe um usuário com o e-mail informado.");
}