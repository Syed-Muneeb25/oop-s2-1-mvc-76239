using System;
using Library.Domain;
using Xunit;

namespace Library.Tests
{
    public class LoanReturn
    {
        [Fact]
        public void ReturnedLoan()
        {
            var book = new Book
            {
                Id = 1,
                Title = "OOP Essentials",
                Author = "John Smith",
                Isbn = "111",
                Category = "Coding",
                IsAvailable = false
            };

            var loan = new Loan
            {
                Id = 1,
                BookId = 1,
                MemberId = 1,
                LoanDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-5)),
                DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(9)),
                ReturnedDate = null
            };

            loan.ReturnedDate = DateOnly.FromDateTime(DateTime.Today);
            book.IsAvailable = true;

            Assert.NotNull(loan.ReturnedDate);
            Assert.True(book.IsAvailable);
        }
    }
}