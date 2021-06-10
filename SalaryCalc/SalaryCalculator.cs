using System;
using System.Collections.Generic;

namespace SalaryCalc
{
    public class SalaryCalculator
    {
        public static double CalcSalary(StaffMember member, bool calcSubordinateSalary, bool calcAllSubordinateSalary)
        {
            double salary;
            List<StaffMember> staffMembers;

            switch (member.StaffGroup)
            {
                case 0:

                    return employeeSalaryCalc(member.Salary, Convert.ToDateTime(member.HireDate));

                case 1:

                    salary = managerSalaryCalc(member.Salary, Convert.ToDateTime(member.HireDate));

                    if(!(calcSubordinateSalary || calcAllSubordinateSalary))
                        return salary;

                    staffMembers = DBManager.Load(new DBManager.Condition().Field(DBManager.Fields.SupervisorId).EqualsTo(member.Id.ToString()));
                    if (calcAllSubordinateSalary)
                    {
                        foreach (StaffMember m in staffMembers)
                            salary += CalcSalary(m, true, true) * 0.005d;
                        return salary;
                    }
                    foreach (StaffMember m in staffMembers)
                        salary += CalcSalary(m, false, false) * 0.005d;
                    return salary;
                    
                default:

                    salary = salesmanSalaryCalc(member.Salary, Convert.ToDateTime(member.HireDate));
                    staffMembers = DBManager.Load(new DBManager.Condition().Field(DBManager.Fields.SupervisorId).EqualsTo(member.Id.ToString()));

                    foreach (StaffMember m in staffMembers)
                        salary += CalcSalary(m, true, true) * 0.003d;
                    return salary;
            }
        }

        private static double employeeSalaryCalc(int baseSalary, DateTime hireDate)
        {
            return baseSalary * (1 + Math.Min(0.3d, calcYears(hireDate) * 0.03d));
        }

        private static double managerSalaryCalc(int baseSalary, DateTime hireDate)
        {
            return baseSalary * (1 + Math.Min(0.4d, calcYears(hireDate) * 0.05d));
        }

        private static double salesmanSalaryCalc(int baseSalary, DateTime hireDate)
        {
            return baseSalary * (1 + Math.Min(0.35d, calcYears(hireDate) * 0.01d));
        }

        private static int calcYears(DateTime hireDate)
        {
            TimeSpan timeSpan = hireDate.Subtract(DateTime.Now);
            return - timeSpan.Days / 365;
        }
    }
}
