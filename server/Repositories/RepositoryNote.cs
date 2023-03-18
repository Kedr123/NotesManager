using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using server.Helpers;
using server.Interfaces;
using server.Models;
using server.Models.Requests;
using server.Models.Respons;

namespace server.Repositories
{
    public class RepositoryNote : IRepositoryNote
    {

        public readonly ApplicationDbContext dbContext;
        public readonly IWebHostEnvironment webHostEnvironment;

        public RepositoryNote(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
            => (this.dbContext, this.webHostEnvironment) = (dbContext, webHostEnvironment);

        public async Task<Note?> CreateNote(RequestCreateNote request, long UserId)
        {
            var column = dbContext.Columns.Where(opt => opt.Id == request.ColumnId && opt.list.User.Id == UserId).FirstOrDefault();

            if (column == null) return null;

            var note = new Note();
            note.Text = request.Text;
            note.Column = column;
            note.CreateTime = DateTime.UtcNow;
            dbContext.Notes.Add(note);

            if(request.files != null)
            {
                var files = await FileHelper.FilesUpload(dbContext, webHostEnvironment, request.files);

                foreach(var file in files)
                {
                    var noteFile = new NoteFile();

                    noteFile.File = file;
                    noteFile.Note = note;

                    dbContext.NoteFiles.Add(noteFile);
                }
            }

            await dbContext.SaveChangesAsync();

            return note;

        }

        public async Task<Note?> DeleteNote(long NoteId, long UserId)
        {
            var note = dbContext.Notes.Where(opt => opt.Id == NoteId && opt.Column.list.User.Id == UserId).FirstOrDefault();

            if (note == null) return null;

            // Получение файлов заметки
            var noteFiles = dbContext.NoteFiles.Where(opt => opt.Note.Id == NoteId).Include(u => u.File).ToList();

            // Удаление всех файлов, относящихся к заметке
            if (noteFiles != null)
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

            dbContext.Notes.Remove(note);
            await dbContext.SaveChangesAsync();

            return note;
        }

        public List<ResponseGetNoteAllFile>? GetAllNotesColumn(long ColumnId, long UserId)
        {
            var listNote = dbContext.Notes.Where(opt => opt.Column.Id == ColumnId && opt.Column.list.Id == UserId).ToList();
            var response = new List<ResponseGetNoteAllFile>();

            if (listNote == null) return null;

            foreach (var note in listNote)
            {
                var newResponse = new ResponseGetNoteAllFile();

                newResponse.Note = dbContext.Notes.Where(opt => opt.Id == note.Id && opt.Column.list.User.Id == UserId).FirstOrDefault();

                if (newResponse.Note != null) 
                { 
                    newResponse.Files = dbContext.NoteFiles.Where(opt => opt.Note == newResponse.Note).ToList(); 
                    response.Add(newResponse);
                }

            }

            return response;
        }

        public ResponseGetNoteAllFile? GetNote(long NoteId, long UserId)
        {
            var response = new ResponseGetNoteAllFile();

            response.Note = dbContext.Notes.Where(opt=> opt.Id == NoteId && opt.Column.list.User.Id == UserId).FirstOrDefault();

            if (response.Note == null) return null;

            response.Files = dbContext.NoteFiles.Where(opt => opt.Note == response.Note).ToList();

            return response;
        }

        public async Task<Note?> UpdateNote(RequestUpdateNote request, long UserId)
        {
            var note = dbContext.Notes.Where(opt => opt.Id == request.NoteId && opt.Column.list.User.Id == UserId).FirstOrDefault();

            if (note == null) return null;

            if (request.Text != null) note.Text = request.Text;
            
            note.UpdateTime = DateTime.UtcNow;

            if (request.Files != null)
            {
                var files = await FileHelper.FilesUpload(dbContext, webHostEnvironment, request.Files);

                foreach (var file in files)
                {
                    var noteFile = new NoteFile();

                    noteFile.File = file;
                    noteFile.Note = note;

                    dbContext.NoteFiles.Add(noteFile);
                }
            }

            if(request.IdNoteFilesDelete != null)
            {

                // Получение файлов заметки, указанных в списке
                var noteFiles = new List<NoteFile>();

                foreach(var id in request.IdNoteFilesDelete)
                {
                    var file = dbContext.NoteFiles.Where(opt=>opt.Id == id && opt.Note.Column.list.User.Id==UserId).FirstOrDefault();

                    if(file != null) noteFiles.Add(file);

                }

                // Удаление файлов, указанных в списке
                if (noteFiles != null)
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
                    dbContext.NoteFiles.RemoveRange(noteFiles);

                }

            }


            await dbContext.SaveChangesAsync();
            return note;

        }
    }
}
