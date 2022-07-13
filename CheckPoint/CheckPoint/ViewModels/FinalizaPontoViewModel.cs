using CheckPointBase.Data.Repository;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CheckPoint.ViewModels
{
    public class FinalizaPontoViewModel : BaseViewModel
    {
        #region -> Propriedades <-
        private PontoRepository _pontoRepository;
        private RelatorioRepository _relatorioRepository;

        private Ponto _ponto;
        private Relatorio _relatorio;
        private Usuario _userObj;
        private string _endereco;
        private double _latitude;
        private double _longitude;

        private Command _finalizaPontoCommand;
        #endregion


        #region -> Construtor <-
        public FinalizaPontoViewModel()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();
        }
        #endregion

        #region -> Encapsulamento <-
        public Ponto Ponto { get { return _ponto; } set { _ponto = value; OnPropertyChanged("Ponto"); } }
        public Relatorio Relatorio { get { return _relatorio; } set { _relatorio = value; OnPropertyChanged("Relatorio"); } }
        public Usuario UserObj { get { return _userObj; } set { _userObj = value; OnPropertyChanged("UserObj"); } }
        public string Endereco { get { return _endereco; } set { _endereco = value; OnPropertyChanged("Endereco"); } }
        public double Latitude { get { return _latitude; } set { _latitude = value; OnPropertyChanged("Latitude"); } }
        public double Longitude { get { return _longitude; } set { _longitude = value; OnPropertyChanged("Longitude"); } }

        #endregion

        #region -> Command's <-
        public Command FinalizaPontoCommand => _finalizaPontoCommand ?? (_finalizaPontoCommand = new Command(FinalizarPonto));
        #endregion

        #region -> Métodos <-

        public void FinalizarPonto()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();

            //var dataFim = new  TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            var dataFim = DateTime.Now;
            _ponto.DataFim = dataFim;
            var p = _pontoRepository.SetPontoFinalizado(_ponto.Id, dataFim, Endereco);

            ContarSaldoEJornada();
        }

        public void ContarSaldoEJornada()
        {

            _relatorio = _relatorioRepository.GetRelatorioById(_ponto.Fk_IdRelatorio);

            //TimeSpan TempoAtual = _relatorio.Saldo;
            //TimeSpan tempoInicial = TimeSpan.Parse(_ponto.DataInicio.ToString("hh:mm:ss"));
            //TimeSpan tempoFinal = TimeSpan.Parse(_ponto.DataFim.ToString("hh:mm:ss"));

            TimeSpan jornadaAtual = _relatorio.TempoJornada;

            TimeSpan tempoInicial = new TimeSpan(_ponto.DataInicio.Hour, _ponto.DataInicio.Minute, _ponto.DataInicio.Second);
            
            TimeSpan tempoFinal = new TimeSpan(_ponto.DataFim.Hour, _ponto.DataFim.Minute, _ponto.DataFim.Second);

            TimeSpan res = tempoFinal.Subtract(tempoInicial);

            res = jornadaAtual.Add(res);

            TimeSpan jornadaTotal;

            if(_userObj.Cargo == "Estagiario")
            {
                jornadaTotal = new TimeSpan(6, 0, 0);
            }
            else
            {
                jornadaTotal = new TimeSpan(8, 0, 0);
            }

            

            TimeSpan saldo = res.Subtract(jornadaTotal);

            _relatorioRepository.UpdateSaldoEJornadaRelatorio(_ponto.Fk_IdRelatorio, saldo, res);

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
        #endregion
    }
}
