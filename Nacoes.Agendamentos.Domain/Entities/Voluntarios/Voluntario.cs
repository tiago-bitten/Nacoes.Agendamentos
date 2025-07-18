using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.ValueObjects;
using MinisterioId = Nacoes.Agendamentos.Domain.ValueObjects.Id<Nacoes.Agendamentos.Domain.Entities.Ministerios.Ministerio>;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public sealed class Voluntario : EntityId<Voluntario>, IAggregateRoot
{
    private readonly List<VoluntarioMinisterio> _ministerios = [];

    #region Constructor
    private Voluntario() { }

    public Voluntario(string nome, Email? email, Celular? celular, CPF? cpf, DataNascimento? dataNascimento)
    {
        Nome = nome;
        Email = email;
        Celular = celular;
        Cpf = cpf;
        DataNascimento = dataNascimento;
    }
    #endregion

    public string Nome { get; private set; } = null!;
    public Email? Email { get; private set; }
    public Celular? Celular { get; private set; }
    public CPF? Cpf { get; private set; }
    public DataNascimento? DataNascimento { get; private set; }

    public IReadOnlyCollection<VoluntarioMinisterio> Ministerios => _ministerios.AsReadOnly();

    public int Idade => DataNascimento?.Idade ?? 0;
    public bool MenorDeIdade => DataNascimento?.MenorDeIdade ?? false;

    #region VincularMinisterio
    public Result VincularMinisterio(MinisterioId ministerioId)
    {
        var existeVinculo = _ministerios.FirstOrDefault(x => x.MinisterioId == ministerioId);
        if (existeVinculo is null)
        {
            _ministerios.Add(new VoluntarioMinisterio(ministerioId));
            return Result.Success();
        }

        return existeVinculo.Ativar();
    }
    #endregion
}
