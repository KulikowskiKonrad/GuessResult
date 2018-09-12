using GuessResult.DB.Models;
using GuessResult.Enum;
using GuessResult.Extensions;
using GuessResult.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuessResult.Models
{
    public class EventListViewModel
    {

        public List<SelectListItem> StatusList { get; set; }


        public EventListViewModel()
        {
            StatusList = new List<SelectListItem>();
            StatusList.Add(new SelectListItem()
            {
                Text = EventStatus.Scheduled.GetEnumDescription(),
                Value = EventStatus.Scheduled.ToString()
            });
            StatusList.Add(new SelectListItem()
            {
                Text = EventStatus.Finished.GetEnumDescription(),
                Value = EventStatus.Finished.ToString()
            });
        }

    }
}