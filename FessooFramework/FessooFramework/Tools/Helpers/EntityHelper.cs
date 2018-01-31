using FessooFramework.Tools.DCT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FessooFramework.Tools.Helpers
{
    /// <summary>   An entity helper. </summary>
    ///
    /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>

    public static class EntityHelper
    {
        /// <summary>
        /// Создает программно строку подключения для контекста EntityFramework.
        /// </summary>
        /// <param name="DBName"> Name of the database.</param>
        /// <param name="DBAddress">The database address.</param>
        /// <param name="Login">Логин пользователя для подключении к SQL Server</param>
        /// <param name="Password">Пароль для учетной записи SQL Server.</param>
        /// <param name="IntegratedSecurity">Задает логическое значение, которое определяет, заданы ли в подключении идентификатор пользователя и пароль (значение false) или же для проверки подлинности используются текущие реквизиты учетной записи Windows (значение true).</param>
        /// <returns></returns>
        public static string CreateConnectionString(EntityProviders provider, string DBName, string DBAddress, bool IntegratedSecurity = false, string Login = "", string Password = "")
        {
            string result = "";
            DCTDefault.Execute((data) =>
            {
                var providerName = EnumHelper.GetDescription(provider);
                switch (provider)
                {
                    case EntityProviders.MSSQL:
                        {
                            var sqlBuilder = new SqlConnectionStringBuilder
                            {
                                DataSource = DBAddress,
                                InitialCatalog = DBName,
                                IntegratedSecurity = IntegratedSecurity,
                                MultipleActiveResultSets = true,
                            };
                            if (!IntegratedSecurity)
                            {
                                sqlBuilder.Password = Password;
                                sqlBuilder.UserID = Login;
                            }
                            var providerString = sqlBuilder.ToString();
                            var entityBuilder = new EntityConnectionStringBuilder
                            {
                                Provider = providerName,
                                ProviderConnectionString = providerString,
                            };
                            result = entityBuilder.ProviderConnectionString;
                        }
                        break;
                    case EntityProviders.SQLCE:
                        {
                            var sqlBuilder = new SqlConnectionStringBuilder { DataSource = DBAddress };
                            var providerString = sqlBuilder.ToString();
                            var entityBuilder = new EntityConnectionStringBuilder
                            {
                                Provider = providerName,
                                ProviderConnectionString = providerString
                            };
                            result = entityBuilder.ProviderConnectionString;
                        }
                        break;
                    case EntityProviders.SQLite:
                        {
                            var entityBuilder =  new SQLiteConnectionStringBuilder()
                            { DataSource = $@"|DataDirectory|\DefaultSQLiteDB.db", ForeignKeys = true, Version = 3 };
                            result = entityBuilder.ConnectionString;
                        }
                        break;
                    default:
                        break;
                }
               
            });
            return result;
        }

        /// <summary>   Creates default SQLCE.
        ///             Подключение к базовой локальной базе SQLCE</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="name"> The name. </param>
        ///
        /// <returns>   The new default ce. </returns>

        public static string CreateDefaultSQLCE(string DBName)
        {
            return CreateConnectionString(EntityProviders.SQLCE, $"{DBName}", $@"Data\{DBName}.db", true);
        }

        /// <summary>   Creates default SQLite. </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="DBName">   Name of the database. </param>
        ///
        /// <returns>   The new default sq lite. </returns>

        public static string CreateDefaultSQLite(string DBName)
        {
            return CreateConnectionString(EntityProviders.SQLite, $"{DBName}", $@"|DataDirectory|\Data\{DBName}.sdf", true);
        }
        /// <summary>   Creates default SQLCE.
        ///             Подключение к базовой локальной базе MSSQL</summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="name"> The name. </param>
        ///
        /// <returns>   The new default ce. </returns>

        public static string CreateLocalSQL(string DBName)
        {
            return CreateConnectionString(EntityProviders.MSSQL, $"{DBName}", ".", true, "","");
        }

        /// <summary>   Creates remote SQL.
        ///             Подключение к серверу с базой MSSQL </summary>
        ///
        /// <remarks>   AM Kozhevnikov, 31.01.2018. </remarks>
        ///
        /// <param name="DBName">       Name of the database. </param>
        /// <param name="DBAddress">    The database address. </param>
        /// <param name="Login">        Логин пользователя для подключении к SQL Server. </param>
        /// <param name="Password">     Пароль для учетной записи SQL Server. </param>
        ///
        /// <returns>   The new remote SQL. </returns>

        public static string CreateRemoteSQL(string DBName, string DBAddress, string Login, string Password)
        {
            return CreateConnectionString(EntityProviders.MSSQL, $"{DBName}", DBAddress, false, Login, Password);
        }
    }
    public enum EntityProviders
    {
        [Description("System.Data.SqlClient")]
        MSSQL = 0,
        [Description("System.Data.SqlServerCe.4.0")]
        SQLCE = 1,
        [Description("System.Data.SQLite")]
        SQLite = 2
    }
}
