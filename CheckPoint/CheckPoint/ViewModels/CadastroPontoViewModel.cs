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
    public class CadastroPontoViewModel : INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly PontoRepository _pontoRepository;
        private readonly RelatorioRepository _relatorioRepository;
        public ICommand CadastraPontoCommand { get; set; }

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
                string hojeLocal = _dataAtual.ToString("dd/MM/yyyy");

                Ponto LastPonto = new Ponto();
                LastPonto = _pontoRepository.GetLastPonto();

                if(LastPonto != null)
                {
                    string LastPontoDate = LastPonto.DataInicio.ToString("dd/MM/yyyy");

                    //Caso seja o primeiro ponto do dia
                    if (hojeLocal != LastPontoDate)
                    {
                        Guid IdRelatorio = AdicionarRelatorio();

                        AdicionarPonto(IdRelatorio);

                    }

                    //Caso não seja o primeiro ponto do dia
                    else
                    {
                        AdicionarPonto(LastPonto.Fk_IdRelatorio);
                    }
                }
                else
                {
                    Guid IdRelatorio = AdicionarRelatorio();

                    AdicionarPonto(IdRelatorio);
                }
                
                
                
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

    }
}
