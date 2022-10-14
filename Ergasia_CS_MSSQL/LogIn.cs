using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ergasia_CS_MSSQL
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-3QILQ20\\SQLEXPRESS;Initial Catalog=ErgasiaDB;Integrated Security=True";
        String username, password;
        SqlConnection con;

        private void btn_conect_Click(object sender, EventArgs e)
        {
            username = txt_user.Text;
            password = txt_pass.Text;

            try
            {
                con = new SqlConnection(conString);
                con.Open();
                String query = "SELECT * FROM tbl_sysAdmins WHERE username = '" + txt_user.Text + "' AND password = '" +txt_pass.Text+ "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if(dtable.Rows.Count > 0)
                {
                    MessageBox.Show("Connection established!");
                    this.Hide();
                    new HomeForm().Show();
                }
                else
                {
                    MessageBox.Show("Invalid credentials.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_user.Clear();
                    txt_pass.Clear();
                    txt_user.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void LogIn_Load(object sender, EventArgs e)
        {
            txt_user.Select();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if(con != null && con.State == ConnectionState.Open)
            {
                con.Close();
            }
            Application.Exit();
        }

        private void txt_user_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
