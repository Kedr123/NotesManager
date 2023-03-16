namespace server.Helpers
{
    public class FileHelper
    {

        public static async Task<List<Models.File>> FilesUpload(ApplicationDbContext dbContext, IWebHostEnvironment appEnvironment, IFormFileCollection Files)
        {
            var newFiles = new List<Models.File>();

            foreach (var formFile in Files)
            {



                string formatFile = formFile.FileName.Split('.')[formFile.FileName.Split('.').Length - 1];
                string newFileName = string.Format(@"{0}.{1}", Guid.NewGuid(), formatFile);

                string path = appEnvironment.WebRootPath + "\\Files\\" + newFileName;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                var file = new Models.File { FileName = newFileName, OldFileName = formFile.FileName };
                newFiles.Add(file);
                dbContext.Files.Add(file);

            }

            dbContext.SaveChanges();

            return newFiles;
        }
        
        public static async Task<Models.File> FileUpload(ApplicationDbContext dbContext, IWebHostEnvironment appEnvironment, IFormFile formFiles)
        {
            string formatFile = formFiles.FileName.Split('.')[formFiles.FileName.Split('.').Length - 1];
            string newFileName = string.Format(@"{0}.{1}", Guid.NewGuid(), formatFile);

            string path = appEnvironment.WebRootPath + "\\Files\\" + newFileName;

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await formFiles.CopyToAsync(fileStream);
            }

            var file = new Models.File { FileName = newFileName, OldFileName = formFiles.FileName };
            dbContext.Files.Add(file);
            dbContext.SaveChanges();

            return file;
        }

    }
}
