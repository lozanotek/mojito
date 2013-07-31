namespace MvcApplication4.Controllers {
	using System.IO;
	using System.Web.Mvc;

	public class PackagesController : Controller {
		public ActionResult DownloadPackage() {
			return File(new MemoryStream(), "application/zip");
		}
	}

	public class RecipesController :Controller {
		public ActionResult Download() {
			return File(new MemoryStream(), "application/zip");
		}
	}
}
