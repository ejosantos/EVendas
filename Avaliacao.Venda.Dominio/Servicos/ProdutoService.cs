using Avaliacao.EVenda.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Dominio.Servicos
{
    public class ProdutoService : IProdutoService
    {
        private IProdutoRepositorio _produtoRepository;
        private IUnitOfWork _unitOfWork;

        public ProdutoService(IProdutoRepositorio produtoRepositorio, IUnitOfWork unitOfWork)
        {
            _produtoRepository = produtoRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Produto>> GetAllParaVenda()
           => await _produtoRepository.GetAllParaVenda();

        public async Task ProcessarAtualizacao(Produto produtoEnviado)
        {
            Produto produto = _produtoRepository.GetAsync(produtoEnviado.Id).Result;
            if (produto != null)
            {
                produto.Codigo = produtoEnviado.Codigo;
                produto.Nome = produtoEnviado.Nome;
                produto.Preco = produtoEnviado.Preco;
                produto.Quantidade = produtoEnviado.Quantidade;

                await _produtoRepository.UpdateAsync(produto);

                await _unitOfWork.CommitAsync();
            }
        }

        public async Task ProcessarCriacao(Produto produto)
        {
            await _produtoRepository.AddAsync(produto);
            await _unitOfWork.CommitAsync();
        }
    }


}
