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
            bool estabaEncriptado = false;

            try
            {
                if (!File.Exists(ruta))
                {
                    return new List<Curso>();
                }

                estabaEncriptado = EsArchivoEncriptado();

                if (estabaEncriptado)
                {
                    if (!Encriptador.DesencriptarArchivo(ruta))
                    {
                        Console.WriteLine("Error: no se pudo desencriptar el archivo.");
                        return new List<Curso>();
                    }
                }

                XmlSerializer serializador = new XmlSerializer(typeof(ListaCursos));

                ListaCursos datos;

                using (FileStream archivo = new FileStream(ruta, FileMode.Open))
                {
                    datos = (ListaCursos)serializador.Deserialize(archivo);
                }

                if (estabaEncriptado)
                {
                    if (!Encriptador.EncriptarArchivo(ruta))
                    {
                        Console.WriteLine("Error: no se pudo volver a encriptar el archivo.");
                        return new List<Curso>();
                    }
                }

                return datos.Cursos;
            }
            catch (IOException)
            {
                Console.WriteLine("Error al leer el archivo XML.");

                if (estabaEncriptado)
                {
                    Encriptador.EncriptarArchivo(ruta);
                }

                return new List<Curso>();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error: el archivo XML tiene un formato invalido.");

                if (estabaEncriptado)
                {
                    Encriptador.EncriptarArchivo(ruta);
                }

                return new List<Curso>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);

                if (estabaEncriptado)
                {
                    Encriptador.EncriptarArchivo(ruta);
                }

                return new List<Curso>();
            }
        }

        private bool EsArchivoEncriptado()
        {
            try
            {
                using (FileStream archivo = new FileStream(ruta, FileMode.Open))
                {
                    byte[] primerosBytes = new byte[5];
                    int cantidad = archivo.Read(primerosBytes, 0, primerosBytes.Length);

                    if (cantidad < 5)
                    {
                        return false;
                    }

                    string inicio = System.Text.Encoding.UTF8.GetString(primerosBytes);

                    return inicio != "<?xml";
                }
            }
            catch
            {
                return false;
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

                if (!Encriptador.EncriptarArchivo(ruta))
                {
                    Console.WriteLine("Error: no se pudo encriptar el archivo.");
                    return false;
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
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
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