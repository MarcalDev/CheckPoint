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
    public partial class HomePage : ContentPage
    {
        HomeViewModel _homeViewModel;
        public HomePage()
        {
            InitializeComponent();

            _homeViewModel = BindingContext as HomeViewModel;
            _homeViewModel.Navigation = Navigation;

            Lista01.ItemsSource = _homeViewModel.lista;
        }
    }
}