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
    public class CadastroUsuarioViewModel : INotifyPropertyChanged
    {
        private readonly UsuarioRepository _usuarioRepository;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public INavigation Navigation { get; set; }
        public ICommand PaginaLoginCommand { get; set; }
        public ICommand CadastroUsuarioCommand { get; set; }

        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; PropertyChanged(this, new PropertyChangedEventArgs("Nome")); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; PropertyChanged(this, new PropertyChangedEventArgs("Email")); }
        }

        private string senha;

        public string Senha
        {
            get { return senha; }
            set { senha = value; PropertyChanged(this, new PropertyChangedEventArgs("Senha")); }
        }



        public CadastroUsuarioViewModel()
        {
            PaginaLoginCommand = new Command(NavegaPaginaLogin);

            CadastroUsuarioCommand = new Command(CadastrarUsuario);

            _usuarioRepository = new UsuarioRepository();
        }

        public void NavegaPaginaLogin()
        {
            Navigation.PushAsync(new LoginPage());           

        }

        public void CadastrarUsuario()
        {
            Usuario user = new Usuario();
            user.NomeEmpresa = "";
            user.Alteracao = null;
            user.Ativo = 1;
            user.NomeUsuario = Nome;
            user.Email = Email;
            user.Senha = Senha;

            var p = _usuarioRepository.InsertOrReplaceUsuario(user);

            if (p)
            {
                NavegaPaginaLogin();
            }
            
        }
    }
}
