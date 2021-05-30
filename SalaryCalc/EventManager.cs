using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace SalaryCalc
{
    public class EventManager
    {
        private MainForm form;

        public EventManager(MainForm mainForm)
        {
            form = mainForm;
            fillTable();
        }

        public void fillTable(string filter = "")
        {
            List<StaffMember> staff = DBManager.Load(filter);
            foreach (StaffMember member in staff)
            {
                form.addStuffMemberInTable(member);
            }
        }

        public void filterTable(string filter)
        {
            form.clearTable();
            List<StaffMember> staff = DBManager.Load(filter);
            if (staff == null)
                return;
            foreach (StaffMember member in staff)
            {
                form.addStuffMemberInTable(member);
            }
        }

        public bool addStaffMember(string name, string hireDate, int group, string salary, string id)
        {
            try
            {
                DateTime date;
                int salaryTemp;
                int supervisorId = 0;

                if (name.Length == 0)
                    return false;

                date = Convert.ToDateTime(hireDate);
                if (date.CompareTo(DateTime.Now) > 0)
                    return false;

                salaryTemp = Convert.ToInt32(salary);

                if (group < 0 || group > 2)
                    return false;

                if(id!="")
                    supervisorId = Convert.ToInt32(id);

                StaffMember staffMember = new StaffMember(name, date.ToShortDateString(), group, salaryTemp);

                if (supervisorId > 0)
                    staffMember.SupervisorId = supervisorId;

                int resId = DBManager.SavePerson(staffMember);
                staffMember.Id = resId;
                form.addStuffMemberInTable(staffMember);
                
                return true;               
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public int calculateSalary(int staffMemberId)
        {
            List<StaffMember> member = DBManager.Load("id = " + staffMemberId);
            return (int)SalaryCalculator.calcSalary(member[0], true, false);
        }
        
        public int calculateAllSalary()
        {
            List<StaffMember> staffMembers = DBManager.Load();
            double totalSalary = 0;
            foreach(StaffMember member in staffMembers)
            {
                totalSalary += SalaryCalculator.calcSalary(member, true, false);
            }
            return (int)totalSalary;
        }
    }
}
