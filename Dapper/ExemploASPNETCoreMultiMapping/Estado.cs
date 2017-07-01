namespace ExemploASPNETCoreMultiMapping
{
    public class Estado
    {
        public string SiglaEstado { get; set; }
        public string NomeEstado { get; set; }
        public string NomeCapital { get; set; }
        public Regiao DadosRegiao { get; set; }
    }
}