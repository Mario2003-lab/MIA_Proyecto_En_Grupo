using System;
using System.IO;
using System.Security.Cryptography;
using system.Text;

namespace Proyecto1.Servicios
{
  public class Encriptador
  {
    private static string clave = "GestionCursos2026";

    //para generar la clase de 32 bytes
    private static byte[] obtenerClave()
    {
      using (SHA256 sha256 = SHA256.Create())
      {
        return sha256.ComputeHash(Encoding.UTF8.GetByte(clave));
      }
    }
    public static bool EncriptarArchivo(string ruta)
    {
      try
      {
        if(!File.Exists(ruta))
        {
          Console.WriteLine("Error: El archivo XML no existe.");
          return false;
        }
        byte[] contenido = File.ReadAllBytes(ruta);

        using (Aes aes = Aes.Create())
        {
          aes.Key = ObtenerClave();
          aes.GenerateIV();

          using (FileStream archivo = new FileStream(ruta, FileMode.Create))
          {
            //Hay que guardar primero el IV
            archivo.Write(aes.IV, 0, aes.IV.Length);
            using (CryptoStream crypto = new CryptoStream(
              archivo;
              aes.CreateEncryptor(),
              CryptoStreamMode.Write))
            {
              crypto.Write(contenido, 0, contenido.Length);
            }
          }
        }

        Console.WriteLine("Archivo encriptafo correctamente.");
        return true;
      }
      catch(UnauthorizedAccessException)
      {
        Console.WriteLine("Error: no tiene permismos para acceder a este archivo .")
          return false;
      }
      catch (IOException)
      {
        Console.WriteLine("Error al leer o escribir el archivo. ")
          return false;
      }
      catch (CryptographicException)
      {
        Console.WriteLine("Error durante la encriptacion. ")
        return false;
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error inesperado" + ex.Message);
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

                byte[] claveAES = ObtenerClave();

                using (FileStream archivo = new FileStream(ruta, FileMode.Open))
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
                        aes.Key = claveAES;
                        aes.IV = iv;

                        using (CryptoStream crypto = new CryptoStream(
                            archivo,
                            aes.CreateDecryptor(),
                            CryptoStreamMode.Read))
                        {
                            using (MemoryStream memoria = new MemoryStream())
                            {
                                crypto.CopyTo(memoria);

                                byte[] contenidoDesencriptado = memoria.ToArray();

                                File.WriteAllBytes(ruta, contenidoDesencriptado);
                            }
                        }
                    }
                }

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
        
    

            
