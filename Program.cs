using Library_web_API.helpers;
using Library_web_API.persistence;
using Library_web_API.persistence.model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("LibraryDb"));
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()));
var app = builder.Build();
app.MapControllers();

app.MapGet("/Books", async (AppDbContext bookContext) =>
        await bookContext.Books.ToListAsync());

app.MapGet("/Books/{id}", async (AppDbContext bookContext, int id) =>
        await bookContext.Books.FindAsync(id)
        is Book book
        ? Results.Ok(book)
        : Results.NotFound());

app.MapPost("/Books", async (Book book, AppDbContext bookContext) =>
{
    //Checks if number that was given as category exists
    if (book.Category >= 0 && (int)book.Category <= 6)
    {
        bookContext.Books.Add(book);
        await bookContext.SaveChangesAsync();
        return Results.Created($"/Books/{book.Id}", book);
    } else
    {
        return Results.BadRequest<Book>(book);
    }
});


app.Run();
