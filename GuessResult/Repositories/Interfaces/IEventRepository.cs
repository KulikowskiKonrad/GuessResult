using GuessResult.DB.Models;
using GuessResult.Enum;
using GuessResult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessResult.Repositories.Interfaces
{
    public interface IEventRepository
    {

        List<EventListItem> GetAllEventListItems(EventStatus? filterEventStatus, bool filterOnlyMyEvents, long userId);
        List<GREvent> GetAll(EventStatus? filterEventStatus, bool filterOnlyMyEvents, long userId);
        GREvent GetById(long eventId);
        UserEventListItem GetUserEventListItemById(long eventId, long userId);
        GREvent GetByExternalMatchId(long externalMatchId);
        long? Save(GREvent singleEvent);
        List<ChartDataItem> GetEffectivityData(long userId, GeneralScoreType? effectivityFilterType);
        List<ChartDataItem> GetTopUsersData();
    }
}
