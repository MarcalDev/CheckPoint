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
    public class CadastroPontoViewModel : INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly PontoRepository _pontoRepository;
        private readonly RelatorioRepository _relatorioRepository;
        public ICommand CadastraPontoCommand { get; set; }
        public INavigation Navigation { get; set; }

        private Guid idRelatorio;

        public Guid IdRelatorio
        {
            get { return idRelatorio; }
            set { idRelatorio = value; }
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

        private string _endereco;

        public string Endereco
        {
            get { return _endereco; }
            set { _endereco = value; PropertyChanged(this, new PropertyChangedEventArgs("Endereco"));}
        }

        private double latitude;

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));}
        }

        private double longitude;

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));}
        }






        public CadastroPontoViewModel()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();

            CadastraPontoCommand = new Command(VerificaPonto);
            
        }

        //public void cadastrarPonto(Guid idUsuario)
        //{

        //}

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
                //relatorio.Fk_IdUsuario = Guid.Parse("f06e6eee-579f-4dda-a167-054661577e1a");
                relatorio.Ativo = 1;
                relatorio.Id = Guid.NewGuid();
                relatorio.Alteracao = null;

                var p = _relatorioRepository.InsertOrReplaceRelatorio(relatorio);

                RelatorioId = relatorio.Id;


            }
            catch (Exception ex)
            {

                App.Current.MainPage.DisplayAlert("Alerta", ex.Message,"OK");
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

                Position position = new Position(latitude, longitude);
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

    }
}
