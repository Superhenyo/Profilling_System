using MySql.Data.MySqlClient;
using System;
using System.Data;


namespace Gundaway_profiling_system
{
    public partial class Form1 : Form
    {
        private sqlconnection SqlConnection;
        public Form1()
        {
            InitializeComponent();
            sqlconnection Sqlconnection = new sqlconnection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            startlogin();
        }

        private void startlogin()
        {
            string query = ("select * from account where username = '" + textBox1.Text + "' and password = '" + textBox2.Text + "' and accounttype ='" + comboBox1.Text + "'");
            DataTable dt = sqlconnection.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                timer1.Start();
            }
            else
            {
                MessageBox.Show("Invalid Credentials!");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            mainform mainform = new mainform();
            mainform.Show();
            this.Hide();
            timer1.Stop();
        }
    }
}