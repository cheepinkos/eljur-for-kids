using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EFK
{
    public partial class reglogP : Form
    {
        public reglogP()
        {
            InitializeComponent();
        }
        public const string requiredLectorCode = "41424345"; // код для рега преподов
        
        private void reglogP_Load(object sender, EventArgs e)
        {
            
            
        }

        private void REG_Click(object sender, EventArgs e)
        {
            loginForm loginForm = new loginForm();
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;
            string role = roleСomboBox.SelectedItem.ToString();
            string lectorCode = lectorCodeTextBox.Text;

            

            if (role == "lector" && lectorCode != requiredLectorCode)
            {
                MessageBox.Show("Неверный код для лекторов.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO users (login, password, role) VALUES (@login, @password, @role)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@role", role);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Регистрация успешна!");
                    loginForm.Show();
                    this.Hide();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при регистрации: {ex.Message}");
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loginForm loginForm = new loginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}
