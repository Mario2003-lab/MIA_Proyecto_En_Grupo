using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto1.Servicios
{
    public class Encriptador
    {
        private static readonly string clave = "GestionCursos2026";

        // Genera una clave de 32 bytes utilizando SHA256
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

                using (Aes aes = Aes.Create())
                {
                    aes.Key = ObtenerClave();
                    aes.GenerateIV();

                    using (MemoryStream memoria = new MemoryStream())
                    {
                        // Guardar primero el IV para utilizarlo al desencriptar
                        memoria.Write(aes.IV, 0, aes.IV.Length);

                        using (CryptoStream crypto = new CryptoStream(
                            memoria,
                            aes.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            crypto.Write(contenido, 0, contenido.Length);
                            crypto.FlushFinalBlock();
                        }

                        File.WriteAllBytes(ruta, memoria.ToArray());
                    }
                }

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

                byte[] contenido = File.ReadAllBytes(ruta);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = ObtenerClave();

                    int tamanoIV = aes.BlockSize / 8;

                    if (contenido.Length <= tamanoIV)
                    {
                        Console.WriteLine("Error: el archivo no contiene informacion encriptada valida.");
                        return false;
                    }

                    byte[] iv = new byte[tamanoIV];
                    Array.Copy(contenido, 0, iv, 0, tamanoIV);

                    aes.IV = iv;

                    int tamanoContenidoEncriptado = contenido.Length - tamanoIV;

                    using (MemoryStream memoriaEntrada = new MemoryStream(
                        contenido,
                        tamanoIV,
                        tamanoContenidoEncriptado))
                    {
                        using (CryptoStream crypto = new CryptoStream(
                            memoriaEntrada,
                            aes.CreateDecryptor(),
                            CryptoStreamMode.Read))
                        {
                            using (MemoryStream memoriaSalida = new MemoryStream())
                            {
                                crypto.CopyTo(memoriaSalida);

                                File.WriteAllBytes(
                                    ruta,
                                    memoriaSalida.ToArray());
                            }
                        }
                    }
                }

                Console.WriteLine("Archivo desencriptado correctamente.");
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
                Console.WriteLine("Error durante la desencriptacion. El archivo puede estar dañado o la clave no es correcta.");
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