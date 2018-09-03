//using GuessResult.DB.Models;
//using GuessResult.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace GuessResult.Models
//{
//    public class ReservationListViewModel
//    {
//        public List<SelectListItem> ListaPomieszczen { get; set; }


//        public ReservationListViewModel()
//        {

//            ListaPomieszczen = new List<SelectListItem>();
//            RoomRepository roomRepository = new RoomRepository();
//            List<RRRoom> pobranePomieszczenia = roomRepository.DownloadAll();
//            foreach (RRRoom pomieszczenie in pobranePomieszczenia)
//            {
//                ListaPomieszczen.Add(new SelectListItem()
//                {
//                    Value = pomieszczenie.Id.ToString(),
//                    Text = pomieszczenie.Name
//                });
//            }
//        }
//    }
//}