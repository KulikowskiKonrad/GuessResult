using GuessResult.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuessResult.DB.Models
{
    public class GRUser
    {

        public long Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        public UserRole Role { get; set; }

    }
}