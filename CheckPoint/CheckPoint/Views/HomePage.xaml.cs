using CheckPoint.ViewModels;
using CheckPointBase.Models;
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
        public HomePage(Usuario userObj)
        {
            InitializeComponent();

            _homeViewModel = BindingContext as HomeViewModel;
            _homeViewModel.Navigation = Navigation;
            _homeViewModel.UserObj = userObj;

            //ListaRelatorios.ItemsSource = _homeViewModel.lista;
        }

        

        private void ListaRelatorios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as Relatorio;

                _homeViewModel.RelatorioItem = item;

                _homeViewModel.NavegaDetalheRelatorio(item);
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta!", ex.Message, "OK");
            }

        }
    }
}