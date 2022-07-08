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

        private Relatorio relatorioItem;

        public Relatorio RelatorioItem
        {
            get { return relatorioItem; }
            set { relatorioItem = value; }
        }


        private Guid idRelatorio;

        public Guid IdRelatorio
        {
            get { return idRelatorio; }
            set { idRelatorio = value; }
        }

        public DetalheRelatorioViewModel()
        {

        }


    }
}
