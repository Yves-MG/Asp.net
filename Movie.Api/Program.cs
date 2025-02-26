using Movie.Api.Dtos;
using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
List<MovieDto> movies = [
    new(1,"malokila","yves",new DateOnly(2014,02,14)),
    new(2,"malokila","Manoela",new DateOnly(2014,02,14)),
    new(3,"malokila","Mirindra",new DateOnly(2014,02,14)),
    new(4,"malokila","Mirindra",new DateOnly(2014,02,14))

    ];

app.MapGet("/Movie/list", () => movies).WithName("ListMovies");
app.MapGet("/Movie/{id}", (int id ) => movies.Find(x=>x.Id==id))
   .WithName("List");
app.MapDelete("Movie/delete/{id}", (int id) =>
{
    movies.RemoveAll(x => x.Id == id);

    return Results.NoContent();

});

app.MapPut("Movie/{id}", (int id,UpdateMovieDto updateMovieDto) =>
{
    var index = movies.FindIndex(x => x.Id == id);
    movies[index] = new MovieDto(id, updateMovieDto.Title, updateMovieDto.Director, updateMovieDto.ReleaseDate);

    return Results.NoContent();
});

app.MapPost("/Movie/", (CreateMovieDto createMovieDto) =>
{
    MovieDto movieDto = new
    (
        Id: movies.Count() + 1,
        Title: createMovieDto.Title,
        Director: createMovieDto.Director,
        ReleaseDate: new DateOnly(2024, 12, 14)
    );

    movies.Add(movieDto);

    return Results.CreatedAtRoute("ListMovies",new {},movies);
    //return Results.CreatedAtRoute("ListMovies", new { id  = movieDto.Id},movieDto);
});

app.MapGet("/", () => "Hello World!");

app.Run();
