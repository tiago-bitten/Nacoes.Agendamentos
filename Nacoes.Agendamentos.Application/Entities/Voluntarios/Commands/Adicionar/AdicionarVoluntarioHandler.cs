using FluentValidation;
using Nacoes.Agendamentos.Application.Abstracts.Messaging;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.AdicionarVoluntario;
using Nacoes.Agendamentos.Application.Entities.Voluntarios.Mappings;
using Nacoes.Agendamentos.Application.Extensions;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Interfaces;
using VoluntarioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Voluntarios.Voluntario>;

namespace Nacoes.Agendamentos.Application.Entities.Voluntarios.Commands.Adicionar;

public sealed class AdicionarVoluntarioHandler(IUnitOfWork uow,
                                               IValidator<AdicionarVoluntarioCommand> voluntarioValidator,
                                               IVoluntarioRepository voluntarioRepository)
    : ICommandHandler<AdicionarVoluntarioCommand, VoluntarioId>
{
    public async Task<Result<VoluntarioId>> Handle(AdicionarVoluntarioCommand command, CancellationToken cancellation = default)
    {
        await voluntarioValidator.CheckAsync(command);

        var voluntario = command.ToEntity();

        /*var cpfExistente = await CpfExistenteSpecification(voluntario.Cpf);
        if (cpfExistente)
        {
            return VoluntarioErrors.VoluntarioComCpfExistente;
        }

        var emailExistente = await EmailExistenteSpecification(voluntario.Email);
        if (emailExistente) 
        {
            return VoluntarioErrors.VoluntarioComEmailExistente;
        }*/

        await uow.BeginAsync();
        await voluntarioRepository.AddAsync(voluntario);
        await uow.CommitAsync(cancellation);

        return Result<VoluntarioId>.Success(voluntario.Id);
    }
}
