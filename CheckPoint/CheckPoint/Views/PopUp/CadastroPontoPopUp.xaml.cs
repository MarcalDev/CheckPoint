using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using CheckPoint.ViewModels;
using Xamarin.Forms.Maps;

namespace CheckPoint.Views.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPontoPopUp : Popup
    {
        CadastroPontoViewModel _cadastroPontoViewModel;
        public CadastroPontoPopUp(Guid IdRelatorio)
        {
            InitializeComponent();

            var mapa = new Map();

            MapContent.Children.Add(mapa);

            _cadastroPontoViewModel = BindingContext as CadastroPontoViewModel;
           
            _cadastroPontoViewModel.CarregaDados();

            LblHoraAtual.Text = DateTime.Now.ToString("hh:mm:ss");

            _cadastroPontoViewModel.IdRelatorio = IdRelatorio;
        }
    }
}