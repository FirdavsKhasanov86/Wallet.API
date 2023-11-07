using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wallet.API.Data;
using Wallet.API.Interface;
using Wallet.API.Repository;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WalletContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Wallet.API")));

// DI
builder.Services.AddScoped<IAuthorization, authorizationRepository>();
builder.Services.AddScoped<IComputerHMAC, computerHMACRepository>();
builder.Services.AddScoped<IGetAllInfoAboutUsers, getAllInfoAboutUsersRepository>();
builder.Services.AddTransient<IEditUserAccount, editUsersAccountRepository>();
builder.Services.AddTransient<IGetUserAuthorizedAccount, getUserAuthorizedAccountRepository>();
builder.Services.AddScoped<IAuthenticationUser, authenticationUserRepository>();
builder.Services.AddScoped<IDebitBankAccount, debitBankAccountRepository>();
builder.Services.AddScoped<IHistoryCheckBalanceAccounts, historyCheckBalanceAccountsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
