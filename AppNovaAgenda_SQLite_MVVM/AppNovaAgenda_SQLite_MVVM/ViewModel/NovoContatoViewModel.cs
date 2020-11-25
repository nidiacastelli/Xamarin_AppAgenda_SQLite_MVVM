using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppNovaAgenda_SQLite_MVVM.ViewModel
{
    class NovoContatoViewModel : INotifyPropertyChanged
    {
        private string nome, email, telefone, observacoes;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        public string Observacoes
        {
            get { return observacoes; }
            set { observacoes = value; }
        }


        public ICommand SavarNovoContato
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await App.Database.SaveContatoAsync(new Model.Contato()
                        {
                            Nome = Nome,
                            Telefone = Telefone,
                            Email = Email,
                            Observacoes = Observacoes
                        });

                        await Application.Current.MainPage.DisplayAlert("Beleza!", "Contato Salvo!", "OK");
                        
                        //ListaContatosViewModel
                        
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                });
            }
        }
    }
}
