using Domain.Shared.Entities;
using Domain.Shared.Results;
using Domain.Shared.ValueObjects;

namespace Domain.Ministerios;

public sealed class Ministerio : RemovableEntity, IAggregateRoot
{
    private readonly List<Atividade> _atividades = [];

    internal Ministerio() { }

    private Ministerio(string nome, string? descricao, Cor cor)
    {
        Nome = nome;
        Descricao = descricao;
        Cor = cor;
    }

    public string Nome { get; private set; } = null!;
    public string? Descricao { get; private set; }
    public Cor Cor { get; private set; } = null!;

    public IReadOnlyCollection<Atividade> Atividades => _atividades.AsReadOnly();

    public void AtualizarNome(string nome) => Nome = nome;

    public void AtualizarDescricao(string descricao) => Descricao = descricao;

    public void AtualizarCor(Cor cor) => Cor = cor;

    public static Result<Ministerio> Criar(string nome, string? descricao, Cor cor)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            return MinisterioErrors.NomeObrigatorio;
        }

        return new Ministerio(nome, descricao, cor);
    }

    public Result<Atividade> AdicionarAtividade(string nome, string? descricao)
    {
        var atividadeResult = Atividade.Criar(nome, descricao);
        if (atividadeResult.IsFailure)
        {
            return atividadeResult.Error;
        }

        var atividade = atividadeResult.Value;
        _atividades.Add(atividade);

        return Result<Atividade>.Success(atividade);
    }
}
