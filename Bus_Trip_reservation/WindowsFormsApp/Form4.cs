using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form4 : Form
    {
        CrystalReport2 cr2;
        public Form4()
        {
            InitializeComponent();
        }

        private void crystalReportViewer2_Load(object sender, EventArgs e)
        {
            cr2 = new CrystalReport2();
            crystalReportViewer2.ReportSource = cr2;

        }
    }
}
