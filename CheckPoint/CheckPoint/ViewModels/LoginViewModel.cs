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
    public class LoginViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly UsuarioRepository _usuarioRepository;
        public INavigation Navigation { get; set; }

        private Usuario userObj;

        public Usuario UserObj
        {
            get { return userObj; }
            set { userObj = value; }
        }

        private string email { get; set; }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string senha { get; set; }
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Senha"));
            }
        }

        public ICommand LoginCommand { protected set; get; }
        public ICommand PaginaCadastroCommand { protected set; get; }


        public LoginViewModel()
        {
            //var navPage = new NavigationPage(new HomePage());
            LoginCommand = new Command(Logar);

            PaginaCadastroCommand = new Command(NavegaPaginaCadastro);

            _usuarioRepository = new UsuarioRepository();
            Email = "joao@gmail.com";
            Senha = "12345";
        }

        public void Logar()
        {
            
            Usuario user = _usuarioRepository.GetUsuarioByLogin(Email,Senha);
                       

            if (user != null)
            {
                // usuário existe
                userObj = user;
                App.Current.MainPage = new NavigationPage(new HomePage(userObj));
                
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Alerta", "Usuário não encontrado", "OK");
                // Usuário não existe
            }

            

        }

        public void NavegaPaginaCadastro()
        {

            Navigation.PushAsync(new CadastroUsuarioPage());
                        
        }


    }
}
