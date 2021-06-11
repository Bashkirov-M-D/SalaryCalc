using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalaryCalc.Other;
using System;

namespace SalaryCalc.UnitTests
{
    [TestClass]
    public class SalaryCalculatorTests
    {
        TreeCollection<StaffMember> tree;
        [TestInitialize]
        public void TestInitialize()
        {
            string[,] data =
            {
                {"1",   "01.05.2000", "1", "150000", "0"},
                {"2",   "07.04.2017", "0", "30000" , "1"},
                {"3",   "24.06.2010", "0", "50000" , "1"},
                {"4",   "16.07.2004", "2", "100000", "0"},
                {"5",   "07.11.2009", "2", "80000" , "4"},
                {"6",   "13.01.2011", "2", "65000" , "4"},
                {"7",   "26.12.2020", "0", "25000" , "5"},
                {"8",   "20.02.2019", "0", "27000" , "5"},
                {"9",   "14.08.2015", "0", "27000" , "6"},
                {"10",  "29.09.2013", "0", "45000" , "6"}
            };

            tree = new TreeCollection<StaffMember>();
            tree.Add(new StaffMember()
            {
                Id = 0
            });

            StaffMember member;

            for (int i = 0; i <= data.GetUpperBound(0); i++) 
            {
                member = new StaffMember
                {
                    Id = Convert.ToInt32(data[i, 0]),
                    HireDate = data[i, 1],
                    StaffGroup = Convert.ToInt32(data[i, 2]),
                    Salary = Convert.ToInt32(data[i, 3]),
                    SupervisorId = Convert.ToInt32(data[i, 4])
                };
                tree.Add(member, new StaffMember()
                {
                    Id = member.SupervisorId
                });
            }
        }
        [TestMethod]
        public void CalculateMemberSalary_Employee30000Salary2Years_TotalSalary()
        {
            var member = new StaffMember
            {
                Salary = 30000,
                StaffGroup = 0,
                HireDate = "17.05.2019"
            };

            var res = SalaryCalculator.CalculateMemberSalary(member, new TreeCollection<StaffMember>(), DateTime.Now);

            Assert.AreEqual(31800, res);
        }

        [TestMethod]
        public void CalculateMemberSalary_Employee72600Salary20Years_TotalSalary()
        {
            var member = new StaffMember
            {
                Salary = 72600,
                StaffGroup = 0,
                HireDate = "17.05.1999"
            };

            var res = SalaryCalculator.CalculateMemberSalary(member, new TreeCollection<StaffMember>(), DateTime.Now);

            Assert.AreEqual(94380, res);
        }

        [TestMethod]
        public void CalculateMemberSalary_Manager63800Salary9Years_TotalSalary()
        {
            var member = new StaffMember
            {
                Salary = 63800,
                StaffGroup = 1,
                HireDate = "17.05.2012"
            };

            var res = SalaryCalculator.CalculateMemberSalary(member, new TreeCollection<StaffMember>(), DateTime.Now);

            Assert.AreEqual(89320, res);
        }
        [TestMethod]
        public void CalculateMemberSalary_Manager54215Salary3Years_TotalSalary()
        {
            var member = new StaffMember
            {
                Salary = 54215,
                StaffGroup = 1,
                HireDate = "17.07.2017"
            };

            var res = SalaryCalculator.CalculateMemberSalary(member, new TreeCollection<StaffMember>(), DateTime.Now);

            Assert.AreEqual(62347.25, res, 0.01);
        }
        [TestMethod]
        public void CalculateMemberSalary_Salesman100000Salary15Years_TotalSalary()
        {
            var member = new StaffMember
            {
                Salary = 100000,
                StaffGroup = 2,
                HireDate = "17.05.2006"
            };

            var res = SalaryCalculator.CalculateMemberSalary(member, new TreeCollection<StaffMember>(), DateTime.Now);

            Assert.AreEqual(115000, res, 0.01);
        }
        [TestMethod]
        public void CalculateMemberSalary_Salesman250000Salary50Years_TotalSalary()
        {
            var member = new StaffMember
            {
                Salary = 250000,
                StaffGroup = 2,
                HireDate = "17.05.1971"
            };

            var res = SalaryCalculator.CalculateMemberSalary(member, new TreeCollection<StaffMember>(), DateTime.Now);

            Assert.AreEqual(337500, res, 0.01);
        }

        [TestMethod]
        public void CalculateMemberSalary_FirstInDataTree_TotalSalary()
        {
            var id = new StaffMember
            {
                Id = 1
            };

            var res = SalaryCalculator.CalculateMemberSalary(tree.GetItem(id), tree, DateTime.Now);

            Assert.AreEqual(210493, res, 0.1);
        }

        [TestMethod]
        public void CalculateMemberSalary_FirstInDataTreeTenYearsLater_TotalSalary()
        {
            var id = new StaffMember
            {
                Id = 1
            };

            var res = SalaryCalculator.CalculateMemberSalary(tree.GetItem(id), tree, DateTime.Now.AddDays(3652));

            Assert.AreEqual(210520, res, 0.1);
        }

        [TestMethod]
        public void CalculateMemberSalary_SixthInDataTree_TotalSalary()
        {
            var id = new StaffMember
            {
                Id = 6
            };

            var res = SalaryCalculator.CalculateMemberSalary(tree.GetItem(id), tree, DateTime.Now);

            Assert.AreEqual(71756.5, res, 0.1);
        }
    }
}
