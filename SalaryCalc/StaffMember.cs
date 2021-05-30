using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalc
{
    public class StaffMember
    { 
        public static readonly string[] GroupNames = { "Employee", "Manager", "Salesman" };

        private int id;
        private string name;
        private string hireDate;
        private int staffGroup;
        private int salary;
        private int supervisorId;

        public string Name { get => name; set => name = value; }
        public string HireDate { get => hireDate; set => hireDate = value; }
        public int StaffGroup { get => staffGroup; set => staffGroup = (value < 0 || value > 2 ? 0 : value); }
        public int Salary { get => salary; set => salary = value; }
        public int SupervisorId { get => supervisorId; set => supervisorId = value; }
        public int Id { get => id; set => id = value; }

        public StaffMember(string name, string hireDate, int group, int baseSalary)
        {
            Name = name;
            HireDate = hireDate;
            StaffGroup = group;
            Salary = baseSalary;
        }

        public StaffMember(){}
    }
}
