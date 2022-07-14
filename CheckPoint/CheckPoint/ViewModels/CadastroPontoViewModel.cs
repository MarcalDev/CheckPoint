using CheckPoint.Views.PopUp;
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
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;

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
        private Ponto _ultimoPonto;
        private Guid _idRelatorio;
        private DateTime _dataAtual;
        private string _endereco;
        private double _latitude;
        private double _longitude;
        private bool _primeiro;
        private double _distancia;
        #endregion


        #region -> Construtor <-
        public CadastroPontoViewModel(INavigation navigationPage, Usuario userObj, bool primeiro)
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();

            _navigation = navigationPage;
            _userObj = userObj;
            _primeiro = primeiro;
            _dataAtual = DateTime.Now;
        }
        #endregion
        #region -> Encapsulamento <-

        public Usuario UserObj { get { return _userObj; } set { _userObj = value; OnPropertyChanged("UserObj"); } }
        public Ponto UltimoPonto { get { return _ultimoPonto; } set { _ultimoPonto = value; OnPropertyChanged("UltimoPonto"); } }
        public Guid IdRelatorio { get { return _idRelatorio; } set { _idRelatorio = value; OnPropertyChanged("IdRelatorio"); } }
        public DateTime DataAtual { get { return _dataAtual; } set { _dataAtual = value; OnPropertyChanged("DataAtual"); } }
        public string Endereco { get { return _endereco; } set { _endereco = value; OnPropertyChanged("Endereco"); } }
        public double Latitude { get { return _latitude; } set { _latitude = value; OnPropertyChanged("Latitude"); } }
        public double Longitude { get { return _longitude; } set { _longitude = value; OnPropertyChanged("Longitude"); } }
        public bool Primeiro { get { return _primeiro; } set { _primeiro = value; OnPropertyChanged("Primeiro"); } }
        public double Distancia { get { return _distancia; } set { _distancia = value; OnPropertyChanged("Distancia"); } }
        #endregion


        #region -> Command's <-
        public Command CadastraPontoCommand => _cadastraPontoCommand ?? (_cadastraPontoCommand = new Command(VerificaPonto));

        #endregion


        #region -> Métodos <-        

        // Verifica se é necessário Gerar relatório
        public void VerificaPonto()
        {
            try
            {
                _ultimoPonto = _pontoRepository.GetLastPonto(_userObj.Id);

                
                //var GuidNulo = new Guid("00000000-0000-0000-0000-000000000000");

                if (_primeiro)
                {
                    _idRelatorio = AdicionarRelatorio();                    
                    
                }
                else
                {
                    _idRelatorio = _ultimoPonto.Fk_IdRelatorio;

                }

                if (GetDistancia())
                {
                    AdicionarPonto(_idRelatorio);
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Alerta", "Voce está à uma distância acima de 200m", "OK");
                }
                
                
                               

            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "OK");
            }

        }

        // Adiciona Relatório
        public Guid AdicionarRelatorio()
        {
            Guid idRelatorio;
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

                idRelatorio = relatorio.Id;
            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "OK");
            }

            return idRelatorio;
        }


        // Adiciona ponto
        public void AdicionarPonto(Guid IdRelatorio)
        {
            try
            {
                Ponto ponto = new Ponto();
                ponto.DataInicio = DataAtual;
                ponto.LocalInicial = _endereco;
                ponto.Fk_IdRelatorio = _idRelatorio;
                ponto.Fk_IdUsuario = _userObj.Id;
                ponto.IsFinalizado = false;
                ponto.Ativo = 1;                

                _pontoRepository.InsertOrReplacePonto(ponto);

            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
       
        
        // Conversão de coordenadas para endereço
        public async Task GeocodEnderecoAsync()
        {
            try
            {
                Geocoder geoCoder = new Geocoder();

                Position position = new Position(_latitude, _longitude);
                IEnumerable<string> possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                string endereco = possibleAddresses.FirstOrDefault();

                _endereco = endereco;
                OnPropertyChanged("Endereco");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta!", ex.Message, "OK");
            }

        }

        public bool GetDistancia()
        {
            Location gamaLocation = new Location(-22.332253255233407, -49.05345442883518);
            Location atualLocation = new Location(_latitude, _longitude);

            double kilometers = Location.CalculateDistance(gamaLocation, atualLocation, DistanceUnits.Kilometers);

            double meters = kilometers * 100;

            double distanciaMax = 200;

            if (meters > distanciaMax)
            {
                return false;
            }
            else
            {
                return true;
            }            
        }
       
        #endregion

    }
}
