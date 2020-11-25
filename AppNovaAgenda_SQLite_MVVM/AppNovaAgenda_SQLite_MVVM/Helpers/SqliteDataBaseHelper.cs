using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppNovaAgenda_SQLite_MVVM.Helpers
{
    public class SqliteDataBaseHelper
    {
        readonly SQLiteAsyncConnection _db;

        public SqliteDataBaseHelper(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);

            _db.CreateTableAsync<Model.Contato>().Wait();
        }

        public Task<List<Model.Contato>> GetAllContatosAsync()
        {
            return _db.Table<Model.Contato>().ToListAsync();
        }

        public Task<int> SaveContatoAsync(Model.Contato contato)
        {            
            return _db.InsertAsync(contato);
        }

        public Task<int> DeleteContatoAsync(int id)
        {
            return _db.Table<Model.Contato>().DeleteAsync(i => i.Id == id);
        }

        public Task<Model.Contato> GetContatoByIdAsync(int id)
        {
            return _db.Table<Model.Contato>().FirstAsync(i => i.Id == id);
        }

        public Task<List<Model.Contato>> UpdateContatoAsync(Model.Contato contato)
        {
           return _db.QueryAsync<Model.Contato>("UPDATE Contato SET Nome=?, Email=?, Telefone=?, Id=?",
                contato.Nome,
                contato.Email,
                contato.Telefone,
                contato.Id
            );
        }

        public Task<List<Model.Contato>> SearchContatoAsync(string q)
        {
            return _db.QueryAsync<Model.Contato>("SELECT * FROM Contato WHERE Nome LIKE '%" + q + "%' ");
        }
    }
}
