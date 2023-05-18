using Application.Dtos;
using Domain;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookShelfService : IBookShelfService
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IBookRepository _bookRepository;

        public BookShelfService(IAuthorRepository authorRepository, IBookRepository bookRepository) 
        {
            _authorRepository = authorRepository;

            _bookRepository = bookRepository;
        }

        public Task AddAuthor(AuthorDto authorDto)
        {
            var author = authorDto.ToAuthor();

            return _authorRepository.Add(author);
        }

        public async Task AddBook(BookDto bookDto)
        {
            var author = await _authorRepository.FindAuthorByName(bookDto.AuthorName);

            var book = bookDto.ToBook();

            if (author == null)
            {

                //create as new Author
                var newAuthor = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = bookDto.AuthorName,
                    Contact = "+254718920920",
                    CreatedDate = DateTime.Now
                };

                await _authorRepository.Add(newAuthor);

                book.AuthorId = newAuthor.Id;

            }
            else
            {
                book.AuthorId = author.Id;
            }


            await _bookRepository.Add(book);
        }

        public Task DeleteAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Id = authorDto.Id,
                Name = authorDto.Name,
                Contact = authorDto.Contact,
                CreatedDate = authorDto.CreatedDate
            };

            return _authorRepository.Delete(author);
        }

        public async Task DeleteBook(Guid id)
        {
            var book = await _bookRepository.Get(id);

            await _bookRepository.Delete(book);
        }

        public async Task<AuthorDto> FindAuthorByName(string name)
        {
            var author = await _authorRepository.FindAuthorByName(name) ?? throw new System.Exception("Author cannot be found");

            var authorDto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Contact = author.Contact,
                CreatedDate = author.CreatedDate
            };

            return authorDto;
        }

        public async Task<List<AuthorDto>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAll();

            var authorDtos = authors.Select(x => new AuthorDto
            {
                Id = x.Id,
                Name = x.Name,
                Contact = x.Contact,
                CreatedDate = x.CreatedDate
            });

            return authorDtos.ToList();
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            var books = await  _bookRepository.GetAll();

            var bookDtos = books.Select(x => new BookDto
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.Category,
                Description = x.Description,
                CreatedDate = x.CreatedDate
            });

            return bookDtos.ToList();
        }

        public Task UpdateAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Id = authorDto.Id,
                Name = authorDto.Name,
                Contact = authorDto.Contact,
                CreatedDate = authorDto.CreatedDate
            };
            return _authorRepository.Update(author);
        }

        public async Task UpdateBook(BookDto bookDto)
        {
            var book = await _bookRepository.Get(bookDto.Id);

            if (book == null)
            {
                throw new Exception("Book does not exist");
            }

            book.Title = bookDto.Title;
            book.Description = bookDto.Description;
            book.Category = bookDto.Category;
            book.CreatedDate = bookDto.CreatedDate;
            

            await _bookRepository.Update(book);
        }
    }
}
