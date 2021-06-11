using SalaryCalc.Other;
using System;
using System.Collections.Generic;

namespace SalaryCalc
{
    public class SalaryCalculator
    {
        
        public static double CalculateSalary(StaffMember member, DateTime targetDate)
        {
            if (member.StaffGroup == 0)
                return CalculateEmployeeSalary(member, targetDate);

            var staff = DBManager.Load();

            return CalculateMemberSalary(member, ConvertToTree(staff), targetDate);
        }

        public static double CalculateTotalSalary(DateTime targetDate)
        {
            double res = 0;

            var staff = DBManager.Load();
            var tree = ConvertToTree(staff);

            foreach (StaffMember member in staff)
                res += CalculateMemberSalary(member, tree, targetDate);

            return res;
        }

        public static double CalculateMemberSalary(StaffMember member, TreeCollection<StaffMember> tree, DateTime targetDate)
        {
            return member.StaffGroup switch
            {
                0 => CalculateEmployeeSalary(member, targetDate),
                1 => CalculateTotalManagerSalary(member, tree, targetDate),
                2 => CalculateTotalSalesmanSalary(member, tree, targetDate),
                _ => 0,
            };
        }

        private static TreeCollection<StaffMember> ConvertToTree(List<StaffMember> members)
        {
            var tree = new TreeCollection<StaffMember>();
            var supervisorEqual = new StaffMember();

            tree.Add(new StaffMember
            {
                Id = 0
            });

            foreach(StaffMember member in members)
            {
                supervisorEqual.Id = member.SupervisorId;
                tree.Add(member, supervisorEqual);
            }
         
            return tree;
        }

        private static double CalculateTotalSalesmanSalary(StaffMember member, TreeCollection<StaffMember> tree, DateTime targetDate)
        {
            double result = 0;
            result += 0.003d * CalculateAllSubordinatesSalary(member, tree, targetDate);
            return result + CalculateSalesmanSalary(member, targetDate);
        }

        private static double CalculateAllSubordinatesSalary(StaffMember member, TreeCollection<StaffMember> tree, DateTime targetDate)
        {
            double result = 0;
            var subordinates = tree.FindChildren(member);
            foreach (StaffMember staffMember in subordinates)
                result += CalculateMemberSalary(staffMember, tree, targetDate) + CalculateAllSubordinatesSalary(staffMember, tree, targetDate);
            return result;
        }
        private static double CalculateTotalManagerSalary(StaffMember member, TreeCollection<StaffMember> tree, DateTime targetDate)
        {
            double result = 0;
            var subordinates = tree.FindChildren(member);
            foreach (StaffMember staffMember in subordinates)
                result += 0.005d * CalculateMemberSalary(staffMember, tree, targetDate);
            return result + CalculateManagerSalary(member, targetDate);
        }

        private static double CalculateEmployeeSalary(StaffMember member, DateTime targetDate)
        {
            DateTime hireDate = DateTime.Parse(member.HireDate);
            return member.Salary * (1 + Math.Min(0.3d, CalcYears(hireDate, targetDate) * 0.03d));
        }

        private static double CalculateManagerSalary(StaffMember member, DateTime targetDate)
        {
            DateTime hireDate = DateTime.Parse(member.HireDate);
            return member.Salary * (1 + Math.Min(0.4d, CalcYears(hireDate, targetDate) * 0.05d));
        }

        private static double CalculateSalesmanSalary(StaffMember member, DateTime targetDate)
        {
            DateTime hireDate = DateTime.Parse(member.HireDate);
            return member.Salary * (1 + Math.Min(0.35d, CalcYears(hireDate, targetDate) * 0.01d));
        }

        private static int CalcYears(DateTime hireDate, DateTime targetDate)
        {
            TimeSpan timeSpan = hireDate.Subtract(targetDate);
            return Math.Max(- timeSpan.Days / 365, 0);
        }
    }
}
