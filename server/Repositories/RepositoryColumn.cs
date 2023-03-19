using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using server.Helpers;
using server.Interfaces;
using server.Models;
using server.Models.Requests;
using System.Collections.Generic;

namespace server.Repositories
{
    public class RepositoryColumn : IRepositoryColumn
    {
        public readonly ApplicationDbContext dbContext;
        public readonly IWebHostEnvironment webHostEnvironment;

        public RepositoryColumn(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
            => (this.dbContext, this.webHostEnvironment) = (dbContext, webHostEnvironment);

        public async Task<Column?> CreateColumn(RequestCreateColumn request, long UserId)
        {
            var list = dbContext.Lists.Where(opt => opt.Id == request.ListId && opt.User.Id == UserId).FirstOrDefault();

            if (list == null) return null;

            Column column = new Column();
            column.list = list;
            column.Title = request.Title;

            dbContext.Columns.Add(column);
            await dbContext.SaveChangesAsync();

            return column;
        }

        public async Task<Column?> DeleteColumn(long ColumnId, long UserId)
        {
            var column = dbContext.Columns.Where(opt => opt.Id == ColumnId && opt.list.User.Id == UserId).FirstOrDefault();

            if (column == null) return null;

            
            // Получение файлов заметок, которые удалятся при удалении колонки
            var noteFiles = dbContext.NoteFiles.Where(opt => opt.Note.Column.Id == ColumnId).Include(u => u.File).ToList();

            

            // Удаление всех файлов, относящихся к заметкам в колонке
            if (noteFiles != null)
            {
                List<string> filesName = new List<string>();
                List<Models.File> files = new List<Models.File>();

                foreach (var element in noteFiles)
                {
                    if(element.File?.FileName == null) continue;

                    filesName.Add(element.File.FileName);
                    files.Add(element.File);
                }

                FileHelper.FilesDelete(dbContext, webHostEnvironment, filesName);
                dbContext.Files.RemoveRange(files);
                await dbContext.SaveChangesAsync();

            }



            dbContext.Columns.Remove(column);
            await dbContext.SaveChangesAsync();

            return column;
        }

        public Column? GetColumn(long ColumnId, long UserId)
        {
            return dbContext.Columns.Where(opt => opt.Id == ColumnId && opt.list.User.Id == UserId).FirstOrDefault();
            
        }

        public List<Column?> GetListColumns(long ListId, long UserId)
        {
            return dbContext.Columns.Where(opt => opt.list.Id == ListId && opt.list.User.Id == UserId).ToList<Column?>();
        }

        public async Task<Column?> UpdateColumn(RequestUpdateColumn request, long UserId)
        {
            var column = dbContext.Columns.Where(opt => opt.Id == request.Id && opt.list.User.Id == UserId).FirstOrDefault();

            if (column == null) return null;

            if(request.Title != null) column.Title = request.Title;

            if(request.ListId != null)
            {
                var list = dbContext.Lists.Where(opt => opt.Id == request.ListId && opt.User.Id == UserId).FirstOrDefault();

                if(list != null) column.list = list;
            }

            dbContext.Columns.Update(column);
            await dbContext.SaveChangesAsync();

            return column;

        }
    }
}
