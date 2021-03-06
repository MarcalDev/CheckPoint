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
    public partial class CadastroUsuarioPage : ContentPage
    {
        CadastroUsuarioViewModel _cadastroUsuarioViewModel;
        public CadastroUsuarioPage()
        {
            InitializeComponent();

            _cadastroUsuarioViewModel = BindingContext as CadastroUsuarioViewModel;
            _cadastroUsuarioViewModel._navigation = Navigation;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Check.IsChecked)
            {
                _cadastroUsuarioViewModel.Cargo = "Estagiario";
            }
            else
            {
                _cadastroUsuarioViewModel.Cargo = "Efetivado";
            }
        }
    }
}