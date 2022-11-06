using System;
using System.Collections.Generic;

namespace Database_Access_Level
{
    public partial class Mem
    {
        public int MemId { get; set; }
        public int UserId { get; set; }
        public int CollectionId { get; set; }
        public string QuestionText { get; set; } = null!;
        public string AnswerText { get; set; } = null!;
        public string? AdditionalInfo { get; set; }
        public string? ImagePath { get; set; }
        public DateTime? ReviewTime { get; set; }

        public virtual Collection Collection { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
