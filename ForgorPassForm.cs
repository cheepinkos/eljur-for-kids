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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EFK
{
    public partial class ForgorPassForm : Form
    {
        public ForgorPassForm()
        {
            InitializeComponent();
        }
        string resscode = "141242";
        string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Login = GetLogin(LogBox.Text);
            if ((Login == LogBox.Text) && (restcodeBox.Text == resscode))
            {
                string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"UPDATE users 
                                 SET password = @Newpassword
                                 WHERE login = @login";

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@login", Login);
                            cmd.Parameters.AddWithValue("@Newpassword", newpass.Text);
                            




                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show($"пароль обновлён");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            loginForm Form = new loginForm();
            Form.Show();
            this.Hide();
        }
       private string GetLogin (string username)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    string query = "SELECT login FROM users WHERE login = @login; ";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", username);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToString(result);

                        }
                        else
                        {
                            MessageBox.Show("Неверный логин.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            return "null";
        }
    }
}
