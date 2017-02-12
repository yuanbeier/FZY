using Abp.Application.Navigation;
using Abp.Localization;
using FZY.Authorization;

namespace FZY.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class FZYNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        L("HomePage"),
                        url: "Home/Index",
                        requiresAuthentication: true
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "About",
                        L("About"),
                        url: "Home/About"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Products",
                        L("Products"),
                        url: "Product/Index"
                      )
                );
                
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FZYConsts.LocalizationSourceName);
        }
    }
}
