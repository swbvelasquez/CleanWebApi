using AutoMapper;
using CleanWebApi.Core.DTOs;
using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Interfaces;
using CleanWebApi.Core.Services;
using CleanWebApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
        {
            IEnumerable<Post> posts = await postService.GetPosts();
            IEnumerable<PostDTO> postsDTO = mapper.Map<IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            Post post = await postService.GetPost(id);
            PostDTO postDTO = mapper.Map<PostDTO>(post);
            return Ok(postDTO);
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

            return CreatedAtAction(nameof(GetPost),new { id = post.PostId},postDTO);
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
