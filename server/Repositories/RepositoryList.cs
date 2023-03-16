using Microsoft.EntityFrameworkCore;
using server.Helpers;
using server.Interfaces;
using server.Models;
using server.Models.Requests;
using System.Collections.Generic;

namespace server.Repositories
{
    public class RepositoryList : IRepositoryList
    {
        public readonly ApplicationDbContext dbContext;
        public readonly IWebHostEnvironment webHostEnvironment;

        public RepositoryList(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment) 
            => (this.dbContext, this.webHostEnvironment) = (dbContext, webHostEnvironment);

        

        public async Task<List> CreateListAsync(RequestCreateList request, long UserId)
        {
            var list = new List();
            list.Title = request.Title;
            var user = dbContext.Users.Find(UserId);
            list.User = user;

            if (request.Image != null)
            {
                var file = await FileHelper.FileUpload(dbContext, webHostEnvironment, request.Image);
                list.File = file;
            }
                
            
            dbContext.Lists.Add(list);
            await dbContext.SaveChangesAsync();

            return list;
        }

        public async Task<List?> DeleteListAsync(long ListId)
        {
            var list = dbContext.Lists.Find(ListId);
            dbContext.Lists.Where(u => u.Id == ListId).ExecuteDelete();
            await dbContext.SaveChangesAsync();
            return list;
        }

        public List<List> GetAllUserLists(long UserId)
        {
            return dbContext.Lists.Where(opt => opt.User.Id == UserId).Include(u => u.File).ToList();
        }

        public List? GetList(long ListId, long UserId)
        {
            return dbContext.Lists.Where(opt => opt.Id == ListId && opt.User.Id == UserId).Include(u=> u.File).FirstOrDefault();
            //return dbContext.Lists.Where(opt => opt.Id == ListId).FirstOrDefault();

                
        }

        

        public async Task<List?> UpdateListAsync(RequestUpdateList requestList, long UserId)
        {
            var list = dbContext.Lists.Where(opt => opt.Id == requestList.Id && opt.User.Id == UserId).FirstOrDefault();

            if (list == null) return null;

            if(requestList.Title != null) list.Title = requestList.Title;

            if(requestList.Image != null)
            {
                var file = await FileHelper.FileUpload(dbContext, webHostEnvironment, requestList.Image);
                list.File = file;
            }

            dbContext.Lists.Update(list);
            await dbContext.SaveChangesAsync();

            return list;
        }
    }
}
