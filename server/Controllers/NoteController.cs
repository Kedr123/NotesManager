using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Interfaces;
using server.Models;
using server.Models.Requests;

namespace server.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class NoteController : Controller
    {
        public readonly IRepositoryNote repositoryNote;

        public NoteController(IRepositoryNote repositoryNote) => this.repositoryNote = repositoryNote;


        [HttpPost]
        public async Task<ActionResult> CreateNote([FromForm] RequestCreateNote request)
        {
            var note = await repositoryNote.CreateNote(request, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (note == null) return StatusCode(500);

            return Ok(new { note });

        }

        [HttpPut]
        public async Task<ActionResult> UpdateNote([FromForm] RequestUpdateNote request)
        {
            //return Ok(request);

            var note = await repositoryNote.UpdateNote(request, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (note == null) return StatusCode(500);

            return Ok(new { note });

        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> DeleteNote(long Id)
        {
            var note = await repositoryNote.DeleteNote(Id, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (note == null) return StatusCode(500);

            return Ok(new { note });

        }

        [HttpGet, Route("{id}")]
        public ActionResult GetNote(long Id)
        {
            var note = repositoryNote.GetNote(Id, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (note == null) return NotFound();

            return Ok(new { note });
        }

        [HttpGet, Route("column/{id}")]
        public ActionResult GetAllNotesColumn(long Id)
        {
            var notes = repositoryNote.GetAllNotesColumn(Id, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (notes == null) return NotFound();

            return Ok(new { notes });
        }

    }
}
