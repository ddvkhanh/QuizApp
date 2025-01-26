using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Database;
using QuizApp.Server.Converters;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5001");


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new PolymorphicJsonConverter());
    });

// Add services to the container.
builder.Services.AddControllers(); 

// Enable CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    { 
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyMethod() 
              .AllowAnyHeader(); 
    });
});

// Configure database connection
builder.Services.AddDbContext<QuizAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//Configure JWT authentication
var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); 
    app.UseHsts();
    app.UseHttpsRedirection();

}

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
    Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");
    Console.WriteLine($"Request Body: {body}");
    context.Request.Body.Position = 0; // Reset the stream for further processing
    await next();
});


app.UseCors(); 

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();