using System.Linq.Expressions;
using Domain.Shared.Specifications;

namespace Domain.Usuarios.Specs;

public sealed class InvitationsByTokenSpec(string token) : Specification<UserInvitation>
{
    public override Expression<Func<UserInvitation, bool>> ToExpression()
        => x => x.Token == token;
}
