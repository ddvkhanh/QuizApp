using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
        policy.AllowAnyOrigin() 
              .AllowAnyMethod() 
              .AllowAnyHeader(); 
    });
});

// Configure database connection
builder.Services.AddDbContext<QuizAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))); 

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

app.UseAuthorization(); 

app.MapControllers(); 

app.Run(); 
