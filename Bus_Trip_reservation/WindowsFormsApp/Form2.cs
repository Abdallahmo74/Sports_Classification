using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace WindowsFormsApp
{
    public partial class Form2 : Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet Ds;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {



        }

 

        private void button3_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(Ds.Tables[0]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu M = new Menu();
            M.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string constr = "Data Source=ORCL;User Id=hr;Password=hr;";

            if ((textBox1.Text == "Aser@gmail.com") && (textBox2.Text == "Manager"))
            {
                string cmdstr = "select * from trip where Manager_ID= 1";



                adapter = new OracleDataAdapter(cmdstr, constr);
                Ds = new DataSet();
                adapter.Fill(Ds);
                dataGridView1.DataSource = Ds.Tables[0];
            }
            else if ((textBox1.Text == "Adam@gmail.com") && (textBox2.Text == "Manager"))
            {
                string cmdstr = "select * from trip where Manager_ID= 2";



                adapter = new OracleDataAdapter(cmdstr, constr);
                Ds = new DataSet();
                adapter.Fill(Ds);
                dataGridView1.DataSource = Ds.Tables[0];
            }
            else
            {
                MessageBox.Show(" Invalid User name or Password ");
            }

        }

       
    }
}