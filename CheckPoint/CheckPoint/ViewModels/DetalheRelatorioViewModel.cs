using CheckPointBase.Data.Repository;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CheckPoint.ViewModels
{
    public class DetalheRelatorioViewModel : BaseViewModel
    {

        #region -> Propriedades <-
        private PontoRepository _pontoRepository;       

        private Relatorio relatorioItem;
        private Usuario userObj;
        private Guid idRelatorio;
        private List<Ponto> pontos;
        #endregion


        #region -> Construtor <-
        public DetalheRelatorioViewModel()
        {
            _pontoRepository = new PontoRepository();

            //AdicionarPonto();  
        }
        #endregion


        #region -> Encapsulamento <-
        public Relatorio RelatorioItem { get { return relatorioItem; } set { relatorioItem = value; OnPropertyChanged("RelatorioItem"); } }
        public Usuario UserObj { get { return userObj; } set { userObj = value; OnPropertyChanged("UserObj"); } }
        public Guid IdRelatorio { get { return idRelatorio; } set { idRelatorio = value; OnPropertyChanged("IdRelatorio"); } }
        public List<Ponto> Pontos { get { return pontos; } set { pontos = value; OnPropertyChanged("Pontos"); } }
        #endregion


        #region -> Métodos <-
        public void ListarPontos()
        {
            pontos = _pontoRepository.GetPontosByRelatorio(relatorioItem.Id);
        }
        // Uso apenas para dev
        //public void AdicionarPonto()
        //{
        //    Ponto ponto = new Ponto();
        //    ponto.DataInicio = new DateTime(2022, 07, 07, 15, 16, 10);
        //    ponto.DataFim = new DateTime(2022, 07, 07, 15, 16, 10);
        //    ponto.Local = "Endereco";
        //    //ponto.Fk_IdRelatorio = Guid.Parse("2980b26f-8fb6-4b6e-b761-6180c926e248");
        //    ponto.Fk_IdRelatorio = relatorioItem.Id;
        //    ponto.Fk_IdUsuario = userObj.Id;
        //    ponto.Ativo = 1;
        //    ponto.Alteracao = null;

        //    var p = _pontoRepository.InsertOrReplacePonto(ponto);
        //}

        public void CarregaDados()
        {
            //AdicionarPonto();

            ListarPontos();
        } 
        #endregion



    }
}
