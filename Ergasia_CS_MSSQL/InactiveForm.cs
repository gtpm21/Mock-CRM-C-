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
    public partial class InactiveForm : Form
    {
        public InactiveForm()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-3QILQ20\\SQLEXPRESS;Initial Catalog=ErgasiaDB;Integrated Security=True";
        SqlConnection con;
        string tbl_query = "select * from v_main_employees_inactive";

        private void InactiveForm_Load(object sender, EventArgs e)
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
            dtRecord.Columns[7].ColumnName = "Department";
            dtRecord.Columns[8].ColumnName = "Termination Date";
            dataGridView1.DataSource = dtRecord;

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
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
            dtRecord.Columns[7].ColumnName = "Department";
            dtRecord.Columns[8].ColumnName = "Termination Date";
            dataGridView1.DataSource = dtRecord;

            con.Close();
        }

        private void btn_remove_Click(object sender, EventArgs e)
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

            try
            {
                string idLocRemv = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string removeVolCred = "DELETE FROM tbl_employees WHERE employee_id = " + idLocRemv;
                using (SqlCommand command = new SqlCommand(removeVolCred, con))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                removeVolCred = "DELETE FROM tbl_off_history WHERE emp_id = " + idLocRemv;
                using (SqlCommand command = new SqlCommand(removeVolCred, con))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                removeVolCred = "DELETE FROM tbl_payroll_details WHERE id = " + idLocRemv;
                using (SqlCommand command = new SqlCommand(removeVolCred, con))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Record successfullly removed from database.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nMake sure you have selected a row.");
            }

            con.Close();
        }

        private void btn_details_Click(object sender, EventArgs e)
        {
            try
            {
                string idLocSelected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                new PayrollDetailsForm(idLocSelected).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\nMake sure you have selected a row.");
            }
        }
    }
}
