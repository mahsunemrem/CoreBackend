using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class UserForLoginDto : IDto
    {
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
