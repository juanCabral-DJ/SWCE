 

namespace SWCE.Aplicatition.Dtos.User
{
    public record class UpdateUserDto
    {
        public int id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
