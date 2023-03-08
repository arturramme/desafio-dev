using api.db;
using domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("transaction/financial/cnab", async (ICollection<FinancialTransaction> financialTransactions, AppDbContext db) =>
{
    await db.FinancialTransactions.AddRangeAsync(financialTransactions);
    await db.SaveChangesAsync();
})
.WithName("PostFinancialTransactions");

app.MapGet("transactions/financial", (AppDbContext db) =>
{
    return db.FinancialTransactions
        .GroupBy(x => x.StoreName)
        .Select((store) => new Store
        {
            Name = store.Key,
            FinancialTransactions = store.Where(x => x.StoreName == store.Key)
        }.ConsolidateAccountBalance())
        .ToListAsync();
})
.WithName("GetFinancialTransactions");

app.MapGet("ping", (AppDbContext db) =>
{
    return $"Pong {db.Database.CanConnect()}";
});

app.Run();