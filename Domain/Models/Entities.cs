namespace Domain;

public class Entities
{

}

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
}

public class Pelicula
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracion { get; set; } // En minutos
    public ICollection<Comentario> Comentarios { get; set; }
}

public class Funcion
{
    public int Id { get; set; }
    public int PeliculaId { get; set; }
    public Pelicula Pelicula { get; set; }
    public DateTime FechaHora { get; set; }
    public string Sala { get; set; }
}

public class Comentario
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int PeliculaId { get; set; }
    public Pelicula Pelicula { get; set; }
    public string Contenido { get; set; }
    public DateTime Fecha { get; set; }
}
