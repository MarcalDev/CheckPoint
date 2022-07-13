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
    public class FinalizaPontoViewModel : BaseViewModel
    {
        #region -> Propriedades <-
        private PontoRepository _pontoRepository;
        private RelatorioRepository _relatorioRepository;

        private Ponto _ponto;
        private Relatorio _relatorio;

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
        #endregion

        #region -> Command's <-
        public Command FinalizaPontoCommand => _finalizaPontoCommand ?? (_finalizaPontoCommand = new Command(FinalizarPonto));
        #endregion

        #region -> Métodos <-

        public void FinalizarPonto()
        {
            var dataFim = DateTime.Now;
            var p = _pontoRepository.SetDataFimPonto(_ponto.Id, dataFim);

            ContarSaldo();
        }

        public void ContarSaldo()
        {

            _relatorio = _relatorioRepository.GetRelatorioById(_ponto.Fk_IdRelatorio);

            TimeSpan TempoAtual = _relatorio.Saldo;
            TimeSpan tempoInicial = TimeSpan.Parse(_ponto.DataInicio.ToString("hh:mm:ss"));
            TimeSpan tempoFinal = TimeSpan.Parse(_ponto.DataFim.ToString("hh:mm:ss"));

            TimeSpan saldo = TempoAtual + (tempoFinal - tempoInicial);

            _relatorioRepository.UpdateSaldoRelatorio(_ponto.Fk_IdRelatorio, saldo);


        } 
        #endregion
    }
}
