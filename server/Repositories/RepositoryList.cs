using Microsoft.EntityFrameworkCore;
using server.Helpers;
using server.Interfaces;
using server.Models;
using server.Models.Requests;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;

namespace server.Repositories
{
    public class RepositoryList : IRepositoryList
    {
        public readonly ApplicationDbContext dbContext;
        public readonly IWebHostEnvironment webHostEnvironment;

        public RepositoryList(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment) 
            => (this.dbContext, this.webHostEnvironment) = (dbContext, webHostEnvironment);

        

        public async Task<List> CreateListAsync(RequestCreateList request, long UserId)
        {
            var list = new List();
            list.Title = request.Title;
            var user = dbContext.Users.Find(UserId);
            list.User = user;

            if (request.Image != null)
            {
                var file = await FileHelper.FileUpload(dbContext, webHostEnvironment, request.Image);
                list.File = file;
            }
                
            
            dbContext.Lists.Add(list);
            await dbContext.SaveChangesAsync();

            return list;
        }

        public async Task<List?> DeleteListAsync(long ListId, long UserId)
        {
            // Получение листа с его изображением
            var list = dbContext.Lists.Where(opt => opt.Id == ListId && opt.User.Id == UserId).Include(u =>u.File).FirstOrDefault();
            

            if (list == null) return null;

            // Получение файлов заметок, которые удалятся при удалении листа
            var noteFiles = dbContext.NoteFiles.Where(opt => opt.Note.Column.list.Id == ListId).Include(u => u.File).ToList();

            // Удаление изображения листа
            if (list.File != null)
            {
                var file = dbContext.Files.Find(list.File.Id);

                if (file != null)
                {
                    FileHelper.FileDelete(dbContext,webHostEnvironment, file.FileName);
                    dbContext.Files.Remove(file);
                    
                }
            }

            // Удаление всех файлов, относящихся к заметкам в листе
            if(noteFiles != null)
            {
                List<string> filesName = new List<string>();
                List<Models.File> files = new List<Models.File>();

                foreach (var element in noteFiles)
                {
                    filesName.Add(element.File.FileName);
                    files.Add(element.File);
                }

                FileHelper.FilesDelete(dbContext, webHostEnvironment, filesName);
                dbContext.Files.RemoveRange(files);

            }


            dbContext.Lists.Remove(list);

            

            await dbContext.SaveChangesAsync();
            return list;
        }

        public List<List> GetAllUserLists(long UserId)
        {
            //System.IO.File.Delete(webHostEnvironment.WebRootPath + "\\Files\\1649b550-d81c-408c-b8a1-12f22df6ef86.xls");
            return dbContext.Lists.Where(opt => opt.User.Id == UserId).Include(u => u.File).ToList();
        }

        public List? GetList(long ListId, long UserId)
        {
            return dbContext.Lists.Where(opt => opt.Id == ListId && opt.User.Id == UserId).Include(u=> u.File).FirstOrDefault();
            //return dbContext.Lists.Where(opt => opt.Id == ListId).FirstOrDefault();

                
        }

        

        public async Task<List?> UpdateListAsync(RequestUpdateList requestList, long UserId)
        {
            var list = dbContext.Lists.Where(opt => opt.Id == requestList.Id && opt.User.Id == UserId).FirstOrDefault();

            if (list == null) return null;

            if(requestList.Title != null) list.Title = requestList.Title;

            if(requestList.Image != null)
            {
                var file = await FileHelper.FileUpload(dbContext, webHostEnvironment, requestList.Image);
                list.File = file;
            }

            dbContext.Lists.Update(list);
            await dbContext.SaveChangesAsync();

            return list;
        }
    }
}
