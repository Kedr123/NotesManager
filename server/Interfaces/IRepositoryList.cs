using server.Models.Requests;

namespace server.Interfaces
{
    public interface IRepositoryList
    {
        List<Models.List> GetAllUserLists(long UserId);

        Models.List? GetList(long ListId, long UserId);

        Task<Models.List> CreateListAsync(RequestCreateList request, long UserId);

        Task<Models.List?> UpdateListAsync(RequestUpdateList requestList, long UserId);

        Task<Models.List?> DeleteListAsync(long ListId);

        
    }
}
