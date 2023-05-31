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
    public partial class Form1 : Form
    {
        string ordb = "Data Source=ORCL;User Id=hr;Password=hr;";
        OracleConnection conn;
        int Payment_method;
        string PayDate = DateTime.Now.ToString();
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();

            // from (cities)
            OracleCommand cmd1 = new OracleCommand();
            cmd1.Connection = conn;
            cmd1.CommandText = "GETSTARTINGPOINT";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("dest", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox1.Items.Add(dr1[0]);

            }
            dr1.Close();



            //Customer ID
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select CUSTOMAR_ID from customer";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr[0]);

            }
            dr.Close();



            //set payment id





        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //To (cities)
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select DESTINATION from trip where STARTING_POINT =:s";
            cmd2.Parameters.Add("s", comboBox1.SelectedItem.ToString());
            cmd2.CommandType = CommandType.Text;
            OracleDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox2.Items.Add(dr2[0]);

            }
            dr2.Close();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //avaliable dates
            OracleCommand cmd4 = new OracleCommand();
            cmd4.Connection = conn;
            cmd4.CommandText = "select TRIP_DATE from trip where STARTING_POINT=:s and DESTINATION=:d";
            cmd4.CommandType = CommandType.Text;
            cmd4.Parameters.Add("s", comboBox1.SelectedItem.ToString());
            cmd4.Parameters.Add("d", comboBox2.SelectedItem.ToString());

            OracleDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                comboBox3.Items.Add(Convert.ToDateTime(dr4[0]));
            }
            dr4.Close();
        }
        int PaymentID = 1;
        private void button1_Click(object sender, EventArgs e)
        {
            // Filling Payment Table
            int maxid, Newid;
            //set payment id
            OracleCommand cmd7 = new OracleCommand();
            cmd7.Connection = conn;
            cmd7.CommandText = "GetPaymentID";
            cmd7.CommandType = CommandType.StoredProcedure;
            cmd7.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            cmd7.ExecuteNonQuery();
            try
            {
                maxid = Convert.ToInt32(cmd7.Parameters["id"].Value.ToString());
                Newid = maxid + 1;
                PaymentID = Newid;
            }
            catch
            {
                Newid = 1;
            }

            if (radioButton1.Checked)
            {
                //Fawry

                Payment_method = 1;
                OracleCommand cmd6 = new OracleCommand();
                cmd6.Connection = conn;
                cmd6.CommandText = "InsertPayment";
                cmd6.CommandType = CommandType.StoredProcedure;
                cmd6.Parameters.Add("PI", Newid);
                cmd6.Parameters.Add("PM", Payment_method);
                cmd6.Parameters.Add("PA", Convert.ToInt32(textBox1.Text));
                cmd6.Parameters.Add("PD", Convert.ToDateTime(PayDate));
                cmd6.ExecuteNonQuery();
            }
            else if (radioButton2.Checked)
            {
                //Visa
                Payment_method = 2;
                OracleCommand cmd8 = new OracleCommand();
                cmd8.Connection = conn;
                cmd8.CommandText = "InsertPayment";
                cmd8.CommandType = CommandType.StoredProcedure;
                cmd8.Parameters.Add("PI", Newid);
                cmd8.Parameters.Add("PM", Payment_method);
                cmd8.Parameters.Add("PA", Convert.ToInt32(textBox1.Text));
                cmd8.Parameters.Add("PD", Convert.ToDateTime(PayDate));
                cmd8.ExecuteNonQuery();
            }


            //Filling Ticket info

            //Get Ticket ID
            int Maximum, NewID;
            OracleCommand cmd9 = new OracleCommand();
            cmd9.Connection = conn;
            cmd9.CommandText = "GETTICKETID";
            cmd9.CommandType = CommandType.StoredProcedure;
            cmd9.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            cmd9.ExecuteNonQuery();
            try
            {
                Maximum = Convert.ToInt32(cmd9.Parameters["id"].Value.ToString());
                NewID = Maximum + 1;
            }
            catch
            {
                NewID = 1;
            }

            //Get Trip ID
            int TripID = 1;
            OracleCommand c11 = new OracleCommand();
            c11.Connection = conn;
            c11.CommandText = "select Trip_id from trip where STARTING_POINT=:s and DESTINATION=:d and COST =:c and TRIP_DATE =:T";
            c11.CommandType = CommandType.Text;
            c11.Parameters.Add("s", comboBox1.SelectedItem.ToString());
            c11.Parameters.Add("d", comboBox2.SelectedItem.ToString());
            c11.Parameters.Add("c", Convert.ToInt32( textBox1.Text));
            c11.Parameters.Add("T", Convert.ToDateTime (comboBox3.SelectedItem.ToString()));
            OracleDataReader dr11 = c11.ExecuteReader();
            while (dr11.Read())
            {
                TripID = Convert.ToInt32(dr11[0]);
            }
            dr11.Close();




            //Get Bus ID
            int BusID=1;
            OracleCommand c12 = new OracleCommand();
            c12.Connection = conn;
            c12.CommandText = "select Bus_id from trip where Trip_id =:id";
            c12.CommandType = CommandType.Text;
            c12.Parameters.Add("id", TripID);
            OracleDataReader dr12 = c12.ExecuteReader();
            while (dr12.Read())
            {
                BusID = Convert.ToInt32(dr12[0]);
            }
            dr12.Close();


            OracleCommand cmd10 = new OracleCommand();
            cmd10.Connection = conn;
            cmd10.CommandText = "INSERTTICKETINFO";
            cmd10.CommandType = CommandType.StoredProcedure;
            cmd10.Parameters.Add("CI", Convert.ToInt32( comboBox4.SelectedItem.ToString()));
            cmd10.Parameters.Add("BI", BusID);
            cmd10.Parameters.Add("TI", TripID);
            cmd10.Parameters.Add("PI", PaymentID);
            cmd10.Parameters.Add("TID", NewID);
            cmd10.ExecuteNonQuery();



            MessageBox.Show("The reservation is Completed, Your Ticket ID =  " + NewID);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cost
            OracleCommand cmd5 = new OracleCommand();
            cmd5.Connection = conn;
            cmd5.CommandText = "select COST from trip where STARTING_POINT=:s and DESTINATION=:d ";
            cmd5.CommandType = CommandType.Text;
            cmd5.Parameters.Add("s", comboBox1.SelectedItem.ToString());
            cmd5.Parameters.Add("d", comboBox2.SelectedItem.ToString());
            OracleDataReader dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                textBox1.Text = dr5[0].ToString();
            }
            dr5.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu M = new Menu();
            M.Show();
        }
    }


}
