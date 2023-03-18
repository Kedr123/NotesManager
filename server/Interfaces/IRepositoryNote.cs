using server.Models;
using server.Models.Requests;
using server.Models.Respons;

namespace server.Interfaces
{
    public interface IRepositoryNote
    {
        ResponseGetNoteAllFile? GetNote(long NoteId, long UserId);
        List<ResponseGetNoteAllFile>? GetAllNotesColumn(long ColumnId, long UserId);
        Task<Note?> CreateNote(RequestCreateNote request, long UserId);
        Task<Note?> UpdateNote(RequestUpdateNote request, long UserId);
        Task<Note?> DeleteNote(long NoteId, long UserId);

        //Task<Note?> CreateNoteFiles(IFormFileCollection files, long NoteId, long UserId);
        //Task<Note?> DeleteNoteFile(long NoteFileId, long UserId);
    }
}
