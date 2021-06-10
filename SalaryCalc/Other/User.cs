using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalc.Other
{
    public class User
    {
        public enum Permissions
        {
            Superuser,
            Other
        }
        private Permissions permission;
        private int id;

        internal Permissions Permission { get => permission; set => permission = value; }
        public int Id { get => id; set => id = value; }
    }
}
