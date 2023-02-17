using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace Rinku.Helpers
{
    public class AlertDecoratorResult : ActionResult
    {
        public IActionResult InnerResult { get; set; }
        public string AlertClass { get; set; }
        public string Message { get; set; }

        public AlertDecoratorResult(IActionResult innerResult, string alertClass, string message)
        {
            InnerResult = innerResult;
            AlertClass = alertClass;
            Message = message;
        }

        //public override void ExecuteResult(ActionContext context)
        //{
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var factory = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;


            var tempData = factory.GetTempData(context.HttpContext);

            tempData["_alert.alertClass"] = AlertClass;
            tempData["_alert.message"] = Message;

            //var alerts = tempData.GetAlerts();
            //alerts.Add(new Alert(AlertClass, Message));
            await InnerResult.ExecuteResultAsync(context);
        }
    }
}
