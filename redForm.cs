using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
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
    public partial class redForm : Form
    {
        private string PrevGrade;
        public static bool IsOpen { get; private set; } = false;
        public redForm(string[] rowData)
        {
            InitializeComponent();
            InitializeTextBoxes(rowData);
            ;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            IsOpen = true;
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            IsOpen = false;
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            switch (GlobalVar.currtable)
            {
                
                case "Objects":
                    
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"UPDATE Objects 
                                 SET Name = @NewName, IDPREP = @NewIDPREP
                                 WHERE IDOBJ = @IDOBJ";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDOBJ", textBox1.Text);
                                cmd.Parameters.AddWithValue("@NewName", textBox2.Text);
                                cmd.Parameters.AddWithValue("@NewIDPREP", textBox3.Text);
                                
                                

                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) обновлена(ы).");
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
                case "Journal":
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"UPDATE Journal 
                                 SET Theme = @NewTheme, DATE =@NewDate,IDOBJ=@newIDOBJ, IDPREP = @NewIDPREP, 
                                 IDGroup = @NewIDGroup, IDStudent = @NewIDST, Homwork = @NewHomework, Grade = @NewGrade 
                                 WHERE IDZap = @IDZap";

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@IDZap", textBox1.Text);
                            cmd.Parameters.AddWithValue("@newIDOBJ", textBox2.Text);
                            cmd.Parameters.AddWithValue("@NewIDPREP", textBox3.Text);
                            cmd.Parameters.AddWithValue("@NewIDGroup", textBox4.Text);
                            cmd.Parameters.AddWithValue("@NewIDST", textBox5.Text);
                            cmd.Parameters.AddWithValue("@NewDate", textBox6.Text);
                            cmd.Parameters.AddWithValue("@NewTheme", textBox7.Text);
                            cmd.Parameters.AddWithValue("@NewHomework", textBox8.Text);
                                cmd.Parameters.AddWithValue("@NewGrade", textBox9.Text);




                                int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show($"{rowsAffected} запись(и) обновлена(ы).");
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
                case "Student":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"UPDATE Student 
                                 SET LastName = @LastName, MidName = @MidName, FirstName = @FirstName, IDGroup = @NewIDG
                                 WHERE IDStudent = @IDST";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                                cmd.Parameters.AddWithValue("@MidName", textBox3.Text);
                                cmd.Parameters.AddWithValue("@FirstName", textBox4.Text);
                                cmd.Parameters.AddWithValue("@NewIDG", textBox5.Text);



                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) обновлена(ы).");
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
                case "Prepod":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"UPDATE Prepod 
                                 SET LastName = @LastName, MidName = @MidName, FirstName = @FirstName, 
                                 WHERE IDPREP = @IDST";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                cmd.Parameters.AddWithValue("@LastName", textBox2.Text);
                                cmd.Parameters.AddWithValue("@MidName", textBox3.Text);
                                cmd.Parameters.AddWithValue("@FirstName", textBox4.Text);
                                


                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) обновлена(ы).");
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
                case "Group":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"UPDATE eljur.Group 
                                 SET GroupN = @NewGroup, IDSpec = @SpecID 
                                 WHERE IDGroup = @IDST";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                cmd.Parameters.AddWithValue("@NewGroup", textBox2.Text);
                                cmd.Parameters.AddWithValue("@SpecID", textBox3.Text);
                                



                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) обновлена(ы).");
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
                case "Spec":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"UPDATE Spec 
                                 SET SpecN = @NewGroup, IDCVAL = @SpecID 
                                 WHERE IDSpec = @IDST";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                cmd.Parameters.AddWithValue("@NewGroup", textBox2.Text);
                                cmd.Parameters.AddWithValue("@SpecID", textBox3.Text);




                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) обновлена(ы).");
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
                case "CVAL":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"UPDATE CVAL 
                                 SET CVALN = @NewGroup
                                 WHERE IDCVAl = @IDST";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                cmd.Parameters.AddWithValue("@NewGroup", textBox2.Text);
                                cmd.Parameters.AddWithValue("@SpecID", textBox3.Text);




                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) обновлена(ы).");
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

        private void redForm_Load(object sender, EventArgs e)
        {
            
                if (GlobalVar.currentrole == "lector")
            {
               
                button1.Visible = false;
                lectorButton.Visible = true;
                lectordelButton.Visible = true;
                button2.Visible = false;
            }
            

        }
        private void InitializeTextBoxes(string[] rowData)
        {
            if (GlobalVar.currentrole == "lector")
            {
                textBox3.Text = rowData[0];
                textBox4.Text = rowData[1];
                textBox5.Text = rowData[2];
                textBox6.Text = rowData[3];
                textBox7.Text = rowData[4];
                textBox8.Text = rowData[5];
                textBox9.Text = rowData[6];
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                PrevGrade = textBox9.Text;
            }
            else
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                textBox6.Visible = true;
                textBox7.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;
                switch (GlobalVar.currtable)
                {
                    case "Student":

                        if (rowData.Length > 4)
                        {
                            textBox1.Text = rowData[0];
                            textBox2.Text = rowData[1];
                            textBox3.Text = rowData[2];
                            textBox4.Text = rowData[3];
                            textBox5.Text = rowData[4];
                            textBox6.Visible = false;
                            textBox7.Visible = false;
                            textBox8.Visible = false;
                            textBox9.Visible = false;
                        }
                        break;
                    case "Journal":


                        textBox1.Text = rowData[0];
                        textBox2.Text = rowData[1];
                        textBox3.Text = rowData[2];
                        textBox4.Text = rowData[3];
                        textBox5.Text = rowData[4];
                        textBox6.Text = rowData[5];
                        textBox7.Text = rowData[6];
                        textBox8.Text = rowData[7];
                        textBox9.Text = rowData[8];

                        break;
                    case "Objects":

                        if (rowData.Length > 2)
                        {
                            textBox1.Text = rowData[0];
                            textBox2.Text = rowData[1];
                            textBox3.Text = rowData[2];
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            textBox7.Visible = false;
                            textBox8.Visible = false;
                            textBox9.Visible = false;
                        }
                        break;
                    case "Prepod":

                        if (rowData.Length > 3)
                        {
                            textBox1.Text = rowData[0];
                            textBox2.Text = rowData[1];
                            textBox3.Text = rowData[2];
                            textBox4.Text = rowData[3];
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            textBox7.Visible = false;
                            textBox8.Visible = false;
                            textBox9.Visible = false;
                        }
                        break;
                    case "Group":

                        if (rowData.Length > 2)
                        {
                            textBox1.Text = rowData[0];
                            textBox2.Text = rowData[1];
                            textBox3.Text = rowData[2];
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            textBox7.Visible = false;
                            textBox8.Visible = false;
                            textBox9.Visible = false;
                        }

                        break;
                    case "Spec":

                        if (rowData.Length > 2)
                        {
                            textBox1.Text = rowData[0];
                            textBox2.Text = rowData[1];
                            textBox3.Text = rowData[2];
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            textBox7.Visible = false;
                            textBox8.Visible = false;
                            textBox9.Visible = false;
                        }
                        break;
                    case "CVAL":
                        if (rowData.Length > 1)
                        {
                            textBox1.Text = rowData[0];
                            textBox2.Text = rowData[1];
                            textBox3.Visible = false;
                            textBox4.Visible = false;
                            textBox5.Visible = false;
                            textBox6.Visible = false;
                            textBox7.Visible = false;
                            textBox8.Visible = false;
                            textBox9.Visible = false;
                        }

                        break;
                }
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            switch (GlobalVar.currtable)
            {
                case "Student":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"delete from Student where IDStudent = @IDST;";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) удалена(ы).");
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
                case "Journal":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"delete from Journal where IDZap = @IDST;";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) удалена(ы).");
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
                case "Objects":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"delete from Objects where IDStudent = @IDST;";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) удалена(ы).");
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
                case "Prepod":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"delete from Prepod where IDPREP = @IDST;";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) удалена(ы).");
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
                case "Group":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"delete from Group where IDGroup = @IDST;";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) удалена(ы).");
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
                case "Spec":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"delete from Spec where IDSpec = @IDST;";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) удалена(ы).");
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
                case "CVAL":
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = @"delete from CVAL where IDCVAL = @IDST;";

                            using (MySqlCommand cmd = new MySqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@IDST", textBox1.Text);
                                int rowsAffected = cmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} запись(и) удалена(ы).");
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

        private void lectorButton_Click(object sender, EventArgs e)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            string studentname = textBox4.Text;
            int StudentID = GetstudentID(studentname);
            int IDZap = GetIDZap( StudentID,PrevGrade);
            int OBJID = GetIDOBJ(textBox5.Text);
            int GroupID = GetGroupID(textBox3.Text);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE Journal 
                                 SET Theme = @NewTheme, DATE =@NewDate,IDOBJ=@newIDOBJ, IDPREP = @NewIDPREP, 
                                 IDGroup = @NewIDGroup, IDStudent = @NewIDST, Homwork = @NewHomework, Grade = @NewGrade 
                                 WHERE IDZap = @IDZap";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IDZap", IDZap);
                        cmd.Parameters.AddWithValue("@newIDOBJ", OBJID);
                        cmd.Parameters.AddWithValue("@NewIDPREP", GlobalVar.curruserid);
                        cmd.Parameters.AddWithValue("@NewIDGroup",GroupID);
                        cmd.Parameters.AddWithValue("@NewIDST", StudentID);
                        cmd.Parameters.AddWithValue("@NewDate", textBox6.Text);
                        cmd.Parameters.AddWithValue("@NewTheme", textBox7.Text);
                        cmd.Parameters.AddWithValue("@NewHomework", textBox8.Text);
                        cmd.Parameters.AddWithValue("@NewGrade", textBox9.Text);




                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} запись(и) обновлена(ы).");
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
            PrevGrade = textBox9.Text;


        }
        private int GetGroupID(string Groupname)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    string query = "SELECT IDGroup FROM eljur.Group WHERE GroupN = @OBJName; ";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OBJName", Groupname);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);

                        }
                        else
                        {
                            Console.WriteLine("Группа не найдена.");
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
    
        private int GetIDOBJ( string OBJname)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    string query = "SELECT IDOBJ FROM Objects WHERE Name = @OBJName; ";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OBJName", OBJname);
                        
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);

                        }
                        else
                        {
                            Console.WriteLine("Предмет не найден.");
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
    
        private int GetIDZap(int StudentID,string prevGrade)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    string query = "SELECT IDZap FROM Journal WHERE IDStudent = @IDStudent and DATE = @date and Theme = @Theme and Homwork = @Homwork and Grade = @Grade; ";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IDStudent", StudentID);
                        command.Parameters.AddWithValue("@date", textBox6.Text);
                        command.Parameters.AddWithValue("@Theme", textBox7.Text);
                        command.Parameters.AddWithValue("@HomWork", textBox8.Text);
                        command.Parameters.AddWithValue("@Grade", prevGrade);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);

                        }
                        else
                        {
                            Console.WriteLine("Запись не найдена.");
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
        private int GetstudentID(string stname)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT IDStudent FROM Student WHERE MidName = @MidName ";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MidName", stname);
                        
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

        private void lectordelButton_Click(object sender, EventArgs e)
        {
            string connectionString = "server=95.183.12.18;port=3306;database=eljur;user=danya;password=danya;";
            string studentname = textBox4.Text;
            int StudentID = GetstudentID(studentname);
            int IDZap = GetIDZap(StudentID, PrevGrade);
           
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"Delete from Journal where IDZap = @IDZap";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IDZap", IDZap);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} запись(и) удалена(ы).");
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
            PrevGrade = textBox9.Text;
        }
    }
}
