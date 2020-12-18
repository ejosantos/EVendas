using System;
using System.Collections.Generic;

namespace Avaliacao.EVenda.Dominio
{
    public class Venda 
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public ProdutoVenda ProdutoVenda { get; set; }

        public Venda() { }

        public Venda(ProdutoVenda produtoVenda)
        {
            ProdutoVenda = produtoVenda;
            DataVenda = DateTime.Now;
            produtoVenda.AtualizarEstoque();
        }
    }
}