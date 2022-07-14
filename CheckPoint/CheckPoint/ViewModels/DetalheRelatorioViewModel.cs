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

        private Relatorio _relatorioItem;
        private Usuario _userObj;
        private Guid _idRelatorio;
        private List<Ponto> _listapontos;
        #endregion


        #region -> Construtor <-
        public DetalheRelatorioViewModel(Relatorio relatorio, Usuario userObj)
        {           
            _userObj = userObj;
            _relatorioItem = relatorio;

            CarregaDados();  
        }
        #endregion


        #region -> Encapsulamento <-
        public Relatorio RelatorioItem { get { return _relatorioItem; } set { _relatorioItem = value; OnPropertyChanged("RelatorioItem"); } }
        public Usuario UserObj { get { return _userObj; } set { _userObj = value; OnPropertyChanged("UserObj"); } }
        public Guid IdRelatorio { get { return _idRelatorio; } set { _idRelatorio = value; OnPropertyChanged("IdRelatorio"); } }
        public List<Ponto> ListaPontos { get { return _listapontos; } set { _listapontos = value; OnPropertyChanged("ListaPontos"); } }
        #endregion


        #region -> Métodos <-
        void CarregaDados()
        {
            _pontoRepository = new PontoRepository();
            ListaPontos = new List<Ponto>();

        }
        
        public void CarregaLista()
        {
            ListaPontos.Clear();
            ListaPontos = _pontoRepository.GetPontosByRelatorio(_relatorioItem.Id);
            OnPropertyChanged("ListaPontos");
        }
        #endregion



    }
}
