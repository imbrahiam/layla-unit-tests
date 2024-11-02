using BusinessLayer.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PeliculasController : ControllerBase
{
    private readonly IPeliculaService _peliculaService;

    public PeliculasController(IPeliculaService peliculaService)
    {
        _peliculaService = peliculaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pelicula>>> ObtenerPeliculas()
    {
        return Ok(await _peliculaService.ObtenerTodasAsync());
    }

    [HttpPost]
    public async Task<ActionResult> CrearPelicula([FromBody] Pelicula pelicula)
    {
        await _peliculaService.CrearAsync(pelicula);
        return CreatedAtAction(nameof(ObtenerPeliculas), new { id = pelicula.Id }, pelicula);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> EliminarPelicula(int id)
    {
        var resultado = await _peliculaService.EliminarAsync(id);
        if (!resultado)
        {
            return NotFound("Pelicula no encontrada.");
        }
        return NoContent();
    }
}
