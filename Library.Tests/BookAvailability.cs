using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests
{
    public class BookAvailabilityTest
    {
        [Fact]
        public void Available_Books_Filter_Returns_Only_Available_Books()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "OOP Essentials",
                    Author = "John",
                    Isbn = "111",
                    Category = "Coding",
                    IsAvailable = true
                },
                new Book
                {
                    Id = 2,
                    Title = "World History",
                    Author = "F Scott",
                    Isbn = "222",
                    Category = "History",
                    IsAvailable = false
                }
            };

            var availableBooks = books.Where(b => b.IsAvailable).ToList();

            Assert.Single(availableBooks);
            Assert.True(availableBooks[0].IsAvailable);
        }
    }
}