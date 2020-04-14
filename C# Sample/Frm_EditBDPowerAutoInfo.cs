using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CSharpTest
{
    public partial class Frm_EditBDPowerAutoInfo : Form
    {
        public NP_BDPOWER_AUTOTIME_INFO CtrlInfo
        {
            get
            {
                NP_BDPOWER_AUTOTIME_INFO ctrlInfo = new NP_BDPOWER_AUTOTIME_INFO();
                ctrlInfo.IsSpecialDate = checkBox_IsSpecialDate.Checked;
                if (checkBox_IsSpecialDate.Checked)
                {
                    ctrlInfo.StartDate = new NP_DATE();
                    ctrlInfo.StartDate.Year = dateTimePicker_StartDate.Value.Year;
                    ctrlInfo.StartDate.Month = dateTimePicker_StartDate.Value.Month;
                    ctrlInfo.StartDate.Day = dateTimePicker_StartDate.Value.Day;

                    ctrlInfo.StopDate = new NP_DATE();
                    ctrlInfo.StopDate.Year = dateTimePicker_EndDate.Value.Year;
                    ctrlInfo.StopDate.Month = dateTimePicker_EndDate.Value.Month;
                    ctrlInfo.StopDate.Day = dateTimePicker_EndDate.Value.Day;
                }
                ctrlInfo.WeekDayIsValid = new bool[7];
                ctrlInfo.WeekDayIsValid[1] = checkBox_Monday.Checked;
                ctrlInfo.WeekDayIsValid[2] = checkBox_Tuesday.Checked;
                ctrlInfo.WeekDayIsValid[3] = checkBox_Wednesday.Checked;
                ctrlInfo.WeekDayIsValid[4] = checkBox_Thursday.Checked;
                ctrlInfo.WeekDayIsValid[5] = checkBox_Friday.Checked;
                ctrlInfo.WeekDayIsValid[6] = checkBox_Saturday.Checked;
                ctrlInfo.WeekDayIsValid[0] = checkBox_Sunday.Checked;

                ctrlInfo.OpenTime = new NP_TIMESPAN();
                ctrlInfo.OpenTime.Hour = dateTimePicker_OpenTime.Value.Hour;
                ctrlInfo.OpenTime.Minute = dateTimePicker_OpenTime.Value.Minute;
                ctrlInfo.OpenTime.Second = dateTimePicker_OpenTime.Value.Second;

                ctrlInfo.CloseTime = new NP_TIMESPAN();
                ctrlInfo.CloseTime.Hour = dateTimePicker_CloseTime.Value.Hour;
                ctrlInfo.CloseTime.Minute = dateTimePicker_CloseTime.Value.Minute;
                ctrlInfo.CloseTime.Second = dateTimePicker_CloseTime.Value.Second;
                return ctrlInfo;
            }
        }
        public Frm_EditBDPowerAutoInfo(NP_BDPOWER_AUTOTIME_INFO ctrlInfo)
        {
            InitializeComponent();
            checkBox_IsSpecialDate.Checked = ctrlInfo.IsSpecialDate;
            if (ctrlInfo.IsSpecialDate)
            {
                dateTimePicker_StartDate.Value = new DateTime(ctrlInfo.StartDate.Year,
                                                              ctrlInfo.StartDate.Month,
                                                              ctrlInfo.StartDate.Day,
                                                              0, 0 , 0);
                dateTimePicker_EndDate.Value = new DateTime(ctrlInfo.StopDate.Year,
                                                            ctrlInfo.StopDate.Month,
                                                            ctrlInfo.StopDate.Day,
                                                            0, 0, 0);
            }
            if (ctrlInfo.WeekDayIsValid == null)
            {
                ctrlInfo.WeekDayIsValid = new bool[7];
            }
            checkBox_Monday.Checked = ctrlInfo.WeekDayIsValid[1];
            checkBox_Tuesday.Checked = ctrlInfo.WeekDayIsValid[2];
            checkBox_Wednesday.Checked = ctrlInfo.WeekDayIsValid[3];
            checkBox_Thursday.Checked = ctrlInfo.WeekDayIsValid[4];
            checkBox_Friday.Checked = ctrlInfo.WeekDayIsValid[5];
            checkBox_Saturday.Checked = ctrlInfo.WeekDayIsValid[6];
            checkBox_Sunday.Checked = ctrlInfo.WeekDayIsValid[0];

            dateTimePicker_OpenTime.Value = new DateTime(2013, 12, 24,
                                                         ctrlInfo.OpenTime.Hour,
                                                         ctrlInfo.OpenTime.Minute,
                                                         ctrlInfo.OpenTime.Second);
            dateTimePicker_CloseTime.Value = new DateTime(2013, 12, 24,
                                                          ctrlInfo.CloseTime.Hour,
                                                          ctrlInfo.CloseTime.Minute,
                                                          ctrlInfo.CloseTime.Second);
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

        private void checkBox_IsSpecialDate_CheckedChanged(object sender, EventArgs e)
        {
            panel_SepecialDate.Enabled = checkBox_IsSpecialDate.Checked;
        }
    }
}