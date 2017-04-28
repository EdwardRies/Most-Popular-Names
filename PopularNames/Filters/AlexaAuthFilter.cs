using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using PopularNames.Models;
using PopularNames.Utilities;

namespace PopularNames.Filters
{
    /// <summary>
    ///     This is a action filter used for validation for an Alexa Application.
    /// </summary>
    public class AlexaAuthFilter : ActionFilterAttribute
    {
        /// <summary>
        ///     Name of Alexa Request Model
        /// </summary>
        private const string AlexaRequestModel = "AlexaRequestModel";

        public AlexaAuthFilter()
        {

        }

        /// <summary>
        ///     Process Validation for Alexa Application
        /// </summary>
        /// <param name="actionContext">Http Action Context</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task OnActionExecutingAsync(
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            var parameters = actionContext.ActionDescriptor.GetParameters();
            var parameter = parameters?.FirstOrDefault();

            if (parameter != null)
            {
                var parameterValue = actionContext.ActionArguments != null && actionContext.ActionArguments.Any()
                    ? actionContext.ActionArguments.First().Value
                    : null;
                
                try
                {
                    var model = parameterValue as AlexaRequestModel;
                    if (parameter.ParameterType.Name == AlexaRequestModel && model != null)
                    {

                        if (model.Session.Application.ApplicationId != AppSettings.ReadSetting("applicationId")
                            && !actionContext.Request.RequestUri.Host.ToLower().Equals("localhost"))
                            {
//#if !DEBUG
                                await Task.Factory.StartNew(() =>
                                {
                                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid Alexa application Id in request");
                                });

                                return;
//#endif
                            }

                        // Set credentials here if needed
                    }
                }
                catch (SqlException ex)
                {
                    // logger.Fatal(ex, "AlexaAuthFilter");
                }
            }

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}