using System;

namespace Avaliacao.Estoque.Entidades
{
    public class Produto
    {
        public Produto(string codigo, string nome, decimal preco, int quantidade)
        {
            ValidarSePrecoEhIgualAZero(preco);
            ValidarSeQuantidadeInformadaEhIgualAZero(quantidade);

            Codigo = codigo;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }
        
        protected Produto()
        {

        }
      
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        private void ValidarSePrecoEhIgualAZero(decimal preco)
        {
            if (PrecoInformadoEhMenorQueZero(preco))
                throw new Exception($"Preço informado deve ser maior que zero.");
        }

        private void ValidarSeQuantidadeInformadaEhIgualAZero(int quantidade)
        {
            if (QuantidadeMenorQueZero(quantidade))
                throw new Exception($"Quantidade informada deve ser maior que zero.");
        }

        private bool QuantidadeMenorQueZero(int quantidade)
           => quantidade < 0;

        internal void Atualizar(decimal preco, int quantidade)
        {
            ValidarSePrecoEhIgualAZero(preco);
            ValidarSeQuantidadeInformadaEhIgualAZero(quantidade);

            Preco = preco;
            Quantidade = quantidade;
        }

        private bool PrecoInformadoEhMenorQueZero(decimal preco)
          => preco < 0;

        internal void DiminuirQuantidade(int quantidade)
        {
            if (Quantidade < quantidade)
                throw new Exception($"Estoque não disponível para venda.");

            Quantidade -= quantidade;
        }
    }
}
