namespace MultiQueueSimulation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.simulationCaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.simulationSystemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.programBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.customerNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.randomInterArrivalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.interArrivalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrivalTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.randomServiceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.assignedServerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeInQueueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simulationCaseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simulationSystemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerNumberDataGridViewTextBoxColumn,
            this.randomInterArrivalDataGridViewTextBoxColumn,
            this.interArrivalDataGridViewTextBoxColumn,
            this.arrivalTimeDataGridViewTextBoxColumn,
            this.randomServiceDataGridViewTextBoxColumn,
            this.serviceTimeDataGridViewTextBoxColumn,
            this.assignedServerDataGridViewTextBoxColumn,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn,
            this.timeInQueueDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.simulationCaseBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1451, 398);
            this.dataGridView1.TabIndex = 0;
            // 
            // simulationCaseBindingSource
            // 
            this.simulationCaseBindingSource.DataSource = typeof(MultiQueueModels.SimulationCase);
            // 
            // simulationSystemBindingSource
            // 
            this.simulationSystemBindingSource.DataSource = typeof(MultiQueueModels.SimulationSystem);
            // 
            // programBindingSource
            // 
            this.programBindingSource.DataSource = typeof(MultiQueueSimulation.Program);
            // 
            // programBindingSource1
            // 
            this.programBindingSource1.DataSource = typeof(MultiQueueSimulation.Program);
            // 
            // customerNumberDataGridViewTextBoxColumn
            // 
            this.customerNumberDataGridViewTextBoxColumn.DataPropertyName = "CustomerNumber";
            this.customerNumberDataGridViewTextBoxColumn.HeaderText = "CustomerNumber";
            this.customerNumberDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.customerNumberDataGridViewTextBoxColumn.Name = "customerNumberDataGridViewTextBoxColumn";
            this.customerNumberDataGridViewTextBoxColumn.Width = 125;
            // 
            // randomInterArrivalDataGridViewTextBoxColumn
            // 
            this.randomInterArrivalDataGridViewTextBoxColumn.DataPropertyName = "RandomInterArrival";
            this.randomInterArrivalDataGridViewTextBoxColumn.HeaderText = "RandomInterArrival";
            this.randomInterArrivalDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.randomInterArrivalDataGridViewTextBoxColumn.Name = "randomInterArrivalDataGridViewTextBoxColumn";
            this.randomInterArrivalDataGridViewTextBoxColumn.Width = 125;
            // 
            // interArrivalDataGridViewTextBoxColumn
            // 
            this.interArrivalDataGridViewTextBoxColumn.DataPropertyName = "InterArrival";
            this.interArrivalDataGridViewTextBoxColumn.HeaderText = "InterArrival";
            this.interArrivalDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.interArrivalDataGridViewTextBoxColumn.Name = "interArrivalDataGridViewTextBoxColumn";
            this.interArrivalDataGridViewTextBoxColumn.Width = 125;
            // 
            // arrivalTimeDataGridViewTextBoxColumn
            // 
            this.arrivalTimeDataGridViewTextBoxColumn.DataPropertyName = "ArrivalTime";
            this.arrivalTimeDataGridViewTextBoxColumn.HeaderText = "ArrivalTime";
            this.arrivalTimeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.arrivalTimeDataGridViewTextBoxColumn.Name = "arrivalTimeDataGridViewTextBoxColumn";
            this.arrivalTimeDataGridViewTextBoxColumn.Width = 125;
            // 
            // randomServiceDataGridViewTextBoxColumn
            // 
            this.randomServiceDataGridViewTextBoxColumn.DataPropertyName = "RandomService";
            this.randomServiceDataGridViewTextBoxColumn.HeaderText = "RandomService";
            this.randomServiceDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.randomServiceDataGridViewTextBoxColumn.Name = "randomServiceDataGridViewTextBoxColumn";
            this.randomServiceDataGridViewTextBoxColumn.Width = 125;
            // 
            // serviceTimeDataGridViewTextBoxColumn
            // 
            this.serviceTimeDataGridViewTextBoxColumn.DataPropertyName = "ServiceTime";
            this.serviceTimeDataGridViewTextBoxColumn.HeaderText = "ServiceTime";
            this.serviceTimeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.serviceTimeDataGridViewTextBoxColumn.Name = "serviceTimeDataGridViewTextBoxColumn";
            this.serviceTimeDataGridViewTextBoxColumn.Width = 125;
            // 
            // assignedServerDataGridViewTextBoxColumn
            // 
            this.assignedServerDataGridViewTextBoxColumn.DataPropertyName = "AssignedServerID";
            this.assignedServerDataGridViewTextBoxColumn.HeaderText = "AssignedServer";
            this.assignedServerDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.assignedServerDataGridViewTextBoxColumn.Name = "assignedServerDataGridViewTextBoxColumn";
            this.assignedServerDataGridViewTextBoxColumn.Width = 125;
            // 
            // startTimeDataGridViewTextBoxColumn
            // 
            this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.HeaderText = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
            this.startTimeDataGridViewTextBoxColumn.Width = 125;
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            this.endTimeDataGridViewTextBoxColumn.Width = 125;
            // 
            // timeInQueueDataGridViewTextBoxColumn
            // 
            this.timeInQueueDataGridViewTextBoxColumn.DataPropertyName = "TimeInQueue";
            this.timeInQueueDataGridViewTextBoxColumn.HeaderText = "TimeInQueue";
            this.timeInQueueDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.timeInQueueDataGridViewTextBoxColumn.Name = "timeInQueueDataGridViewTextBoxColumn";
            this.timeInQueueDataGridViewTextBoxColumn.Width = 125;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1533, 422);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simulationCaseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simulationSystemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource programBindingSource;
        private System.Windows.Forms.BindingSource simulationSystemBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource simulationCaseBindingSource;
        private System.Windows.Forms.BindingSource programBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn randomInterArrivalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn interArrivalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrivalTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn randomServiceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn assignedServerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeInQueueDataGridViewTextBoxColumn;
    }
}

