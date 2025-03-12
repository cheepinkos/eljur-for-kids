using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace EFK
{
    public partial class table : Form
    {
        private string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
        private string currentUser;
        
        public table()
        {
            InitializeComponent();
        }

        private void table_Load(object sender, EventArgs e)
        {
            
            string LogName = GlobalVar.currentUser;
            switch (GlobalVar.currentrole)
            {
                case  "student":
                    if (comboBox1.Items.Contains("Группа"))
                    {
                        comboBox1.Items.Remove("Группа");
                        comboBox1.Items.Add("Студент");
                    }
                        
                    SearchButton.Visible = true;
                    
                    int studentID = GetstudentID(LogName);
                    GlobalVar.curruserid = studentID;
                    ParseDataGrid (studentID);
                   
                    break;

                case "admin":
                   
                    SearchButton.Visible = true;
                    comboBox2.Visible = true;
                    label2.Visible = true;
                    RedButton.Visible = true;
                    
                    RedaktoringButton.Visible = true;
                    dataGridView1.ReadOnly = false;
                    break;

                case "lector":
                    
                    comboBox1.Items.Add("Группа");
                    comboBox1.Items.Add("Студент");
                    int prepodID = GetPrepodtID(LogName);
                    GlobalVar.curruserid = prepodID;
                    ParseDataGridForPrepod(prepodID);
                    RedButton.Visible = true;
                    SearchButton.Visible = true;
                    RedaktoringButton.Visible = true;
                    RedButton.Visible = true;
                    break;
                   
            }
            RedaktoringButton.Click += RedaktoringButton_Click;

        }

        
        private void ParseDataGrid (int studentID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @" SELECT   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ WHERE J.IDStudent = @IDStudent";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IDStudent", studentID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private void ParseDataGridForPrepod(int prepodID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT G.GroupN as Группа , S.MidName as Студент,   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ  JOIN eljur.Group G on J.IDGroup = G.IDGroup JOIN Student S on J.IDStudent = S.IDStudent    WHERE J.IDPREP = @IDPREP ; ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IDPREP", prepodID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private int GetstudentID (string login)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDStudent FROM Student WHERE MidName = @login";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);

                        }
                        else
                        {
                            Console.WriteLine("Студент не найден.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            return -1;
        }

        private int GetPrepodtID(string login)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDPREP FROM Prepod WHERE LastName = @login";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);

                        }
                        else
                        {
                            Console.WriteLine("Преподаватель не найден.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            return -1;
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            
            
            RedButton.Visible = false;
            
            RedaktoringButton.Visible = false;
            loginForm loginForm = new loginForm();
            loginForm.Show();
            this.Hide();
        }

        private void RedButton_Click(object sender, EventArgs e)
        {
            redaktorForm redaktorForm = new redaktorForm();
            redaktorForm.Show();
            this.Hide();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string LogName = GlobalVar.currentUser;
            int studentID = GetstudentID(LogName);
            int prepodID = GetPrepodtID(LogName);
            int FWID;
            string filterword = SearchTextBox.Text;
            switch (GlobalVar.currentrole)
            {
                case "student":
                    switch (comboBox1.SelectedItem)
                    {

                        case "Предмет":

                            FWID = GetFWOID(filterword);
                            searchByObject(FWID, studentID);
                            break;
                        case "Дата":
                            searchByDate(filterword, studentID);
                            break;
                        case "Тема":
                            searchByTheme(filterword, studentID);
                            break;
                        case "Оценка":
                            searchByGrade(filterword, studentID);
                            break;
                        case "Без поиска":
                            ParseDataGrid(studentID);
                            break;
                        
                    }
                    break;
                case "lector":
                    switch (comboBox1.SelectedItem)
                    {

                        case "Студент":
                            FWID = GetFWOIDS(filterword);
                            searchByStudentPREP(FWID, prepodID);
                            break;
                        case "Группа":
                            FWID = GetFWOIDG(filterword);
                            searchByGroupPREP(FWID, prepodID);
                            break;
                        case "Дата":
                            searchByDatePrep(filterword, prepodID);
                            break;
                        case "Тема":
                            searchByThemePrep(filterword, prepodID);
                            break;
                        case "Оценка":
                            searchByGradePrep(filterword, prepodID);
                            break;
                        case "Без поиска":
                            ParseDataGridForPrepod(prepodID);
                            break;

                    }
                    break;
            }
           
                
        }
        private void searchByThemePrep(string filterword, int prepodID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @"  SELECT G.GroupN as Группа , S.MidName as Студент,   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ  JOIN eljur.Group G on J.IDGroup = G.IDGroup JOIN Student S on J.IDStudent = S.IDStudent    WHERE J.IDPREP = @IDPREP and J.Theme = @OBJID; ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@OBJID",filterword);
                        cmd.Parameters.AddWithValue("@IDPREP", prepodID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
       private void searchByGradePrep(string filterword, int prepodID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @"  SELECT G.GroupN as Группа , S.MidName as Студент,   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ  JOIN eljur.Group G on J.IDGroup = G.IDGroup JOIN Student S on J.IDStudent = S.IDStudent    WHERE J.IDPREP = @IDPREP and J.Grade = @OBJID; ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@OBJID", filterword);
                        cmd.Parameters.AddWithValue("@IDPREP", prepodID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private int GetFWOIDS(string FW)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDStudent FROM Student WHERE MidName = @OBJName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OBJName", FW);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);

                        }
                        else
                        {
                            Console.WriteLine("Преподаватель не найден.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            return -1;
        }
        private int GetFWOIDG(string FW)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDGroup FROM eljur.Group WHERE GroupN = @OBJName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OBJName", FW);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);

                        }
                        else
                        {
                            Console.WriteLine("Преподаватель не найден.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            return -1;
        }
        private int GetFWOID (string FW)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDOBJ FROM Objects WHERE Name = @OBJName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OBJName", FW);
                        
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);

                        }
                        else
                        {
                            Console.WriteLine("Преподаватель не найден.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
            return -1;
        }

        private void searchByObject(int OBJID,int studentID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @" SELECT   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ WHERE O.IDOBJ = @OBJID and J.IDStudent = @studentID ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@OBJID",OBJID);
                        cmd.Parameters.AddWithValue("@studentID",studentID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private void searchByGroupPREP(int OBJID, int PrepID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @" SELECT G.GroupN as Группа , S.MidName as Студент,   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ  JOIN eljur.Group G on J.IDGroup = G.IDGroup JOIN Student S on J.IDStudent = S.IDStudent    WHERE J.IDPREP = @IDPREP and J.IDGroup = @OBJID;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@IDPREP", PrepID);
                        cmd.Parameters.AddWithValue("@OBJID", OBJID);
                        
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private void searchByStudentPREP(int OBJID, int PrepID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @" SELECT G.GroupN as Группа , S.MidName as Студент,   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ  JOIN eljur.Group G on J.IDGroup = G.IDGroup JOIN Student S on J.IDStudent = S.IDStudent    WHERE J.IDPREP = @IDPREP and J.IDStudent = @OBJID;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@IDPREP", PrepID);
                        cmd.Parameters.AddWithValue("@OBJID", OBJID);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private void searchByDate(string Date, int studentID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @" SELECT   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ WHERE J.DATE = @OBJID  and J.IDStudent = @IDStudent ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@OBJID", Date);
                        cmd.Parameters.AddWithValue("@IDStudent", studentID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private void searchByDatePrep(string Date, int PrepID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @"  SELECT G.GroupN as Группа , S.MidName as Студент,   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ  JOIN eljur.Group G on J.IDGroup = G.IDGroup JOIN Student S on J.IDStudent = S.IDStudent    WHERE J.IDPREP = @IDPREP and J.DATE = @OBJID; ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@OBJID", Date);
                        cmd.Parameters.AddWithValue("@IDPREP", PrepID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private void searchByTheme(string Theme,int studentID)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @" SELECT   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ WHERE J.Theme = @OBJID and J.IDStudent = @studentID ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@OBJID",Theme);
                        cmd.Parameters.AddWithValue("@studentID", studentID);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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
        private void searchByGrade(string Grade, int IDStudent)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = @" SELECT   O.Name AS Предмет, J.DATE as Дата, J.Theme as Тема, J.Homwork as ДЗ, J.Grade as Оценка FROM Journal J JOIN Objects O ON J.IDOBJ = O.IDOBJ WHERE J.Grade = @OBJID and J.IDStudent = @IDStudent ";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        //  cmd.Parameters.AddWithValue("@O.IDOBJ", comboboxitem);
                        cmd.Parameters.AddWithValue("@OBJID", Grade);
                        cmd.Parameters.AddWithValue("@IDStudent", IDStudent);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable table = new DataTable(); adapter.Fill(table);
                        dataGridView1.DataSource = table;
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    GlobalVar.currtable = comboBox2.Text;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @" SELECT * from Student";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                
                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                DataTable table = new DataTable(); adapter.Fill(table);
                                dataGridView1.DataSource = table;
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
                    break;
                    case 1:
                    GlobalVar.currtable = comboBox2.Text;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @" SELECT * from Journal";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {

                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                DataTable table = new DataTable(); adapter.Fill(table);
                                dataGridView1.DataSource = table;
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
                    break;
                    case 2:
                    GlobalVar.currtable = comboBox2.Text;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @" SELECT * from Objects";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {

                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                DataTable table = new DataTable(); adapter.Fill(table);
                                dataGridView1.DataSource = table;
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
                    break;
                    case 3:
                    GlobalVar.currtable = comboBox2.Text;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @" SELECT * from Prepod";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {

                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                DataTable table = new DataTable(); adapter.Fill(table);
                                dataGridView1.DataSource = table;
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
                    break;
                    case 4:
                    GlobalVar.currtable = comboBox2.Text;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @" SELECT * from eljur.Group";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {

                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                DataTable table = new DataTable(); adapter.Fill(table);
                                dataGridView1.DataSource = table;
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
                    break;
                    case 5:
                    GlobalVar.currtable = comboBox2.Text;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @" SELECT * from Spec";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {

                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                DataTable table = new DataTable(); adapter.Fill(table);
                                dataGridView1.DataSource = table;
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
                    break;
                case 6:
                    GlobalVar.currtable = comboBox2.Text;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @" SELECT * from CVAL";
                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {

                                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                                DataTable table = new DataTable(); adapter.Fill(table);
                                dataGridView1.DataSource = table;
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
               
                    break;
                    
            }
        }

        private void RedaktoringButton_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0]; 
                string[] rowData = new string[selectedRow.Cells.Count]; 
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    rowData[i] = selectedRow.Cells[i].Value.ToString();
                }
                redForm redForm = new redForm(rowData);
                if (redForm.IsOpen)
                {
                    // Если Form1 открыта, закрываем ее и создаем заново
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form is redForm)
                        {
                            form.Close();
                            break;
                        }
                    }
                }

                // Открываем новую форму Form1
                redForm.Show();
            }
            else 
            {
                MessageBox.Show("Пожалуйста, выберите ряд для переноса данных."); 
            }
        }
    }
    
}
