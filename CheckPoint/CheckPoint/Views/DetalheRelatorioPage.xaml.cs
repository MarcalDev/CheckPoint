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
        public DetalheRelatorioPage(Relatorio relatorio)
        {
            InitializeComponent();

            _detalheRelatorioViewModel = BindingContext as DetalheRelatorioViewModel;
            
        }
    }
}