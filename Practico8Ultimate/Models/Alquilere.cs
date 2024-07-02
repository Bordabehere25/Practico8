using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practico8Ultimate.Models;

public partial class Alquilere
{
    public long Id { get; set; }

    public long IdCopia { get; set; }

    public long IdCliente { get; set; }

    public DateTime FechaAlquiler { get; set; }

    public DateTime FechaTope { get; set; }

    public DateTime? FechaEntregada { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Copia? IdCopiaNavigation { get; set; }

    [NotMapped]
    public string? PeliculaTitulo { get; private set; }

    public async Task SetPeliculaTituloAsync(Practico8Context context)
    {
        if (IdCopiaNavigation != null && IdCopiaNavigation.IdPelicula != 0)
        {
            var pelicula = await context.Peliculas.FindAsync(IdCopiaNavigation.IdPelicula);
            PeliculaTitulo = pelicula?.Titulo ?? "Sin título";
        }
        else
        {
            PeliculaTitulo = "Sin título";
        }
    }
}
