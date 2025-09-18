namespace Blaise.Questionnaire.Data.Tool.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Blaise.Nuget.Api.Api;
    using Blaise.Nuget.Api.Contracts.Enums;
    using Blaise.Nuget.Api.Contracts.Extensions;
    using Blaise.Nuget.Api.Contracts.Interfaces;
    using Blaise.Nuget.Api.Contracts.Models;
    using StatNeth.Blaise.API.ServerManager;

    public class QuestionnaireHelper
    {
        private readonly IBlaiseQuestionnaireApi _blaiseQuestionnaireApi;

        public QuestionnaireHelper(ConnectionModel connectionModel)
        {
            _blaiseQuestionnaireApi = new BlaiseQuestionnaireApi(connectionModel);
        }

        public static QuestionnaireHelper GetInstance(ConnectionModel connectionModel)
        {
            return new QuestionnaireHelper(connectionModel);
        }

        public IEnumerable<string> GetQuestionnaires(string serverParkName)
        {
            var questionnaires = _blaiseQuestionnaireApi.GetQuestionnaires(serverParkName);
            return questionnaires.Select(i => i.Name);
        }

        public void InstallQuestionnaire(string questionnaireName, string serverPark, string questionnaireFile)
        {
            var installOptions = new InstallOptions
            {
                DataEntrySettingsName = QuestionnaireDataEntryType.StrictInterviewing.ToString(),
                InitialAppLayoutSetGroupName = QuestionnaireInterviewType.Cati.FullName(),
                LayoutSetGroupName = QuestionnaireInterviewType.Cati.FullName(),
                OverwriteMode = DataOverwriteMode.Always,
            };

            _blaiseQuestionnaireApi.InstallQuestionnaire(questionnaireName, serverPark, questionnaireFile, installOptions);
        }
    }
}
