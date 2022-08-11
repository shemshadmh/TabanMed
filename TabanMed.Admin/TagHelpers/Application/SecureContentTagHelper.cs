using Application;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TabanMed.Admin.TagHelpers.Application
{
    [HtmlTargetElement("admin-secure-content")]
    public class AdminSecureContentTagHelper : TagHelper
    {
        //private readonly IPermissionRepository permissionService;

        //public AdminSecureContentTagHelper(IPermissionRepository permissionService)
        //{
        //    this.permissionService = permissionService;
        //}

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; } = null!;

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; } = null!;

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; } = null!;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            return;
            try
            {
                output.TagName = null;
                var user = ViewContext.HttpContext.User;

                //اگر کاربر وارد حساب خود نشده بود
                if(!user.Identity!.IsAuthenticated)
                {
                    output.SuppressOutput();
                    return;
                }

                if(user.IsInRole(AppConstants.AdminRole))
                    return;

                var actionFullName = $"Admin:{Controller}:{Action}";

                //if(await permissionService.HasAccess(actionFullName, user.GetUserRoles()))
                //    return;
                if(true)
                    return;

                output.SuppressOutput();
            }
            catch(Exception)
            {
                output.TagName = null;
                output.SuppressOutput();
            }
        }
    }
}
