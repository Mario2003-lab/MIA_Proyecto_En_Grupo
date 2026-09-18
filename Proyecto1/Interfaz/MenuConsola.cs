using System;
using System.Collections.Generic;
using Proyecto1.Modelos;
using Proyecto1.Servicios;

namespace Proyecto1.Interfaz
{
    public class MenuConsola
    {
        private readonly GestorXML gestor;

        public MenuConsola(string rutaXml)
        {
            gestor = new GestorXML(rutaXml);
        }

        public void Mostrar()
        {
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("       SISTEMA DE GESTION DE CURSOS");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("1. Registrar curso");
                Console.WriteLine("2. Consultar curso");
                Console.WriteLine("3. Buscar por identificador");
                Console.WriteLine("4. Modificar curso");
                Console.WriteLine("5. Eliminar curso");
                Console.WriteLine("6. Listar cursos");
                Console.WriteLine("7. Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opcion: ");

                string entrada = Console.ReadLine() ?? "";

                if (!int.TryParse(entrada, out opcion))
                {
                    MostrarMensaje("Debe ingresar un numero del 1 al 7.");
                    continue;
                }

                Console.Clear();

                switch (opcion)
                {
                    case 1:
                        Registrar();
                        break;

                    case 2:
                        Consultar();
                        break;

                    case 3:
                        Buscar();
                        break;

                    case 4:
                        Modificar();
                        break;

                    case 5:
                        Eliminar();
                        break;

                    case 6:
                        Listar();
                        break;

                    case 7:
                        Console.WriteLine("Cerrando el sistema...");
                        break;

                    default:
                        MostrarMensaje("Opcion no valida.");
                        break;
                }

            } while (opcion != 7);
        }

        private void Registrar()
        {
            Console.WriteLine("===== REGISTRAR CURSO =====");
            Console.WriteLine();

            Console.Write("ID: ");
            string id = Console.ReadLine() ?? "";

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";

            Console.Write("Codigo: ");
            string codigo = Console.ReadLine() ?? "";

            Console.Write("Creditos: ");
            if (!int.TryParse(Console.ReadLine(), out int creditos))
            {
                MostrarMensaje("Los creditos deben ser numericos.");
                return;
            }

            Console.Write("Catedratico: ");
            string catedratico = Console.ReadLine() ?? "";

            Console.Write("Horario: ");
            string horario = Console.ReadLine() ?? "";

            Console.Write("Cupo maximo: ");
            if (!int.TryParse(Console.ReadLine(), out int cupoMaximo))
            {
                MostrarMensaje("El cupo maximo debe ser numerico.");
                return;
            }

            Console.Write("Cantidad de inscritos: ");
            if (!int.TryParse(Console.ReadLine(), out int inscritos))
            {
                MostrarMensaje("La cantidad de inscritos debe ser numerica.");
                return;
            }

            Curso nuevo = new Curso(
                id,
                nombre,
                codigo,
                creditos,
                catedratico,
                horario,
                cupoMaximo,
                inscritos
            );

            if (inscritos < 0 || inscritos > cupoMaximo)
            {
                MostrarMensaje("La cantidad de inscritos no es valida.");
                return;
            }

            if (gestor.CrearCurso(nuevo))
            {
                MostrarMensaje("Curso registrado correctamente.");
            }
            else
            {
                MostrarMensaje("No fue posible registrar el curso.");
            }
        }

        private void Consultar()
        {
            Console.WriteLine("===== CONSULTAR CURSO =====");
            Console.WriteLine();

            Console.Write("Ingrese el ID del curso: ");
            string id = Console.ReadLine() ?? "";

            List<Curso> cursos = gestor.Leer();

            Curso curso = cursos.Find(c =>
                c.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (curso == null)
            {
                MostrarMensaje("No existe un curso con ese identificador.");
                return;
            }

            MostrarCurso(curso);
            Pausar();
        }

        private void Buscar()
        {
            Console.WriteLine("===== BUSCAR POR IDENTIFICADOR =====");
            Console.WriteLine();

            Console.Write("Ingrese el ID del curso: ");
            string id = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(id))
            {
                MostrarMensaje("El identificador no puede estar vacio.");
                return;
            }

            List<Curso> cursos = gestor.Leer();

            Buscador<Curso> buscador = new Buscador<Curso>();

            foreach (Curso curso in cursos)
            {
                buscador.Agregar(curso.Id, curso);
            }

            if (buscador.Buscar(id, out Curso encontrado))
            {
                MostrarCurso(encontrado);
                Pausar();
            }
            else
            {
                MostrarMensaje("No se encontro el curso.");
            }
        }

        private void Modificar()
        {
            Console.WriteLine("===== MODIFICAR CURSO =====");
            Console.WriteLine();

            Console.Write("Ingrese el ID del curso a modificar: ");
            string id = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(id))
            {
                MostrarMensaje("El identificador no puede estar vacio.");
                return;
            }

            List<Curso> cursos = gestor.Leer();

            Curso curso = cursos.Find(c =>
                c.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (curso == null)
            {
                MostrarMensaje("No existe un curso con ese identificador.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Datos actuales:");
            MostrarCurso(curso);

            Console.WriteLine();
            Console.WriteLine(
                "Presione ENTER si desea conservar el valor actual.");
            Console.WriteLine();

            Console.Write($"Nombre ({curso.Nombre}): ");
            string nombre = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                curso.Nombre = nombre;
            }

            Console.Write($"Codigo ({curso.Codigo}): ");
            string codigo = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                curso.Codigo = codigo;
            }

            Console.Write($"Creditos ({curso.Creditos}): ");
            string creditosTexto = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(creditosTexto))
            {
                if (!int.TryParse(creditosTexto, out int creditos) ||
                    creditos <= 0)
                {
                    MostrarMensaje("Los creditos ingresados no son validos.");
                    return;
                }

                curso.Creditos = creditos;
            }

            Console.Write($"Catedratico ({curso.Catedratico}): ");
            string catedratico = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(catedratico))
            {
                curso.Catedratico = catedratico;
            }

            Console.Write($"Horario ({curso.Horario}): ");
            string horario = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(horario))
            {
                curso.Horario = horario;
            }

            Console.Write($"Cupo maximo ({curso.CupoMaximo}): ");
            string cupoTexto = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(cupoTexto))
            {
                if (!int.TryParse(cupoTexto, out int cupo) ||
                    cupo <= 0)
                {
                    MostrarMensaje("El cupo maximo ingresado no es valido.");
                    return;
                }

                curso.CupoMaximo = cupo;
            }

            Console.Write($"Inscritos ({curso.Inscritos}): ");
            string inscritosTexto = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(inscritosTexto))
            {
                if (!int.TryParse(inscritosTexto, out int inscritos) ||
                    inscritos < 0)
                {
                    MostrarMensaje(
                        "La cantidad de inscritos ingresada no es valida.");
                    return;
                }

                curso.Inscritos = inscritos;
            }

            if (curso.Inscritos > curso.CupoMaximo)
            {
                MostrarMensaje(
                    "Los inscritos no pueden superar el cupo maximo.");
                return;
            }

            if (!curso.EsValido())
            {
                MostrarMensaje("Los datos modificados no son validos.");
                return;
            }

            if (gestor.Guardar(cursos))
            {
                MostrarMensaje("Curso modificado correctamente.");
            }
            else
            {
                MostrarMensaje("No fue posible modificar el curso.");
            }
        }

        private void Eliminar()
        {
            Console.WriteLine("===== ELIMINAR CURSO =====");
            Console.WriteLine();

            Console.Write("Ingrese el ID del curso a eliminar: ");
            string id = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(id))
            {
                MostrarMensaje("El identificador no puede estar vacio.");
                return;
            }

            List<Curso> cursos = gestor.Leer();

            Curso curso = cursos.Find(c =>
                c.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (curso == null)
            {
                MostrarMensaje("No existe un curso con ese identificador.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Curso encontrado:");
            MostrarCurso(curso);

            Console.WriteLine();
            Console.Write("¿Desea eliminar este curso? (S/N): ");

            string confirmacion = Console.ReadLine() ?? "";

            if (!confirmacion.Equals(
                    "S",
                    StringComparison.OrdinalIgnoreCase))
            {
                MostrarMensaje("Eliminacion cancelada.");
                return;
            }

            cursos.Remove(curso);

            if (gestor.Guardar(cursos))
            {
                MostrarMensaje("Curso eliminado correctamente.");
            }
            else
            {
                MostrarMensaje("No fue posible eliminar el curso.");
            }
        }

        private void Listar()
        {
            Console.WriteLine("===== LISTADO DE CURSOS =====");
            Console.WriteLine();

            List<Curso> cursos = gestor.Leer();

            if (cursos.Count == 0)
            {
                MostrarMensaje("No existen cursos registrados.");
                return;
            }

            foreach (Curso curso in cursos)
            {
                Console.WriteLine(curso);
            }

            Pausar();
        }

        private void MostrarCurso(Curso curso)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"ID:          {curso.Id}");
            Console.WriteLine($"Nombre:      {curso.Nombre}");
            Console.WriteLine($"Codigo:      {curso.Codigo}");
            Console.WriteLine($"Creditos:    {curso.Creditos}");
            Console.WriteLine($"Catedratico: {curso.Catedratico}");
            Console.WriteLine($"Horario:     {curso.Horario}");
            Console.WriteLine($"Cupo maximo: {curso.CupoMaximo}");
            Console.WriteLine($"Inscritos:   {curso.Inscritos}");
            Console.WriteLine("----------------------------------------");
        }

        private void MostrarMensaje(string mensaje)
        {
            Console.WriteLine();
            Console.WriteLine(mensaje);
            Pausar();
        }

        private void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}