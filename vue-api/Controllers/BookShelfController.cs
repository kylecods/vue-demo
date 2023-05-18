using Application.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using vue_api.Models;

namespace vue_api.Controllers
{
    [ApiController]
    [Route("bookshelf")]
    public class BookShelfController : ControllerBase
    {
        private readonly IBookShelfService _bookShelfService;

        public BookShelfController(IBookShelfService bookShelfService)
        {
            _bookShelfService = bookShelfService;
        }


        [HttpPost("create-book")]
        public async Task<Results<Ok<Response<BookDto>>, BadRequest<Response<BookDto>>>> PostBook([Required] BookDto book)
        {
            try
            {
                await _bookShelfService.AddBook(book);

                return TypedResults.Ok(new Response<BookDto>("Created successfully", null));

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(new Response<BookDto>(ex.Message, null));
            }
        }

        [HttpPatch("update-book")]
        public async Task<Results<Ok<Response<BookDto>>, BadRequest<Response<BookDto>>>> UpdateBook([Required] BookDto book)
        {
            try
            {
                await _bookShelfService.UpdateBook(book);

                return TypedResults.Ok(new Response<BookDto>("Created successfully", null));

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(new Response<BookDto>(ex.Message, null));
            }
        }

        [HttpDelete("delete-book")]
        public async Task<Results<Ok<Response<BookDto>>, BadRequest<Response<BookDto>>>> DeleteBook([Required] Guid id)
        {
            try
            {
                await _bookShelfService.DeleteBook(id);

                return TypedResults.Ok(new Response<BookDto>("Created successfully", null));

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(new Response<BookDto>(ex.Message, null));
            }
        }

        [HttpPost("create-author")]
        public async Task<Results<Ok<Response<AuthorDto>>, BadRequest<Response<AuthorDto>>>> PostAuthor([Required] AuthorDto authorDto)
        {
            try
            {
                await _bookShelfService.AddAuthor(authorDto);

                return TypedResults.Ok(new Response<AuthorDto>("Created successfully", null));

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(new Response<AuthorDto>(ex.Message, null));
            }
        }

        [HttpGet("all-books")]
        public async Task<Results<Ok<Response<BookDto>>, BadRequest<Response<BookDto>>>> GetBooks()
        {
            try
            {
                var books = await _bookShelfService.GetAllBooks();

                return TypedResults.Ok(new Response<BookDto>("Sucess", books));

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(new Response<BookDto>(ex.Message, null));
            }
        }

        [HttpGet("all-authors")]
        public async Task<Results<Ok<Response<AuthorDto>>, BadRequest<Response<AuthorDto>>>> GetAuthors()
        {
            try
            {
                var authors = await _bookShelfService.GetAllAuthors();

                return TypedResults.Ok(new Response<AuthorDto>("Sucess", authors));

            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(new Response<AuthorDto>(ex.Message, null));
            }
        }
    }
}