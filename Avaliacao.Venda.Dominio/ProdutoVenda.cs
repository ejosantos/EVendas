using System;

namespace Avaliacao.EVenda.Dominio
{
    public class ProdutoVenda
    {
        public ProdutoVenda(Produto produto, int quantidadeVendida)
        {
            if (!produto.TemQuantidadeDisponivelPara(quantidadeVendida))
                throw new Exception("Produto indisponível para venda.");

            Produto = produto;
            Quantidade = quantidadeVendida;
        }

        public ProdutoVenda()
        {

        }

        internal void AtualizarEstoque()
        {
            Produto.Quantidade = Produto.Quantidade - Quantidade;
        }

        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}