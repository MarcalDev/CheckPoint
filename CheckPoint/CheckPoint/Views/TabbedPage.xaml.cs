using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheckPoint.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage : ContentPage
    {
        //private TabItemCollection items { get; set; }

        //public TabItemCollection Items
        //{
        //    get { return items; }

        //    set
        //    {
        //        items = value;
        //        OnPropertyChanged("Items");
        //    }
        //}

        //SfTabView tabView;
        public TabbedPage()
        {
            //    Items = new TabItemCollection();
            //    Items.Add(new SfTabItem { Content = new HomePage().Content, Title="HomePage" });
            //    Items.Add(new SfTabItem { Content = new CadastroPontoPage().Content, Title="CadastroPontoPage" });

            InitializeComponent();

            //var tabView = new SfTabView();
            //Grid allPage1 = new Grid { Bac}


        }
    }
}