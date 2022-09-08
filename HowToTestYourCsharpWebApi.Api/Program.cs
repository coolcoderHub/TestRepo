using HowToTestYourCsharpWebApi.Api;
using HowToTestYourCsharpWebApi.Api.Ports;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IPOSoperationConfigService, POSoperationConfigService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    { Title = builder.Environment.ApplicationName, Version = "v1" });
});

await using var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1"));
}

app.UseHttpsRedirection();
app.UseRouting();


List<TestAccount> accounts = new List<TestAccount>{  
new TestAccount(4755, 1001.88m, true),
new TestAccount(9834, 456.45m, true),
new TestAccount(7735, 89.36m, true)};

List<POSoperation> posOps = new List<POSoperation>();  // Not OK values should come from source, VISA etc should be generic
POSoperation posOp = new POSoperation();
posOp.MessageType = "PAYMENT";
posOp.TransactionId = "12345";
posOp.Amount = 1.3m;
posOp.Origin = "VISA";
posOp.AccountId = 4755;
posOps.Add(posOp);

List <Transaction> transactions = new List<Transaction>(); // To save payment and adjustment log about balances


app.MapGet("/POSoperation", (IPOSoperationConfigService _posOperationConfigService) =>
{
    bool accountOK = _posOperationConfigService.AccountCheck(accounts[0].AccountId); // Acoount exists and not blocked

    //GET is for query existing balances

    // var posOp = list of pos res
    //List<POSoperation> listOp = new List<POSoperation>();
    //{
    //    new POSoperation("PAYMENT",4444444444,555,"VISA",10.00m),
    //    new POSoperation("PAYMENT",4444644444,333,"VISA",12.00m),
    //    new POSoperation("PAYMENT",4444644444,333,"VISA",12.00m)
    //};

    return Results.Ok(posOps);

});

app.MapPost("/POSoperation", (IPOSoperationConfigService _posOperationConfigService) =>
{
    bool accountOK = _posOperationConfigService.AccountCheck(accounts[0].AccountId); // Acoount exists and not blocked
    if(!accountOK) return Results.BadRequest();

    bool balanceOK = _posOperationConfigService.BalanceCheck();  // 
    if (!balanceOK) return Results.BadRequest(posOps);

    decimal paymentAmount = 22.45m; // if visa %1 if master %2
    bool commAdded = _posOperationConfigService.ApplyCommRate(accounts[0],paymentAmount,"VISA");

    // add a line to transaction list about current process transation line will include surrent balance and input msg
    bool tranAdded = _posOperationConfigService.AddTransaction();

    return Results.Ok(posOps);
});

app.MapPut("/POSoperation", (IPOSoperationConfigService _posOperationConfigService) =>
{
    //bool accountOK = _posOperationConfigService.AccountCheck(); // Acoount exists and not blocked

    // var balance = _posOperationConfigService.BalanceCheck();  // 
    // if (balance <= 0) return Results.BadRequest();

    //List<POSoperation> listOp = new List<POSoperation>();
    return Results.Ok(posOps);
});

await app.RunAsync();

public partial class Program { }
