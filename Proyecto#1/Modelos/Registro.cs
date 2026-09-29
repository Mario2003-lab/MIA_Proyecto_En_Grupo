using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Proyecto1.Modelos
{
    // === 1. AQUÍ ESTÁ LA CLASE ABSTRACTA PARA CUMPLIR EL REQUISITO ===
    public abstract class RegistroBase
    {
        [XmlElement("id")]
        public string Id { get; set; }

        // Método abstracto: obliga a cualquier clase que herede de esta a programar su propia validación
        public abstract bool EsValido(); 
    }

    // === 2. LA CLASE CURSO AHORA HEREDA DE LA CLASE ABSTRACTA ===
    [XmlRoot("curso")]
    public class Curso : RegistroBase
    {
        // El atributo 'Id' ya no se declara aquí porque se hereda automáticamente de RegistroBase

        [XmlElement("nombre")]
        public string Nombre { get; set; }

        [XmlElement("codigo")]
        public string Codigo { get; set; }

        [XmlElement("creditos")]
        public int Creditos { get; set; }

        [XmlElement("catedratico")]
        public string Catedratico { get; set; }

        [XmlElement("horario")]
        public string Horario { get; set; }

        [XmlElement("cupoMaximo")]
        public int CupoMaximo { get; set; }

        [XmlElement("inscritos")]
        public int Inscritos { get; set; }

        public Curso()
        {
        }

        public Curso(string id, string nombre, string codigo, int creditos, string catedratico, string horario, int cupoMaximo, int inscritos)
        {
            Id = id; // Lo asignamos normal, pero pertenece a la clase padre abstracta
            Nombre = nombre;
            Codigo = codigo;
            Creditos = creditos;
            Catedratico = catedratico;
            Horario = horario;
            CupoMaximo = cupoMaximo;
            Inscritos = inscritos;
        }

        public bool TieneCupoDisponible()
        {
            return Inscritos < CupoMaximo;
        }

        // === 3. SE USA 'OVERRIDE' PARA IMPLEMENTAR EL MÉTODO ABSTRACTO ===
        public override bool EsValido()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Codigo))
            {
                return false;
            }
            if (Creditos <= 0)
            {
                return false;
            }
            if (CupoMaximo <= 0)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"{Id} - {Nombre} ({Codigo}) - {Catedratico} - {Inscritos}/{CupoMaximo}";
        }
    }

    [XmlRoot("cursos")]
    public class ListaCursos
    {
        [XmlElement("curso")]
        public List<Curso> Cursos { get; set; }

        public ListaCursos()
        {
            Cursos = new List<Curso>();
        }
    }
}