using System;
using System.Collections.Generic;

namespace Avaliacao.EVenda.ViewModel
{
    public class VendaViewModel
    {
        public DateTime DataVenda { get; set; }

        public ICollection<ProdutoVendaViewModel> ProdutosVendidos { get; set; }

    }
}
