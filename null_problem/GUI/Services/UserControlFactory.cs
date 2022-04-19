using GUI.Interfaces;
using Microsoft.UI.Xaml.Controls;

namespace GUI.Services;

internal class UserControlFactory : IUserControlFactory
{
    private readonly Dictionary<Type, Func<UserControl>> _userControlBuilderDictionary;

    public UserControlFactory(Dictionary<Type, Func<UserControl>> userControlBuilderDictionary)
    {
        _userControlBuilderDictionary = userControlBuilderDictionary;
    }

    public TUserControl CreateUserControl<TUserControl, TViewModel>(object model) where TUserControl : UserControl where TViewModel : IViewModel
    {
        return (TUserControl)CreateUserControl<TViewModel>(model);
    }

    public UserControl CreateUserControl<TViewModel>(object model) where TViewModel : IViewModel
    {
        var userControl = _userControlBuilderDictionary[typeof(TViewModel)]();
        var viewModel = (IViewModel)userControl.DataContext;

        viewModel.Initialize(model: model);

        return userControl;
    }
}