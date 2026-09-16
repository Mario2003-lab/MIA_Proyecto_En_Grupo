using System;

namespace Proyecto1.Interfaz
{
    public class MenuConsola
    {
        public void Mostrar()
        {
            int opcion = 0;

            do
            {
                Console.Clear();

                Console.WriteLine("========================================");
                Console.WriteLine("   SISTEMA DE GESTION DE INFORMACION");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("1. Registrar informacion");
                Console.WriteLine("2. Consultar informacion");
                Console.WriteLine("3. Buscar por identificador");
                Console.WriteLine("4. Modificar informacion");
                Console.WriteLine("5. Eliminar informacion");
                Console.WriteLine("6. Listar informacion");
                Console.WriteLine("7. Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opcion: ");

                string entrada = Console.ReadLine() ?? "";

                if (!int.TryParse(entrada, out opcion))
                {
                    MostrarMensaje("Debe ingresar una opcion numerica.");
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
                        MostrarMensaje("Opcion no valida. Seleccione una opcion del 1 al 7.");
                        break;
                }

            } while (opcion != 7);
        }

        private void Registrar()
        {
            Console.WriteLine("===== REGISTRAR INFORMACION =====");
            Console.WriteLine();
            Console.WriteLine("Funcion pendiente de integrar con GestorXML.");
            Pausar();
        }

        private void Consultar()
        {
            Console.WriteLine("===== CONSULTAR INFORMACION =====");
            Console.WriteLine();
            Console.WriteLine("Funcion pendiente de integrar con GestorXML.");
            Pausar();
        }

        private void Buscar()
        {
            Console.WriteLine("===== BUSCAR POR IDENTIFICADOR =====");
            Console.WriteLine();
            Console.WriteLine("Funcion pendiente de integrar con Buscador.");
            Pausar();
        }

        private void Modificar()
        {
            Console.WriteLine("===== MODIFICAR INFORMACION =====");
            Console.WriteLine();
            Console.WriteLine("Funcion preparada para integrarse con GestorXML.");
            Pausar();
        }

        private void Eliminar()
        {
            Console.WriteLine("===== ELIMINAR INFORMACION =====");
            Console.WriteLine();
            Console.WriteLine("Funcion preparada para integrarse con GestorXML.");
            Pausar();
        }

        private void Listar()
        {
            Console.WriteLine("===== LISTADO DE INFORMACION =====");
            Console.WriteLine();
            Console.WriteLine("Funcion pendiente de integrar con los registros almacenados.");
            Pausar();
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