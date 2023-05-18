using System;
using Domain;

namespace Application.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public static class BookExtensions
    {
        public static Book ToBook(this BookDto bookDto)
        {
            return new Book
            {
                Id = Guid.NewGuid(),
                Title = bookDto.Title,
                Category = bookDto.Category,
                Description = bookDto.Description,
                CreatedDate = DateTime.Now
            };
        }
    }
}