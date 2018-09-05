using GuessResult.DB.Models;
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
        public List<GREvent> GetAll()
        {
            try
            {
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    List<GREvent> listEvents = db.Event.Where(x=>x.IsDeleted==false).ToList();
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
                    result = db.Event.Where(x => x.Id == eventId).SingleOrDefault();
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