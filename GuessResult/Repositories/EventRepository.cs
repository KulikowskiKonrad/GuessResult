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
using GuessResult.Repositories.Interfaces;

namespace GuessResult.Repositories
{
    public class EventRepository : IEventRepository
    {

        public List<EventListItem> GetAllEventListItems(EventStatus? filterEventStatus, bool filterOnlyMyEvents, long userId)
        {
            try
            {
                List<EventListItem> result = GetAll(filterEventStatus, filterOnlyMyEvents, userId)
                    .Select(x => new EventListItem()
                    {
                        AwayTeamName = x.AwayTeamName,
                        HomeTeamName = x.HomeTeamName,
                        Id = x.Id,
                        AwayTeamScore = x.AwayTeamScore,
                        HomeTeamScore = x.HomeTeamScore,
                        StartDate = x.StartDate,
                        UserAwayTeamScore = x.UserEvents.Where(y => y.UserId == userId && y.IsDeleted == false).SingleOrDefault()?.AwayTeamScore,
                        UserHomeTeamScore = x.UserEvents.Where(y => y.UserId == userId && y.IsDeleted == false).SingleOrDefault()?.HomeTeamScore,
                        EventPredictionType = x.PredictionType,
                        GeneralScoreType = x.UserEvents.Where(y => y.UserId == userId && y.IsDeleted == false).SingleOrDefault()?.GeneralScoreType
                    })
                .OrderByDescending(x => x.StartDate)
                .ToList();
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

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

        public UserEventListItem GetUserEventListItemById(long eventId, long userId)
        {
            try
            {
                UserEventRepository userEventRepository = new UserEventRepository();
                UserEventListItem result = new UserEventListItem();
                var eventFromDb = GetById(eventId);
                result.EventId = eventId;
                result.AwayTeamName = eventFromDb.AwayTeamName;
                result.HomeTeamName = eventFromDb.HomeTeamName;
                result.StartDate = eventFromDb.StartDate;
                result.EventPredictionType = eventFromDb.PredictionType;
                var userEventFromDb = userEventRepository.GetByEventIdAndUserId(eventId, userId);
                if (userEventFromDb != null)
                {
                    result.Id = userEventFromDb.Id;
                    result.AwayTeamScore = userEventFromDb.AwayTeamScore;
                    result.HomeTeamScore = userEventFromDb.HomeTeamScore;
                    result.GeneralScoreType = userEventFromDb.GeneralScoreType;
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

        public List<ChartDataItem> GetEffectivityData(long userId, GeneralScoreType? effectivityFilterType)
        {
            try
            {
                List<ChartDataItem> result = new List<ChartDataItem>();
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    List<GRUserEvent> listUserScore = db.UserEvents
                        .Where(x => x.UserId == userId && !x.IsDeleted
                            && x.IsCorrectPrediction.HasValue
                            && (!effectivityFilterType.HasValue || (effectivityFilterType == GeneralScoreType.AwayTeamWin && x.AwayTeamScore > x.HomeTeamScore)
                                || (effectivityFilterType == GeneralScoreType.HomeTeamWin && x.HomeTeamScore > x.AwayTeamScore)
                                || (effectivityFilterType == GeneralScoreType.Tie && x.AwayTeamScore == x.HomeTeamScore))).ToList();

                    int correctResultsCount = listUserScore.Where(x => x.IsCorrectPrediction.Value).Count();

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
                    List<GRUserEvent> listUserScore = db.UserEvents.Include(x => x.User)
                        .Where(x => !x.IsDeleted && x.IsCorrectPrediction.HasValue).ToList();
                    foreach (var user in listUserScore.Select(x => new { x.UserId, x.User.Email }).Distinct())
                    {
                        var userResults = listUserScore.Where(x => x.UserId == user.UserId).ToList();
                        int correctResultsCount = userResults.Where(x => x.IsCorrectPrediction.Value).Count();
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