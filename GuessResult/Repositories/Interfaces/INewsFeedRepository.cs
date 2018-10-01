using GuessResult.DB.Models;
using GuessResult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessResult.Repositories.Interfaces
{
    public interface INewsFeedRepository
    {
        List<GRNewsFeed> GetAll();
        List<NewsFeedListItem> GetAllNewsFeedListItems();
        GRNewsFeed GetNewsFeedById(long id);
        bool Delete(long id);
        long? Save(NewsFeedListItem model, long userId);
    }
}
