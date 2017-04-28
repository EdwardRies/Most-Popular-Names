using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using PopularNames.Filters;
using PopularNames.Models;
using PopularNames.Utilities;

namespace PopularNames.Controllers
{
    [RoutePrefix("api/Alexa")]
    [AllowAnonymous]
    [AlexaAuthFilter]
    public class AlexaController : ApiController
    {

        [Route("Session")]
        [HttpPost]
        public AlexaResponseModel Session(AlexaRequestModel alexaRequestModel)
        {
            var alexaResponseModel = BuildAlexaResponseModel();

            if (alexaRequestModel.Request.Intent.Name == "AMAZON.HelpIntent")
            {
                return SetErrorResponse(alexaResponseModel, StaticText.HelpMessage);
            }

            var gender = GetSlotValue(alexaRequestModel, "gender");
            if (string.IsNullOrWhiteSpace(gender))
            {
                return SetErrorResponse(alexaResponseModel, StaticText.MissingGender);
            }

            var year = GetSlotValue(alexaRequestModel, "year");
            if (string.IsNullOrWhiteSpace(year))
            {
                year = DateTime.UtcNow.Year.ToString();
            }

            var entry = WebApiApplication.Data.SingleOrDefault(o => o.Year == year);
            if (entry == null)
            {
                return SetErrorResponse(alexaResponseModel, StaticText.UnknownYear);
            }

            var name = (gender == "boy")
                ? entry.Boys.First()
                : entry.Girls.First();

            return SetResponse(alexaResponseModel, gender, year, name);
        }

        private static string GetSlotValue(AlexaRequestModel alexaRequestModel, string key)
        {
            return alexaRequestModel.Request.Intent.Slots
                .SingleOrDefault(o => o.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)).Value ?? string.Empty;
        }

        private static AlexaResponseModel BuildAlexaResponseModel()
        {
            var alexaResponseModel = new AlexaResponseModel();
            alexaResponseModel.Response.Card.Title = StaticText.Title;
            alexaResponseModel.Response.Card.Content = StaticText.PromptForInput;
            alexaResponseModel.Response.Reprompt.OutputSpeech.Text = StaticText.RepromptForInput;
            alexaResponseModel.Response.ShouldEndSession = true;

            return alexaResponseModel;
        }

        private static AlexaResponseModel SetResponse(AlexaResponseModel alexaResponseModel, string gender, string year, string name)
        {
            var response = $"The most popular {gender} name in {year} was {name}";
            alexaResponseModel.Response.OutputSpeech.Text = response;
            alexaResponseModel.Response.Card.Title = "Popular Names";
            alexaResponseModel.Response.Card.Content = response;

            return alexaResponseModel;
        }

        private static AlexaResponseModel SetErrorResponse(AlexaResponseModel alexaResponseModel, string errorMessage)
        {;
            alexaResponseModel.Response.OutputSpeech.Text = errorMessage;
            alexaResponseModel.Response.Card.Title = StaticText.Title;
            alexaResponseModel.Response.Card.Content = errorMessage;

            return alexaResponseModel;
        }
    }
}
