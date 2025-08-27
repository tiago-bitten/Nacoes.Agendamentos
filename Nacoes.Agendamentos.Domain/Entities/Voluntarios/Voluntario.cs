using Nacoes.Agendamentos.Domain.Abstracts;
using Nacoes.Agendamentos.Domain.Abstracts.Interfaces;
using Nacoes.Agendamentos.Domain.Common;
using Nacoes.Agendamentos.Domain.Entities.Voluntarios.Errors;
using Nacoes.Agendamentos.Domain.ValueObjects;

namespace Nacoes.Agendamentos.Domain.Entities.Voluntarios;

public sealed class Voluntario : EntityId, IAggregateRoot
{
    private readonly List<VoluntarioMinisterio> _ministerios = [];

    #region Constructor
    private Voluntario() { }

    private Voluntario(string nome, EOrigemCadastroVoluntario origemCadastro, Email? email, Celular? celular, CPF? cpf, DataNascimento? dataNascimento)
    {
        Nome = nome;
        OrigemCadastro = origemCadastro;
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
    public EOrigemCadastroVoluntario OrigemCadastro { get; private set; }

    public IReadOnlyCollection<VoluntarioMinisterio> Ministerios => _ministerios.AsReadOnly();

    public string EmailAddress => Email?.Address ?? string.Empty;
    public int Idade => DataNascimento?.Idade ?? 0;
    public bool MenorDeIdade => DataNascimento?.MenorDeIdade ?? false;

    #region Criar
    public static Result<Voluntario> Criar(string nome, Email? email, Celular? celular, CPF? cpf,
        DataNascimento? dataNascimento, EOrigemCadastroVoluntario origemCadastro)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return VoluntarioErrors.NomeObrigatorio;
        }
        
        if (origemCadastro is not EOrigemCadastroVoluntario.Sistema && (email is null || cpf is null || dataNascimento is null))
        {
            var dadosObrigatoriosAusentes = new List<string>();

            if (email is null)
            {
                dadosObrigatoriosAusentes.Add("e-mail");
            }
            
            if (cpf is null)
            {
                dadosObrigatoriosAusentes.Add("CPF");
            }
            
            if (dataNascimento is null)
            {
                dadosObrigatoriosAusentes.Add("data de nascimento");
            }
            
            var dadosObrigatoriosFormatado = dadosObrigatoriosAusentes.ToSingleMessage();
            return VoluntarioErrors.DadosPessoaisObrigatorio(dadosObrigatoriosFormatado);
        }

        
        var voluntario = new Voluntario(nome, origemCadastro, email, celular, cpf, dataNascimento);
        return Result<Voluntario>.Success(voluntario);
    }
    #endregion
    
    #region VincularMinisterio
    public Result VincularMinisterio(Guid ministerioId)
    {
        var existeVinculo = _ministerios.SingleOrDefault(x => x.MinisterioId == ministerioId);
        if (existeVinculo is null)
        {
            _ministerios.Add(new VoluntarioMinisterio(ministerioId));
            return Result.Success();
        }

        return existeVinculo.Restaurar();
    }
    #endregion
    
    #region DesvincularMinisterio
    public Result DesvincularMinisterio(Guid ministerioId)
    {
        var existeVinculo = _ministerios.SingleOrDefault(x => x.MinisterioId == ministerioId);
        if (existeVinculo is null)
        {
            return VoluntarioMinisterioErrors.VoluntarioNaoEstaVinculadoAoMinisterio;
        }

        return existeVinculo.Inativar();
    }
    #endregion
}

public enum EOrigemCadastroVoluntario
{
    Sistema = 0,
    Site = 1,
    App = 2
}