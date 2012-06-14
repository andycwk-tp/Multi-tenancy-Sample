namespace MultiTenancy.Tenants.Sample2.Controllers
{
  public class HomeController: MultiTenancy.Web.Controllers.HomeController
  {
    public override System.Web.Mvc.ActionResult Index()
    {
      return base.Index();
    }
  }
}