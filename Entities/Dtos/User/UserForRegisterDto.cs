using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class UserForRegisterDto : IDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "E-Posta")]
        public string Email { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        [Display(Name = "Ad")]
        public string FirstName { get; set; }
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
    }
}
