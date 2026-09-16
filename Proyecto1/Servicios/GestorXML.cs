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

                XmlSerializer serializador = new XmlSerializer(typeof(ListaCursos));

                using (FileStream archivo = new FileStream(ruta, FileMode.Open))
                {
                    ListaCursos datos = (ListaCursos)serializador.Deserialize(archivo);
                    return datos.Cursos;
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Error al leer el archivo XML.");
                return new List<Curso>();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error: el archivo XML tiene un formato invalido.");
                return new List<Curso>();
            }
        }

        public bool Guardar(List<Curso> cursos)
        {
            try
            {
                ListaCursos datos = new ListaCursos();
                datos.Cursos = cursos;

                XmlSerializer serializador = new XmlSerializer(typeof(ListaCursos));

                using (FileStream archivo = new FileStream(ruta, FileMode.Create))
                {
                    serializador.Serialize(archivo, datos);
                }

                return true;
            }
            catch (IOException)
            {
                Console.WriteLine("Error al escribir el archivo XML.");
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: no tiene permisos para escribir el archivo.");
                return false;
            }
        }

        public bool CrearCurso(Curso nuevo)
        {
            if (nuevo == null || !nuevo.EsValido())
            {
                Console.WriteLine("Error: los datos del curso no son validos.");
                return false;
            }

            List<Curso> cursos = Leer();

            bool existeId = cursos.Exists(c => c.Id.Equals(nuevo.Id, StringComparison.OrdinalIgnoreCase));

            if (existeId)
            {
                Console.WriteLine("Error: ya existe un curso con ese identificador.");
                return false;
            }

            cursos.Add(nuevo);

            return Guardar(cursos);
        }
    }
}