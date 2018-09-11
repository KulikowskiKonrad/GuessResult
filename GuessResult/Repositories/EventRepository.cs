using GuessResult.DB.Models;
using GuessResult.Enum;
using GuessResult.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuessResult.Repositories
{
    public class EventRepository
    {
        public List<GREvent> GetAll(EventStatus? filterEventStatus)
        {
            try
            {
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    List<GREvent> listEvents = db.Events.Include(x => x.UserEvents).Where(x => x.IsDeleted == false
                        && (!filterEventStatus.HasValue
                            || (filterEventStatus.Value == EventStatus.Przyszly && x.StartDate > DateTime.Now)
                            || (filterEventStatus.Value == EventStatus.Zakonczony && x.StartDate <= DateTime.Now))).ToList();
                    return listEvents;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }


        public GREvent GetById(long eventId)
        {
            try
            {
                GREvent result = null;
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    result = db.Events.Where(x => x.Id == eventId).SingleOrDefault();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Save(GREvent singleEvent)
        {
            try
            {
                long? result = null;
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    db.Entry(singleEvent).State = singleEvent.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.SaveChanges();
                    result = singleEvent.Id;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

    }
}