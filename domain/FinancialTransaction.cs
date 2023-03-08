using System.Globalization;

namespace domain
{
    public class FinancialTransaction
    {
        private readonly string formatDate = "yyyyMMdd";
        private readonly string formatTime = "HHmmss";

        public int Id { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public long Value { get; set; }
        public string? CPF { get; set; }
        public string? Card { get; set; }
        public string? StoreOwner { get; set; }
        public string? StoreName { get; set; }

        public void PopulateFromCnab(
            ReadOnlySpan<char> type,
            ReadOnlySpan<char> date,
            ReadOnlySpan<char> value,
            ReadOnlyMemory<char> cpf,
            ReadOnlyMemory<char> card,
            ReadOnlySpan<char> time,
            ReadOnlyMemory<char> storeOwner,
            ReadOnlyMemory<char> storeName)
        {
            if (string.IsNullOrEmpty(type.ToString()))
            {
                throw new ArgumentException("Tipo da ocorrência inválida!");
            }

            Type = int.Parse(type);
            Date = FormatDateTime(date, time);

            if (string.IsNullOrEmpty(value.ToString()))
            {
                throw new ArgumentException("Valor inválido!");
            }

            Value = long.Parse(value) / 100;
            CPF = cpf.ToString();
            Card = card.ToString();
            StoreOwner = storeOwner.ToString().TrimEnd();
            StoreName = storeName.ToString().TrimEnd();
        }

        public DateTime FormatDateTime(ReadOnlySpan<char> date, ReadOnlySpan<char> time)
        {
            DateOnly dateFormated;
            TimeOnly timeFormated;

            if (!DateOnly.TryParseExact(date, formatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFormated))
            {
                throw new ArgumentException("Data da ocorrência inválida!");
            }

            if (!TimeOnly.TryParseExact(time, formatTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out timeFormated))
            {
                throw new ArgumentException("Hora da ocorrência inválida!");
            }

            return dateFormated.ToDateTime(timeFormated);
        }
    }
}