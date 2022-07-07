using CheckPointBase.Data.Repository;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace CheckPoint.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly RelatorioRepository _relatorioRepository;
        public INavigation Navigation { get; set; }
        public List<Relatorio> lista;

        public HomeViewModel()
        {
            _relatorioRepository = new RelatorioRepository();

            AdicionarRelatorio();
            ListarRelatorios();

            
        }

        public  void ListarRelatorios()
        {       
            lista = _relatorioRepository.GetRelatorios(1);
        }

        public void AdicionarRelatorio()
        {
            Relatorio relatorio = new Relatorio();
            relatorio.Data = new DateTime(2022, 07, 07, 15, 16, 10);
            relatorio.TempoJornada = new DateTime(2022, 07, 07, 15, 16, 10);
            relatorio.Saldo = new DateTime(2022, 07, 07, 15, 16, 10);
            relatorio.Status = "a";
            relatorio.Fk_IdUsuario = 1;
            relatorio.Ativo = 1;
            relatorio.Alteracao = null;


            var p = _relatorioRepository.InsertOrReplaceRelatorio(relatorio);
        }
    }
}
