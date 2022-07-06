using CheckPointBase.Data;
using CheckPointBase.Data.Interface;
using CheckPointBase.Models;
using CheckPointBase.Models.Base;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CheckPointBase.Context
{
    public class CheckPointContext
    {
        private static SQLiteConnection _sqliteConnection;
        
        public static CheckPointContext _lazy;

        public static CheckPointContext Current
        {
            get
            {
                if(_lazy == null)
                    _lazy = new CheckPointContext();

                return _lazy;
            }
        }

        private CheckPointContext()
        {
            _sqliteConnection = new SQLiteConnection(DependencyService.Get<IDBPath>().GetDbPath());
            _sqliteConnection.CreateTable<Usuario>();
            _sqliteConnection.CreateTable<Ponto>();
            _sqliteConnection.CreateTable<Relatorio>();
        }

        public SQLiteConnection Conexao
        {
            get { return _sqliteConnection; }
            set { _sqliteConnection = value; }
        }
    }
}
