//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace GuessResult.Models
//{
//    public class RegisterViewModel
//    {
//        //[Required(ErrorMessage = "Pole wymagane")]
//        //[EmailAddress]
//        [Display(Name = "Email")]
//        public string Email { get; set; }

//        //[Required(ErrorMessage = "Pole wymagane")]
//        //[StringLength(100, ErrorMessage = "Niepoprawna ilość znaków.", MinimumLength = 6)]
//        [DataType(DataType.Password)]
//        [Display(Name = "Hasło")]
//        public string Password { get; set; }

//        //[Required(ErrorMessage = "Pole wymagane")]
//        [DataType(DataType.Password)]
//        [Display(Name = "Powtórz hasło")]
//        //[Compare("Password", ErrorMessage = "Hasła niezgodne.")]
//        public string ConfirmPassword { get; set; }
//    }
//}