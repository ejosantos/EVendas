using AutoMapper;
using Avaliacao.EVenda.Dominio;
using Avaliacao.EVenda.ViewModel;

namespace Avaliacao.Venda
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoViewModel>();
            CreateMap<ProdutoViewModel, Produto>();
        }
    }
}