using CheckPointBase.Data.Repository;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CheckPoint.ViewModels
{
    public class CadastroPontoViewModel : BaseViewModel
    {
        #region -> Propriedades <-
        public INavigation _navigation;

        private PontoRepository _pontoRepository;
        private RelatorioRepository _relatorioRepository;

        public Command _cadastraPontoCommand;        

        private Usuario _userObj;
        private Guid _idRelatorio;
        private DateTime _dataAtual;
        private string _endereco;
        private double _latitude;
        private double _longitude;
        #endregion


        #region -> Construtor <-
        public CadastroPontoViewModel()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();

        }
        #endregion
        #region -> Encapsulamento <-

        public Usuario UserObj { get { return _userObj; } set { _userObj = value; OnPropertyChanged("UserObj"); } }
        public Guid IdRelatorio { get { return _idRelatorio; } set { _idRelatorio = value; OnPropertyChanged("IdRelatorio"); } }
        public DateTime DataAtual { get { return _dataAtual; } set { _dataAtual = value; OnPropertyChanged("DataAtual"); } }
        public string Endereco { get { return _endereco; } set { _endereco = value; OnPropertyChanged("Endereco"); } }
        public double Latitude { get { return _latitude; } set { _latitude = value; OnPropertyChanged("Latitude"); } }
        public double Longitude { get { return _longitude; } set { _longitude = value; OnPropertyChanged("Longitude"); } }

        #endregion


        #region -> Command's <-
        public Command CadastraPontoCommand => _cadastraPontoCommand ?? (_cadastraPontoCommand = new Command(VerificaPonto));

        #endregion


        #region -> Métodos <-
        public void VerificaPonto()
        {
            try
            {
                AdicionarPonto(IdRelatorio);


            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "OK");
            }


        }


        public void AdicionarPonto(Guid IdRelatorio)
        {
            try
            {
                Ponto ponto = new Ponto();
                ponto.DataInicio = DataAtual;
                ponto.Local = _endereco;
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


        public void CarregaHorario()
        {
            _dataAtual = DateTime.Now;
        }

        public void CarregaDados()
        {
            CarregaHorario();
        }

        public async Task GeocodEnderecoAsync()
        {

            try
            {

                Geocoder geoCoder = new Geocoder();

                Position position = new Position(_latitude, _longitude);
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                string endereco = possibleAddresses.FirstOrDefault();

                _endereco = endereco;
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Alerta!", ex.Message, "OK");
            }


        }

        //public void PuxaGeo()
        //{
        //    GeocodEnderecoAsync().GetAwaiter();
        //    _endereco = GeocodEnderecoAsync().GetAwaiter().GetResult();
        //} 
        #endregion

    }
}
