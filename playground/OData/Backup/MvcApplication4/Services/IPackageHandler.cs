namespace MvcApplication4.Services
{
    using System.Web.Routing;

    public interface IPackageHandler
    {
        void CreatePackage(RequestContext context);
        void DeletePackage(RequestContext context);
        void PublishPackage(RequestContext context);
    }
}