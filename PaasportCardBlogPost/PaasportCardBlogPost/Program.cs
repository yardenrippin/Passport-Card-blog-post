using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaasportCardBlogPost.Data;
using PaasportCardBlogPost.DataUtilities;
using PaasportCardBlogPost.Interfaces;
using PassportCardPost.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("defualt", (potion) =>
     {
         potion.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
     });
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<Ipost, PostUtils>();
builder.Services.AddScoped<Icomment, CommentUtils>();
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Key Auth", Version = "v1" });
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "ApiKey must appear in header",
        Type = SecuritySchemeType.ApiKey,
        Name = "ApiKey",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    var key = new OpenApiSecurityScheme()
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };
    var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
    c.AddSecurityRequirement(requirement);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleware>();

    
    app.UseSwagger();

    app.UseSwaggerUI();
}
  app.UseCors("defualt");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
