using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts;
using Nacoes.Agendamentos.Application.Common.Results;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Errors;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Mappings;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Specifications;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.AdicionarVoluntario;

public sealed class AdicionarVoluntarioHandler(IUnitOfWork uow,
                                               IValidator<AdicionarVoluntarioCommand> voluntarioValidator,
                                               IVoluntarioRepository voluntarioRepository)
    : BaseHandler(uow), IAdicionarVoluntarioHandler
{
    public async Task<Result<Id<Voluntario>, Error>> ExecutarAsync(AdicionarVoluntarioCommand command, CancellationToken cancellation = default)
    {
        await voluntarioValidator.CheckAsync(command);

        var voluntario = command.GetEntidade();

        var cpfExistente = await CpfExistenteSpecification(voluntario.Cpf);
        if (cpfExistente)
        {
            return VoluntarioErrors.VoluntarioComCpfExistente;
        }

        var emailExistente = await EmailExistenteSpecification(voluntario.Email);
        if (emailExistente) 
        {
            return VoluntarioErrors.VoluntarioComEmailExistente;
        }

        await Uow.BeginAsync();
        await voluntarioRepository.AddAsync(voluntario);
        await Uow.CommitAsync(cancellation);

        return voluntario.Id;
    }

    #region CpfExistenteSpecification
    private async Task<bool> CpfExistenteSpecification(CPF? cpf)
    {
        if (cpf is null)
        {
            return false;
        }

        return await GetSpecification(new VoluntarioComCpfExistenteSpecification(cpf!),
                                      voluntarioRepository);
    }
    #endregion

    #region EmailExistenteSpecification
    private async Task<bool> EmailExistenteSpecification(Email? email)
    {
        if (email is null)
        {
            return false;
        }

        return await GetSpecification(new VoluntarioComEmailExistenteSpecification(email!),
                                      voluntarioRepository);
    }
    #endregion
}
