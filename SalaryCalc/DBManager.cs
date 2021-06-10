using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace SalaryCalc
{
    public class DBManager
    {
        /*public static List<StaffMember> LoadAll()
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                var output = connection.Query<StaffMember>("select * from Staff");
                return output.AsList();
            }
        }*/

        public static List<StaffMember> Load(string filter = "")
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
                {
                    if (filter != "")
                        filter = "where " + filter;
                    var output = connection.Query<StaffMember>("select * from Staff " + filter, new DynamicParameters());
                    return output.AsList();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Invalid argument \n" + e.Message);
                return null;
            }
        }

        public static int SavePerson(StaffMember staffMember)
        {
            using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
            {
                connection.Execute("insert into Staff (Name, HireDate, StaffGroup, Salary, SupervisorId, Login, Password) values " +
                    "(@Name, @HireDate, @StaffGroup, @Salary, @SupervisorId, @Login, @Password)", staffMember);
                return connection.QueryFirst<int>("select last_insert_rowid()");
            }
        }

        private static string LoadConnectionString(string id = "Database")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
