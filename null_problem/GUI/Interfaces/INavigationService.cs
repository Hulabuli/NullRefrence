using Microsoft.UI.Xaml.Controls;

namespace GUI.Interfaces;

public interface INavigationService
{
    void NavigateTo<TViewModel>(Frame frame, object model = null) where TViewModel : IViewModel;
    void NavigateTo(Type type, Frame frame, object model = null);
}