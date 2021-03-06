﻿using GuessResult.DB.Models;
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
        List<NewsFeedListItem> GetAllNewsFeedListItems(long userId);
        GRNewsFeed GetNewsFeedById(long id);
        bool Delete(long id);
        long? Save(SaveNewsFeedViewModel model, long userId);
        bool Like(long id, long userId);
        //bool Comment(long id, long userId);
    }
}
