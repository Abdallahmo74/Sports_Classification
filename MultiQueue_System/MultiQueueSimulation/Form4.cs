using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiQueueModels;
using MultiQueueTesting;

namespace MultiQueueSimulation
{
    public partial class Form4 : Form
    {
        List<Server> Servers1 = new List<Server>();
        List<SimulationCase> sim = new List<SimulationCase>();
        public Form4()
        {
            InitializeComponent();
            SimulationSystem system = new SimulationSystem();
            PerformanceMeasures PerformanceMeasures = new PerformanceMeasures();
            system.AllSystem(ref system, ref PerformanceMeasures);
            Servers1 = system.Servers;
            sim = system.SimulationTable;
        }

     

    

    

        private void Form4_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Servers1.Count; i++)
            {
                comboBox1.Items.Add(i+1);
            }
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int j = int.Parse(comboBox1.Text) ;
            for (int i = 0; i < Servers1[j].X.Count; i++)
            {
                chart1.Series["Server"].Points.AddXY(Servers1[j].X[i], Servers1[j].y[i]);
            }
        }
    }
}
