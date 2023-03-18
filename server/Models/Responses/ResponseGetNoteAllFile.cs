namespace server.Models.Respons
{
    public class ResponseGetNoteAllFile
    {
        public Note? Note { get; set; }
        public List<NoteFile>? Files { get; set; }
    }
}
