using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockAppSQ20.Dtos.Comment;
using StockAppSQ20.Interfaces;
using StockAppSQ20.Mapper;

namespace StockAppSQ20.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null) { return NotFound(); }

            return Ok(comment.ToComment());

        }

        [HttpPost("{stockId:int}")]
        /*[Authorize]*/
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _stockRepository.StockExist(stockId)) return BadRequest("Stock does not exist");

            var commentModel = commentDto.ToCommentFromCreateDTO(stockId);

            await _commentRepository.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToComment());
        }


    }
}
