using BusinessLayer.Services;
using DataLayer.Repositories;
using Domain;
using Moq;

namespace Tests;
public class MovieServiceTests
{
    private readonly Mock<IPeliculaRepository> _mockMovieRepository;
    private readonly PeliculaService _movieService;

    public MovieServiceTests()
    {
        _mockMovieRepository = new Mock<IPeliculaRepository>();
        _movieService = new PeliculaService(_mockMovieRepository.Object);
    }

    [Fact]
    public async Task GetAllMoviesAsync_ReturnsAllMovies()
    {
        // Arrange
        var movies = new List<Pelicula> { new Pelicula { Titulo = "Movie 1" }, new Pelicula { Titulo = "Movie 2" } };
        _mockMovieRepository.Setup(repo => repo.ObtenerTodasAsync()).ReturnsAsync(movies);

        // Act
        var result = await _movieService.ObtenerTodasAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetMovieByIdAsync_ReturnsCorrectMovie()
    {
        // Arrange
        var movie = new Pelicula { Id = 1, Titulo = "Movie 3" };
        _mockMovieRepository.Setup(repo => repo.ObtenerPorIdAsync(1)).ReturnsAsync(movie);

        // Act
        var result = await _movieService.ObtenerPorIdAsync(1);

        // Assert
        Assert.Equal("Movie 3", result.Titulo);
    }

    [Fact]
    public async Task AddMovieAsync_CallsAddMovieOnce()
    {
        // Arrange
        var movie = new Pelicula { Titulo = "New Movie" };

        // Act
        await _movieService.CrearAsync(movie);

        // Assert
        _mockMovieRepository.Verify(repo => repo.CrearAsync(movie), Times.Once);
    }

    [Fact]
    public async Task UpdateMovieAsync_CallsUpdateMovieOnce()
    {
        // Arrange
        var movie = new Pelicula { Id = 1, Titulo = "Updated Movie" };

        // Act
        await _movieService.ActualizarAsync(movie);

        // Assert
        _mockMovieRepository.Verify(repo => repo.ActualizarAsync(movie), Times.Once);
    }

    [Fact]
    public async Task DeleteMovieAsync_CallsDeleteMovieOnce()
    {
        // Arrange
        var movieId = 1;

        // Act
        await _movieService.EliminarAsync(movieId);

        // Assert
        _mockMovieRepository.Verify(repo => repo.EliminarAsync(movieId), Times.Once);
    }
}