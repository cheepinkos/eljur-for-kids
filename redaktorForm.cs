using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class redaktorForm : Form
    {
       
        public redaktorForm()
        {
            InitializeComponent();
            
        }
        private string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";



        private void redaktorForm_Load(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            comboBox1.SelectedIndex = 0;
            comboBox1.Visible = true;
            if (GlobalVar.currentrole == "lector")
            {
                comboBox1.SelectedIndex = 5;
                comboBox1.Visible = false;
                panel7.Visible = true;

            }
        }
       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                    control.Visible = false;
            }
                

            switch (comboBox1.Text) 
            {
                case "Студента":
                    panel1.Visible = true;
                    break;

                case "Группу":
                    panel2.Visible = true;
                    break;

                case "Специальность":
                    panel3.Visible = true;
                    break;

                case "Преподавателя":
                    panel4.Visible = true;
                    break;

                case "Предмет":
                    panel5.Visible = true;
                    break;
                case "Квалификация":
                    panel6.Visible = true;
                    break;
                case "Запись в журнале":
                    panel7.Visible = true;
                    break;
            }
            

        }

        private void GroupNTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }
        private int GetGroupIDByName(string groupName)
        { 
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try 
                { 
                    connection.Open(); 
                    string query = "SELECT IDGroup FROM `Group` WHERE GroupN = @GroupName";
                    using (MySqlCommand command = new MySqlCommand(query, connection)) 
                    { 
                        command.Parameters.AddWithValue("@GroupName", groupName);
                        object result = command.ExecuteScalar(); if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    } 
                }
                catch (Exception ex) 
                { 
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            } 
            return -1; 
        }
        private int GetCvalID(string CvalName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDCVAL FROM CVAL WHERE CVALN = @CvalName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CvalName", CvalName);
                        object result = command.ExecuteScalar(); if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            return -1;
        }
        private int GetPrepID (string plastname, string pfirstname)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDPREP FROM Prepod WHERE FirstName = @FirstName AND LastName = @LastName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", pfirstname);
                        command.Parameters.AddWithValue("@LastName", plastname);
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

        private void AddSpecToDatabase (string specName , int IDCval)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Spec (SpecN,IDCval) VALUES (@SpecN, @IDCval)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SpecN", specName);
                        command.Parameters.AddWithValue("@IDCval", IDCval);
                        
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Специальность успешна добавлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void AddStudentToDatabase(string lastName, string midName, string firstName, int groupID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            { 
                try 
                { 
                    connection.Open();
                    string query = "INSERT INTO Student (LastName, MidName, FirstName, IDGroup) VALUES (@LastName, @MidName, @FirstName, @IDGroup)";
                    using (MySqlCommand command = new MySqlCommand(query, connection)) 
                    { 
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@MidName", midName);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@IDGroup", groupID); 
                        command.ExecuteNonQuery(); } MessageBox.Show("Студент успешно добавлен!"); 
                } 
                catch (Exception ex) 
                {
                    MessageBox.Show("Ошибка: " + ex.Message); 
                }
            }
        }
        private void AddGroupToDatabase(string groupName, int SpecID)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO eljur.Group (GroupN, IDSpec) VALUES (@GroupN, @IDSpec)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GroupN", groupName);
                        command.Parameters.AddWithValue("@IDspec",SpecID);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Группа успешно добавлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void AddPrepodToDataBase(string Plastname, string Pmidname, string Pfirstname)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Prepod (LastName, MidName, FirstName) VALUES (@LastName, @MidName, @FirstName)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LastName", Plastname);
                        command.Parameters.AddWithValue("@MidName", Pmidname);
                        command.Parameters.AddWithValue("@FirstName", Pfirstname);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Преподаватель успешно добавлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void AddOBJToDataBase (string objname,int prepid)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Objects (Name,IDPREP) VALUES (@Name,@IDPREP)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", objname);
                        command.Parameters.AddWithValue("@IDPREP", prepid);
                       
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Предмет успешно добавлен!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
                    {
                        case "Студента":
                            string lastName = LastNameTextBox.Text;
                            string firstName = FirstNameTextBox.Text;
                            string midName = MidNameTextBox.Text;
                            string groupName = GroupBox.Text;

                            int groupID = GetGroupIDByName(groupName);
                            if (groupID != -1) 
                            {
                                AddStudentToDatabase(lastName, midName, firstName, groupID);
                            } 
                            else 
                            {
                                MessageBox.Show("Название группы не найдено!"); 
                            }
                            break;
                        case "Группу":
                            string GroupName = GroupNTextBox.Text;
                            int SpecID =int.Parse(IDSpecTextBox.Text);
                            AddGroupToDatabase(GroupName, SpecID);
                        break;
                case"Специальность":
                    string SpecName = specNameTextBox.Text;
                    string CvalName = CvalTextBox.Text;
                    int CvalID = GetCvalID(CvalName);
                    if (CvalID != -1)
                    {
                        AddSpecToDatabase(SpecName, CvalID);
                    }
                    else
                    {
                        MessageBox.Show("Название квалификации не найдено!");
                    }
                    break;
                case "Преподавателя":
                    
                    string LastName = PLastnameTextBox.Text;
                    string FirstName = PFirstNameTextBox.Text;
                    string MidName = PMidNameTextBox.Text;
                    AddPrepodToDataBase(LastName, FirstName, MidName);
                    break;
                case "Предмет":
                    string OBJName = OBJTextBox.Text;
                    string pmidname = textBox1.Text;
                    string pfirstname = PrepNTextBox.Text;
                    int prepID = GetPrepID (pmidname, pfirstname);
                    AddOBJToDataBase(OBJName, prepID);
                    break;
                case "Квалификация":
                    AddCVALToDataBase(textBox3.Text);
                    break;
                case "Запись в журнале":
                    groupName = textBox5.Text;
                    groupID = GetGroupIDByName(groupName);
                    if (groupID != -1)
                    {
                        int objid = obj(textBox2.Text);
                        int prepid = prep(textBox4.Text);
                        int studentid = stud(textBox6.Text);
                        addZapToData(objid,prepid,groupID, studentid, textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text);
                    }
                    else
                    {
                        MessageBox.Show("Название группы не найдено!");
                    }
                   
                   
                    break;
            }
            //private void addZapToData(string OBJNAME, string firstPrepName, int GroupID, string firstStudName, string date, string theme, string HomeWork, string Grade)





        }
        private int prep (string prepname)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDPREP FROM Prepod WHERE LastName = @prepName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prepName", prepname);
                        object result = command.ExecuteScalar(); if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            return 1;
        }
    private int stud (string stdname)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDStudent FROM Student WHERE MidName = @objName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@objName",stdname);
                        object result = command.ExecuteScalar(); if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            return 1;
        }
        private int obj (string objname)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDOBJ FROM Objects WHERE Name = @objName";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@objName", objname);
                        object result = command.ExecuteScalar(); if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
            return 1;
        }
        private void addZapToData(int OBJNAME, int firstPrepName, int GroupID, int firstStudName, string date, string theme, string HomeWork, string Grade)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                

               

                try
                {
                    connection.Open();
                    string query = "INSERT INTO Journal (IDOBJ,IDPREP,IDGroup,IDStudent,DATE,Theme,Homwork,Grade) VALUES (@IDOBJ,@IDPREP,@IDGroup,@IDStudent,@DATE,@Theme,@Homwork,@Grade)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IDOBJ", OBJNAME );
                        command.Parameters.AddWithValue("@IDPREP", firstPrepName);
                        command.Parameters.AddWithValue("@IDGroup",GroupID);
                        command.Parameters.AddWithValue("@IDStudent", firstStudName);
                        command.Parameters.AddWithValue("@DATE", date);
                        command.Parameters.AddWithValue("@Theme", theme);
                        command.Parameters.AddWithValue("@Homwork", HomeWork);
                        command.Parameters.AddWithValue("@Grade", Grade);
                        


                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Запись успешно добавлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }

            }
        }
        private void AddCVALToDataBase(string objname)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO CVAL (CVALN) VALUES (@Name)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", objname);
                        

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Квалификация успешно добавлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Exit_button_Click(object sender, EventArgs e)
        {
            table table = new table();
            this.Hide();
            table.Show();
        }
    }
}
