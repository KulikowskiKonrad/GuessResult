using GuessResult.DB.Models;
using GuessResult.Enum;
using GuessResult.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using GuessResult.Models;

namespace GuessResult.Repositories
{
    public class EventRepository
    {
        public List<GREvent> GetAll(EventStatus? filterEventStatus, bool filterOnlyMyEvents, long userId)
        {
            try
            {
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    List<GREvent> listEvents = db.Events.Include(x => x.UserEvents).Where(x => x.IsDeleted == false
                        && (!filterEventStatus.HasValue
                            || (filterEventStatus.Value == EventStatus.Scheduled && x.StartDate <= DateTime.Now)
                            || (filterEventStatus.Value == EventStatus.Finished && x.StartDate > DateTime.Now))
                        && (filterOnlyMyEvents == false || x.UserEvents.Where(y => y.UserId == userId && !y.IsDeleted).Any())
                           ).ToList();
                    return listEvents;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        //public List<GREvent> GetAll(EventStatus? filterEventStatus, bool filterOnlyMyEvents, long userId)
        //{
        //    try
        //    {
        //        using (DB.GuessResultContext db = new DB.GuessResultContext())
        //        {
        //            List<GREvent> listEvents = db.Events.Include(x => x.UserEvents).Where(x => x.IsDeleted == false
        //              && x.
        //                   )).ToList();
        //            return listEvents;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log.Error(ex);
        //        return null;
        //    }
        //}
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

        public GREvent GetByExternalMatchId(long externalMatchId)
        {
            try
            {
                GREvent result = null;
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    result = db.Events.Where(x => x.ExternalMatchId == externalMatchId).SingleOrDefault();
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

        public List<ChartDataItem> GetEffectivityData(long userId, EffectivityFilterType? effectivityFilterType)
        {
            try
            {
                List<ChartDataItem> result = new List<ChartDataItem>();
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    List<GRUserEvent> listUserScore = db.UserEvents.Include(x => x.Event)
                        .Where(x => x.UserId == userId && !x.IsDeleted
                            && x.Event.AwayTeamScore != null
                            && (!effectivityFilterType.HasValue || (effectivityFilterType == EffectivityFilterType.AwayTeamWin && x.AwayTeamScore > x.HomeTeamScore)
                                || (effectivityFilterType == EffectivityFilterType.HomeTeamWin && x.HomeTeamScore > x.AwayTeamScore)
                                || (effectivityFilterType == EffectivityFilterType.AwayTeamWin && x.AwayTeamScore == x.HomeTeamScore))).ToList();
                    int correctResultsCount = listUserScore.Where(x => (x.HomeTeamScore > x.AwayTeamScore && x.Event.HomeTeamScore > x.Event.AwayTeamScore)
                        || (x.HomeTeamScore == x.AwayTeamScore && x.Event.HomeTeamScore == x.Event.AwayTeamScore)
                        || (x.HomeTeamScore < x.AwayTeamScore && x.Event.HomeTeamScore < x.AwayTeamScore)).Count();
                    int wrongResultsCount = listUserScore.Count - correctResultsCount;
                    result.Add(new ChartDataItem()
                    {
                        Label = "poprawne",
                        Value = correctResultsCount
                    });
                    result.Add(new ChartDataItem()
                    {
                        Label = "błędne",
                        Value = wrongResultsCount
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public List<ChartDataItem> GetTopUsersData()
        {
            try
            {
                List<ChartDataItem> result = new List<ChartDataItem>();
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    List<GRUserEvent> listUserScore = db.UserEvents.Include(x => x.Event).Include(x => x.User)
                        .Where(x => !x.IsDeleted
                            && x.Event.AwayTeamScore != null).ToList();
                    foreach (var user in listUserScore.Select(x => new { x.UserId, x.User.Email }).Distinct())
                    {
                        var userResults = listUserScore.Where(x => x.UserId == user.UserId).ToList();
                        int correctResultsCount = userResults.Where(x => (x.HomeTeamScore > x.AwayTeamScore && x.Event.HomeTeamScore > x.Event.AwayTeamScore)
                        || (x.HomeTeamScore == x.AwayTeamScore && x.Event.HomeTeamScore == x.Event.AwayTeamScore)
                        || (x.HomeTeamScore < x.AwayTeamScore && x.Event.HomeTeamScore < x.AwayTeamScore)).Count();
                        result.Add(new ChartDataItem()
                        {
                            Label = user.Email,
                            Value = (decimal)Math.Round(((double)correctResultsCount / userResults.Count * 100))
                        });
                    }
                }
                return result.OrderByDescending(x => x.Value).Take(10).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }
    }
}