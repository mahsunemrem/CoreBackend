using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public class UserForHomePageDto
    {
        [Display(Name = "Kullanıcı Adı")]
        public string Name { get; set; }
    }
}
