using GUI.Interfaces;
using Microsoft.UI.Xaml.Controls;

namespace GUI.Services;

internal class NavigationService : INavigationService, ISessionControlNavigationService
{
    private readonly IPageService _pageService;

    public NavigationService(IPageService pageService)
    {
        _pageService = pageService;
    }

    public void NavigateTo<TViewModel>(Frame frame, object model = null) where TViewModel : IViewModel
    {
        NavigateTo(typeof(TViewModel), frame, model);
    }

    public void NavigateTo(Type type, Frame frame, object model = null)
    {
        Page page = _pageService.GetPage(type);
        var viewModel = (IViewModel)page.DataContext;

        viewModel.Initialize(model: model);

        frame.Content = page;
    }
}