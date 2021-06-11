using SalaryCalc.Other;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalaryCalc
{
    public partial class MainForm : Form
    {
        private readonly EventManager manager;

        private DataGridView staffTable;

        private Button addStaffMember;
        private Button calcSalary;
        private Button calcAllSalary;
        private Button filter;

        private TextBox nameTextBox;
        private TextBox hireDateTextBox;
        private TextBox salaryTextBox;
        private TextBox idTextBox;
        private TextBox loginTextBox;
        private TextBox passwordTextBox;

        private ComboBox groupBox;

        private Label label;
    
        public MainForm(User user)
        {
            InitializeComponent();
            LoadForm();
            manager = new EventManager(this, user);
        }

        public void AddStuffMemberInTable(StaffMember staffMember)
        {
            int rowNumber = staffTable.Rows.Count;
            staffTable.Rows.Add(staffMember.Name, staffMember.HireDate, 
                StaffMember.GroupNames[staffMember.StaffGroup], staffMember.Salary, staffMember.SupervisorId);
            staffTable.Rows[rowNumber].HeaderCell.Value = staffMember.Id + "";
        }

        public void ClearTable()
        {
            staffTable.Rows.Clear();
        }

        public void disableSuperuserPermissionControls()
        {
            addStaffMember.Enabled = false;
            calcAllSalary.Enabled = false;
        }
        private void LoadForm()
        {
            this.Size = new Size(1000, 850);
            staffTable = CreateGridView();
            CreateButtons();
            CreateTextBoxes();
        }

        private DataGridView CreateGridView()
        {
            DataGridView table = new DataGridView();

            this.Controls.Add(table);

            table.ColumnCount = 5;

            table.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            table.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            table.ColumnHeadersDefaultCellStyle.Font =
                new Font(table.Font, FontStyle.Bold);

            table.Name = "staffTable";
            table.Location = new Point(8, 8);
            table.Size = new Size(970, 500);
            table.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            table.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            table.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            table.GridColor = Color.Black;
            table.AllowUserToAddRows = false;
            table.MultiSelect = false;

            table.Columns[0].Name = "Name";
            table.Columns[1].Name = "Hire date";
            table.Columns[2].Name = "Group";
            table.Columns[3].Name = "Base salary";
            table.Columns[4].Name = "Supervisor id";

            return table;
        }

        private void CreateButtons()
        {
            addStaffMember = new Button();
            calcSalary = new Button();
            calcAllSalary = new Button();
            filter = new Button();

            this.Controls.Add(addStaffMember);
            this.Controls.Add(calcSalary);
            this.Controls.Add(calcAllSalary);
            this.Controls.Add(filter);

            addStaffMember.Location = new Point(100, 650);
            addStaffMember.Size = new Size(150, 75);
            calcSalary.Location = new Point(300, 650);
            calcSalary.Size = new Size(150, 75);
            calcAllSalary.Location = new Point(500, 650);
            calcAllSalary.Size = new Size(150, 75);
            filter.Location = new Point(100, 590);          
            filter.Size = new Size(100, 50);                

            addStaffMember.Text = "Add staff member";
            calcSalary.Text = "Calculate salary";
            calcAllSalary.Text = "Calculate total salary";
            filter.Text = "Filter";

            addStaffMember.Click += AddStaffMember_Click;
            calcSalary.Click += CalcSalary_Click;
            calcAllSalary.Click += CalcAllSalary_Click;
            filter.Click += Filter_Click;
        }
        

        private void Filter_Click(object sender, EventArgs e)
        {
            string id = staffTable.CurrentRow.HeaderCell.Value.ToString();
            manager.FilterTableById(id);
        }

        private void CalcAllSalary_Click(object sender, EventArgs e)
        {
            int salary;
            salary = manager.CalculateAllSalary(hireDateTextBox.Text);
            MessageBox.Show("Total salary: " + salary);
        }

        private void CalcSalary_Click(object sender, EventArgs e)
        {
            int salary;
            salary = manager.CalculateSalary(staffTable.CurrentRow.HeaderCell.Value.ToString(), hireDateTextBox.Text);
            MessageBox.Show(staffTable.CurrentRow.Cells[0].Value.ToString() + "'s Salary: " + salary);
        }

        private void AddStaffMember_Click(object sender, EventArgs e)
        {
            bool res = manager.AddStaffMember(
                nameTextBox.Text,
                hireDateTextBox.Text,
                groupBox.SelectedIndex,
                salaryTextBox.Text,
                loginTextBox.Text,
                passwordTextBox.Text,
                idTextBox.Text
                );
            if (!res) MessageBox.Show("Wrong arguments");
        }

        private void CreateTextBoxes()
        {
            nameTextBox = new TextBox();
            hireDateTextBox = new TextBox();
            salaryTextBox = new TextBox();
            idTextBox = new TextBox();
            loginTextBox = new TextBox();
            passwordTextBox = new TextBox();

            TextBox[] textBoxes = { nameTextBox, hireDateTextBox, salaryTextBox, idTextBox, loginTextBox, passwordTextBox};
            string[] textBoxesTexts = { "Name", "date: dd.mm.yyyy", "Base salary", "Staff member id", "login", "password"};

            for (int i = 0; i < textBoxes.Length; i++)
            {
                this.Controls.Add(textBoxes[i]);
                textBoxes[i].Location = new Point(100 + 150 * i, 550);
                textBoxes[i].Size = new Size(130, 40);
                textBoxes[i].PlaceholderText = textBoxesTexts[i];
            }

            label = new Label();
            this.Controls.Add(label);
            label.Location = new Point(700, 590);
            label.Size = new Size(200, 200);
            label.Text = "Staff member id text box is used to identify who will be supervisor of a staff member you are adding\n" +
                "click any row to choose whose salary to calculate or to filter this person's subordinates (empty or 0 for all)";

            groupBox = new ComboBox();
            this.Controls.Add(groupBox);
            groupBox.Items.Add("Employee");
            groupBox.Items.Add("Manager");
            groupBox.Items.Add("Salesman");
            groupBox.Location = new Point(500, 600);
            groupBox.Size = new Size(130, 40);
            groupBox.SelectedIndex = 0;
        }
    }
}
