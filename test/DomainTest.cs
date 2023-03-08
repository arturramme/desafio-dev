using domain;

namespace test
{
    public class DomainTest
    {
        [Fact]
        public void Add_Invalid_Date()
        {
            var transaction = new FinancialTransaction();

            var invalidDate = "123456789".AsSpan();

            try
            {
                transaction.FormatDateTime(invalidDate, "");
            }
            catch (Exception e)
            {
                Assert.True(e is ArgumentException);
            }
        }

        [Fact]
        public void Add_Invalid_Time()
        {
            var transaction = new FinancialTransaction();

            var invalidTime = "123456789".AsSpan();

            try
            {
                transaction.FormatDateTime("", invalidTime);
            }
            catch (Exception e)
            {
                Assert.True(e is ArgumentException);
            }
        }

        [Fact]
        public void Add_Valid_DateTime()
        {
            var transaction = new FinancialTransaction();

            var validDate = "20190301".AsSpan();
            var validTime = "153453".AsSpan();

            try
            {
                transaction.FormatDateTime(validDate, validTime);
            }
            catch (Exception e)
            {
                Assert.True(e is ArgumentException);
            }
        }

        [Fact]
        public void Populate_Valid_Cnab()
        {
            var transaction = new FinancialTransaction();

            try
            {
                transaction.PopulateFromCnab(
                "3",
                "20190301",
                "0000014200",
                "09620676017".AsMemory(),
                "4753****3153".AsMemory(),
                "153453",
                "JOÃO MACEDO".AsMemory(),
                "BAR DO JOÃO".AsMemory());
            }
            catch (Exception)
            {
                Assert.False(true);
            }

            Assert.Equal(3, transaction.Type);
            Assert.Equal(new DateTime(2019, 03, 01, 15, 34, 53).Ticks, transaction.Date.Ticks);
            Assert.Equal(142, transaction.Value);
            Assert.Equal("09620676017", transaction.CPF);
            Assert.Equal("4753****3153", transaction.Card);
            Assert.Equal("JOÃO MACEDO", transaction.StoreOwner);
            Assert.Equal("BAR DO JOÃO", transaction.StoreName);
        }

        [Theory]
        [InlineData("", "20190301", "0000014200", "153453")]
        [InlineData("3", "", "0000014200", "153453")]
        [InlineData("3", "20190301", "", "153453")]
        [InlineData("3", "20190301", "0000014200", "")]
        public void Populate_Invalid_Cnab(string type, string date, string value, string time)
        {
            var transaction = new FinancialTransaction();

            try
            {
                transaction.PopulateFromCnab(
                type,
                date,
                value,
                "".AsMemory(),
                "".AsMemory(),
                time,
                "".AsMemory(),
                "".AsMemory());
            }
            catch (Exception e)
            {
                Assert.True(e is ArgumentException);
                return;
            }

            Assert.False(true);
        }

        [Fact]
        public void Consolidate_Invalid_AccountBalance()
        {
            var store = new Store();

            store.ConsolidateAccountBalance();

            store.FinancialTransactions = new List<FinancialTransaction>
            {
                new FinancialTransaction { Type = 10, Value = 100 },
                new FinancialTransaction { Type = 11, Value = 100 },
                new FinancialTransaction { Type = 12, Value = 100 },
                new FinancialTransaction { Type = 13, Value = 100 }
            };

            store.ConsolidateAccountBalance();

            Assert.True(store.AccountBalance == 0);
        }

        [Fact]
        public void Consolidate_Valid_AccountBalance()
        {
            var store = new Store
            {
                FinancialTransactions = new List<FinancialTransaction>
                {
                   new FinancialTransaction { Type = 1, Value = 100 },
                   new FinancialTransaction { Type = 2, Value = 100 },
                   new FinancialTransaction { Type = 3, Value = 100 },
                   new FinancialTransaction { Type = 4, Value = 100 },
                   new FinancialTransaction { Type = 5, Value = 100 },
                   new FinancialTransaction { Type = 6, Value = 100 },
                   new FinancialTransaction { Type = 7, Value = 100 },
                   new FinancialTransaction { Type = 8, Value = 100 },
                   new FinancialTransaction { Type = 9, Value = 100 },
                }
            };

            store.ConsolidateAccountBalance();

            Assert.True(store.AccountBalance == 300);
        }
    }
}