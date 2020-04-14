namespace CSharpTest
{
    partial class Frm_EditBDPowerAutoInfo
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
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel_SepecialDate = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_EndDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_StartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_IsSpecialDate = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel_ValidWeekDay = new System.Windows.Forms.Panel();
            this.checkBox_Sunday = new System.Windows.Forms.CheckBox();
            this.checkBox_Monday = new System.Windows.Forms.CheckBox();
            this.checkBox_Saturday = new System.Windows.Forms.CheckBox();
            this.checkBox_Tuesday = new System.Windows.Forms.CheckBox();
            this.checkBox_Friday = new System.Windows.Forms.CheckBox();
            this.checkBox_Wednesday = new System.Windows.Forms.CheckBox();
            this.checkBox_Thursday = new System.Windows.Forms.CheckBox();
            this.groupBox_CtrlTime = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker_CloseTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_OpenTime = new System.Windows.Forms.DateTimePicker();
            this.label = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel_SepecialDate.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel_ValidWeekDay.SuspendLayout();
            this.groupBox_CtrlTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(256, 321);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 35);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(119, 321);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 35);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel_SepecialDate);
            this.groupBox1.Controls.Add(this.checkBox_IsSpecialDate);
            this.groupBox1.Location = new System.Drawing.Point(22, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 84);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sepecial date";
            // 
            // panel_SepecialDate
            // 
            this.panel_SepecialDate.Controls.Add(this.label2);
            this.panel_SepecialDate.Controls.Add(this.dateTimePicker_EndDate);
            this.panel_SepecialDate.Controls.Add(this.dateTimePicker_StartDate);
            this.panel_SepecialDate.Controls.Add(this.label1);
            this.panel_SepecialDate.Enabled = false;
            this.panel_SepecialDate.Location = new System.Drawing.Point(34, 42);
            this.panel_SepecialDate.Name = "panel_SepecialDate";
            this.panel_SepecialDate.Size = new System.Drawing.Size(343, 32);
            this.panel_SepecialDate.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "to";
            // 
            // dateTimePicker_EndDate
            // 
            this.dateTimePicker_EndDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_EndDate.Location = new System.Drawing.Point(223, 3);
            this.dateTimePicker_EndDate.Name = "dateTimePicker_EndDate";
            this.dateTimePicker_EndDate.Size = new System.Drawing.Size(114, 21);
            this.dateTimePicker_EndDate.TabIndex = 6;
            // 
            // dateTimePicker_StartDate
            // 
            this.dateTimePicker_StartDate.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_StartDate.Location = new System.Drawing.Point(53, 3);
            this.dateTimePicker_StartDate.Name = "dateTimePicker_StartDate";
            this.dateTimePicker_StartDate.Size = new System.Drawing.Size(114, 21);
            this.dateTimePicker_StartDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "From";
            // 
            // checkBox_IsSpecialDate
            // 
            this.checkBox_IsSpecialDate.AutoSize = true;
            this.checkBox_IsSpecialDate.Location = new System.Drawing.Point(14, 25);
            this.checkBox_IsSpecialDate.Name = "checkBox_IsSpecialDate";
            this.checkBox_IsSpecialDate.Size = new System.Drawing.Size(114, 16);
            this.checkBox_IsSpecialDate.TabIndex = 0;
            this.checkBox_IsSpecialDate.Text = "Is special date";
            this.checkBox_IsSpecialDate.UseVisualStyleBackColor = true;
            this.checkBox_IsSpecialDate.CheckedChanged += new System.EventHandler(this.checkBox_IsSpecialDate_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel_ValidWeekDay);
            this.groupBox2.Location = new System.Drawing.Point(22, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(396, 102);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Valid week day";
            // 
            // panel_ValidWeekDay
            // 
            this.panel_ValidWeekDay.Controls.Add(this.checkBox_Sunday);
            this.panel_ValidWeekDay.Controls.Add(this.checkBox_Monday);
            this.panel_ValidWeekDay.Controls.Add(this.checkBox_Saturday);
            this.panel_ValidWeekDay.Controls.Add(this.checkBox_Tuesday);
            this.panel_ValidWeekDay.Controls.Add(this.checkBox_Friday);
            this.panel_ValidWeekDay.Controls.Add(this.checkBox_Wednesday);
            this.panel_ValidWeekDay.Controls.Add(this.checkBox_Thursday);
            this.panel_ValidWeekDay.Location = new System.Drawing.Point(31, 28);
            this.panel_ValidWeekDay.Name = "panel_ValidWeekDay";
            this.panel_ValidWeekDay.Size = new System.Drawing.Size(336, 63);
            this.panel_ValidWeekDay.TabIndex = 6;
            // 
            // checkBox_Sunday
            // 
            this.checkBox_Sunday.AutoSize = true;
            this.checkBox_Sunday.Checked = true;
            this.checkBox_Sunday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Sunday.Location = new System.Drawing.Point(165, 42);
            this.checkBox_Sunday.Name = "checkBox_Sunday";
            this.checkBox_Sunday.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Sunday.TabIndex = 8;
            this.checkBox_Sunday.Text = "Sunday";
            this.checkBox_Sunday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Monday
            // 
            this.checkBox_Monday.AutoSize = true;
            this.checkBox_Monday.Checked = true;
            this.checkBox_Monday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Monday.Location = new System.Drawing.Point(3, 8);
            this.checkBox_Monday.Name = "checkBox_Monday";
            this.checkBox_Monday.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Monday.TabIndex = 2;
            this.checkBox_Monday.Text = "Monday";
            this.checkBox_Monday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Saturday
            // 
            this.checkBox_Saturday.AutoSize = true;
            this.checkBox_Saturday.Location = new System.Drawing.Point(79, 42);
            this.checkBox_Saturday.Name = "checkBox_Saturday";
            this.checkBox_Saturday.Size = new System.Drawing.Size(72, 16);
            this.checkBox_Saturday.TabIndex = 7;
            this.checkBox_Saturday.Text = "Saturday";
            this.checkBox_Saturday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Tuesday
            // 
            this.checkBox_Tuesday.AutoSize = true;
            this.checkBox_Tuesday.Checked = true;
            this.checkBox_Tuesday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Tuesday.Location = new System.Drawing.Point(79, 8);
            this.checkBox_Tuesday.Name = "checkBox_Tuesday";
            this.checkBox_Tuesday.Size = new System.Drawing.Size(66, 16);
            this.checkBox_Tuesday.TabIndex = 3;
            this.checkBox_Tuesday.Text = "Tuesday";
            this.checkBox_Tuesday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Friday
            // 
            this.checkBox_Friday.AutoSize = true;
            this.checkBox_Friday.Checked = true;
            this.checkBox_Friday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Friday.Location = new System.Drawing.Point(3, 42);
            this.checkBox_Friday.Name = "checkBox_Friday";
            this.checkBox_Friday.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Friday.TabIndex = 6;
            this.checkBox_Friday.Text = "Friday";
            this.checkBox_Friday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Wednesday
            // 
            this.checkBox_Wednesday.AutoSize = true;
            this.checkBox_Wednesday.Checked = true;
            this.checkBox_Wednesday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Wednesday.Location = new System.Drawing.Point(165, 8);
            this.checkBox_Wednesday.Name = "checkBox_Wednesday";
            this.checkBox_Wednesday.Size = new System.Drawing.Size(78, 16);
            this.checkBox_Wednesday.TabIndex = 4;
            this.checkBox_Wednesday.Text = "Wednesday";
            this.checkBox_Wednesday.UseVisualStyleBackColor = true;
            // 
            // checkBox_Thursday
            // 
            this.checkBox_Thursday.AutoSize = true;
            this.checkBox_Thursday.Checked = true;
            this.checkBox_Thursday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Thursday.Location = new System.Drawing.Point(254, 8);
            this.checkBox_Thursday.Name = "checkBox_Thursday";
            this.checkBox_Thursday.Size = new System.Drawing.Size(72, 16);
            this.checkBox_Thursday.TabIndex = 5;
            this.checkBox_Thursday.Text = "Thursday";
            this.checkBox_Thursday.UseVisualStyleBackColor = true;
            // 
            // groupBox_CtrlTime
            // 
            this.groupBox_CtrlTime.Controls.Add(this.label3);
            this.groupBox_CtrlTime.Controls.Add(this.dateTimePicker_CloseTime);
            this.groupBox_CtrlTime.Controls.Add(this.dateTimePicker_OpenTime);
            this.groupBox_CtrlTime.Controls.Add(this.label);
            this.groupBox_CtrlTime.Location = new System.Drawing.Point(22, 242);
            this.groupBox_CtrlTime.Name = "groupBox_CtrlTime";
            this.groupBox_CtrlTime.Size = new System.Drawing.Size(396, 68);
            this.groupBox_CtrlTime.TabIndex = 6;
            this.groupBox_CtrlTime.TabStop = false;
            this.groupBox_CtrlTime.Text = "CtrlTime";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "Close time:";
            // 
            // dateTimePicker_CloseTime
            // 
            this.dateTimePicker_CloseTime.CustomFormat = "HH:mm";
            this.dateTimePicker_CloseTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_CloseTime.Location = new System.Drawing.Point(298, 29);
            this.dateTimePicker_CloseTime.Name = "dateTimePicker_CloseTime";
            this.dateTimePicker_CloseTime.Size = new System.Drawing.Size(82, 21);
            this.dateTimePicker_CloseTime.TabIndex = 10;
            // 
            // dateTimePicker_OpenTime
            // 
            this.dateTimePicker_OpenTime.CustomFormat = "HH:mm";
            this.dateTimePicker_OpenTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_OpenTime.Location = new System.Drawing.Point(97, 32);
            this.dateTimePicker_OpenTime.Name = "dateTimePicker_OpenTime";
            this.dateTimePicker_OpenTime.Size = new System.Drawing.Size(92, 21);
            this.dateTimePicker_OpenTime.TabIndex = 8;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(26, 38);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(65, 12);
            this.label.TabIndex = 9;
            this.label.Text = "Open time:";
            // 
            // Frm_EditBDPowerAutoInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(443, 378);
            this.Controls.Add(this.groupBox_CtrlTime);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_EditBDPowerAutoInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditBDPowerAutoInfo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_SepecialDate.ResumeLayout(false);
            this.panel_SepecialDate.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel_ValidWeekDay.ResumeLayout(false);
            this.panel_ValidWeekDay.PerformLayout();
            this.groupBox_CtrlTime.ResumeLayout(false);
            this.groupBox_CtrlTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_IsSpecialDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_StartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_EndDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_Tuesday;
        private System.Windows.Forms.CheckBox checkBox_Monday;
        private System.Windows.Forms.CheckBox checkBox_Wednesday;
        private System.Windows.Forms.CheckBox checkBox_Sunday;
        private System.Windows.Forms.CheckBox checkBox_Saturday;
        private System.Windows.Forms.CheckBox checkBox_Friday;
        private System.Windows.Forms.CheckBox checkBox_Thursday;
        private System.Windows.Forms.Panel panel_ValidWeekDay;
        private System.Windows.Forms.Panel panel_SepecialDate;
        private System.Windows.Forms.GroupBox groupBox_CtrlTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker_CloseTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker_OpenTime;
        private System.Windows.Forms.Label label;
    }
}