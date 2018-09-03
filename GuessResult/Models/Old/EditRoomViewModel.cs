//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace GuessResult.Models
//{
//    public class EditRoomViewModel
//    {

//        public long? Id { get; set; }

//        [StringLength(200)]
//        [Required(ErrorMessage = "Pole wymagane")]
//        [Display(Name = "Szczegóły")]
//        public string Details { get; set; }

//        [StringLength(100, ErrorMessage = "Niepoprawna ilość znaków")]
//        [Required(ErrorMessage = "Pole wymagane")]
//        [Display(Name = "Nazwa")]
//        public string Name { get; set; }

//        public bool IsDeleted { get; set; }

//    }
//}