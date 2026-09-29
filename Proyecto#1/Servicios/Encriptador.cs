using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto1.Servicios
{
    public class Encriptador
    {
        private static string clave = "GestionCursos2026";

        private static byte[] ObtenerClave()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
            }
        }

        public static bool EncriptarArchivo(string ruta)
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    Console.WriteLine("Error: el archivo XML no existe.");
                    return false;
                }

                byte[] contenido = File.ReadAllBytes(ruta);

                byte[] contenidoEncriptado;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = ObtenerClave();
                    aes.GenerateIV();

                    using (MemoryStream memoria = new MemoryStream())
                    {
                        memoria.Write(aes.IV, 0, aes.IV.Length);

                        using (CryptoStream crypto = new CryptoStream(
                            memoria,
                            aes.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            crypto.Write(contenido, 0, contenido.Length);
                        }

                        contenidoEncriptado = memoria.ToArray();
                    }
                }

                File.WriteAllBytes(ruta, contenidoEncriptado);

                Console.WriteLine("Archivo encriptado correctamente.");
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: no tiene permisos para acceder a este archivo.");
                return false;
            }
            catch (IOException)
            {
                Console.WriteLine("Error al leer o escribir el archivo.");
                return false;
            }
            catch (CryptographicException)
            {
                Console.WriteLine("Error durante la encriptacion.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return false;
            }
        }

        public static bool DesencriptarArchivo(string ruta)
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    Console.WriteLine("Error: el archivo XML no existe.");
                    return false;
                }

                byte[] contenidoDesencriptado;

                using (FileStream archivo = new FileStream(
                    ruta,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read))
                {
                    byte[] iv = new byte[16];

                    int bytesLeidos = archivo.Read(iv, 0, iv.Length);

                    if (bytesLeidos != iv.Length)
                    {
                        Console.WriteLine("Error: el archivo no tiene un formato valido.");
                        return false;
                    }

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = ObtenerClave();
                        aes.IV = iv;

                        using (CryptoStream crypto = new CryptoStream(
                            archivo,
                            aes.CreateDecryptor(),
                            CryptoStreamMode.Read))
                        {
                            using (MemoryStream memoria = new MemoryStream())
                            {
                                crypto.CopyTo(memoria);
                                contenidoDesencriptado = memoria.ToArray();
                            }
                        }
                    }
                }

                File.WriteAllBytes(ruta, contenidoDesencriptado);

                Console.WriteLine("Archivo desencriptado correctamente.");
                return true;
            }
            catch (CryptographicException)
            {
                Console.WriteLine("Error: no se pudo desencriptar el archivo.");
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: no tiene permisos para acceder al archivo.");
                return false;
            }
            catch (IOException)
            {
                Console.WriteLine("Error al leer o escribir el archivo.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return false;
            }
        }
    }
}