namespace mte.Helpers
{
    using System;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using mte.Models;

    public static class MenuExtensions
    {
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string text, string action, string controller, string area = null)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            var currentArea = routeData.DataTokens["area"] as string;

            if ((string.Equals(currentAction, "Details", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(currentAction, "Edit", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(currentAction, "Delete", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(currentAction, "Create", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase)) &&
                string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentArea, area, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("uk-active");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller, new { area }, null).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuItemNav(this HtmlHelper htmlHelper, string text, string action, string controller, string area = null)
        {
            var li = new TagBuilder("li");
            var routeData = htmlHelper.ViewContext.RouteData;
            var currentAction = routeData.GetRequiredString("action");
            var currentController = routeData.GetRequiredString("controller");
            var currentArea = routeData.DataTokens["area"] as string;

            if (string.Equals(currentArea, area, StringComparison.OrdinalIgnoreCase))
            {
                li.AddCssClass("uk-active");
            }
            li.InnerHtml = htmlHelper.ActionLink(text, action, controller, new { area }, null).ToHtmlString();
            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MenuItemNavImg(this HtmlHelper htmlHelper, string imgurl, string action, string controller, string area = null, string alturl = null)
        {
            var activeTag = "";
            var routeData = htmlHelper.ViewContext.RouteData;
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var url = urlHelper.Action(action, controller, new { area });
            var imgUrl = urlHelper.Content(imgurl);

            if (string.Equals(routeData.DataTokens["area"] as string, area, StringComparison.OrdinalIgnoreCase)) { activeTag = "class=\"uk-active\""; };
            var html = string.Format("<li {0}><a href=\"{1}\"><img src=\"{2}\" alt=\"{3}\" width=\"32\" /></a></li>", activeTag, url, imgUrl, alturl);
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString PageLinks(this HtmlHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tag_li = new TagBuilder("li");

            var tag_a_href = "";

            if (pagingInfo.TotalPages > 1)
            {
                result.Append("<ul class=\"uk-pagination uk-pagination-left uk-margin-remove-bottom\">");

                if (pagingInfo.CurrentPage == 1)
                {
                    TagBuilder tag_a = new TagBuilder("span");
                    tag_a.AddCssClass("uk-disabled");
                    tag_a.InnerHtml = "<i uk-icon=\"icon: chevron-left\"></i>";
                    tag_li.InnerHtml = tag_a.ToString();
                }
                else
                {
                    TagBuilder tag_a = new TagBuilder("a");
                    tag_a_href = pageUrl(1);
                    tag_a.InnerHtml = "<i uk-icon=\"icon: chevron-left\"></i>";
                    tag_a.MergeAttribute("href", tag_a_href);
                    tag_li.InnerHtml = tag_a.ToString();
                }

                result.Append(tag_li.ToString());

                for (int i = 1; i <= pagingInfo.TotalPages; i++)
                {
                    tag_li = new TagBuilder("li");
                    if (i == pagingInfo.CurrentPage)
                    {
                        TagBuilder tag_a = new TagBuilder("span");
                        tag_a.InnerHtml = i.ToString();
                        tag_li.AddCssClass("uk-active");
                        tag_li.InnerHtml = tag_a.ToString();
                    }
                    else
                    {
                        TagBuilder tag_a = new TagBuilder("a");
                        tag_a.InnerHtml = i.ToString();
                        tag_a.MergeAttribute("href", pageUrl(i));
                        tag_li.InnerHtml = tag_a.ToString();
                    }
                    result.Append(tag_li.ToString());
                }

                if (pagingInfo.CurrentPage == pagingInfo.TotalPages)
                {
                    tag_a_href = "#";
                    TagBuilder tag_a = new TagBuilder("span");
                    tag_li = new TagBuilder("li");
                    tag_a.InnerHtml = "<i uk-icon=\"icon: chevron-right\"></i>";
                    tag_li.InnerHtml = tag_a.ToString();
                }
                else
                {
                    tag_a_href = pageUrl(pagingInfo.TotalPages);
                    TagBuilder tag_a = new TagBuilder("a");
                    tag_li = new TagBuilder("li");
                    tag_a.InnerHtml = "<i uk-icon=\"icon: chevron-right\"></i>";
                    tag_a.MergeAttribute("href", tag_a_href);
                    tag_li.InnerHtml = tag_a.ToString();
                }

                result.Append(tag_li.ToString());

                result.Append("</ul>");
            }

            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString _ErrorNoData(this HtmlHelper htmlHelper)
        {
            var container = new TagBuilder("div");
            container.AddCssClass("uk-alert uk-alert-danger");
            container.InnerHtml = "Нет данных для отображения";
            return MvcHtmlString.Create(container.ToString());
        }

        public static MvcHtmlString GuideFlexCard(this HtmlHelper htmlHelper, string GuideController, string SName, string SDesc)
        {
            var container = new TagBuilder("div");
            container.AddCssClass("uk-card uk-card-default");

            var header = new TagBuilder("div");
            header.AddCssClass("uk-card-header");
            header.InnerHtml = 
                "<div class=\"uk-grid-small uk-flex-middle\" uk-grid><div class=\"uk-width-auto\"><span uk-icon=\"grid\"></span></div><div class=\"uk-width-expand\">" +
                "<h3 class=\"uk-card-title uk-margin-remove-bottom\">" +
                SName +
                "</h3></div></div>";

            var body = new TagBuilder("div");
            body.AddCssClass("uk-card-body");
            body.InnerHtml = "<p>" + SDesc + "</p>";

            var footer = new TagBuilder("div");
            footer.AddCssClass("uk-card-footer");
            footer.InnerHtml = htmlHelper.ActionLink("Перейти", "Index", "Enterprises", new { area = "Guides" }, new { @class = "uk-button uk-button-text" }).ToString();

            container.InnerHtml = header.ToString() + body.ToString() + footer.ToString();

            return MvcHtmlString.Create("<div>" + container.ToString() + "</div>");
        }
    }
}