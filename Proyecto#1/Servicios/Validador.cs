using System;
using System.Collections.Generic;

namespace Proyecto1.Servicios
{
    public static class Validador
    {
        // Verifica que un texto obligatorio no este vacio.
        public static bool TextoObligatorio(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }

        // Verifica que el identificador tenga contenido.
        public static bool IdentificadorValido(string id)
        {
            return !string.IsNullOrWhiteSpace(id);
        }
        // Verifica que un numero sea mayor que cero.
        // Puede utilizarse posteriormente para validar creditos.
        public static bool NumeroPositivo(int numero)
        {
            return numero > 0;
        }
        // Comprueba que el identificador no exista previamente.
        public static bool IdentificadorUnico(string id,IEnumerable<string> idsExistentes)
        {
            if (!IdentificadorValido(id))
            {
                return false;
            }

            foreach (string idExistente in idsExistentes)
            {
                if (string.Equals(id,idExistente,StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }
    }
}