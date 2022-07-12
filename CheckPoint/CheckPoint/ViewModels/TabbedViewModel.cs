using CheckPoint.Views;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CheckPoint.ViewModels
{
    public class TabbedViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private TabItemCollection items { get; set; }
        public TabItemCollection Items
        {
            get { return items; }
            set
            {
                items = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }
        public TabbedViewModel()
        {
            Items = new TabItemCollection();
        
            Items.Add(new SfTabItem { Content = new HomePage().Content, Title="Histórico"});
            Items.Add(new SfTabItem { Content = new CadastroUsuarioPage().Content, Title = "Usuario"});
        


        }
    }
}
