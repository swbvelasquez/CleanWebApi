using AutoMapper;
using CleanWebApi.Api.Responses;
using CleanWebApi.Core.CustomEntities;
using CleanWebApi.Core.DTOs;
using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Interfaces;
using CleanWebApi.Core.QueryFilters;
using CleanWebApi.Core.Services;
using CleanWebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CleanWebApi.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            this.postService = postService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<PagedList<PostDTO>>))] //Para indicar en la documentacion el tipo de dato y respuesta
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<PagedList<PostDTO>>))]
        public IActionResult GetPosts([FromQuery] PostQueryFilter filters) //PostQueryFilter objeto complejo para mandar multiples parametros
        {
            PagedList<Post> posts = postService.GetPosts(filters);
            PagedList<PostDTO> postsDTO = mapper.Map<PagedList<PostDTO>>(posts);
            ApiResponse<PagedList<PostDTO>> response = new ApiResponse<PagedList<PostDTO>>(postsDTO);

            //Agregando al header de la respuesta un json
            var metadata = new {
                                posts.TotalCount,
                                posts.TotalPages,
                                posts.PageSize,
                                posts.CurrentPage,
                                posts.HasNextPage,
                                posts.HasPreviousPage
                               };

            Response.Headers.Add("X-pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            Post post = await postService.GetPost(id);
            PostDTO postDTO = mapper.Map<PostDTO>(post);
            ApiResponse<PostDTO> response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> InsertPost(PostDTO postDTO)
        {
            Post post = mapper.Map<Post>(postDTO);
            int result = await postService.InsertPost(post);

            if (result <= 0)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetPost),new { id = post.Id},postDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id,PostDTO postDTO)
        {
            Post post = mapper.Map<Post>(postDTO);

            if (postDTO.PostId != id)
            {
                return BadRequest();
            }

            int result = await postService.UpdatePost(post);

            if (result <= 0)
            {
                return Conflict();
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id)
        {
            int result = await postService.DeletePost(id);

            if (result <= 0)
            {
                return Conflict();
            }

            return NoContent();
        }
    }
}
