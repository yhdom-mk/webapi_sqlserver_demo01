using Microsoft.Extensions.FileProviders;
using TuneWebApp01.Models;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using System.Drawing;
using Microsoft.AspNetCore.Http;
//using System.Text.Encodings.Web;
//using System.Text.Json;
//using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });
//builder.Services.AddControllers()
//.AddNewtonsoftJson(jsonOptions =>
//{
//    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
//});
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = null;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//service cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app cors
app.UseCors("corsapp");

app.UseHttpsRedirection();
//app.UseCors(prodCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Photos")
    ),
    RequestPath = "/Photos"
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/SaveFile", async context =>
    {
        Bitmap bmp = new Bitmap("image.bmp");
        context.Response.ContentType = "text/plain; charset=utf-8";
        await context.Response.WriteAsync( "bitmap‚Ì•"+ bmp.Width, System.Text.Encoding.UTF8);
    });
});

app.Run();
