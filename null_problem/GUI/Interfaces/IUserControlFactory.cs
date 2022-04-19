using Microsoft.UI.Xaml.Controls;

namespace GUI.Interfaces;

public interface IUserControlFactory
{
    TUserControl CreateUserControl<TUserControl, TViewModel>(object model)
        where TUserControl : UserControl
        where TViewModel : IViewModel;

    UserControl CreateUserControl<TViewModel>(object model)
        where TViewModel : IViewModel;
}