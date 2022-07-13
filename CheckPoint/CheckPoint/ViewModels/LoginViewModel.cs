using System;
using CheckPointBase.Models;
using CheckPoint.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CheckPointBase.Data.Repository;

namespace CheckPoint.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region -> Propriedades <-
        public INavigation _navigation;

        private UsuarioRepository _usuarioRepository;

        private Usuario _userObj;
        private string _email;
        private string _senha;

        private Command _loginCommand;
        private Command _paginaCadastroCommand;
        #endregion



        #region -> Construtor <-
        public LoginViewModel()
        {           
            _usuarioRepository = new UsuarioRepository();


            Email = "joao@gmail.com";
            Senha = "12345";
        }
        #endregion

        #region -> Encapsulamento <-
        public Usuario UserObj { get { return _userObj; } set { _userObj = value; OnPropertyChanged("UserObj"); } }

        public string Email { get { return _email; } set { _email = value; OnPropertyChanged("Email"); } }

        public string Senha { get { return _senha; } set { _senha = value; OnPropertyChanged("Senha"); } }
        #endregion


        #region -> Command's <-
        public Command LoginCommand => _loginCommand ?? (_loginCommand = new Command(Logar));

        public Command PaginaCadastroCommand => _paginaCadastroCommand ?? (_paginaCadastroCommand = new Command(NavegaPaginaCadastro));

        #endregion



        #region -> Métodos <-
        public void Logar()
        {

            Usuario user = _usuarioRepository.GetUsuarioByLogin(Email, Senha);


            if (user != null)
            {
                // usuário existe
                _userObj = user;
                App.Current.MainPage = new NavigationPage(new HomePage(_userObj));

            }
            else
            {
                App.Current.MainPage.DisplayAlert("Alerta", "Usuário não encontrado", "OK");
                // Usuário não existe
            }



        }

        public void NavegaPaginaCadastro()
        {

            _navigation.PushAsync(new CadastroUsuarioPage());

        } 
        #endregion


    }
}
