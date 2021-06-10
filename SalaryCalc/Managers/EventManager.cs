using SalaryCalc.Other;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SalaryCalc
{
    public class EventManager
    {
        private readonly MainForm form;
        private readonly User user;

        public EventManager(MainForm mainForm, User user)
        {
            this.user = user;
            form = mainForm;
            FillTable();
        }

        public void FillTable()
        {
            if (user.Permission == User.Permissions.Superuser)
                FilterTable(new DBManager.Condition());
            else
            {
                FilterTableById(user.Id.ToString());
                form.disableSuperuserPermissionControls();
            }
        }

        public void FilterTable(DBManager.Condition condition)
        {
            form.ClearTable();
            List<StaffMember> staff = DBManager.Load(condition);
            if (staff == null)
                return;
            foreach (StaffMember member in staff)
            {
                form.AddStuffMemberInTable(member);
            }
        }

        public void FilterTableById(string id)
        {
            FilterTable(new DBManager.Condition()
                .Field(DBManager.Fields.Id)
                .EqualsTo(id)
                .Or()
                .Field(DBManager.Fields.SupervisorId)
                .EqualsTo(id));
        }

        public bool AddStaffMember(string name, string hireDate, int group, string salary, string login, string password, string id = null)
        {
            try
            {
                DateTime date;
                int salaryTemp;
                int supervisorId = 0;

                date = Convert.ToDateTime(hireDate);
                salaryTemp = Convert.ToInt32(salary);
                if(!string.IsNullOrEmpty(id))
                    supervisorId = Convert.ToInt32(id);

                if (!ValidateStaffMemberData(name, date, group, login, password))
                    return false;

                StaffMember staffMember = new StaffMember(name, date.ToShortDateString(), group, salaryTemp, login, password);

                if (supervisorId > 0)
                    staffMember.SupervisorId = supervisorId;

                int resId = DBManager.SavePerson(staffMember);
                staffMember.Id = resId;
                form.AddStuffMemberInTable(staffMember);
                
                return true;               
            }
            catch(Exception e)
            {
                MessageBox.Show("error while creating new staff member\n" + e.Message);
                return false;
            }
        }

        public int CalculateSalary(int staffMemberId)
        {
            List<StaffMember> member = DBManager.Load(new DBManager.Condition()
                .Field(DBManager.Fields.Id)
                .EqualsTo(staffMemberId.ToString()));
            return (int)SalaryCalculator.CalcSalary(member[0], true, false);
        }
        
        public int CalculateAllSalary()
        {
            List<StaffMember> staffMembers = DBManager.Load();
            double totalSalary = 0;
            foreach(StaffMember member in staffMembers)
            {
                totalSalary += SalaryCalculator.CalcSalary(member, true, false);
            }
            return (int)totalSalary;
        }

        private bool ValidateStaffMemberData(string name, DateTime hireDate, int group, string login, string password)
        {
            if (name.Length == 0
                || hireDate.CompareTo(DateTime.Now) > 0
                || group < 0 || group > 2
                || string.IsNullOrEmpty(login)
                || string.IsNullOrEmpty(password)
                )
                return false;
            return true;
        }
    }
}
