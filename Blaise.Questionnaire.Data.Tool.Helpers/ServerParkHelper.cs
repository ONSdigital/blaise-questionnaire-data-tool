namespace Blaise.Questionnaire.Data.Tool.Helpers
{
    using System.Collections.Generic;
    using Blaise.Nuget.Api.Api;
    using Blaise.Nuget.Api.Contracts.Interfaces;
    using Blaise.Nuget.Api.Contracts.Models;

    public class ServerParkHelper
    {
        private readonly IBlaiseServerParkApi _blaiseServerParkApi;

        public ServerParkHelper(ConnectionModel connectionModel)
        {
            _blaiseServerParkApi = new BlaiseServerParkApi(connectionModel);
        }

        public static ServerParkHelper GetInstance(ConnectionModel connectionModel)
        {
            return new ServerParkHelper(connectionModel);
        }

        public IEnumerable<string> GetServerParks()
        {
            return _blaiseServerParkApi.GetNamesOfServerParks();
        }
    }
}
