using DataLayer.Repositories;
using Domain;

namespace BusinessLayer.Services;

public interface IPeliculaService
{
    Task<IEnumerable<Pelicula>> ObtenerTodasAsync();
    Task<Pelicula> ObtenerPorIdAsync(int id);
    Task<Pelicula> CrearAsync(Pelicula pelicula);
    Task<bool> ActualizarAsync(Pelicula pelicula);
    Task<bool> EliminarAsync(int id);
}

public class PeliculaService : IPeliculaService
{
    private readonly IPeliculaRepository _peliculaRepository;

    public PeliculaService(IPeliculaRepository peliculaRepository)
    {
        _peliculaRepository = peliculaRepository;
    }

    public async Task<IEnumerable<Pelicula>> ObtenerTodasAsync()
    {
        return await _peliculaRepository.ObtenerTodasAsync();
    }

    public async Task<Pelicula> ObtenerPorIdAsync(int id)
    {
        return await _peliculaRepository.ObtenerPorIdAsync(id);
    }

    public async Task<Pelicula> CrearAsync(Pelicula pelicula)
    {
        return await _peliculaRepository.CrearAsync(pelicula);
    }

    public async Task<bool> ActualizarAsync(Pelicula pelicula)
    {
        return await _peliculaRepository.ActualizarAsync(pelicula);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        return await _peliculaRepository.EliminarAsync(id);
    }
}

