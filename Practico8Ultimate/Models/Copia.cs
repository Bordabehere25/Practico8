using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practico8Ultimate.Models;

public partial class Copia
{
    public long Id { get; set; }

    public long IdPelicula { get; set; }

    public bool Deteriorada { get; set; }

    [RegularExpression("^(DVD|BluRay)$", ErrorMessage = "El tipo de medio debe ser 'DVD' o 'BluRay'.")]
    public string Formato { get; set; } = null!;

    public double PrecioAlquiler { get; set; }

    public virtual ICollection<Alquilere> Alquileres { get; set; } = new List<Alquilere>();

    public virtual Pelicula? IdPeliculaNavigation { get; set; }

    public static List<Copia> peliculasDisponibles(DbSet<Copia> copias)//Para filtar las copias disponibles.
    {
        var copiasDisponibles = copias.Where(x => x.Alquileres == null || x.Alquileres.All(x => x.FechaEntregada != null)).ToList();
        return copiasDisponibles;
    }

    public static async Task<List<Copia>> ObtenerCopiasDisponiblesLista(DbSet<Copia> copias)
    {
        return await copias.Include(c => c.IdPeliculaNavigation)
                        .Include(c => c.Alquileres)
            .Where(x => x.Alquileres == null || x.Alquileres.All(x => x.FechaEntregada != null)).ToListAsync();

    }
}

