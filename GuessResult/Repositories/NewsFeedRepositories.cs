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
        public List<NewsFeedListItem> GetAllNewsFeedListItems(long userId)
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
                        LikeCount = x.LikeCount,
                        CommentCount = x.CommentCount,
                        IsLikedByCurrentUser = x.NewsFeedLikes.Where(y => y.GRUserId == userId).Any()
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
                    allNewsFeed = db.NewsFeeds.Include(x => x.User).Include(x => x.NewsFeedLikes).Where(x => x.IsDeleted == false).ToList();
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

        public long? Save(SaveNewsFeedViewModel model, long userId)
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
                using (GuessResultContext db = new GuessResultContext())
                {
                    GRNewsFeed gRNewsFeed = null;
                    gRNewsFeed = db.NewsFeeds.Where(x => x.Id == id).Single();
                    gRNewsFeed.IsDeleted = true;
                    db.SaveChanges();
                    isDeleted = true;
                }
                return isDeleted;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return false;
            }
        }

        public bool Like(long id, long userId)
        {
            try
            {
                bool result = true;
                using (GuessResultContext db = new GuessResultContext())
                {
                    GRNewsFeed gRNewsFeed = null;
                    gRNewsFeed = db.NewsFeeds.Where(x => x.Id == id).Single();

                    GRNewsFeedLike gRNewsFeedLike = db.NewsFeedLikes.Where(x => x.GRNewsFeedId == id && x.GRUserId == userId).SingleOrDefault();
                    if (gRNewsFeedLike == null)
                    {
                        gRNewsFeed.LikeCount++;
                        db.NewsFeedLikes.Add(new GRNewsFeedLike()
                        {
                            GRNewsFeedId = id,
                            GRUserId = userId
                        });
                        db.SaveChanges();
                    }

                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return false;
            }
        }

        //public bool Comment(long id, long userId)
        //{
        //    try
        //    {
        //        bool result = true;
        //        using (GuessResultContext db = new GuessResultContext())
        //        {
        //            GRNewsFeed gRNewsFeed = null;
        //            gRNewsFeed = db.NewsFeed.Where(x => x.Id == id && x.User.Id == userId).Single();
        //            gRNewsFeed.CommentCount++;
        //            db.SaveChanges();
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Log.Error(ex);
        //        return false;
        //    }
        //}
    }
}