using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App7
{
    public class MachineAsyncRepository
    {
        SQLiteAsyncConnection database;

        public MachineAsyncRepository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<DataMachine>();
        }
        public async Task<List<DataMachine>> GetItemsAsync()
        {
            return await database.Table<DataMachine>().ToListAsync();

        }
        public async Task<DataMachine> GetItemAsync(int id)
        {
            return await database.GetAsync<DataMachine>(id);
        }
        public async Task<int> DeleteItemAsync(DataMachine item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemAsync(DataMachine item)
        {
            if (item.Id != 0)
            {
                await database.UpdateAsync(item);
                return item.Id;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
    }
}
