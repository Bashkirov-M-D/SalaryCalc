using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalc.Other
{
    class User
    {
        public enum Permission
        {
            Superuser,
            Other
        }
        private Permission permission;
        private int id;
    }
}
