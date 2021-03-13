using System.Text;

namespace Model
{
    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Logradouro : {Logradouro}");
            sb.AppendLine($"Bairro : {Bairro}");
            sb.AppendLine($"Cidade : {Cidade}");
            sb.AppendLine($"Estado : {Estado}");
            sb.AppendLine($"CEP : {CEP}");

            return sb.ToString();
        }
    }
}