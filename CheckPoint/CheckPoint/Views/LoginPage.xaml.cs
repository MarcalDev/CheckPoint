using CheckPoint.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckPoint.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel _loginViewModel;
        
        public LoginPage()
        {
            InitializeComponent();

            _loginViewModel = BindingContext as LoginViewModel;
            _loginViewModel.Navigation = Navigation;
        }
    }
}