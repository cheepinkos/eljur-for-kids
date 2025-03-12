using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFK
{
    
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }
        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Ошибка при авторизации: Неверный логин или пароль");
            //string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            //string login = loginTextBox.Text;
            //string password = passwordTextBox.Text;

            //using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            //    try
            //    {
            //        connection.Open();
            //        string query = "SELECT role FROM users WHERE login = @login AND password = @password";
            //        MySqlCommand command = new MySqlCommand(query, connection);
            //        command.Parameters.AddWithValue("@login", login);
            //        command.Parameters.AddWithValue("@password", password);
            //        MySqlDataReader reader = command.ExecuteReader();

            //        if (reader.Read())
            //        {

            //            string  role = reader["role"].ToString();
            //            GlobalVar.currentrole = role;
            //            MessageBox.Show($"Добро пожаловать, {role}!");

            //            GlobalVar.currentUser = login;
            //            Console.WriteLine(GlobalVar.currentrole, GlobalVar.currentUser);

            //            table table = new table();
            //            table.Show();
            //            this.Hide();
            //            // Здесь можно добавить логику для открытия различного интерфейса для разных ролей
            //        }
            //        else
            //        {
            //            MessageBox.Show("Неверный логин или пароль.");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Ошибка при авторизации: {ex.Message}");
            //    }
            //}
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reglogP reglogP = new reglogP();
            reglogP.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgorPassForm form = new ForgorPassForm();
            form.Show();
            this.Hide();
        }
    }

}
