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
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly RelatorioRepository _relatorioRepository;
        private readonly PontoRepository _pontoRepository;

        public INavigation Navigation { get; set; }

        
        public ICommand DetalheRelatorioCommand { get; set; }
        public ICommand CadastraPontoCommand { get; set; }

        private Usuario userObj;

        public Usuario UserObj
        {
            get { return userObj; }
            set { userObj = value; PropertyChanged(this, new PropertyChangedEventArgs("UserObj")); }
        }


        private DateTime _dataAtual;

        public DateTime DataAtual
        {
            get { return _dataAtual; }
            set
            {
                _dataAtual = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HoraAtual"));
            }
        }

        private Guid idRelatorio;

        public Guid IdRelatorio
        {
            get { return idRelatorio; }
            set { idRelatorio = value; }
        }

        private Relatorio relatorioItem;

        public Relatorio RelatorioItem
        {
            get { return relatorioItem; }
            set { relatorioItem = value; }
        }

        private List<Relatorio> lista;

        public List<Relatorio> Lista
        {
            get { return lista; }
            set { lista = value; PropertyChanged(this, new PropertyChangedEventArgs("Lista")); }
        }

        public HomeViewModel()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();

            //AdicionarRelatorio();
            
            CarregaHorario();

            CadastraPontoCommand = new Command(CadastraPonto);
        }


        public void CadastraPonto()
        {
            string hojeLocal = _dataAtual.ToString("dd/MM/yyyy");

             Ponto LastPonto = new Ponto();
            LastPonto = _pontoRepository.GetLastPonto(userObj.Id);

            // Se existir um ponto anterior
            if (LastPonto != null)
            {
                string LastPontoDateInicial = LastPonto.DataInicio.ToString("dd/MM/yyyy");

                string LastPontoDateFinal = LastPonto.DataFim.ToString();

                string dataDefault = "01/01/0001 00:00:00";

                //Caso seja o primeiro ponto do dia
                if (hojeLocal != LastPontoDateInicial)
                {
                    App.Current.MainPage.Navigation.ShowPopup(new CadastroPontoPopUp(LastPonto.Fk_IdRelatorio));
                }                

                else
                {
                    if (LastPontoDateFinal != dataDefault)
                    {
                        App.Current.MainPage.Navigation.ShowPopup(new CadastroPontoPopUp(LastPonto.Fk_IdRelatorio));
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


        public  void ListarRelatorios()
        {       
            lista = _relatorioRepository.GetRelatorios(userObj.Id);
            
        }

        public Guid AdicionarRelatorio()
        {
            Guid RelatorioId;
            try
            {
                Relatorio relatorio = new Relatorio();
                relatorio.Data = DataAtual;
                relatorio.Status = "Ativo";
                //relatorio.Fk_IdUsuario = Guid.Parse("f06e6eee-579f-4dda-a167-054661577e1a");
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
            
            Navigation.PushAsync(new DetalheRelatorioPage(relatorio, userObj));

        }

        public void AdicionarPonto(Guid IdRelatorio)
        {
            try
            {
                Ponto ponto = new Ponto();
                ponto.DataInicio = DataAtual;
                ponto.Local = "Endereco";
                ponto.Fk_IdRelatorio = IdRelatorio;
                ponto.Ativo = 1;
                ponto.Alteracao = null;

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


    }
}
