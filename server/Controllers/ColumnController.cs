using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Interfaces;
using server.Models;
using server.Models.Requests;

namespace server.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ColumnController : Controller
    {
        public readonly IRepositoryColumn repositoryColumn;


        public ColumnController(IRepositoryColumn repositoryColumn) => (this.repositoryColumn) = (repositoryColumn);

        [HttpGet]
        public ActionResult<List<Column?>> GetListColumns([FromForm] long ListId)
        {
            return repositoryColumn.GetListColumns(ListId, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

        }
        
        [HttpGet, Route("{Id}")]
        public ActionResult<Column?> GetColumn(long Id)
        {
            var column = repositoryColumn.GetColumn(Id, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (column == null) return NotFound();

            return Ok(column);

        }
                
        [HttpPost]
        public async Task<ActionResult<Column?>> CreateColumn([FromForm]RequestCreateColumn request)
        {
            var column =  await repositoryColumn.CreateColumn(request, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (column == null) return BadRequest("Лист не существует или у вас нет доступа к нему!");

            return Ok(column);
        }

        [HttpPut]
        public async Task<ActionResult<Column?>> UpdateColumn([FromForm]RequestUpdateColumn request)
        {
            var column = await repositoryColumn.UpdateColumn(request, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (column == null) return BadRequest("Колонка не существует или у вас нет доступа к её изменению!");

            return Ok(column);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Column?>> DeleteColumn(long Id)
        {
            var column = await repositoryColumn.DeleteColumn(Id, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (column == null) return NotFound();

            return Ok(column);
        }

        
    }
}
