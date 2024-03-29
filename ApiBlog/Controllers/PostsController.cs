﻿using ApiBlog.Models;
using ApiBlog.Models.Dtos;
using ApiBlog.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlog.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IPostRepository _postRepo;
        private readonly IMapper _mapper;

        public PostsController(IPostRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPosts()
        {
            var listaPosts = _postRepo.GetPosts();

            var listaPostsDto = new List<PostDto>();

            foreach (var lista in listaPosts)
            {
                listaPostsDto.Add(_mapper.Map<PostDto>(lista));
            }

            return Ok(listaPostsDto);
        }

        [HttpGet("{postId:int}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPost(int postId)
        {
            var itemPost = _postRepo.GetPost(postId);

            if (itemPost == null)
            {
                return NotFound();
            }

            var itemPostDto = _mapper.Map<PostDto>(itemPost);

            return Ok(itemPostDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PostCrearDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearPost([FromBody] PostCrearDto crearPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crearPostDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_postRepo.PostExists(crearPostDto.Titulo))
            {
                ModelState.AddModelError("", "El post ya existe");
                return StatusCode(404, ModelState);
            }

            var post = _mapper.Map<Post>(crearPostDto);
            if (!_postRepo.CreatePost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {post.Titulo}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPost", new {postId = post.Id}, post);
        }

        [HttpPatch("{postId:int}", Name = "ActualizarPatchPost")]
        [ProducesResponseType(201, Type = typeof(PostCrearDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchPost(int postId, [FromBody] PostActualizarDto actualizarPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (actualizarPostDto == null || postId != actualizarPostDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (_postRepo.PostExists(actualizarPostDto.Titulo))
            {
                ModelState.AddModelError("", "El post ya existe");
                return StatusCode(404, ModelState);
            }

            var post = _mapper.Map<Post>(actualizarPostDto);

            if (!_postRepo.UpdatePost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {post.Titulo}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{postId:int}", Name = "BorrarPost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarPost(int postId)
        {
            if (_postRepo.PostExists(postId))
            {
                return NotFound();
            }

            var post = _postRepo.GetPost(postId);

            if (!_postRepo.DeletePost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {post.Titulo}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
