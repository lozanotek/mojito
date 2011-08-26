namespace Mojito.Server.Services {
    using Mojito.Server.Models;

    public class InfoService : MojitoDataService<MojitoContext> {
        public InfoService(MojitoContext context) : base(context) {
            Context = context;
        }
    }
}