﻿namespace server.Helpers
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
        
        public static bool FileDelete(ApplicationDbContext dbContext, IWebHostEnvironment appEnvironment, string fileName)
        {
            string path = appEnvironment.WebRootPath + "\\Files\\" + fileName;
            try
            {
                System.IO.File.Delete(path);
            }
            catch
            {
                return false;
            }

            return true;
        }
        
        public static int FilesDelete(ApplicationDbContext dbContext, IWebHostEnvironment appEnvironment, List<string> filesName)
        {

            int count = 0;

            foreach (var file in filesName)
            {
                string path = appEnvironment.WebRootPath + "\\Files\\" + file;
                try
                {
                    System.IO.File.Delete(path);
                    count++;
                }
                catch
                {
                }
            }  

            return count;
        }

    }
}
