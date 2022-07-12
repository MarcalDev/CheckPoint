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
    public class FinalizaPontoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly PontoRepository _pontoRepository;
        private readonly RelatorioRepository _relatorioRepository;

        public ICommand FinalizaPontoCommand { get; set; }

        private Ponto ponto;

        public Ponto Ponto
        {
            get { return ponto; }
            set { ponto = value; PropertyChanged(this, new PropertyChangedEventArgs("Ponto")); }
        }

        private Relatorio relatorio;

        public Relatorio Relatorio
        {
            get { return relatorio; }
            set { relatorio = value; PropertyChanged(this, new PropertyChangedEventArgs("Relatorio")); }
        }

        public FinalizaPontoViewModel()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();

            FinalizaPontoCommand = new Command(FinalizarPonto);
        }

        public void FinalizarPonto()
        {
            var dataFim = DateTime.Now;
            var p = _pontoRepository.SetDataFimPonto(ponto.Id, dataFim);

            ContarSaldo();
        }

        public void ContarSaldo()
        {         

            relatorio = _relatorioRepository.GetRelatorioById(ponto.Fk_IdRelatorio);

            TimeSpan TempoAtual = relatorio.Saldo;
            TimeSpan tempoInicial = TimeSpan.Parse(ponto.DataInicio.ToString("hh:mm:ss"));
            TimeSpan tempoFinal = TimeSpan.Parse(ponto.DataFim.ToString("hh:mm:ss"));

            TimeSpan saldo = TempoAtual + (tempoFinal - tempoInicial);

            _relatorioRepository.UpdateSaldoRelatorio(ponto.Fk_IdRelatorio, saldo);

            
        }
    }
}
