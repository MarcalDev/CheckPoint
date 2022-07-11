using CheckPoint.ViewModels;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckPoint.Views.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalizaPontoPopUp : Popup
    {
        FinalizaPontoViewModel _finalizaPontoViewModel;
        public FinalizaPontoPopUp(Ponto ponto)
        {
            InitializeComponent();

            _finalizaPontoViewModel = BindingContext as FinalizaPontoViewModel;

            _finalizaPontoViewModel.CarregaDados();
            _finalizaPontoViewModel.PontoId = ponto;

            LblHoraAtual.Text = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}