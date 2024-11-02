using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories;

public interface IPeliculaRepository
{
    Task<IEnumerable<Pelicula>> ObtenerTodasAsync();
    Task<Pelicula> ObtenerPorIdAsync(int id);
    Task<Pelicula> CrearAsync(Pelicula pelicula);
    Task<bool> ActualizarAsync(Pelicula pelicula);
    Task<bool> EliminarAsync(int id);
}

public class PeliculaRepository : IPeliculaRepository
{
    private readonly MovieDbContext _context;

    public PeliculaRepository(MovieDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pelicula>> ObtenerTodasAsync()
    {
        return await _context.Peliculas.ToListAsync();
    }

    public async Task<Pelicula> ObtenerPorIdAsync(int id)
    {
        return await _context.Peliculas.FindAsync(id);
    }

    public async Task<Pelicula> CrearAsync(Pelicula pelicula)
    {
        _context.Peliculas.Add(pelicula);
        await _context.SaveChangesAsync();
        return pelicula;
    }

    public async Task<bool> ActualizarAsync(Pelicula pelicula)
    {
        _context.Peliculas.Update(pelicula);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var pelicula = await ObtenerPorIdAsync(id);
        if (pelicula == null) return false;

        _context.Peliculas.Remove(pelicula);
        return await _context.SaveChangesAsync() > 0;
    }
}
