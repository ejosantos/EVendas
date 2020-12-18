using Avaliacao.EVenda.Dominio.Servicos;
using Avaliacao.EVenda.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Avaliacao.EVenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IProdutoVendaFactory _produtoVendaFactory;
        private readonly IVendaService _vendaService;

        public VendaController(IProdutoVendaFactory produtoVendaFactory, IVendaService vendaService)
        {
            _produtoVendaFactory = produtoVendaFactory;
            _vendaService = vendaService;
        }

        /// <summary>
        /// Realiza a Venda de um Produto.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] VendaRequestViewModel model)
        {
            var vendaProduto = await _produtoVendaFactory.Criar(model.Id, model.Quantidade);

            return Created(Url.Action("Post"), await _vendaService.RealizarVenda(vendaProduto));
        }
    }
}
