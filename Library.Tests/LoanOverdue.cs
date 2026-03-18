using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tests
{
    public class LoanOverdue
    {
        [Fact]
        public void OverdueLogic_ReturnedDate_Null()
        {
            var loan = new Loan
            {
                Id = 1,
                BookId = 1,
                MemberId = 1,
                LoanDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-20)),
                DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-2)),
                ReturnedDate = null
            };

            bool isOverdue = loan.DueDate < DateOnly.FromDateTime(DateTime.Today)
                             && loan.ReturnedDate == null;

            Assert.True(isOverdue);
        }
    }
}
