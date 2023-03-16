using server.Models;
using server.Models.Requests;

namespace server.Interfaces
{
    public interface IRepositoryColumn
    {
        Task<Column?> CreateColumn(RequestCreateColumn request, long UserId);
        Task<Column?> UpdateColumn(RequestUpdateColumn request, long UserId);
        Task<Column?> DeleteColumn(long ColumnId,long UserId);
        List<Column?> GetListColumns(long ListId,long UserId);
        Column? GetColumn(long ColumnId, long UserId);
    }
}
