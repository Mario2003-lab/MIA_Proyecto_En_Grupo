using System.Collections.Generic;
using Proyecto1.Modelos;

namespace Proyecto1.Servicios
{
    public abstract class RepositorioBase
    {
        protected readonly string ruta;

        protected RepositorioBase(string ruta)
        {
            this.ruta = ruta;
        }

        public abstract List<Curso> Leer();

        public abstract bool Guardar(List<Curso> cursos);
    }
}