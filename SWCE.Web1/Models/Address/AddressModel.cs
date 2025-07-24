using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SWCE.Web1.Models.Address
{
    public class AddressModel
    {
        public int id { get; set; }
        public int iD_Usuario { get; set; }
        public string calle { get; set; }
        public string ciudad { get; set; }
        public string estado_provincia { get; set; }
        public string codigo_postal { get; set; }
        public string pais { get; set; }
        public bool es_predeterminada { get; set; }
        public bool isDeleted { get; set; }
    }

    public class GetByIdAddressResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public  AddressModel  data { get; set; }
    }

    public class GetByPredeterminadaAddressResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public AddressModel data { get; set; }
    }

    public class GetByUserIdAddressResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public List<AddressModel> data { get; set; }
    }

    public class GetAllAddressResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public List<AddressModel> data { get; set; }
    }
}
