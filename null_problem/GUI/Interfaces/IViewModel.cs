using Microsoft.UI.Xaml.Controls;

namespace GUI.Interfaces;

public interface IViewModel
{
    void Initialize(object model = null);
}