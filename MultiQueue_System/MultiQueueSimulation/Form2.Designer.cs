namespace MultiQueueSimulation
{
    partial class Form2
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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.averageWaitingTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxQueueLengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waitingProbabilityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.performanceMeasuresBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.performanceMeasuresBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceMeasuresBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceMeasuresBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.averageWaitingTimeDataGridViewTextBoxColumn,
            this.maxQueueLengthDataGridViewTextBoxColumn,
            this.waitingProbabilityDataGridViewTextBoxColumn});
            this.dataGridView2.DataSource = this.performanceMeasuresBindingSource1;
            this.dataGridView2.Location = new System.Drawing.Point(172, 50);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(440, 337);
            this.dataGridView2.TabIndex = 0;

            // 
            // averageWaitingTimeDataGridViewTextBoxColumn
            // 
            this.averageWaitingTimeDataGridViewTextBoxColumn.DataPropertyName = "AverageWaitingTime";
            this.averageWaitingTimeDataGridViewTextBoxColumn.HeaderText = "AverageWaitingTime";
            this.averageWaitingTimeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.averageWaitingTimeDataGridViewTextBoxColumn.Name = "averageWaitingTimeDataGridViewTextBoxColumn";
            this.averageWaitingTimeDataGridViewTextBoxColumn.Width = 125;
            // 
            // maxQueueLengthDataGridViewTextBoxColumn
            // 
            this.maxQueueLengthDataGridViewTextBoxColumn.DataPropertyName = "MaxQueueLength";
            this.maxQueueLengthDataGridViewTextBoxColumn.HeaderText = "MaxQueueLength";
            this.maxQueueLengthDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.maxQueueLengthDataGridViewTextBoxColumn.Name = "maxQueueLengthDataGridViewTextBoxColumn";
            this.maxQueueLengthDataGridViewTextBoxColumn.Width = 125;
            // 
            // waitingProbabilityDataGridViewTextBoxColumn
            // 
            this.waitingProbabilityDataGridViewTextBoxColumn.DataPropertyName = "WaitingProbability";
            this.waitingProbabilityDataGridViewTextBoxColumn.HeaderText = "WaitingProbability";
            this.waitingProbabilityDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.waitingProbabilityDataGridViewTextBoxColumn.Name = "waitingProbabilityDataGridViewTextBoxColumn";
            this.waitingProbabilityDataGridViewTextBoxColumn.Width = 125;
            // 
            // performanceMeasuresBindingSource1
            // 
            this.performanceMeasuresBindingSource1.DataSource = typeof(MultiQueueModels.PerformanceMeasures);
            // 
            // performanceMeasuresBindingSource
            // 
            this.performanceMeasuresBindingSource.DataSource = typeof(MultiQueueModels.PerformanceMeasures);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceMeasuresBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceMeasuresBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource performanceMeasuresBindingSource;
        private System.Windows.Forms.BindingSource performanceMeasuresBindingSource1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn averageWaitingTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxQueueLengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn waitingProbabilityDataGridViewTextBoxColumn;
    }
}