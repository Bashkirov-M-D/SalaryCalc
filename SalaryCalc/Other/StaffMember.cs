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
        private string login;
        private string password;

        public string Name { get => name; set => name = value; }
        public string HireDate { get => hireDate; set => hireDate = value; }
        public int StaffGroup { get => staffGroup; set => staffGroup = (value < 0 || value > 2 ? 0 : value); }
        public int Salary { get => salary; set => salary = value; }
        public int SupervisorId { get => supervisorId; set => supervisorId = value; }
        public int Id { get => id; set => id = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }

        public StaffMember(string name, string hireDate, int group, int baseSalary, string login, string password)
        {
            Name = name;
            HireDate = hireDate;
            StaffGroup = group;
            Salary = baseSalary;
            Login = login;
            Password = password;
        }

        public StaffMember(){}

        public override bool Equals(object obj)
        {
            return obj is StaffMember member &&
                   id == member.id;
        }
    }
}
