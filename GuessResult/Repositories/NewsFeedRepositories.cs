using GuessResult.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuessResult.DB.Models;
using GuessResult.Helpers;
using GuessResult.DB;
using GuessResult.Models;
using System.Data.Entity;

namespace GuessResult.Repositories
{
    public class NewsFeedRepositories : INewsFeedRepository
    {
        public List<NewsFeedListItem> GetAllNewsFeedListItems()
        {
            try
            {
                List<NewsFeedListItem> result = GetAll()
                    .Select(x => new NewsFeedListItem()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        InsertDate = x.InsertDate,
                        InsertUserEmail = x.User.Email,
                        LikeCount = x.LikeCount
                    })
                .OrderByDescending(x => x.InsertDate)
                .ToList();
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public List<GRNewsFeed> GetAll()
        {
            try
            {
                List<GRNewsFeed> allNewsFeed = null;
                using (GuessResultContext db = new GuessResultContext())
                {
                    allNewsFeed = db.NewsFeed.Include(x => x.User).Where(x => x.IsDeleted == false).ToList();
                }
                return allNewsFeed;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public GRNewsFeed GetNewsFeedById(long id)
        {
            throw new NotImplementedException();
        }

        public long? Save(NewsFeedListItem model, long userId)
        {
            try
            {
                long? result = null;

                GRNewsFeed newsFeed = null;
                if (model.Id.HasValue)
                {
                    //userEvent = GetById(model.Id.Value);
                }
                else
                {
                    newsFeed = new GRNewsFeed()
                    {
                        InsertDate = DateTime.Now,
                        GRUserId = userId
                    };
                }

                newsFeed.Content = model.Content;

                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    db.Entry(newsFeed).State = newsFeed.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.SaveChanges();
                    result = newsFeed.Id;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                bool isDeleted = false;
                using(GuessResultContext db = new GuessResultContext())
                {
                    GRNewsFeed gRNewsFeed = null;
                    gRNewsFeed = db.NewsFeed.Where(x => x.Id == id).Single();
                    gRNewsFeed.IsDeleted = true;
                    db.SaveChanges();
                    isDeleted = true;
                }
                return isDeleted;
            }
            catch(Exception ex)
            {
                LogHelper.Log.Error(ex);
                return false;
            }
        }
    }
}