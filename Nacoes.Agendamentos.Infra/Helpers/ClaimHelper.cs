using Microsoft.AspNetCore.Http;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Infra.Extensions;
using System.Security.Claims;
using UsuarioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Usuarios.Usuario>;

namespace Nacoes.Agendamentos.Infra.Helpers;

public static class ClaimHelper
{
    #region Values
    public static readonly string IsAutenticado = "IsAutenticado";
    public static readonly string UsuarioId = "UsuarioId";
    public static readonly string UsuarioEmailAddress = "UsuarioEmailAddress";
    public static readonly string Environment = "Environment";
    public static readonly string IsBot = "IsBot";
    #endregion

    #region Invoke
    public static Claim[] Invoke(Usuario usuario)
    {
        return
        [
            new Claim(UsuarioId, usuario.Id.ToString()),
            new Claim(UsuarioEmailAddress, usuario.Email.Address),
            new Claim(IsBot, bool.FalseString)
        ];
    }

    public static Claim[] InvokeBot()
    {
        throw new NotImplementedException();
    }
    #endregion

    #region IsAuthenticated
    public static bool GetIsAuthenticated(IHttpContextAccessor context)
    {
        return context.GetIdentity().IsAuthenticated;
    }
    #endregion

    #region GetUserId
    public static UsuarioId GetUsusarioId(IHttpContextAccessor context)
    {
        return new UsuarioId(context.GetClaim(UsuarioId).Value);
    }
    #endregion

    #region GetUserEmail
    public static string GetUsusarioEmailAddress(IHttpContextAccessor context)
    {
        return context.GetClaim(UsuarioEmailAddress).Value;
    }
    #endregion

    //#region GetEnvironment
    //public static EEnvironment GetEnvironment(IHttpContextAccessor context)
    //{
    //    return (EEnvironment)int.Parse(context.GetClaim(Environment).Value);
    //}
    //#endregion
}