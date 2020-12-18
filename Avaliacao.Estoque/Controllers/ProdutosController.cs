using Avaliacao.Estoque.Entidades;
using Avaliacao.Estoque.Servicos;
using Avaliacao.Estoque.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Avaliacao.Estoque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _produtoService.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            Produto produto = await _produtoService.GetByIdAsync(id);

            if (produto == null) return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] ProdutoViewModel model)
        {
            Produto produto = await _produtoService.AddAsync(model.Codigo, model.Nome, model.Preco, model.Quantidade);

            string action = Url.Action("Get", this.ControllerContext.ActionDescriptor.ControllerName, new { id = model.Id });

            return Created(action, produto);
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] ProdutoViewModel model) => Ok(await _produtoService.UpdateAsync(model.Id, model.Preco, model.Quantidade));
    }
}
