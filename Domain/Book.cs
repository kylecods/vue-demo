using Domain.Base;
using System;

namespace Domain
{
    public class Book : Entity
    {
        public Guid? AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

    }
}