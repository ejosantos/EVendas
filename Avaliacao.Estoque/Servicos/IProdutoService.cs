using Avaliacao.Estoque.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.Estoque.Servicos
{
    public interface IProdutoService
    {
        Task<Produto> AddAsync(string codigoProduto, string nome, decimal preco, int quantidade);
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<Produto> UpdateAsync(int idProduto, decimal preco, int quantidade);
        Task<Produto> AtualizarProdutoVendido(int idProduto, int quantidade);
    }
}