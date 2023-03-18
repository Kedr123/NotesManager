﻿using System.ComponentModel.DataAnnotations;

namespace server.Models.Requests
{
    public class RequestUpdateNote
    {
        [Required]
        public long NoteId { get; set; }

        public string? Text { get; set; }

        public List<long>? IdNoteFilesDelete { get; set; }

        public IFormFileCollection? Files { get; set; }
    }
}
