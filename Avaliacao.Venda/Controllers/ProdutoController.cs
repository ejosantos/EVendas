using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Avaliacao.EVenda.Dominio;
using Avaliacao.EVenda.Dominio.Servicos;
using Avaliacao.EVenda.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Avaliacao.EVenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        protected readonly IMapper _Mapper;
        private readonly IProdutoService _produtoService;

        public ProdutoController(IServiceProvider serviceProvider, IProdutoService produtoService) 
        {
            _Mapper = serviceProvider.GetRequiredService<IMapper>();
            _produtoService = produtoService;
        }

        /// <summary>
        /// Listagem de todos os produtos disponíveis para venda.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Produto> entities = await _produtoService.GetAllParaVenda();
            IEnumerable<ProdutoViewModel> models = _Mapper.Map<IEnumerable<ProdutoViewModel>>(entities);
            return Ok(models);
        }
    }
}
