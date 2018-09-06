using GuessResult.DB.Models;
using GuessResult.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuessResult.Repositories
{
    public class UserEventRepository
    {

        public List<GRUserEvent> GetAll()
        {
            try
            {
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    List<GRUserEvent> listUserEvents = db.UserEvents.Include(x => x.Event).Include(x => x.User).Where(x => x.IsDeleted == false).ToList();
                    return listUserEvents;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public GRUserEvent GetByEventIdAndUserId(long eventId, long userId)
        {
            try
            {
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    GRUserEvent result = db.UserEvents.Where(x => x.EventId == eventId && x.UserId == userId && !x.IsDeleted).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public GRUserEvent GetByUserId(long userId)
        {
            try
            {
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    GRUserEvent result = db.UserEvents.Where(x => x.UserId == userId).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public long? Save(GRUserEvent userEvent)
        {
            try
            {
                long? result = null;
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    db.Entry(userEvent).State = userEvent.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.SaveChanges();
                    result = userEvent.Id;
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