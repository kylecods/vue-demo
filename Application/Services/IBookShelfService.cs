using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IBookShelfService
    {
        Task AddBook(BookDto bookDto);

        Task UpdateBook(BookDto bookDto);

        Task DeleteBook(Guid id);

        Task<List<BookDto>> GetAllBooks();

        Task AddAuthor(AuthorDto authorDto);

        Task UpdateAuthor(AuthorDto authorDto);

        Task DeleteAuthor(AuthorDto authorDto);

        Task<List<AuthorDto>> GetAllAuthors();

        Task<AuthorDto> FindAuthorByName(string name);
    }
}
