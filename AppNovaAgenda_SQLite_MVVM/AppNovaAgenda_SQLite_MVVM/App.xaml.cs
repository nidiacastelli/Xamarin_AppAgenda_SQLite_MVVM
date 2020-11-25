using AppNovaAgenda_SQLite_MVVM.Helpers;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppNovaAgenda_SQLite_MVVM
{
    public partial class App : Application
    {
        static SqliteDataBaseHelper database;

        public static SqliteDataBaseHelper Database
        {
            get
            {
                if (database == null)
                {
                    database = new SqliteDataBaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ProfTiagoAgenda.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.ListaContatos());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
