using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests
{
    public class BookSearch
    {
        [Fact]
        public void Book_Search()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "OOP Essentials",
                    Author = "John Smith",
                    Isbn = "123",
                    Category = "Coding",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 2,
                    Title = "World History",
                    Author = "F Scott",
                    Isbn = "456",
                    Category = "History",
                    IsAvailable = true
                }
            };

            string search = "C#";

            var result = books
                .Where(b => b.Title.Contains(search) || b.Author.Contains(search))
                .ToList();

            Assert.Single(result);
            Assert.Equal("OOP Essentials", result[0].Title);
        }
    }
}
