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

        public Relatorio relatorioItem;  

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
            set { 
                pontos = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Pontos"));
            }
        }

        public DetalheRelatorioViewModel()
        {
            _pontoRepository = new PontoRepository();

            //AdicionarPonto();

            


        }

        public void ListarPontos()
        {
            pontos = _pontoRepository.GetPontosByRelatorio(relatorioItem.Id);
        }

        // Uso apenas para dev
        public void AdicionarPonto()
        {
            Ponto ponto = new Ponto();
            ponto.DataInicio = new DateTime(2022, 07, 07, 15, 16, 10);
            ponto.DataFim = new DateTime(2022, 07, 07, 15, 16, 10);
            ponto.Local = "Endereco";
            //ponto.Fk_IdRelatorio = Guid.Parse("2980b26f-8fb6-4b6e-b761-6180c926e248");
            ponto.Fk_IdRelatorio = relatorioItem.Id;
            ponto.Ativo = 1;
            ponto.Alteracao = null;

            var p = _pontoRepository.InsertOrReplacePonto(ponto);
        }

        public void CarregaDados()
        {
            //AdicionarPonto();

            ListarPontos();
        }



    }
}
