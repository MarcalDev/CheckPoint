using CheckPointBase.Data.Repository;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CheckPoint.ViewModels
{
    public class DetalheRelatorioViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private readonly PontoRepository _pontoRepository;

        private Relatorio relatorioItem;

        public Relatorio RelatorioItem
        {
            get { return relatorioItem; }
            set { relatorioItem = value; 
                  PropertyChanged(this, new PropertyChangedEventArgs("RelatorioItem")); }
        }


        private Guid idRelatorio;

        public Guid IdRelatorio
        {
            get { return idRelatorio; }
            set { idRelatorio = value; }
        }

        private List<Ponto> pontos;

        public List<Ponto> Pontos
        {
            get { return pontos; }
            set { pontos = value; }
        }

        public DetalheRelatorioViewModel()
        {
            _pontoRepository = new PontoRepository();


            ListarPontos();


        }

        public void ListarPontos()
        {
            pontos = _pontoRepository.GetPontosByRelatorio(RelatorioItem.Id);
        }

        // Uso apenas para dev
        public void AdicionarPonto()
        {

        }


    }
}
