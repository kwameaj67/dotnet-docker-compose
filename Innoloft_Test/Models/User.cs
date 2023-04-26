
using Microsoft.EntityFrameworkCore;

namespace Innoloft_Test.Models
{
    public class User
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public UserAddress? address { get; set; }
        public string? phone { get; set; }
        public string? website { get; set; }
        public UserCompany? company { get; set; }
    }

    [Keyless]
    public class UserAddress
    {
        public string? street { get; set; }
        public string? suite { get; set; }
        public string? city { get; set; }
        public string? zipcode { get; set; }
        public UserGeo? geo { get; set; }
    }

    [Keyless]
    public class UserGeo
    {
        public string? lat { get; set; }
        public string? lng { get; set; }
    }

    [Keyless]
    public class UserCompany
    {
        public string? name { get; set; }
        public string? catchPhrase { get; set; }
        public string? bs { get; set; }

    }
}
