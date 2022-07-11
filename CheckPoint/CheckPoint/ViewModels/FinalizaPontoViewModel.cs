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
            set { ponto = value; }
        }

        public FinalizaPontoViewModel()
        {
            _relatorioRepository = new RelatorioRepository();
            _pontoRepository = new PontoRepository();

            FinalizaPontoCommand = new Command();
        }

        public void FinalizarPonto()
        {
            string dataFim = DateTime.Now.ToString("YYYYMMDD");
            var p = _pontoRepository.SetDataFimPonto(ponto.Id, dataFim);
        }
    }
}
