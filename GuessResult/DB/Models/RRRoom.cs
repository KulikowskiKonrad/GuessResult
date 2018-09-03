//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace GuessResult.DB.Models
//{
//    public class RRRoom
//    {
//        public long Id { get; set; }

//        [StringLength(200)]
//        [Required]
//        public string Details { get; set; }

//        [StringLength(100)]
//        [Required]
//        public string Name { get; set; }

//        public bool IsDeleted { get; set; }

//        public virtual ICollection<RRReservation> Reservations { get; set; }
//    }
//}