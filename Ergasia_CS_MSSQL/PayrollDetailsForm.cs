using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ergasia_CS_MSSQL
{
    public partial class PayrollDetailsForm : Form
    {
        string passedID;
        public PayrollDetailsForm(string passedSelID)
        {
            InitializeComponent();
            this.passedID = passedSelID;
        }

        public string conString = "Data Source=DESKTOP-3QILQ20\\SQLEXPRESS;Initial Catalog=ErgasiaDB;Integrated Security=True;MultipleActiveResultSets=True";
        SqlConnection con;

        private void PayrollDetailsForm_Load(object sender, EventArgs e)
        {
            this.label14.Text = passedID;
            try
            {
                con = new SqlConnection(conString);
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string query = "SELECT firstname, lastname, dept_name, hire_date FROM v_main_employees WHERE employee_id =" + passedID;
            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            label12.Text = reader.GetString(0) + " " + reader.GetString(1);
                            label13.Text = reader.GetString(2);
                            label16.Text = reader["hire_date"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            query = "SELECT * FROM tbl_payroll_details WHERE id =" + passedID;
            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if ((Convert.ToInt32(reader["rate"])) == 1)
                            {
                                radioButton1.Checked = true;
                            }
                            else if ((Convert.ToInt32(reader["rate"])) == 2)
                            {
                                radioButton2.Checked = true;
                            }
                            else if ((Convert.ToInt32(reader["rate"])) == 3)
                            {
                                radioButton3.Checked = true;
                            }
                            textBox2.Text = reader["salary_yearly"].ToString();
                            textBox3.Text = reader["salary_monthly_mixed"].ToString();
                            textBox4.Text = reader["insurance_fee"].ToString();
                            textBox5.Text = reader["sal_mixed_before_tax"].ToString();
                            textBox6.Text = reader["tax"].ToString();
                            textBox7.Text = reader["net_salary"].ToString();
                            textBox8.Text = reader["employee_tax"].ToString();
                            textBox9.Text = reader["employee_cost_total"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            query = "SELECT * FROM tbl_off_history WHERE emp_id =" + passedID;
            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox10.Text = reader["start_date"].ToString();
                            textBox11.Text = reader["end_date"].ToString();
                            textBox12.Text = reader["days_worked"].ToString();
                            textBox13.Text = reader["days_off_with_pay"].ToString();
                            textBox14.Text = reader["days_off_without_pay"].ToString();
                            textBox15.Text = reader["parental_leave"].ToString();
                            textBox16.Text = reader["sick_leave"].ToString();
                            textBox17.Text = reader["days_off_total"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            query = "SELECT employee_id, firstname, lastname, email, phone, DOB, dept_name  FROM v_main_employees WHERE employee_id =" + passedID;
            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox18.Text = reader["employee_id"].ToString();
                            textBox19.Text = reader["firstname"].ToString();
                            textBox20.Text = reader["lastname"].ToString();
                            textBox21.Text = reader["email"].ToString();
                            textBox22.Text = reader["phone"].ToString();
                            textBox23.Text = reader["DOB"].ToString();
                            textBox24.Text = reader["dept_name"].ToString();
                        }
                    }
                    SqlDataAdapter da = new SqlDataAdapter("SELECT dept_name FROM tbl_departments", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "dept_name";
                    comboBox1.ValueMember = "dept_name";

                    SqlCommand cmd = new SqlCommand("SELECT dept_id FROM tbl_employees WHERE employee_id = " + passedID, con);
                    comboBox1.SelectedIndex = (int)cmd.ExecuteScalar() - 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nWUT");
                }
            }

            con.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                label18.Show();
                textBox1.Show();
                label34.Show();
                comboBox1.Show();
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                textBox13.Enabled = true;
                textBox14.Enabled = true;
                textBox15.Enabled = true;
                textBox16.Enabled = true;
            }
            if (tabControl1.SelectedTab == tabPage3)
            {
                textBox19.Enabled = true;
                textBox20.Enabled = true;
                textBox21.Enabled = true;
                textBox22.Enabled = true;
                textBox23.Enabled = true;
            }
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(conString))
                    {
                        String query = "UPDATE tbl_employees SET rate = @rate, salary_monthly = @salary_monthly WHERE employee_id =" + passedID;

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            if (radioButton1.Checked)
                            {
                                command.Parameters.AddWithValue("@rate", 1);
                                command.Parameters.AddWithValue("@salary_monthly", 8 * 22 * float.Parse(textBox1.Text));
                            }
                            else if (radioButton2.Checked)
                            {
                                command.Parameters.AddWithValue("@rate", 2);
                                command.Parameters.AddWithValue("@salary_monthly", 22 * float.Parse(textBox1.Text));
                            }
                            else if (radioButton3.Checked)
                            {
                                command.Parameters.AddWithValue("@rate", 3);
                                command.Parameters.AddWithValue("@salary_monthly", float.Parse(textBox1.Text));
                            }

                            connection.Open();
                            int result = command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(conString))
                    {
                        String query = "UPDATE tbl_payroll_details SET rate = @rate, salary_monthly_mixed = @salary_monthly_mixed WHERE id =" + passedID;

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            if (radioButton1.Checked)
                            {
                                command.Parameters.AddWithValue("@rate", 1);
                                command.Parameters.AddWithValue("@salary_monthly_mixed", 8 * 22 * float.Parse(textBox1.Text));
                            }
                            else if (radioButton2.Checked)
                            {
                                command.Parameters.AddWithValue("@rate", 2);
                                command.Parameters.AddWithValue("@salary_monthly_mixed", 22 * float.Parse(textBox1.Text));
                            }
                            else if (radioButton3.Checked)
                            {
                                command.Parameters.AddWithValue("@rate", 3);
                                command.Parameters.AddWithValue("@salary_monthly_mixed", float.Parse(textBox1.Text));
                            }

                            connection.Open();
                            int result = command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Error updating tbl_payroll_details.");
                }
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    String query = "UPDATE tbl_employees SET dept_id = @dept_id  WHERE employee_id =" + passedID;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        switch (comboBox1.SelectedIndex)
                        {
                            case 0:
                                command.Parameters.AddWithValue("@dept_id", 1);
                                break;
                            case 1:
                                command.Parameters.AddWithValue("@dept_id", 2);
                                break;
                            case 2:
                                command.Parameters.AddWithValue("@dept_id", 3);
                                break;
                            case 3:
                                command.Parameters.AddWithValue("@dept_id", 4);
                                break;
                            case 4:
                                command.Parameters.AddWithValue("@dept_id", 5);
                                break;
                            case 5:
                                command.Parameters.AddWithValue("@dept_id", 6);
                                break;
                        }

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error updating department.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    String query = "UPDATE tbl_off_history SET end_date = @end_date, days_off_with_pay = @days_off_with_pay, days_off_without_pay = @days_off_without_pay, parental_leave = @parental_leave, sick_leave = @sick_leave WHERE emp_id =" + passedID;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (string.IsNullOrWhiteSpace(textBox11.Text)) 
                        { 
                            command.Parameters.AddWithValue("@end_date", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@end_date", textBox11.Text);
                        }
                        command.Parameters.AddWithValue("@days_off_with_pay", textBox13.Text);
                        command.Parameters.AddWithValue("@days_off_without_pay", textBox14.Text);
                        command.Parameters.AddWithValue("@parental_leave", textBox15.Text);
                        command.Parameters.AddWithValue("@sick_leave", textBox16.Text);

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error updating tbl_off_history.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    String query = "UPDATE tbl_employees SET firstname = @firstname, lastname = @lastname, email = @email, phone = @phone, DOB = @DOB WHERE employee_id =" + passedID;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", textBox19.Text);
                        command.Parameters.AddWithValue("@lastname", textBox20.Text);
                        command.Parameters.AddWithValue("@email", textBox21.Text);
                        command.Parameters.AddWithValue("@phone", textBox22.Text);
                        command.Parameters.AddWithValue("@DOB", DateTime.Parse(textBox23.Text));

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error updating tbl_employees/personal.");
            }

            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            label18.Hide();
            textBox1.Hide();
            label34.Hide();
            comboBox1.Hide();

            textBox13.Enabled = false;
            textBox14.Enabled = false;
            textBox15.Enabled = false;
            textBox16.Enabled = false;

            textBox19.Enabled = false;
            textBox20.Enabled = false;
            textBox21.Enabled = false;
            textBox22.Enabled = false;
            textBox23.Enabled = false;

            button2.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new PayrollDetailsForm(passedID).Show();
            this.Close();
        }
    }
}
