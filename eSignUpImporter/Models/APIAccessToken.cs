using System.ComponentModel.DataAnnotations;

namespace eSignUpImporter.Models
{
    public class APIAccessToken
    {
        [Key]
        public string? Token { get; set; }

        public int? ExpireIn { get; set; }
    }
}
