using Avaliacao.Estoque.Entidades;
using Avaliacao.Estoque.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.Estoque.Servicos
{
    public class ProdutoService : IProdutoService
    {
        private EstoqueContext _contexto;
        private readonly IProdutoServiceBus _produtoServiceBus;

        public ProdutoService(EstoqueContext contexto, IProdutoServiceBus produtoServiceBus)
        {
            _contexto = contexto;
            _produtoServiceBus = produtoServiceBus;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync() =>
            await Task.FromResult<IEnumerable<Produto>>(_contexto.Produtos.AsNoTracking());

        public async Task<Produto> GetByIdAsync(int id) =>
            await _contexto.Produtos.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Produto> AddAsync(string codigoProduto, string nome, decimal preco, int quantidade)
        {
            ValidarSeProdutoPodeSerCriado(codigoProduto, nome, quantidade, preco);

            var produto = new Produto(codigoProduto, nome, preco, quantidade);

            await _contexto.AddAsync(produto);
            _contexto.SaveChanges();

            await _produtoServiceBus.EnviarProdutoMessage(produto);

            return produto;
        }

        public async Task<Produto> UpdateAsync(int idProduto, decimal preco, int quantidade)
        {
            var produto = await GetByIdAsync(idProduto);

            produto.Atualizar(preco, quantidade);

            _contexto.SaveChanges();

            await _produtoServiceBus.EnviarMensagemAtualizacaoProduto(produto);

            return produto;
        }

        public async Task<Produto> AtualizarProdutoVendido(int idProduto, int quantidade)
        {
            var produto = await GetByIdAsync(idProduto);

            produto.DiminuirQuantidade(quantidade);

            _contexto.SaveChanges();
            
            return produto;
        }

        private void ValidarSeProdutoPodeSerCriado(string codigoProduto, string nome, int quantidade, decimal preco)
        {
            if (PossuiProdutoComMesmoNome(nome))
                throw new Exception($"Já existe produto cadastrado para esse nome ({nome}).");

            if (PossuiProdutoComMesmoCodigo(codigoProduto))
                throw new Exception($"Já existe produto cadastrado para esse código ({codigoProduto}).");
        }

        private bool PossuiProdutoComMesmoNome(string nome)
            => _contexto.Produtos.Count(x => x.Nome.Equals(nome)) > 0;

        private bool PossuiProdutoComMesmoCodigo(string codigoProduto)
           => _contexto.Produtos.Count(x => x.Codigo.Equals(codigoProduto)) > 0;
    }
}
