using HR.WebApi.Data.Entities;
using HR.WebApi.Register.AccountTests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HR.WebApi.Register
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserModel user = new UserModel();

            user.UserName = username.Text;
            user.Password = password.Text;
            user.ConfirmPassword = confirmpassword.Text;

            AccountTest test = new AccountTest();
            test.Should_regester_an_account(user);


        }
    }
}
