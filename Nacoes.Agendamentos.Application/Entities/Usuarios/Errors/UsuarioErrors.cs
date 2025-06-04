﻿using Nacoes.Agendamentos.Application.Common.Results;

namespace Nacoes.Agendamentos.Application.Entities.Usuarios.Errors;

public static class UsuarioErrors
{
    public static readonly Error UsuarioComEmailExistente = 
        new("Usuario.Adicionar.EmailExistente", "Já existe um usuário com o e-mail informado.");

    public static readonly Error UsuarioContaNaoEstaAprovada =
        new("Usuario.Solicitacoes.Status", "Sua conta não está aprovada. Aguarde uma avaliação");

}