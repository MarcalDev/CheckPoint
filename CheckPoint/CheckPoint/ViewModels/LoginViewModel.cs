using System;
using CheckPointBase.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CheckPoint.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

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


        public LoginViewModel()
        {
            LoginCommand = new Command(Logar);

        }

        public void Logar()
        {

            var objUser = GetUsuarioByEmail(Email);
            string emailBd = objUser.Email.ToString();
            string senhaBd = objUser.Senha.ToString();
            bool certo = false;
            if (senha == senhaBd)
            {
                certo = true;
            }
            else
            {
                certo = false;
            }
        }


        public List<Usuario> GetUsuarios()
        {
            return new List<Usuario>()
            {
                new Usuario(){ Id = 1, Email = "joao@email.com", Senha = "12345" },
                new Usuario(){ Id = 2, Email = "pedro@email.com", Senha = "54321" },
            };
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            List<Usuario> _listaUsuarios = GetUsuarios();
            var user = _listaUsuarios.Find(x => x.Email == email);

            return user;
        }
    }
}
