using System;
using Domain;


namespace Application.Dtos
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Contact { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public static class AuthorExtensions
    {
        public static Author ToAuthor(this AuthorDto authorDto)
        {
            return new Author
            {
                Id = Guid.NewGuid(),
                Name = authorDto.Name,
                Contact = authorDto.Contact,
                CreatedDate = DateTime.Now
            };
        }
    }
}
