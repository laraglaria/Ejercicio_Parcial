using AccesoDatos.Data;
using AccesoDatos.Models;
using AccesoDatos.Repositories;

using (var context = new MusicaDbContext())
{
    int opcion = -1;

    while (opcion != 0)
    {
        Console.Clear();

        Console.WriteLine("PLATAFORMA DE MÚSICA");
        Console.WriteLine("1. Alta artista");
        Console.WriteLine("2. Alta canción");
        Console.WriteLine("3. Ver canciones");
        Console.WriteLine("4. Mostrar canciones más largas");
        Console.WriteLine("5. Cantidad total de canciones");
        Console.WriteLine("6. Mostrar canciones ordenadas alfabéticamente por título");
        Console.WriteLine("7. Verificar si existen canciones registradas");
        Console.WriteLine("0. Salir");
        Console.Write("Seleccione una opción: ");

        opcion = int.Parse(Console.ReadLine()!);

        Console.Clear();

        switch (opcion)
        {
            case 1:
                Console.WriteLine("ALTA ARTISTA");

                Console.Write("Ingrese el nombre del artista: ");
                string nombre = Console.ReadLine()!;

                Artista artista = new Artista();
                artista.Nombre = nombre;

                GenericRepository<Artista> artistaRepository = new GenericRepository<Artista>(context);
                artistaRepository.Agregar(artista);

                Console.WriteLine("Artista registrado correctamente.");
                break;

            case 2:
                Console.WriteLine("ALTA CANCIÓN");

                GenericRepository<Artista> artistaRepo = new GenericRepository<Artista>(context);

                List<Artista> artistas = artistaRepo.ObtenerTodos();

                if (artistas.Count == 0)
                {
                    Console.WriteLine("No hay artistas registrados.");
                    break;
                }

                Console.Write("Ingrese el título de la canción: ");
                string titulo = Console.ReadLine()!;

                Console.Write("Ingrese la duración en segundos: ");
                int duracion = int.Parse(Console.ReadLine()!);

                Console.WriteLine();
                Console.WriteLine("Artistas disponibles:");

                for (int i = 0; i < artistas.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + artistas[i].Nombre);
                }

                Console.Write("Seleccione el artista: ");
                int opcionArtista = int.Parse(Console.ReadLine()!);

                Artista artistaSeleccionado = artistas[opcionArtista - 1];

                Cancion cancion = new Cancion();
                cancion.Titulo = titulo;
                cancion.Duracion = duracion;
                cancion.ArtistaId = artistaSeleccionado.Id;

                CancionRepository cancionRepository = new CancionRepository(context);
                cancionRepository.Agregar(cancion);

                Console.WriteLine("Canción registrada correctamente.");
                break;

            case 3:
                Console.WriteLine("CANCIONES REGISTRADAS");

                CancionRepository cancionRepoVer = new CancionRepository(context);
                List<Cancion> listaCanciones = cancionRepoVer.ObtenerTodos();

                if (listaCanciones.Count == 0)
                {
                    Console.WriteLine("No hay canciones registradas.");
                    break;
                }

                GenericRepository<Artista> artistaRepoVer = new GenericRepository<Artista>(context);

                foreach (Cancion cancionActual in listaCanciones)
                {
                    Artista artistaActual = artistaRepoVer.ObtenerPorId(cancionActual.ArtistaId);

                    Console.WriteLine();
                    Console.WriteLine("Título: " + cancionActual.Titulo);
                    Console.WriteLine("Duración: " + cancionActual.Duracion + " segundos");
                    Console.WriteLine("Artista: " + artistaActual.Nombre);
                }

                break;

            case 4:
                Console.WriteLine("CANCIONES MÁS LARGAS");

                CancionRepository cancionRepoLargas = new CancionRepository(context);
                List<Cancion> cancionesLargas = cancionRepoLargas.ObtenerCancionesMasLargas();

                if (cancionesLargas.Count == 0)
                {
                    Console.WriteLine("No hay canciones registradas.");
                    break;
                }

                foreach (Cancion cancionLarga in cancionesLargas)
                {
                    Console.WriteLine();
                    Console.WriteLine("Título: " + cancionLarga.Titulo);
                    Console.WriteLine("Duración: " + cancionLarga.Duracion + " segundos");
                }

                break;

            case 5:
                Console.WriteLine("CANTIDAD TOTAL DE CANCIONES");

                CancionRepository cancionRepoCantidad = new CancionRepository(context);
                int cantidadTotal = cancionRepoCantidad.ObtenerCantidadCanciones();

                Console.WriteLine("Cantidad total de canciones: " + cantidadTotal);

                break;

            case 6:
                Console.WriteLine("CANCIONES ORDENADAS ALFABÉTICAMENTE");

                CancionRepository cancionRepoOrdenadas = new CancionRepository(context);
                List<Cancion> cancionesOrdenadas = cancionRepoOrdenadas.ObtenerCancionesOrdenadas();

                if (cancionesOrdenadas.Count == 0)
                {
                    Console.WriteLine("No hay canciones registradas.");
                    break;
                }

                foreach (Cancion cancionOrdenada in cancionesOrdenadas)
                {
                    Console.WriteLine(cancionOrdenada.Titulo);
                }

                break;

            case 7:
                Console.WriteLine("VERIFICAR CANCIONES");

                CancionRepository cancionRepoExiste = new CancionRepository(context);

                bool existenCanciones = cancionRepoExiste.ExistenCanciones();

                if (existenCanciones)
                {
                    Console.WriteLine("Existen canciones registradas.");
                }
                else
                {
                    Console.WriteLine("No existen canciones registradas.");
                }

                break;

            case 0:
                Console.WriteLine("Saliendo...");
                break;

            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        if (opcion != 0)
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}