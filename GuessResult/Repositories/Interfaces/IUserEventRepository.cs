using GuessResult.DB.Models;
using GuessResult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessResult.Repositories.Interfaces
{
    public interface IUserEventRepository
    {

        List<GRUserEvent> GetAll();
        GRUserEvent GetByEventIdAndUserId(long eventId, long userId);
        GRUserEvent GetById(long id);
        GRUserEvent GetByUserId(long userId);
        long? Save(UserEventListItem model, long userId, bool isAdmin);
        long? Save(GRUserEvent userEvent);
        bool UpdateIsPredictionCorrect(long eventId);
    }
}
