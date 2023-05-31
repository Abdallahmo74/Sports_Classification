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
    public partial class Form1 : Form
    {

        List<SimulationCase> sim = new List<SimulationCase>();
        
        public Form1()
        {
            InitializeComponent();
            SimulationSystem system = new SimulationSystem();
            PerformanceMeasures PerformanceMeasures = new PerformanceMeasures();
            system.AllSystem(ref system, ref PerformanceMeasures);
            sim = system.SimulationTable;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = sim;
        }

   
    }
}
