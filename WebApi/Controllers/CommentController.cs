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
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {

        private readonly CommentService _service;
        private readonly ILogger<CommentController> _logger;

        public CommentController(CommentService service, ILogger<CommentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromForm] Comment comment)
        {
            try
            {
                await _service.CreateComment(comment);
                return CreatedAtAction("CreateComment", comment);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, e);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentById(int id)
        {
            try
            {
                var result = await _service.GetCommentById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, e);
                throw new ArgumentException(e.Message);
                return BadRequest();
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var result = await _service.GetAllComments();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, e);
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                await _service.DeleteComment(id);
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
