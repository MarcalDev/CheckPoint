using CheckPoint.Views;
using CheckPointBase.Data.Repository;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckPoint.ViewModels
{
    public class CadastroUsuarioViewModel : BaseViewModel
    {

        #region -> Propriedades <-
        public INavigation _navigation;

        private UsuarioRepository _usuarioRepository;

        private string _nome;
        private string _email;
        private string _senha;
        private string _empresa;
        private string _cargo;

        private Command _paginaLoginCommand;
        private Command _cadastroUsuarioCommand;   

        #endregion        
        

        #region -> Construtor <-
        public CadastroUsuarioViewModel()
        {
            _usuarioRepository = new UsuarioRepository();
        }
        #endregion


        #region -> Encapsulamento <-
        public string Nome { get { return _nome; } set { _nome = value; OnPropertyChanged("Nome"); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged("Email"); } }
        public string Senha { get { return _senha; } set { _senha = value; OnPropertyChanged("Senha"); } } 
        public string Empresa { get { return _empresa; } set { _empresa = value; OnPropertyChanged("Empresa"); } } 
        public string Cargo { get { return _cargo; } set { _cargo = value; OnPropertyChanged("Cargo"); } } 
        #endregion


        #region -> Command's <-
        public Command PaginaLoginCommand => _paginaLoginCommand ?? (_paginaLoginCommand = new Command(NavegaPaginaLogin));
        public Command CadastroUsuarioCommand => _cadastroUsuarioCommand ?? (_cadastroUsuarioCommand = new Command(CadastrarUsuario));

        #endregion

        #region -> Métodos <-
        public void NavegaPaginaLogin()
        {
            _navigation.PushAsync(new LoginPage());

        }

        public void CadastrarUsuario()
        {

            Usuario user = new Usuario();
            user.NomeEmpresa = "";
            user.Alteracao = null;
            user.Ativo = 1;
            user.NomeUsuario = _nome;
            user.Email = _email;
            user.Senha = _senha;
            user.Cargo = _cargo;
            user.NomeEmpresa = _empresa;

            var p = _usuarioRepository.InsertOrReplaceUsuario(user);

            if (p)
            {
                NavegaPaginaLogin();
            }

        } 
        #endregion
    }
}
