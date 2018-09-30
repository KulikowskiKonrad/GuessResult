using GuessResult.DB.Models;
using GuessResult.Enum;
using GuessResult.Helpers;
using GuessResult.Models;
using GuessResult.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GuessResult.Repositories
{
    public class UserEventRepository : IUserEventRepository
    {
        private DB.GuessResultContext _db = new DB.GuessResultContext();
        private IEventRepository _eventRepository;

        public UserEventRepository()
        {

        }


        public UserEventRepository(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

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

        public GRUserEvent GetById(long id)
        {
            try
            {
                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    GRUserEvent result = db.UserEvents.Where(x => x.Id == id).SingleOrDefault();
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

        public long? Save(UserEventListItem model, long userId, bool isAdmin)
        {
            try
            {
                GRUserEvent userEvent = null;
                if (model.Id.HasValue)
                {
                    userEvent = GetById(model.Id.Value);
                }
                else
                {
                    userEvent = new GRUserEvent();
                }

                var eventFromDb = _eventRepository.GetById(model.EventId);
                if (eventFromDb.AwayTeamScore != null)
                {
                    return null;
                }
                if (isAdmin && model.EventPredictionType.HasValue)
                {
                    eventFromDb.PredictionType = model.EventPredictionType.Value;
                    _eventRepository.Save(eventFromDb);
                }

                if (eventFromDb.PredictionType == Enum.EventPredictionType.ExactScore)
                {
                    userEvent.HomeTeamScore = model.HomeTeamScore.Value;
                    userEvent.AwayTeamScore = model.AwayTeamScore.Value;
                    userEvent.GeneralScoreType = null;
                }
                else
                {
                    userEvent.HomeTeamScore = null;
                    userEvent.AwayTeamScore = null;
                    userEvent.GeneralScoreType = model.GeneralScoreType;
                }

                model.UserId = userId;
                userEvent.UserId = model.UserId;
                userEvent.EventId = model.EventId;

                long? saveResult = Save(userEvent);
                return saveResult;
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
                _db.Entry(userEvent).State = userEvent.Id > 0 ? EntityState.Modified : EntityState.Added;
                //db.UserEvents.Add(new GRUserEvent
                //{
                //    AwayTeamScore = userEvent.AwayTeamScore,
                //    EventId = userEvent.EventId,
                //    HomeTeamScore = userEvent.AwayTeamScore,
                //    UserId = userEvent.UserId
                //});
                _db.SaveChanges();
                result = userEvent.Id;
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public bool UpdateIsPredictionCorrect(long eventId)
        {
            try
            {
                GREvent singleEvent = _db.Events.Where(x => x.Id == eventId).Single();
                if (singleEvent.HomeTeamScore.HasValue)
                {
                    List<GRUserEvent> listgRUserEvent = _db.UserEvents.Where(x => x.EventId == singleEvent.Id).ToList();
                    foreach (var singleUserEvent in listgRUserEvent)
                    {
                        if (singleEvent.PredictionType == EventPredictionType.ExactScore)
                        {
                            if (singleEvent.HomeTeamScore == singleUserEvent.HomeTeamScore && singleEvent.AwayTeamScore == singleUserEvent.AwayTeamScore)
                            {
                                singleUserEvent.IsCorrectPrediction = true;
                            }
                            if (singleEvent.HomeTeamScore != singleUserEvent.HomeTeamScore)
                            {
                                singleUserEvent.IsCorrectPrediction = false;
                            }
                        }
                        else if (singleEvent.PredictionType == EventPredictionType.GeneralScore)
                        {
                            if (singleEvent.HomeTeamScore > singleEvent.AwayTeamScore && GeneralScoreType.HomeTeamWin == singleUserEvent.GeneralScoreType)
                            {
                                singleUserEvent.IsCorrectPrediction = true;
                            }
                            else if (singleEvent.HomeTeamScore < singleEvent.AwayTeamScore && GeneralScoreType.AwayTeamWin == singleUserEvent.GeneralScoreType)
                            {
                                singleUserEvent.IsCorrectPrediction = true;
                            }
                            else if (singleEvent.HomeTeamScore == singleEvent.AwayTeamScore && GeneralScoreType.Tie == singleUserEvent.GeneralScoreType)
                            {
                                singleUserEvent.IsCorrectPrediction = true;
                            }
                            else
                            {
                                singleUserEvent.IsCorrectPrediction = false;
                            }
                        }
                        Save(singleUserEvent);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return false;
            }
        }
    }
}
