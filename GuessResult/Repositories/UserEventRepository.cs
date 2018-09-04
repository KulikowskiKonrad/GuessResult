//using GuessResult.DB.Models;
//using GuessResult.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace GuessResult.Repositories
//{
//    public class UserEventRepository
//    {
//        public GRUserEvent GetById(long id)
//        {
//            try
//            {
//                using(DB.GuessResultContext db = new DB.GuessResultContext())
//                {
//                    GRUserEvent result = db.UserEvents.Where(x => x.Id == id).SingleOrDefault();
//                    return result;
//                }
//            }catch(Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return null;
//            }
//        }

//        public long? Save(GRUserEvent userEvent)
//        {
//            try
//            {
//                long? result = null;
//                using (DB.GuessResultContext db = new DB.GuessResultContext())
//                {
//                    db.Entry(userEvent).State = userEvent.Id > 0 ? EntityState.Modified : EntityState.Added;
//                    db.SaveChanges();
//                    result = userEvent.Id;
//                }
//                return result;
//            }
//            catch (Exception ex)
//            {
//                LogHelper.Log.Error(ex);
//                return null;
//            }
//        }
//    }
//}