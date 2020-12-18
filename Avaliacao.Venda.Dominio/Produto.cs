using System;

namespace Avaliacao.EVenda.Dominio
{
    public class Produto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public bool TemQuantidadeDisponivelPara(int quantidadeDeVenda)
        {
            return Quantidade >= quantidadeDeVenda;
        }
    }
}
