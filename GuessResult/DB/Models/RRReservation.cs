//using GuessResult.Enum;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Web;

//namespace GuessResult.DB.Models
//{
//    public class RRReservation
//    {
//        public long Id { get; set; }

//        public long RRRoomId { get; set; }

//        public long RRUserId { get; set; }

//        [DataType(DataType.Date)]
//        [Column(TypeName = "Date")]
//        public DateTime Date { get; set; }

//        public ReservationStatus Status { get; set; }

//        public bool IsDeleted { get; set; }

//        public virtual RRRoom Room { get; set; }

//        public virtual RRUser User { get; set; }
//    }
//}