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
    public partial class Form2 : Form
    {

        List<PerformanceMeasures> Sys = new List<PerformanceMeasures>();
        
        

        public Form2()
        {
            InitializeComponent();
            SimulationSystem system = new SimulationSystem();
            PerformanceMeasures PerformanceMeasures = new PerformanceMeasures();
            List<PerformanceMeasures> Sys1 = new List<PerformanceMeasures>();
            system.AllSystem(ref system, ref PerformanceMeasures);
            Sys1.Add(PerformanceMeasures);
            Sys1.Add(system.PerformanceMeasures);
            Sys = Sys1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView2.DataSource = Sys;
        }

       
    }
}
