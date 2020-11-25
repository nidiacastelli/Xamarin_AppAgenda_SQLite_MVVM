using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace AppNovaAgenda_SQLite_MVVM.Model
{
    public class Contato
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Observacoes { get; set; }
    }
}
