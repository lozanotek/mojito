namespace MvcApplication4.Services
{
    using System.Linq;
    using System.Collections.Generic;

    public class PackageContext
    {

        public IQueryable<Package> Packages
        {
            get
            {
                var list = new List<Package>();
                for (var i = 0; i < 10; i++) {
                    var title = "Package " + (i + 1);
                    var pkg = new Package {Id =title, Title = title, Version = "1.0.0.0"};
                    list.Add(pkg);
                }

                return list.AsQueryable();
            }
        }
    }
}
