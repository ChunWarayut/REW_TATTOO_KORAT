using System.Web;
using System.Web.Mvc;

namespace REW_TATTOO_KORAT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
