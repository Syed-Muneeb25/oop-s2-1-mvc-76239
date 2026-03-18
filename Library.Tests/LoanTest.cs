using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain;
using Xunit;

namespace Library.Tests
{
    public class LoanTest
    {
        [Fact]
        public void BookAlreadyOnLoan()
        {
            var loans = new List<Loan>
            {
                new Loan
                {
                    Id = 1,
                    BookId = 1,
                    MemberId = 1,
                    LoanDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
                    DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(12)),
                    ReturnedDate = null
                }
            };

            int selectedBookId = 1;

            bool hasActiveLoan = loans.Any(l => l.BookId == selectedBookId && l.ReturnedDate == null);

            Assert.True(hasActiveLoan);
        }
    }
}