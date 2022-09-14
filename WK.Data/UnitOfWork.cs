using System.Data;
using WK.Data.Repositories;
using WK.Domain;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace WK.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection connection;

        public UnitOfWork(IConfiguration configuration)
        {
            IConfigurationSection connString = configuration.GetSection(BaseRepository.ConnectionStringKey);
            if (string.IsNullOrWhiteSpace(connString.Value))
            {
                throw new ArgumentNullException($"Connection string not found");
            }
            connection = new MySqlConnection(connString.Value);
        }

        public IUnitOfWorkTransaction Begin(params IRepository[] repositories)
        {
            if (repositories == null || repositories.Length == 0)
            {
                throw new ArgumentNullException("Repositories is required", nameof(repositories));
            }
            var unitOfWorkTransaction = new UnitOfWorkTransaction(connection, repositories);
            return unitOfWorkTransaction;
        }
    }
}
