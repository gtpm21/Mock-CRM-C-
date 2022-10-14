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
    public partial class InsertForm : Form
    {
        public InsertForm()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-3QILQ20\\SQLEXPRESS;Initial Catalog=ErgasiaDB;Integrated Security=True";
        private void btn_insert_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    String query = "INSERT INTO tbl_employees (firstname,lastname,email,phone,DOB,hire_date,dept_id,rate,salary_monthly) VALUES (@firstname,@lastname,@email,@phone,@DOB,@hire_date,@dept_id,@rate,@salary_monthly)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@firstname", textBox1.Text);
                        command.Parameters.AddWithValue("@lastname", textBox2.Text);
                        command.Parameters.AddWithValue("@email", textBox3.Text);
                        command.Parameters.AddWithValue("@phone", textBox4.Text);
                        command.Parameters.AddWithValue("@DOB", textBox5.Text);
                        command.Parameters.AddWithValue("@hire_date", textBox6.Text);
                        command.Parameters.AddWithValue("@dept_id", textBox7.Text);
                        if (radioButton1.Checked)
                        {
                            command.Parameters.AddWithValue("@rate", 1);
                            command.Parameters.AddWithValue("@salary_monthly", 8 * 22 * float.Parse(textBox10.Text));
                        }
                        else if (radioButton2.Checked)
                        {
                            command.Parameters.AddWithValue("@rate", 2);
                            command.Parameters.AddWithValue("@salary_monthly", 22 * float.Parse(textBox10.Text));
                        }
                        else if (radioButton3.Checked)
                        {
                            command.Parameters.AddWithValue("@rate", 3);
                            command.Parameters.AddWithValue("@salary_monthly", float.Parse(textBox10.Text));
                        }

                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                Action<Control.ControlCollection> func = null;

                func = (controls) =>
                {
                    foreach (Control control in controls)
                        if (control is TextBox)
                            (control as TextBox).Clear();
                        else
                            func(control.Controls);
                };

                func(Controls);
                MessageBox.Show("Successful Insertion.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
