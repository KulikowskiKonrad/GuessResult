//using GuessResult.DB.Models;
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
//    public class ApiRoomController : ApiController
//    {
//        //private const int MaxIloscZnakow = 20;

//        private RoomRepository _roomRepository = new RoomRepository();

//        [HttpGet]
//        public IHttpActionResult GetAll()
//        {
//            //KeyValuePair<>

//            try
//            {
//                List<RoomListItem> result = _roomRepository.DownloadAll().Select(x => new RoomListItem()
//                {
//                    Details = x.Details,
//                    Id = x.Id,
//                    Name = x.Name
//                }).ToList();
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return InternalServerError();
//            }
//        }

//        [HttpDelete]
//        public IHttpActionResult Delete([FromUri]long id)
//        {
//            try
//            {
//                RRRoom roomToDelete = _roomRepository.Download(id);
//                roomToDelete.IsDeleted = true;
//                long? saveResult = _roomRepository.Save(roomToDelete);
//                if (saveResult.HasValue)
//                {
//                    return Ok();
//                }
//                else
//                {
//                    return InternalServerError();
//                }
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return InternalServerError();
//            }
//        }

//        [HttpPost]
//        public IHttpActionResult SaveDetails([FromBody]EditRoomViewModel model)
//        {
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    RRRoom room = null;
//                    if (model.Id.HasValue)
//                    {
//                        room = _roomRepository.Download(model.Id.Value);
//                    }
//                    else
//                    {
//                        room = new RRRoom();
//                    }
//                    room.Name = model.Name;
//                    room.Details = model.Details;
//                    long? rezultatZapisu = _roomRepository.Save(room);
//                    if (rezultatZapisu == null)
//                    {
//                        return InternalServerError();
//                    }
//                    else
//                    {
//                        return Ok();
//                    }
//                }
//                else
//                {
//                    return InternalServerError();
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
