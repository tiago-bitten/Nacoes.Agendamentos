﻿using Microsoft.EntityFrameworkCore;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.Entities.Usuarios.Interfaces;
using Nacoes.Agendamentos.Infra.Abstracts;
using Nacoes.Agendamentos.Infra.Contexts;

namespace Nacoes.Agendamentos.Infra.Entities.Usuarios;
public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    #region Construtor
    public UsuarioRepository(NacoesDbContext dbContext)
        : base(dbContext)
    {
    }
    #endregion
}
