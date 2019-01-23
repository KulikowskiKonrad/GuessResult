using GuessResult.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuessResult.DB.Models;
using GuessResult.DB;
using GuessResult.Helpers;
using GuessResult.Models;
using System.Data.Entity;

namespace GuessResult.Repositories
{
    public class NewsFeedCommentRepositories : INewsFeedCommentRepository
    {
        public List<GRNewsFeedComment> GetAll()
        {
            try
            {
                List<GRNewsFeedComment> allNewsFeed = null;
                using (GuessResultContext db = new GuessResultContext())
                {
                    allNewsFeed = db.NewsFeedComments.Where(x => x.IsDeleted == false).ToList();
                }
                return allNewsFeed;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public List<GRNewsFeedComment> GetByNewsFeedId(long newsFeedId)
        {
            try
            {
                List<GRNewsFeedComment> listNewsFeedComment = null;
                using (GuessResultContext db = new GuessResultContext())
                {
                    listNewsFeedComment = db.NewsFeedComments.Include(x => x.User).Where(x => x.GRNewsFeedId == newsFeedId).ToList();
                }
                return listNewsFeedComment;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
                return null;
            }
        }

        public List<NewsFeedCommentListItem> GetAllNewsFeedComment(long newsFeedId)
        {
            try
            {
                List<NewsFeedCommentListItem> result = GetByNewsFeedId(newsFeedId)
                    .Select(x => new NewsFeedCommentListItem()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        InsertDate = x.InsertDate,
                        InsertUserEmail = x.User.Email,
                        LikeCount = x.CountLike
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

        public GRNewsFeedComment GetById(long id)
        {
            throw new NotImplementedException();
        }

        public long? Save(SaveNewsFeedCommentViewModel model, long userId)
        {
            try
            {
                long? result = null;

                GRNewsFeedComment newsFeedComment = null;
                if (model.Id.HasValue)
                {
                    //userEvent = GetById(model.Id.Value);
                }
                else
                {
                    newsFeedComment = new GRNewsFeedComment()
                    {
                        InsertDate = DateTime.Now,
                        GRUserId = userId,
                        GRNewsFeedId = model.NewsFeedId
                    };
                }
                newsFeedComment.Content = model.Content;

                using (DB.GuessResultContext db = new DB.GuessResultContext())
                {
                    db.Entry(newsFeedComment).State = newsFeedComment.Id > 0 ? EntityState.Modified : EntityState.Added;
                    if (newsFeedComment.Id == 0)
                    {
                        GRNewsFeed gRNewsFeed = db.NewsFeeds.Where(x => x.Id == model.NewsFeedId).Single();
                        gRNewsFeed.CommentCount++;
                    }
                    db.SaveChanges();
                    result = newsFeedComment.Id;
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