using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        //viewcomponent eki yap ! 



            /// <summary>
            /// view de metodu çağırırken
            /// @await Component.InvokeAsync("Menu")   bu şekilde çağırabilirsin
            /// </summary>
            /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            //veritabanına bağlan işemleri hallet 


            return View();  // view model de yollayabilirsin controller gibi iş görüyor yani ! 


        }

       

    }
}
