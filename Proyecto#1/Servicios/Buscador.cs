using System;
using System.Collections.Generic;

namespace Proyecto1.Servicios
{
    public class Buscador<T>
    {
        // Indice en memoria que relaciona un ID con su registro
        private readonly Dictionary<string, T> indice;
        public Buscador()
        {
            indice = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        }

        // Agrega un registro al indice
        public bool Agregar(string id, T registro)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            if (indice.ContainsKey(id))
            {
                return false;
            }
            indice.Add(id, registro);
            return true;
        }
        // Busca un registro mediante su identificador
        public bool Buscar(string id, out T registro)
        {
            return indice.TryGetValue(id, out registro);
        }
        // Comprueba si un identificador existe
        public bool Existe(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            return indice.ContainsKey(id);
        }

        // Elimina un registro del indice
        public bool Eliminar(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            return indice.Remove(id);
        }
        // Devuelve todos los registros almacenados
        public IEnumerable<T> Listar()
        {
            return indice.Values;
        }
        // Devuelve la cantidad de registros indexados
        public int Cantidad()
        {
            return indice.Count;
        }
        // Vacía el indice
        public void Limpiar()
        {
            indice.Clear();
        }
    }
}