using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web;

namespace USiteBuilderContrib.Umbraco
{
    public static class GridTemplateExtensions
    {
        public static IHtmlString RenderGrid(this UmbracoHelper helper, object propertyValue, string framework = "bootstrap3")
        {
            var view = "Grid/" + framework;
            return new HtmlString(RenderPartialViewToString(view, propertyValue));
        }

        private static string RenderPartialViewToString(string viewName, object model)
        {
            using (var sw = new StringWriter())
            {
                HttpRequestBase httpRequest = UmbracoContext.Current.HttpContext.Request;
                var cc = new ControllerContext
                {
                    RequestContext = new RequestContext(UmbracoContext.Current.HttpContext,
                        new RouteData { Route = RouteTable.Routes["Umbraco_default"] })
                };
                cc.RequestContext.RouteData.Values.Add("action", httpRequest.RequestContext.RouteData.Values["action"]);
                cc.RequestContext.RouteData.Values.Add("controller", httpRequest.RequestContext.RouteData.Values["controller"]);

                var partialView = ViewEngines.Engines.FindPartialView(cc, viewName);
                var tempData = new TempDataDictionary();
                var viewData = new ViewDataDictionary
                {
                    Model = model
                };

                var innerViewContext = new ViewContext(cc, partialView.View, viewData, tempData, sw);
                partialView.View.Render(innerViewContext, sw);
                partialView.ViewEngine.ReleaseView(cc, partialView.View);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
