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
    public partial class DepartmentsForm : Form
    {
        public DepartmentsForm()
        {
            InitializeComponent();

        }

        public string conString = "Data Source=DESKTOP-3QILQ20\\SQLEXPRESS;Initial Catalog=ErgasiaDB;Integrated Security=True;MultipleActiveResultSets=true";
        SqlConnection con;

        private void DepartmentsForm_Load(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(conString);
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SqlDataAdapter da = new SqlDataAdapter("SELECT dept_name FROM tbl_departments", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "dept_name";
            comboBox1.ValueMember = "dept_name";

            string tbl_query = "select employee_id,firstname,lastname,email,phone,DOB,hire_date,salary_monthly from tbl_employees where is_active = 1 and dept_id =" + (comboBox1.SelectedIndex + 1).ToString();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = tbl_query;
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            dtRecord.Columns[0].ColumnName = "Employee ID";
            dtRecord.Columns[1].ColumnName = "First Name";
            dtRecord.Columns[2].ColumnName = "Last Name";
            dtRecord.Columns[3].ColumnName = "Email";
            dtRecord.Columns[4].ColumnName = "Phone Number";
            dtRecord.Columns[5].ColumnName = "Date of Birth";
            dtRecord.Columns[6].ColumnName = "Date Hired";
            dtRecord.Columns[7].ColumnName = "Salary";
            dataGridView1.DataSource = dtRecord;

            label27.Text = comboBox1.SelectedValue.ToString();

            string query = "SELECT manager_id, staff_num FROM tbl_departments WHERE dept_id =" + (comboBox1.SelectedIndex + 1).ToString();
            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            label21.Text = reader.GetInt32(0).ToString(); 
                            label20.Text = reader.GetInt32(1).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            query = "SELECT firstname, lastname FROM tbl_employees INNER JOIN tbl_departments ON tbl_employees.employee_id = tbl_departments.manager_id and tbl_departments.dept_id =" + (comboBox1.SelectedIndex + 1).ToString();
            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            label22.Text = reader.GetString(0) + " " + reader.GetString(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            query = "SELECT SUM(employee_cost_total) FROM tbl_payroll_details INNER JOIN tbl_employees ON tbl_employees.is_active = 1 AND tbl_employees.employee_id = tbl_payroll_details.id AND tbl_employees.dept_id =" + (comboBox1.SelectedIndex + 1).ToString();
            using (SqlCommand command = new SqlCommand(query, con))
            {
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            label19.Text = reader.GetDecimal(0).ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            con.Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                string tbl_query = "select employee_id,firstname,lastname,email,phone,DOB,hire_date,salary_monthly from tbl_employees where is_active = 1 and dept_id =" + (comboBox1.SelectedIndex + 1).ToString();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = tbl_query;
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                dtRecord.Columns[0].ColumnName = "Employee ID";
                dtRecord.Columns[1].ColumnName = "First Name";
                dtRecord.Columns[2].ColumnName = "Last Name";
                dtRecord.Columns[3].ColumnName = "Email";
                dtRecord.Columns[4].ColumnName = "Phone Number";
                dtRecord.Columns[5].ColumnName = "Date of Birth";
                dtRecord.Columns[6].ColumnName = "Date Hired";
                dtRecord.Columns[7].ColumnName = "Salary";
                dataGridView1.DataSource = dtRecord;

                label27.Text = comboBox1.SelectedValue.ToString();

                string query = "SELECT manager_id, staff_num FROM tbl_departments WHERE dept_id =" + (comboBox1.SelectedIndex + 1).ToString();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                label21.Text = reader.GetInt32(0).ToString();
                                label20.Text = reader.GetInt32(1).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                query = "SELECT firstname, lastname FROM tbl_employees INNER JOIN tbl_departments ON tbl_employees.employee_id = tbl_departments.manager_id and tbl_departments.dept_id =" + (comboBox1.SelectedIndex + 1).ToString();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                label22.Text = reader.GetString(0) + " " + reader.GetString(1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                query = "SELECT SUM(employee_cost_total) FROM tbl_payroll_details INNER JOIN tbl_employees ON tbl_employees.is_active = 1 AND tbl_employees.employee_id = tbl_payroll_details.id AND tbl_employees.dept_id =" + (comboBox1.SelectedIndex + 1).ToString();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                label19.Text = reader.GetDecimal(0).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
