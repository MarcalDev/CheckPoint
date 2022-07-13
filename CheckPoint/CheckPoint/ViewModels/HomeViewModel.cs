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
        private DateTime _dataAtual;
        private Guid _idRelatorio;
        private Relatorio _relatorioItem;
        private List<Relatorio> _listaRelatorios;
        #endregion


        #region -> Construtor <-
        public HomeViewModel()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();

            //AdicionarRelatorio();

            CarregaHorario();

        }
        #endregion

        #region -> Encapsulamento <-

        public Usuario UserObj { get { return _userObj; } set { _userObj = value; OnPropertyChanged("UserObj"); } }
        public DateTime DataAtual { get { return _dataAtual; } set { _dataAtual = value; OnPropertyChanged("DataAtual"); } }
        public Guid IdRelatorio { get { return _idRelatorio; } set { _idRelatorio = value; OnPropertyChanged("IdRelatorio"); } }
        public Relatorio RelatorioItem { get { return _relatorioItem; } set { _relatorioItem = value; OnPropertyChanged("RelatorioItem"); } }
        public List<Relatorio> ListaRelatorios { get { return _listaRelatorios; } set { _listaRelatorios = value; OnPropertyChanged("Lista"); } }

        #endregion

        #region -> Command's <-

        public Command CadastraPontoCommand => _cadastraPontoCommand ?? (_cadastraPontoCommand = new Command(CadastraPonto));

        #endregion

        #region -> Métodos <-
        public void CadastraPonto()
        {
            string hojeLocal = _dataAtual.ToString("dd/MM/yyyy");

            Ponto LastPonto = new Ponto();
            LastPonto = _pontoRepository.GetLastPonto(_userObj.Id);

            // Se existir um ponto anterior
            if (LastPonto != null)
            {
                string LastPontoDateInicial = LastPonto.DataInicio.ToString("dd/MM/yyyy");

                string LastPontoDateFinal = LastPonto.DataFim.ToString();

                string dataDefault = "01/01/0001 00:00:00";

                //Caso seja o primeiro ponto do dia
                if (hojeLocal != LastPontoDateInicial)
                {
                    App.Current.MainPage.Navigation.ShowPopup(new CadastroPontoPopUp(LastPonto.Fk_IdRelatorio, _userObj));
                }

                else
                {
                    if (LastPonto.IsFinalizado)
                    {
                        App.Current.MainPage.Navigation.ShowPopup(new CadastroPontoPopUp(LastPonto.Fk_IdRelatorio, _userObj));
                    }

                    else
                    {
                        App.Current.MainPage.Navigation.ShowPopup(new FinalizaPontoPopUp(LastPonto));
                    }
                }

                //Caso não seja o primeiro ponto do dia

                // Caso o último ponto não tenha sido finalizado

            }
            else
            {
                Guid IdRelatorio = AdicionarRelatorio();

                AdicionarPonto(IdRelatorio);


            }


        }


        public void ListarRelatorios()
        {
            _listaRelatorios = _relatorioRepository.GetRelatorios(_userObj.Id);

        }

        public Guid AdicionarRelatorio()
        {
            Guid RelatorioId;
            try
            {
                Relatorio relatorio = new Relatorio();
                relatorio.Data = DataAtual;
                relatorio.Status = "Ativo";
                relatorio.Fk_IdUsuario = _userObj.Id;
                relatorio.Ativo = 1;
                relatorio.Id = Guid.NewGuid();
                relatorio.Alteracao = null;

                var p = _relatorioRepository.InsertOrReplaceRelatorio(relatorio);

                RelatorioId = relatorio.Id;

            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "OK");
            }

            return RelatorioId;
        }



        public void NavegaDetalheRelatorio(Relatorio relatorio)
        {

            _navigation.PushAsync(new DetalheRelatorioPage(relatorio, _userObj));

        }

        public void AdicionarPonto(Guid IdRelatorio)
        {
            try
            {
                Ponto ponto = new Ponto();
                ponto.DataInicio = DataAtual;
                ponto.LocalInicial = "Endereco";
                ponto.Fk_IdRelatorio = IdRelatorio;
                ponto.Ativo = 1;
                ponto.Alteracao = null;
                ponto.IsFinalizado = false;

                var p = _pontoRepository.InsertOrReplacePonto(ponto);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "OK");
            }
        }

        public void CarregaHorario()
        {
            _dataAtual = DateTime.Now;
        } 
        #endregion


    }
}
