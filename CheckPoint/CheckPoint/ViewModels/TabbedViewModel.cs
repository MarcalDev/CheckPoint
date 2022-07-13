using CheckPoint.Views;
using CheckPointBase.Models;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CheckPoint.ViewModels
{
    public class TabbedViewModel : BaseViewModel
    {
        #region -> Propriedades <-
        private Usuario _userObj;
        private TabItemCollection _items; 
        #endregion

        #region -> Construtor <-
        public TabbedViewModel()
        {
            Items = new TabItemCollection();

            Items.Add(new SfTabItem { Content = new HomePage(_userObj).Content, Title = "Histórico" });
            Items.Add(new SfTabItem { Content = new CadastroUsuarioPage().Content, Title = "Usuario" });

        } 
        #endregion

        #region -> Encapsulamento <-
        public Usuario UserObj { get { return _userObj; } set { _userObj = value; OnPropertyChanged("UserObj"); } }
        public TabItemCollection Items { get { return _items; } set { _items = value; OnPropertyChanged("Items"); } }

        #endregion



    }
}
