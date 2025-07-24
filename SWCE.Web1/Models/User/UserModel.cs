namespace SWCE.Web1.Models.User
{
    public class UserModel
    {
        public int id { get; set; }
        public int id_rol { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public object password { get; set; }
        public DateTime fecha_Creacion { get; set; }
        public bool isDeleted { get; set; }

    }
    public class GetByIdUserResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public  UserModel data { get; set; }
    }

    public class GetByEmailUserResponse
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public UserModel data { get; set; }
    }

    public class GetAllUserResponse
        {
            public string message { get; set; }
            public bool isSuccess { get; set; }
            public List<UserModel> data { get; set; }
        }

}
