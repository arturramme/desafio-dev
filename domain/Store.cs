namespace domain
{
    public class Store
    {
        public Store() => FinancialTransactions = new List<FinancialTransaction>();

        public string Name { get; set; }
        public IEnumerable<FinancialTransaction>? FinancialTransactions { get; set; }
        public long AccountBalance { get; set; }

        public Store ConsolidateAccountBalance()
        {
            foreach (var transaction in FinancialTransactions)
            {
                var transactionType = (TransactionTypes)transaction.Type;

                if (transactionType == TransactionTypes.Debito
                     || transactionType == TransactionTypes.Credito
                     || transactionType == TransactionTypes.Emprestimo
                     || transactionType == TransactionTypes.Vendas
                     || transactionType == TransactionTypes.TED
                     || transactionType == TransactionTypes.DOC)
                    AccountBalance += transaction.Value;

                if (transactionType == TransactionTypes.Boleto
                    || transactionType == TransactionTypes.Financiamento
                    || transactionType == TransactionTypes.Aluguel)
                    AccountBalance -= transaction.Value;
            }

            return this;
        }
    }
}
