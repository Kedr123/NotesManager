namespace server.Models.Respons
{
    public class ResponseGetNoteAllFile
    {
        public Note? Note { get; set; }
        public List<File>? Files { get; set; }
    }
}
