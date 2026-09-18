using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Proyecto1.Modelos
{
    [XmlRoot("curso")]
    public class Curso
    {
        [XmlElement("id")]
        public string Id { get; set; }

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
            Id = id;
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

        public bool EsValido()
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