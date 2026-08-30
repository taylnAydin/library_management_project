using LibraryManagement.API;
using LibraryManagement.API.Middleware;
using LibraryManagement.Business;
using LibraryManagement.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);

   
builder.Services.AddBusinessServices();

builder.Services.AddDataAccessService(builder.Configuration);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
  options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters // bak  authencition auhtrozioton farkli seyler
      {
          ValidateIssuer = true,
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidateAudience = true,
          ValidAudience = builder.Configuration["Jwt:Audience"],
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not found"))) ,
      };
  }
 );


builder.Services.AddAuthorization();
// Add services to the container.


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // generic class niye parantez var consturcot mic agirio
builder.Services.AddApiServices();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


//alinan referanslar bir  + asp.net web api olstrdk bak + niye add controller