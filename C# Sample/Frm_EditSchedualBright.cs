using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSharpTest
{
    public partial class Frm_EditSchedualBright : Form
    {
        public NP_BRIGHTADJUST_INFO BrightInfo
        {
            get 
            {
                NP_BRIGHTADJUST_INFO brightInfo = new NP_BRIGHTADJUST_INFO();
                brightInfo.AdjustTime = new NP_TIMESPAN();
                brightInfo.AdjustTime.Hour = dateTimePicker_AdjustTime.Value.Hour;
                brightInfo.AdjustTime.Minute = dateTimePicker_AdjustTime.Value.Minute;
                brightInfo.AdjustTime.Second = dateTimePicker_AdjustTime.Value.Second;

                brightInfo.BrightValue = Convert.ToByte(numericUpDown_Bright.Value);
                return brightInfo;
            }
        }
        public Frm_EditSchedualBright(NP_BRIGHTADJUST_INFO brightInfo)
        {
            InitializeComponent();

            dateTimePicker_AdjustTime.Value = new DateTime(2013, 12, 24,
                                                           brightInfo.AdjustTime.Hour,
                                                           brightInfo.AdjustTime.Minute,
                                                           brightInfo.AdjustTime.Second);
            numericUpDown_Bright.Value = brightInfo.BrightValue;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}