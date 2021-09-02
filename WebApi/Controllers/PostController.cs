using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Domain.Models;
using WebApi.DTOs;
using WebApi.Services;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {

        private readonly PostService _service;
        private readonly ILogger<PostController> _logger;

        public PostController(PostService service, ILogger<PostController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostDTO post)
        {
            try
            {
                await _service.CreatePost(post);
                return CreatedAtAction("CreatePost", post);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, e);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var result = await _service.GetPostById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, e);
                return BadRequest();
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var result = await _service.GetAllPosts();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, e);
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _service.DeletePost(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, e);
                return BadRequest();
            }
        }
    }
}
