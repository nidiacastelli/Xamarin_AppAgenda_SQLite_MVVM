using AppNovaAgenda_SQLite_MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppNovaAgenda_SQLite_MVVM.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaContatos : ContentPage
    {
        public ListaContatos()
        {
            InitializeComponent();

            this.BindingContext = new ViewModel.ListaContatosViewModel();
        }

        protected override async void OnAppearing()
        {
            var viewModel = (ListaContatosViewModel)BindingContext;

            viewModel.AtualizarLista.Execute(null);
        }
    }
}