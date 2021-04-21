using AutoMapper;
using CleanWebApi.Core.DTOs;
using CleanWebApi.Core.Entities;
using CleanWebApi.Core.Interfaces;
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
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
        {
            IEnumerable<Post> posts = await postRepository.GetPosts();
            IEnumerable<PostDTO> postsDTO = mapper.Map<IEnumerable<PostDTO>>(posts);
            return Ok(postsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            Post post = await postRepository.GetPost(id);
            PostDTO postDTO = mapper.Map<PostDTO>(post);
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> InsertPost(PostDTO postDTO)
        {
            Post post = mapper.Map<Post>(postDTO);
            int result = await postRepository.InsertPost(post);
            return CreatedAtAction(nameof(GetPost),new { id = post.PostId},post);
        }
    }
}
