using GuessResult.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuessResult.DB.Models;
using GuessResult.DB;
using GuessResult.Helpers;

namespace GuessResult.Repositories
{
    public class NewsFeedCommentRepositories : INewsFeedCommentRepository
    {
        public List<GRNewsFeedComment> GetAllNewsFeedComment()
        {
            try
            {
                List<GRNewsFeedComment> allNewsFeed = null;
                using (GuessResultContext db = new GuessResultContext())
                {
                    allNewsFeed = db.NewsFeedComment.Where(x => x.IsDeleted == false).ToList();
                }
                return allNewsFeed;
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

        public long? Save(GRNewsFeedComment gRNewsFeedComment)
        {
            throw new NotImplementedException();
        }
    }
}