using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.DB.Models
{
    public class GRFile
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string OriginalFileName { get; set; }

        [Required]
        [StringLength(50)]
        public string GeneratedFileName { get; set; }

        [Required]
        [StringLength(10)]
        public string Extension { get; set; }
    }
}