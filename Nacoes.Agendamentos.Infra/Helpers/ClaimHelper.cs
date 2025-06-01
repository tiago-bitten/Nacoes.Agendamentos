using Microsoft.AspNetCore.Http;
using Nacoes.Agendamentos.Domain.Entities.Usuarios;
using Nacoes.Agendamentos.Domain.ValueObjects;
using Nacoes.Agendamentos.Infra.Extensions;
using System.Security.Claims;

namespace Nacoes.Agendamentos.Infra.Helpers;

public static class ClaimHelper
{
    #region Values
    public static string IsAutenticado = "IsAutenticado";
    public static string UsuarioId = "UsuarioId";
    public static string UsuarioEmailAddress = "UsuarioEmailAddress";
    public static string IsContaAprovada = "IsContaAprovada";
    public static string Environment = "Environment";
    public static string IsBot = "IsBot";
    #endregion

    #region Invoke
    public static Claim[] Invoke(Usuario usuario)
    {
        return
        [
            new Claim(UsuarioId, usuario.Id.ToString()),
            new Claim(UsuarioEmailAddress, usuario.Email.Address.ToString()),
            new Claim(IsContaAprovada, usuario.EstaAprovado.ToString()),
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
    public static Id<Usuario> GetUsusarioId(IHttpContextAccessor context)
    {
        return new Id<Usuario>(context.GetClaim(UsuarioId).Value);
    }
    #endregion

    #region GetUserEmail
    public static string GetUsusarioEmailAddress(IHttpContextAccessor context)
    {
        return context.GetClaim(UsuarioEmailAddress).Value;
    }
    #endregion

    #region GetIsAccountConfirmed
    public static bool GetIsContaAprovada(IHttpContextAccessor context)
    {
        return bool.Parse(context.GetClaim(IsContaAprovada).Value);
    }
    #endregion

    //#region GetEnvironment
    //public static EEnvironment GetEnvironment(IHttpContextAccessor context)
    //{
    //    return (EEnvironment)int.Parse(context.GetClaim(Environment).Value);
    //}
    //#endregion
}