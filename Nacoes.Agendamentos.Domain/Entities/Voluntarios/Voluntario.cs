using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Entities.Ministerios;
using Nacoes.Agendamentos.Domain.Entities.VoluntariosMinisterios;
using Nacoes.Agendamentos.Domain.Exceptions;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public sealed class Voluntario : EntityId<Voluntario>, IAggregateRoot
{
    private List<VoluntarioMinisterio> _ministerios = [];

    #region Constructor
    internal Voluntario() { }

    public Voluntario(string nome, Email? email, Celular? celular, CPF? cpf, DataNascimento? dataNascimento)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentNullException(nameof(nome), "O nome do voluntário é obrigatório");
        }

        Nome = nome;
        Email = email;
        Celular = celular;
        Cpf = cpf;
        DataNascimento = dataNascimento;
    }
    #endregion

    public string Nome { get; private set; }
    public Email? Email { get; private set; }
    public Celular? Celular { get; private set; }
    public CPF? Cpf { get; private set; }
    public DataNascimento? DataNascimento { get; private set; }

    public IReadOnlyCollection<VoluntarioMinisterio> Ministerios => _ministerios.AsReadOnly();

    public int Idade => DataNascimento?.Idade ?? 0;
    public bool MenorDeIdade => DataNascimento?.MenorDeIdade ?? false;

    #region VincularMinisterio
    public void VincularMinisterio(Id<Ministerio> ministerioId)
    {
        var existeVinculo = _ministerios.FirstOrDefault(x => x.MinisterioId == ministerioId);
        if (existeVinculo is null)
        {
            _ministerios.Add(new VoluntarioMinisterio(ministerioId));
            return;
        }

        existeVinculo.Ativar();
    }
    #endregion
}
