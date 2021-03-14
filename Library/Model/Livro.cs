using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Livro
    {
        public long NumeroTombo { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime DataPublicao { get; set; }
        public string Autor { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Numero do Tombo: {NumeroTombo}");
            sb.AppendLine($"ISBN: {ISBN}");
            sb.AppendLine($"Titulo: {Titulo}");
            sb.AppendLine($"Genero: {Genero}");
            sb.AppendLine($"Data da Publição: {DataPublicao:dd/MM/yyyy}");
            sb.AppendLine($"Autor: {Autor}");

            return sb.ToString();
        }
    }
}
