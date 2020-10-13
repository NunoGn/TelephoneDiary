using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TelephoneDiary
{


    public partial class Contacts : Form
    {

        SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Contacts;Integrated Security=True");

        public Contacts()

        {
            InitializeComponent();
        }
        private void Contacts_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void btNew_Click(object sender, EventArgs e) //button1
        {
            txtFirstName.Text = "";
            txtLastName.Clear();
            txtMobile.Text = "";
            txtEmail.Clear();
            cbxCategory.SelectedIndex = -1;
            txtFirstName.Focus();
        }

        private void btInsert_Click(object sender, EventArgs e) //button2
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Contact (FirstName,LastName,Mobile,Email,Category)
                        values('" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtMobile.Text + "','" + txtEmail.Text + "','" + cbxCategory.Text + "')", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Successfully Saved!");
            Display();
        }

        void Display()
        {
            SqlDataAdapter adapt = new SqlDataAdapter("Select * from Contact", connection);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = item["FirstName"].ToString();
                dataGridView.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Contact WHERE(Mobile='" + txtMobile.Text + "')", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Delete Successfully!");
            Display();

        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            txtFirstName.Text = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
            txtLastName.Text = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtMobile.Text = dataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtEmail.Text = dataGridView.SelectedRows[0].Cells[3].Value.ToString();
            cbxCategory.Text = dataGridView.SelectedRows[0].Cells[4].Value.ToString();
        }


        private void btUpdate_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE Contact
                                    SET FirstName='" + txtFirstName.Text + "', LastName='" + txtLastName.Text + "'," +
                                    "Mobile='" + txtMobile.Text + "',Email='" + txtEmail.Text + "',Category='" + cbxCategory.Text + "'" +
                                    " WHERE (Mobile='" + txtMobile.Text + "')", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Update Successfully!");
            Display();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter adapt = new SqlDataAdapter("Select * from Contact WHERE Mobile LIKE '" + txtSearch.Text + "%' OR FirstName LIKE '%" + txtSearch.Text + "%'", connection);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = item["FirstName"].ToString();
                dataGridView.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }
    }
}


