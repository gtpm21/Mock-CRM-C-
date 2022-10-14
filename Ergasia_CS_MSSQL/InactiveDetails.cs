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
    public partial class InactiveDetails : Form
    {
        string passedID;
        public InactiveDetails(string passedSelID)
        {
            InitializeComponent();
            this.passedID = passedSelID;
        }

        public string conString = "Data Source=DESKTOP-3QILQ20\\SQLEXPRESS;Initial Catalog=ErgasiaDB;Integrated Security=True;MultipleActiveResultSets=True";
        SqlConnection con;

        private void InactiveDetails_Load(object sender, EventArgs e)
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

            string query = "SELECT firstname, lastname, dept_name, hire_date FROM v_main_employees_inactive WHERE employee_id =" + passedID;
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

            query = "SELECT employee_id, firstname, lastname, email, phone, DOB, dept_name FROM v_main_employees_inactive WHERE employee_id =" + passedID;
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\nWUT");
                }
            }

            con.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
