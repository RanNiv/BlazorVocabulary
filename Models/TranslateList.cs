using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Blazorvocabulary.Models
{
    public partial class TranslateList
    {
  
      [Key]
        public int RowId { get; set; }
        public string Entry { get; set; }
        public string TranslationFull { get; set; }
        public string EnglishTranslate { get; set; }
    }
}
