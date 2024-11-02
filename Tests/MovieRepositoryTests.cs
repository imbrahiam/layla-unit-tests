using DataLayer;
using DataLayer.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Tests;

public class MovieRepositoryTests
{
    private readonly MovieDbContext _context;
    private readonly PeliculaRepository _movieRepository;

    public MovieRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MovieDbContext>()
            .UseInMemoryDatabase(databaseName: "MovieTestDb")
            .Options;

        _context = new MovieDbContext(options);
        _movieRepository = new PeliculaRepository(_context);
    }

    [Fact]
    public async Task GetAllMoviesAsync_ReturnsAllMovies()
    {
        // Arrange
        var movies = new List<Pelicula>
        {
            new Pelicula { Titulo = "Movie 1", Genero = "Action", Duracion = 120 },
            new Pelicula { Titulo = "Movie 2", Genero = "Comedy", Duracion = 90 }
        };
        await _context.Peliculas.AddRangeAsync(movies);
        await _context.SaveChangesAsync();

        // Act
        var result = await _movieRepository.ObtenerTodasAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetMovieByIdAsync_ReturnsCorrectMovie()
    {
        // Arrange
        var movie = new Pelicula { Titulo = "Movie 3", Genero = "Drama", Duracion = 110 };
        await _context.Peliculas.AddAsync(movie);
        await _context.SaveChangesAsync();

        // Act
        var result = await _movieRepository.ObtenerPorIdAsync(movie.Id);

        // Assert
        Assert.Equal(movie.Titulo, result.Titulo);
    }

    [Fact]
    public async Task AddMovieAsync_AddsMovieToDatabase()
    {
        // Arrange
        var movie = new Pelicula { Titulo = "Movie 4", Genero = "Horror", Duracion = 130 };

        // Act
        await _movieRepository.CrearAsync(movie);
        var result = await _context.Peliculas.FindAsync(movie.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(movie.Titulo, result.Titulo);
    }

    [Fact]
    public async Task UpdateMovieAsync_UpdatesMovieInDatabase()
    {
        // Arrange
        var movie = new Pelicula { Titulo = "Movie 5", Genero = "Sci-Fi", Duracion = 100 };
        await _context.Peliculas.AddAsync(movie);
        await _context.SaveChangesAsync();

        // Act
        movie.Titulo = "Updated Movie";
        await _movieRepository.ActualizarAsync(movie);
        var result = await _context.Peliculas.FindAsync(movie.Id);

        // Assert
        Assert.Equal("Updated Movie", result.Titulo);
    }

    [Fact]
    public async Task DeleteMovieAsync_RemovesMovieFromDatabase()
    {
        // Arrange
        var movie = new Pelicula { Titulo = "Movie 6", Genero = "Fantasy", Duracion = 140 };
        await _context.Peliculas.AddAsync(movie);
        await _context.SaveChangesAsync();

        // Act
        await _movieRepository.EliminarAsync(movie.Id);
        var result = await _context.Peliculas.FindAsync(movie.Id);

        // Assert
        Assert.Null(result);
    }
}