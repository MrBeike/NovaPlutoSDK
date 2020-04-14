namespace CSharpTest
{
    partial class Frm_EditSchedualBright
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_AdjustTime = new System.Windows.Forms.DateTimePicker();
            this.numericUpDown_Bright = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Bright)).BeginInit();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(72, 110);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 35);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(209, 110);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 35);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(50, 27);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(77, 12);
            this.label.TabIndex = 2;
            this.label.Text = "Adjust time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bright:";
            // 
            // dateTimePicker_AdjustTime
            // 
            this.dateTimePicker_AdjustTime.CustomFormat = "HH:mm";
            this.dateTimePicker_AdjustTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_AdjustTime.Location = new System.Drawing.Point(168, 27);
            this.dateTimePicker_AdjustTime.Name = "dateTimePicker_AdjustTime";
            this.dateTimePicker_AdjustTime.ShowUpDown = true;
            this.dateTimePicker_AdjustTime.Size = new System.Drawing.Size(126, 21);
            this.dateTimePicker_AdjustTime.TabIndex = 6;
            // 
            // numericUpDown_Bright
            // 
            this.numericUpDown_Bright.Location = new System.Drawing.Point(164, 67);
            this.numericUpDown_Bright.Name = "numericUpDown_Bright";
            this.numericUpDown_Bright.Size = new System.Drawing.Size(130, 21);
            this.numericUpDown_Bright.TabIndex = 7;
            // 
            // Frm_EditSchedualBright
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(351, 174);
            this.Controls.Add(this.numericUpDown_Bright);
            this.Controls.Add(this.dateTimePicker_AdjustTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_EditSchedualBright";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditSchedualBright";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Bright)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_AdjustTime;
        private System.Windows.Forms.NumericUpDown numericUpDown_Bright;
    }
}