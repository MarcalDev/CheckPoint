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
            _homeViewModel = new HomeViewModel(this.Navigation, userObj);
            BindingContext = _homeViewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _homeViewModel.CarregaLista();
        }

        private void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var selecionado = e.ItemData as Relatorio;
            _homeViewModel.RelatorioSelecionado(selecionado);
        }

    }
}