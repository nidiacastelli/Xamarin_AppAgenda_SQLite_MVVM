using AppNovaAgenda_SQLite_MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppNovaAgenda_SQLite_MVVM.ViewModel
{
    class ListaContatosViewModel : INotifyPropertyChanged
    {
        /**
         * Propriedade que trata o evento da mudança de valor de uma propriedade.
         * Usamos com o objetivo de notificar a View da mudança.
         */ 
        public event PropertyChangedEventHandler PropertyChanged;



        /**
         *  Campo e propriedade para armazenarmos o estado o RefreshView, ou seja,
         *  se está atualizando a listagem ou não.
         */ 
        bool estaAtualizando = false;
        public bool EstaAtualizando
        {
            get => estaAtualizando; // mesma coisa que { return isRefreshing; }
            set
            {
                estaAtualizando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EstaAtualizando"));
            }
        }


        /**
         * Campo e propriedade que armazenam uma coleção observavel de Contatos (Model)
         */
        ObservableCollection<Model.Contato> listaContatos = new ObservableCollection<Model.Contato>();
        public ObservableCollection<Model.Contato> ListaContatos 
        { 
            get => listaContatos; 
            set => listaContatos = value; 
        }


        /**
         * Propriedade que armazena o valor digitado pelo usuário no campo de busca.
         */
        public string ParametroBusca { get; set; }


        public ICommand IrParaNovoContato
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new View.NovoContato());
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                });
            }
        }

        public ICommand AtualizarLista
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {                     
                        // Se ainda estiver ocupado, return, ou seja, não executa outra atualização.
                        if (EstaAtualizando)
                            return;

                        EstaAtualizando = true;

                        List<Model.Contato> tmp_list = await App.Database.GetAllContatosAsync();

                        ListaContatos.Clear();                

                        tmp_list.ForEach(x => ListaContatos.Add(x));

                        //await System.Threading.Tasks.Task.Delay(5000);
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");

                    } finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }

        public ICommand BuscarContato
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        List<Model.Contato> tmp_list = await App.Database.SearchContatoAsync(ParametroBusca);

                        ListaContatos.Clear();

                        tmp_list.ForEach(x => ListaContatos.Add(x));


                        //var msg = String.Format("Vamos implementar uma busca para {0}", ParametroBusca);

                        //await Application.Current.MainPage.DisplayAlert("Ops", msg, "OK");
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");
                    }
                });
            }
        }

        public ICommand RemoveContato
        {
            get
            {
                return new Command<int>(async (int id) =>
                {
                    try
                    {
                       /* var msg = string.Format("Vou excluir {0}", id);

                        await Application.Current.MainPage.DisplayAlert("Ops", msg, "OK");

                        return;*/

                        bool confirmacao = await Application.Current.MainPage.DisplayAlert("Tem Certeza?", "Excluir", "Sim", "Não");

                        if (confirmacao)
                        {
                            await App.Database.DeleteContatoAsync(id);
                            AtualizarLista.Execute(null);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", ex.Message, "OK");

                    }
                    finally
                    {
                        EstaAtualizando = false;
                    }
                });
            }
        }


        public ListaContatosViewModel()
        {

        }        
    }
}
