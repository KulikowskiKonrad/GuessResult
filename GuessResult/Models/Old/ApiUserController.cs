//using GuessResult.DB.Models;
//using GuessResult.Enum;
//using GuessResult.Helpers;
//using GuessResult.Models;
//using GuessResult.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace GuessResult.Api
//{
//    public class ApiUserController : ApiController
//    {
//        private UserRepository _userRepository = new UserRepository();

//        [HttpPost]
//        public IHttpActionResult RegisterUser([FromBody] RegisterViewModel model)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    RRUser pobranyUzytkownik = _userRepository.GetByLogin(model.Email);
//                    if (pobranyUzytkownik == null)
//                    {
//                        string salt = Guid.NewGuid().ToString();

//                        RRUser uzytkownik = new RRUser()
//                        {
//                            Salt = salt,
//                            Email = model.Email,
//                            Password = MD5Helper.GenerateMD5(model.Password + salt),

//                            Role = UserRole.Standard
//                        };
//                        long? registeredUserId = _userRepository.Save(uzytkownik);
//                        if (registeredUserId != null)
//                        {
//                            return Ok(registeredUserId);
//                        }
//                        else
//                        {
//                            return Content(HttpStatusCode.BadRequest, "Błąd zapisu użytkownika");
//                        }
//                    }
//                    else
//                    {
//                        return Content(HttpStatusCode.BadRequest, "Login jest już zajęty");
//                    }
//                }
//                else
//                {
//                    return Content(HttpStatusCode.BadRequest, "Błędne dane");
//                }
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return InternalServerError();
//            }
//        }
//    }
//}
