using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Proyecto1.Modelos;

namespace Proyecto1.Servicios
{
    public class GestorXML
    {
        private readonly string ruta;

        public GestorXML(string ruta)
        {
            this.ruta = ruta;
        }

        public List<Curso> Leer()
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    return new List<Curso>();
                }

                // Primero intentamos leerlo como XML normal.
                // Esto sirve para la primera ejecucion.
                try
                {
                    List<Curso> cursos = LeerXML();

                    // Despues de leerlo, lo dejamos encriptado.
                    Encriptador.EncriptarArchivo(ruta);

                    return cursos;
                }
                catch (InvalidOperationException)
                {
                    // Si no se pudo leer como XML,
                    // asumimos que esta encriptado.
                }

                // Desencriptamos para poder leerlo.
                if (!Encriptador.DesencriptarArchivo(ruta))
                {
                    Console.WriteLine("Error: no se pudo desencriptar el archivo.");
                    return new List<Curso>();
                }

                List<Curso> cursosDesencriptados = LeerXML();

                // Lo volvemos a encriptar despues de leer.
                Encriptador.EncriptarArchivo(ruta);

                return cursosDesencriptados;
            }
            catch (IOException)
            {
                Console.WriteLine("Error al leer el archivo XML.");
                return new List<Curso>();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: no tiene permisos para acceder al archivo.");
                return new List<Curso>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return new List<Curso>();
            }
        }

        private List<Curso> LeerXML()
        {
            XmlSerializer serializador =
                new XmlSerializer(typeof(ListaCursos));

            using (FileStream archivo =
                new FileStream(ruta, FileMode.Open))
            {
                ListaCursos datos =
                    (ListaCursos)serializador.Deserialize(archivo);

                return datos.Cursos;
            }
        }

        public bool Guardar(List<Curso> cursos)
        {
            try
            {
                ListaCursos datos = new ListaCursos();
                datos.Cursos = cursos;

                XmlSerializer serializador =
                    new XmlSerializer(typeof(ListaCursos));

                using (FileStream archivo =
                    new FileStream(ruta, FileMode.Create))
                {
                    serializador.Serialize(archivo, datos);
                }

                // Despues de guardar el XML,
                // lo encriptamos.
                if (!Encriptador.EncriptarArchivo(ruta))
                {
                    Console.WriteLine(
                        "Error: no se pudo encriptar el archivo.");

                    return false;
                }

                return true;
            }
            catch (IOException)
            {
                Console.WriteLine(
                    "Error al escribir el archivo XML.");

                return false;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(
                    "Error: no tiene permisos para escribir el archivo.");

                return false;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(
                    "Error al generar el archivo XML.");

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "Error inesperado: " + ex.Message);

                return false;
            }
        }

        public bool CrearCurso(Curso nuevo)
        {
            if (nuevo == null || !nuevo.EsValido())
            {
                Console.WriteLine(
                    "Error: los datos del curso no son validos.");

                return false;
            }

            List<Curso> cursos = Leer();

            bool existeId = cursos.Exists(c =>
                c.Id.Equals(
                    nuevo.Id,
                    StringComparison.OrdinalIgnoreCase));

            if (existeId)
            {
                Console.WriteLine(
                    "Error: ya existe un curso con ese identificador.");

                return false;
            }

            cursos.Add(nuevo);

            return Guardar(cursos);
        }
    }
}
