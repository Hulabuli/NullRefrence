using GUI.Interfaces;
using Microsoft.UI.Xaml.Controls;

namespace GUI.Services;

internal class PageService : IPageService, ISessionControlPageService
{
    private readonly Dictionary<Type, Func<Page>> _pageDictionary;

    public PageService(Dictionary<Type, Func<Page>> pageDictionary)
    {
        _pageDictionary = pageDictionary;
    }

    public Page GetPage(Type pageType)
    {
        if (_pageDictionary.ContainsKey(pageType))
        {
            return _pageDictionary[pageType]();
        }
        else
        {
            throw new InvalidOperationException("Unknown page: " + pageType);
        }
    }
}