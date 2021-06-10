using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SalaryCalc
{
    public class DBManager
    {
        public enum Fields
        {
            Id,
            Name,
            HireDate,
            StaffGroup,
            Salary,
            SupervisorId,
            Login,
            Password
        }

        private static readonly string[] DBFields = {"id", "Name", "HireDate", "StaffGroup", "Salary", "SupervisorId", "Login", "Password"};

        public static List<StaffMember> Load(Condition condition)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = connection.Query<StaffMember>("select * from Staff " + condition.ConditionString, new DynamicParameters());
                    return output.AsList();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Invalid argument \n" + e.Message);
                return null;
            }
        }

        public static List<StaffMember> Load()
        {
            return Load(new Condition());
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

        public class Condition
        {
            private string conditionString = string.Empty;

            public string ConditionString { get {
                    if (string.IsNullOrEmpty(conditionString))
                        return conditionString;
                    else return "where " + conditionString;
                }
            }

            public Condition Field (Fields field)
            {
                if (!Enum.IsDefined(typeof(Fields), field))
                    throw new ArgumentException("field argument out of range");
                conditionString += DBFields[(int)field];
                return this;
            }

            public Condition EqualsTo(string value)
            {
                conditionString += "=" + value;
                return this;
            }
            public Condition LessThen(string value)
            {
                conditionString += "<" + value;
                return this;
            }
            public Condition MoreThen(string value)
            {
                conditionString += ">" + value;
                return this;
            }
            public Condition And()
            {
                conditionString += " and ";
                return this;
            }
            public Condition Or()
            {
                conditionString += " or ";
                return this;
            }

        }
    }
}
