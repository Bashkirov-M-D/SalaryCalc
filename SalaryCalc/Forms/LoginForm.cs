using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SalaryCalc
{
    public partial class LoginForm : Form
    {
        private TextBox loginTextBox;
        private TextBox passwordTextBox;

        private Label hintLabel;

        private Button loginButton;

        public LoginForm()
        {
            InitializeComponent();
            LoadForm();
            CreateComponents();
        }

        private void LoadForm()
        {
            this.Size = new Size(300, 200);
            this.CenterToScreen();
        }

        private void CreateComponents()
        {
            loginTextBox = new TextBox();
            passwordTextBox = new TextBox();
            hintLabel = new Label();
            loginButton = new Button();

            this.Controls.Add(loginTextBox);
            this.Controls.Add(passwordTextBox);
            this.Controls.Add(hintLabel);
            this.Controls.Add(loginButton);

            loginTextBox.Location = new Point(10, 20);
            loginTextBox.Size = new Size(125, 40);
            passwordTextBox.Location = new Point(150, 20);
            passwordTextBox.Size = new Size(125, 40);

            loginTextBox.PlaceholderText = "login";
            passwordTextBox.PlaceholderText = "password";

            hintLabel.Location = new Point(40, 50);
            hintLabel.Size = new Size(200, 20);
            hintLabel.Text = "admin admin for superuser";

            loginButton.Location = new Point(60, 80);
            loginButton.Size = new Size(150, 60);
            loginButton.Text = "Login";
            loginButton.Select();

            loginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            var res = Login(loginTextBox.Text, passwordTextBox.Text);
            if(!res)
                MessageBox.Show("wrong login or password");
        }

        private bool Login(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return false;



            return true;
        }
    }
}
