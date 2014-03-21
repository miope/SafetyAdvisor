using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SafetyAdvisor.Helpers
{
    public enum AlertType
    { 
        Success,
        Info,
        Warning,
        Danger
    }

    public class Alert
    {
        public AlertType Type { get; set; }
        public string Message { get; set; }
    }

    public class Alerts
    {
        private const string ALERTS_TEMPDATA_KEY = "FLASH.ALERTS";
        private IList<Alert> alerts;

        public Alerts(TempDataDictionary tempDataDict)
        {
            if (!tempDataDict.ContainsKey(ALERTS_TEMPDATA_KEY))
            {
                tempDataDict[ALERTS_TEMPDATA_KEY] = new List<Alert>();
            }

            alerts = tempDataDict[ALERTS_TEMPDATA_KEY] as IList<Alert>;
        }

        public IEnumerable<Alert> All
        {
            get
            {
                return alerts;
            }
        }

        public void Add(AlertType type, string message)
        {
            alerts.Add(new Alert() { Type = type, Message = message });
        }
        
    }

    public class ActionResultWithAlert : ActionResult
    {
        private ActionResult result;
        private AlertType alertType;
        private string alertMessage;

        public ActionResultWithAlert(ActionResult result, AlertType alertType, string alertMessage)
        {
            this.result = result;
            this.alertType = alertType;
            this.alertMessage = alertMessage;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.GetAlerts().Add(alertType, alertMessage);
            result.ExecuteResult(context);
        }
    }
 
    public static class ActionResultExtensions
    {
        public static Alerts GetAlerts(this ControllerBase @this)
        {
            return new Alerts(@this.TempData);
        }

        public static Alerts GetAlerts(this HtmlHelper @this)
        {
            return new Alerts(@this.ViewContext.TempData);
        }

        public static MvcHtmlString RenderAlerts(this HtmlHelper @this)
        {
            return @this.Partial("_Alerts", @this.GetAlerts().All);
        }

        public static ActionResult Alert(this ActionResult @this, AlertType type, string message)
        {
            return new ActionResultWithAlert(@this, type, message);
        }



    }
}