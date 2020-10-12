using System.Web;
using System.Web.Mvc;

namespace MoviesRL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Ovdje registrujemo globalne filtere

            filters.Add(new HandleErrorAttribute()); // defaultni filter - redirecta na error stranicu ukoliko neka akcija baci izuzetak
            filters.Add(new AuthorizeAttribute()); // redirecta na login stranicu ukoliko nismo loginovani (ako hocemo da ipak omogucimo pristup nekoj stranici, kod kontrolera definisemo [AllowAnonymous] atribut)
            filters.Add(new RequireHttpsAttribute()); // zabrana pristupa aplikaciji na http protokolu, omogucavanje samo za https
        }
    }
}
