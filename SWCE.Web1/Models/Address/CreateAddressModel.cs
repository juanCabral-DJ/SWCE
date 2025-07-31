namespace SWCE.Web1.Models.Address
{
    public class CreateAddressModel
    {
        public int iD_Usuario { get; set; }
        public string calle { get; set; }
        public string ciudad { get; set; }
        public string estado_provincia { get; set; }
        public string codigo_postal { get; set; }
        public string pais { get; set; }
        public bool es_predeterminada { get; set; }
    }


}
