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
    public partial class DetalheRelatorioPage : ContentPage
    {
        DetalheRelatorioViewModel _detalheRelatorioViewModel;
        public DetalheRelatorioPage(Relatorio relatorio, Usuario userObj)
        {
            InitializeComponent();

            _detalheRelatorioViewModel = BindingContext as DetalheRelatorioViewModel;
            _detalheRelatorioViewModel.relatorioItem = relatorio;
            _detalheRelatorioViewModel.UserObj = userObj;
            _detalheRelatorioViewModel.CarregaDados();

            LblDataTitulo.Text = _detalheRelatorioViewModel.relatorioItem.Data.ToString("dd/MM/yyyy");
            ListaPontos.ItemsSource = _detalheRelatorioViewModel.Pontos;

        }

    }
}