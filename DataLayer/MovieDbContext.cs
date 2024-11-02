using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class MovieDbContext : DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pelicula> Peliculas { get; set; }
    public DbSet<Funcion> Funciones { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración para la entidad Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // Configuración para la entidad Pelicula
        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(e => e.Genero)
                  .IsRequired()
                  .HasMaxLength(50);
            entity.Property(e => e.Duracion)
                  .IsRequired();

            // Relación uno-a-muchos entre Pelicula y Comentarios
            entity.HasMany(e => e.Comentarios)
                  .WithOne(e => e.Pelicula)
                  .HasForeignKey(e => e.PeliculaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración para la entidad Funcion
        modelBuilder.Entity<Funcion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FechaHora)
                  .IsRequired();
            entity.Property(e => e.Sala)
                  .IsRequired()
                  .HasMaxLength(50);

            // Relación muchos-a-uno entre Funcion y Pelicula
            entity.HasOne(e => e.Pelicula)
                  .WithMany()
                  .HasForeignKey(e => e.PeliculaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración para la entidad Comentario
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Contenido)
                  .IsRequired()
                  .HasMaxLength(500);
            entity.Property(e => e.Fecha)
                  .IsRequired();

            // Relación muchos-a-uno entre Comentario y Usuario
            entity.HasOne(e => e.Usuario)
                  .WithMany()
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relación muchos-a-uno entre Comentario y Pelicula
            entity.HasOne(e => e.Pelicula)
                  .WithMany(e => e.Comentarios)
                  .HasForeignKey(e => e.PeliculaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}