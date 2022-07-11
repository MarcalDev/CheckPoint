using CheckPoint.Views;
using CheckPointBase.Data.Repository;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using CheckPoint.Views.PopUp;

namespace CheckPoint.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private readonly RelatorioRepository _relatorioRepository;
        public INavigation Navigation { get; set; }

        
        public ICommand DetalheRelatorioCommand { get; set; }
        public ICommand CadastraPontoCommand { get; set; }

        private Guid idRelatorio;

        public Guid IdRelatorio
        {
            get { return idRelatorio; }
            set { idRelatorio = value; }
        }

        private Relatorio relatorioItem;

        public Relatorio RelatorioItem
        {
            get { return relatorioItem; }
            set { relatorioItem = value; }
        }

        private List<Relatorio> lista;

        public List<Relatorio> Lista
        {
            get { return lista; }
            set { lista = value; }
        }

        public HomeViewModel()
        {
            _relatorioRepository = new RelatorioRepository();


            //AdicionarRelatorio();
            ListarRelatorios();

            CadastraPontoCommand = new Command(CadastraPonto);
        }

        public void CadastraPonto()
        {
            App.Current.MainPage.Navigation.ShowPopup(new CadastroPontoPopUp());
        }

        public  void ListarRelatorios()
        {       
            lista = _relatorioRepository.GetRelatorios();
            
        }

        // Uso apenas para dev
        public void AdicionarRelatorio()
        {
            Relatorio relatorio = new Relatorio();
            relatorio.Data = new DateTime(2022, 07, 07, 15, 16, 10);
            relatorio.TempoJornada = new DateTime(2022, 07, 07, 15, 16, 10);
            relatorio.Saldo = new DateTime(2022, 07, 07, 15, 16, 10);
            relatorio.Status = "Ativo";
            relatorio.Fk_IdUsuario = Guid.Parse("f06e6eee-579f-4dda-a167-054661577e1a");
            relatorio.Ativo = 1;
            relatorio.Alteracao = null;
            

            var p = _relatorioRepository.InsertOrReplaceRelatorio(relatorio);
        }

        public void NavegaDetalheRelatorio(Relatorio relatorio)
        {
            
            Navigation.PushAsync(new DetalheRelatorioPage(relatorio));

        }
    }
}
