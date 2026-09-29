using System.IO;
using Proyecto1.Interfaz;

string rutaXml = Path.Combine("Datos", "datos.xml");

MenuConsola menu = new MenuConsola(rutaXml);

menu.Mostrar();