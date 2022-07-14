using CheckPoint.Views;
using CheckPointBase.Data.Repository;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using CheckPoint.Views.PopUp;

namespace CheckPoint.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region -> Propriedades <-
        public INavigation _navigation;

        private RelatorioRepository _relatorioRepository;
        private PontoRepository _pontoRepository;
        
        public Command _cadastraPontoCommand;

        private Usuario _userObj;
        private Ponto _ultimoPonto;
        private DateTime _dataAtual;
        private Guid _idRelatorio;
        private Relatorio _relatorioItem;
        private List<Relatorio> _listaRelatorios;
        #endregion


        #region -> Construtor <-
        public HomeViewModel(INavigation navigationPage, Usuario userObj)
        {
            _navigation = navigationPage;
            _userObj = userObj;
            _dataAtual = DateTime.Now;

            CarregaDados();

           

        }
        #endregion

        #region -> Encapsulamento <-

        public Usuario UserObj { get { return _userObj; } set { _userObj = value; OnPropertyChanged("UserObj"); } }
        public Ponto UltimoPonto { get { return _ultimoPonto; } set { _ultimoPonto = value; OnPropertyChanged("UltimoPonto"); } }
        public DateTime DataAtual { get { return _dataAtual; } set { _dataAtual = value; OnPropertyChanged("DataAtual"); } }
        public Guid IdRelatorio { get { return _idRelatorio; } set { _idRelatorio = value; OnPropertyChanged("IdRelatorio"); } }
        public Relatorio RelatorioItem { get { return _relatorioItem; } set { _relatorioItem = value; OnPropertyChanged("RelatorioItem"); } }
        public List<Relatorio> ListaRelatorios { get { return _listaRelatorios; } set { _listaRelatorios = value; OnPropertyChanged("Lista"); } }

        #endregion

        #region -> Command's <-

        public Command CadastraPontoCommand => _cadastraPontoCommand ?? (_cadastraPontoCommand = new Command(VerificaPonto));

        #endregion

        #region -> Métodos <-
        
        // Instanciação dos repositórios
        void CarregaDados()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();
            ListaRelatorios = new List<Relatorio>();


            CarregaLista();
        }
                

        // Limpeza, carregamento e refresh da lista
        public void CarregaLista()
        {
            ListaRelatorios.Clear();
            ListaRelatorios = _relatorioRepository.GetRelatorios(_userObj.Id);
            OnPropertyChanged("ListaRelatorios");
        }        


        // Navegação para tela de detalhes apartir do tap na listView
        public void RelatorioSelecionado(Relatorio relatorioSelecionado)
        {
            _relatorioItem = relatorioSelecionado;
            OnPropertyChanged("RelatorioItem");

            _navigation.PushAsync(new DetalheRelatorioPage(_relatorioItem, _userObj));
        }

        // Verifica se é necessário navegar para a tela de registro ou finalização do ponto
        public void VerificaPonto()
        {
            
            _ultimoPonto = _pontoRepository.GetLastPonto(_userObj.Id);

            // Se existir um ponto anterior

            if (_ultimoPonto != null)
            {
                if(!_ultimoPonto.IsFinalizado)
                {
                    _navigation.ShowPopup(new FinalizaPontoPopUp(_navigation, _ultimoPonto, _userObj));
                }

                else
                {
                    _navigation.ShowPopup(new CadastroPontoPopUp(_navigation, _userObj, false));
                }              

            }
            else
            {
                _navigation.ShowPopup(new CadastroPontoPopUp(_navigation, _userObj, true));
            }            
        }


        
        #endregion


    }
}
