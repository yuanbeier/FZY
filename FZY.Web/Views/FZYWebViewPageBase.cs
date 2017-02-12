using Abp.Web.Mvc.Views;

namespace FZY.Web.Views
{
    public abstract class FZYWebViewPageBase : FZYWebViewPageBase<dynamic>
    {

    }

    public abstract class FZYWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected FZYWebViewPageBase()
        {
            LocalizationSourceName = FZYConsts.LocalizationSourceName;
        }
    }
}