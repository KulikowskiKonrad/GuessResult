using GuessResult.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessResult.Repositories.Interfaces
{
    public interface INewsFeedCommentRepository
    {
        List<GRNewsFeedComment> GetAllNewsFeedComment();
        long? Save(GRNewsFeedComment gRNewsFeedComment);
        GRNewsFeedComment GetById(long id);
    }
}
