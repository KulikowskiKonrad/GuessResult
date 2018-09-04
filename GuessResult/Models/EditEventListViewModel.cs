//using GuessResult.DB.Models;
//using GuessResult.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace GuessResult.Models
//{
//    public class EditEventListViewModel
//    {

//       public List<SelectListItem> EventList { get; set; }


//        public EditEventListViewModel()
//        {

//            EventList = new List<SelectListItem>();
//            EventRepository eventRepository = new EventRepository();
//            List<GREvent> pobranePomieszczenia = eventRepository.GetAll();
//            foreach (GREvent singleEvent in pobranePomieszczenia)
//            {
//                EventList.Add(new SelectListItem()
//                {
//                    Value = singleEvent.Id.ToString(),
//                    Text = singleEvent.Name
//                });
//            }
//        }

//    }
//}