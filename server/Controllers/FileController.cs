using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using server.Helpers;

namespace server.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class FileController : Controller
    {
        ApplicationDbContext dbContext;
        IWebHostEnvironment appEnvironment;

        public FileController(ApplicationDbContext dbContext, IWebHostEnvironment appEnvironment)
            => (this.dbContext, this.appEnvironment) = (dbContext, appEnvironment);


        [HttpPost]
        public async Task<ActionResult> UploudFiles(IFormFileCollection Files)
        {
            var newFiles = new List<Models.File>();

            try
            {
                newFiles = await FileHelper.FilesUpload(dbContext, appEnvironment, Files);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
            
            return Ok(new {files = newFiles });
        }

    }
}
