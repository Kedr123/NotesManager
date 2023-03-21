﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Interfaces;
using server.Models.Requests;
using server.Repositories;

namespace server.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ListController : Controller
    {
        public readonly IRepositoryList repositoryList;

        public ListController(IRepositoryList repositoryList) => this.repositoryList = repositoryList;


        [HttpGet]
        [Route("{id}")]
        public ActionResult GetList(long Id)
        {
            //return Ok(Id);

            var list = repositoryList.GetList(Id, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (list == null) NotFound();

            return Ok(new { list });
        }

        [HttpGet]
        public ActionResult GetAllUserList()
        {
            var lists = repositoryList.GetAllUserLists(Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (lists == null) NotFound(null);

            return Ok(new {lists});
        }

        [HttpPost]
        [RequestSizeLimit(1024*1024*10)]
        public async Task<ActionResult<Models.List>> CreateList([FromForm] RequestCreateList request)
        {
            
            if (request == null) NotFound(null);


            var list = await repositoryList.CreateListAsync(request, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));
            

            return Ok(new { list });
        }

        [HttpPut]
        [RequestSizeLimit(1024 * 1024 * 10)]
        public async Task<ActionResult<Models.List>> UpdateList([FromForm] RequestUpdateList request)
        {
            var list = await repositoryList.UpdateListAsync(request, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));
            
            if (list == null) NotFound(null);

            return Ok(new { list });
        }

        [HttpDelete, Route("{Id}")]
        public async Task<ActionResult> DeleteList(long Id)
        {
            var list = await repositoryList.DeleteListAsync(Id, Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value));

            if (list == null) NotFound(null);

            return Ok(new { list });
        }
    }
}
