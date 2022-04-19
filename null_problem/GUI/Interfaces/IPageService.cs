using Microsoft.UI.Xaml.Controls;

namespace GUI.Interfaces;

internal interface IPageService
{
    Page GetPage(Type pageType);
}