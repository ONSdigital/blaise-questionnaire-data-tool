namespace Blaise.Questionnaire.Data.Tool.Helpers
{
    using Blaise.Nuget.Api.Api;
    using Blaise.Nuget.Api.Contracts.Interfaces;
    using Blaise.Nuget.Api.Contracts.Models;

    public class ConnectionHelper
    {
        private readonly IBlaiseHealthApi _blaiseHealthApi;

        public ConnectionHelper(ConnectionModel connectionModel)
        {
            _blaiseHealthApi = new BlaiseHealthApi(connectionModel);
        }

        public bool ConnectionSuccessful => ConnectionModelValid &&
                                            _blaiseHealthApi.ConnectionToBlaiseIsHealthy() &&
                                            _blaiseHealthApi.RemoteConnectionToBlaiseIsHealthy();

        public bool ConnectionModelValid => _blaiseHealthApi.ConnectionModelIsHealthy();

        public static ConnectionHelper GetInstance(ConnectionModel connectionModel)
        {
            return new ConnectionHelper(connectionModel);
        }
    }
}
