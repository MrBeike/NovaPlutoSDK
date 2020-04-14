using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Nova.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Diagnostics;


namespace CSharpTest
{
    public partial class NovaPlutoSDKTest : Form
    {
        [DllImport("Kernel32.dll", EntryPoint = "RtlZeroMemory", SetLastError = false)]
        private static extern void ZeroMemory(IntPtr dest, int size);

        private static int s_MsgID = 0x800;
        private static string s_MonitorInfoSaveFolder = "";

        private List<IntPtr> _playProgramPtrList = null;
        private List<IntPtr> _playLogPtrList = null;
        private List<IntPtr> _monitorFilePtrList = null;
        private List<NP_CARD_INFO> _cardInfoList = null;

        public NovaPlutoSDKTest()
        {
            InitializeComponent();
            _playProgramPtrList = new List<IntPtr>();
            _playLogPtrList = new List<IntPtr>();
            _monitorFilePtrList = new List<IntPtr>();
            _cardInfoList = new List<NP_CARD_INFO>();
            s_MonitorInfoSaveFolder = Application.StartupPath + @"\MonitorFile\";
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == s_MsgID)
            {
                int ptr = (int)m.WParam;
                string result = "";
                string filePath = "";
                switch (ptr)
                {
                    case (int)CustomMessageType.WM_CardDisconnect:
                        string cardID = Marshal.PtrToStringBSTR(m.LParam);
                        DisconnectCardList(cardID);
                        Debug.WriteLine(cardID+" -disconnected");
                        break;
                    case (int)CustomMessageType.WM_CardInfo:
                        NP_CARD_INFO cardInfo;
                        cardInfo = (NP_CARD_INFO)Marshal.PtrToStructure(m.LParam, typeof(NP_CARD_INFO));
                        CompleteConnectCardList(cardInfo);
                        GetCardInfo(cardInfo);
                        Debug.WriteLine(cardInfo.ID + " -connected");
                        break;
                    case (int)CustomMessageType.WM_GetPlayLogError:
                        result = "Get playlog failed,net result is:" + (int)m.LParam;
                        EnableGetInfoPercent(false, Color.Red, result);
                        break;
                    case (int)CustomMessageType.WM_GetPlayLogOK:
                        filePath = Marshal.PtrToStringBSTR(m.LParam);
                        result = "Get playlog success,log address is:\n" + filePath;
                        EnableGetInfoPercent(false, Color.Green, result);
                        break;
                    case (int)CustomMessageType.WM_SendInsertPlay:
                        result = "Insert play,net result is:" + (int)m.LParam;
                        EnableGetInfoPercent(false, Color.Green, result);
                        break;
                    case (int)CustomMessageType.WM_SendNotify:
                        result = "Insert notify,net result is:" + (int)m.LParam;
                        EnableGetInfoPercent(false, Color.Green, result);
                        break;
                    case (int)CustomMessageType.WM_SendPlayProgram:
                        result = "Set play program,net result is:" + (int)m.LParam;
                        EnableGetInfoPercent(false, Color.Green, result);
                        break;
                    case (int)CustomMessageType.WM_DetectPointFailed:
                        result = "Detect point failed, result is: " + (int)m.LParam;
                        UpdateDetectPointResult(false, new NP_DETECTPOINT_RESULT());
                        EnableGetInfoPercent(false, Color.Red, result);
                        break;
                    case (int)CustomMessageType.WM_DetectPointOK:
                        result = "Detect point OK!";
                        NP_DETECTPOINT_RESULT detectPointResult;
                        detectPointResult = (NP_DETECTPOINT_RESULT)Marshal.PtrToStructure(m.LParam, typeof(NP_DETECTPOINT_RESULT));
                        UpdateDetectPointResult(true, detectPointResult);
                        EnableGetInfoPercent(false, Color.Green, result);
                        break;
                    case (int)CustomMessageType.WM_RefreshMonitorFailed:
                        result = "Refresh monitor info failed, result is: " + (int)m.LParam;
                        UpdateRefreshMonitorRes(Color.Red, result);
                        EnableGetInfoPercent(false, Color.Red, result);
                        break;
                    case (int)CustomMessageType.WM_RefreshMonitorOK:
                        filePath = Marshal.PtrToStringBSTR(m.LParam);
                        result = "Time: " + DateTime.Now.ToLongTimeString() + ", File: " + filePath;
                        UpdateRefreshMonitorRes(Color.Green, result);
                        EnableGetInfoPercent(false, Color.Green, result);
                        break;
                }
            }
            base.WndProc(ref m);
        }

        private void button_Init_Click(object sender, EventArgs e)
        {
            int windowPtr = APIMessage.FindWindow(null, this.Name);

            string curIP = GetAvaliableIPAddress();
            label_CurIP.Text = curIP;
            if (!InterfaceImport.NP_Initialize((IntPtr)windowPtr, s_MsgID, curIP, 25000, "D:\\NovaPluto"))
            {
                MessageBox.Show("Initialize failed!");
            }
            else
            {
                MessageBox.Show("Initialize succeed!");
            }
        }

        private void button_Uninit_Click(object sender, EventArgs e)
        {
            InterfaceImport.NP_UnInitialize();
            string cardInfoStr = "";
            for (int i = 0; i < _cardInfoList.Count; i++)
            {
                cardInfoStr = _cardInfoList[i].Name + "(ID: "
                             + _cardInfoList[i].ID + ")"
                             + "  " + _cardInfoList[i].IP
                             + "  - DisConnect"; ;
                listBox_CardList.Items[i] = cardInfoStr;
            }
            MessageBox.Show("UnInitialize finished!");
        }

        #region CardInfo
        private void button_ConnectCard_Click(object sender, EventArgs e)
        {
            if (textBox_ConnectCardIP.Text == "")
            {
                MessageBox.Show("Please input ip!");
                return;
            }
            bool isSync = checkBox_IsSyncConnect.Checked;
            NP_CARD_INFO cardInfo;
            if (!InterfaceImport.NP_ConnectCardOfLocalNet(textBox_ConnectCardIP.Text,
                                          isSync, out cardInfo))
            {
                MessageBox.Show("Connect failed!");
            }
            else
            {
                if (isSync)
                {
                    CompleteConnectCardList(cardInfo);
                    MessageBox.Show("Connect Succeed!");
                }
                else
                {
                    MessageBox.Show("Send command succeed!");
                }
            }
        }
        private void button_GetCardInfo_Click(object sender, EventArgs e)
        {
            if (textBox_CardID.Text == "")
            {
                MessageBox.Show("Please input Card ID!");
                return;
            }
            bool isSync = checkBox_IsSyncGetInfo.Checked;
            NP_CARD_INFO cardInfo;
            if (!InterfaceImport.NP_GetCardInfo(textBox_CardID.Text,
                                          isSync, out cardInfo))
            {
                MessageBox.Show("Connect failed!");
            }
            else
            {
                if (isSync)
                {
                    GetCardInfo(cardInfo);
                    MessageBox.Show("Connect Succeed!");
                }
                else
                {
                    MessageBox.Show("Send command succeed!");
                }
            }
        }
        private void button_Clear_Click(object sender, EventArgs e)
        {
            listBox_CardInfo.Items.Clear();
        }

        private void button_SetIsEnableConnectDetect_Click(object sender, EventArgs e)
        {
            if (textBox_ConnectDetectCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (!InterfaceImport.NP_SetConnectDetectEnable(textBox_ConnectDetectCardID.Text,
                                                           checkBox_IsEanbleConnectDetect.Checked))
            {
                MessageBox.Show("Set connect detect enable failed!");
            }
            else
            {
                MessageBox.Show("Set connect detect enable succeed!");
            }
        }
        private void button_GetIsEnableConnectDetect_Click(object sender, EventArgs e)
        {
            if (textBox_ConnectDetectCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            bool isEanble = false;
            if (!InterfaceImport.NP_GetConnectDetectEnable(textBox_ConnectDetectCardID.Text, ref isEanble))
            {
                label_GetIsEnableConnectDetectResult.ForeColor = Color.Red;
                label_GetIsEnableConnectDetectResult.Text = "Get connect detect enable failed!";
                MessageBox.Show("Get connect detect enable failed!");
            }
            else
            {
                label_GetIsEnableConnectDetectResult.ForeColor = Color.Green;
                label_GetIsEnableConnectDetectResult.Text = "Connect detect enable: " + isEanble.ToString();
                MessageBox.Show("Get connect detect enable succeed!");
            }
        }

        private void comboBox_ConnectDetectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_VirConnectInterval.Enabled = comboBox_ConnectDetectType.SelectedIndex == 2 ? true : false;
        }
        private void button_SetCommunicateDetect_Click(object sender, EventArgs e)
        {
            if (textBox_ConnectDetectCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_CONNECTDETECT_INFO detectParameter = new NP_CONNECTDETECT_INFO();
            detectParameter.DetectType = (byte)comboBox_ConnectDetectType.SelectedIndex;
            detectParameter.VirConnectMinInterval = Convert.ToUInt16(numericUpDown_VirConnectInterval.Value * 60);
            if (!InterfaceImport.NP_SetConnectDetectPara(textBox_ConnectDetectCardID.Text, detectParameter))
            {
                MessageBox.Show("Set connect detect enable failed!");
            }
            else
            {
                MessageBox.Show("Set connect detect enable succeed!");
            }
        }
        private void button_GetCommunicateDetect_Click(object sender, EventArgs e)
        {
            if (textBox_ConnectDetectCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_CONNECTDETECT_INFO detectParameter;
            if (!InterfaceImport.NP_GetConnectDetectPara(textBox_ConnectDetectCardID.Text, out detectParameter))
            {
                label_GetCommunicateDetectRes.ForeColor = Color.Red;
                label_GetCommunicateDetectRes.Text = "Get connect detect parameter failed!";
                MessageBox.Show("Get connect detect parameter failed!");
            }
            else
            {
                label_GetCommunicateDetectRes.ForeColor = Color.Green;
                label_GetCommunicateDetectRes.Text = "DetectType: "
                                                     + comboBox_ConnectDetectType.Items[detectParameter.DetectType]
                                                     + ", VirConnectMinInterval: "
                                                     + detectParameter.VirConnectMinInterval.ToString();

                MessageBox.Show("Get connect detect parameter succeed!");
            }
        }
        #endregion

        #region PlayProgram
        private void button_CreatePlayProgram_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDia = new SaveFileDialog();
            if (saveFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = saveFileDia.FileName;
            fileName = Path.GetDirectoryName(fileName) + @"\"
                       + Path.GetFileNameWithoutExtension(fileName)
                       + ".plym";
            NP_SIZE screenSize = new NP_SIZE();
            screenSize.Width = 256;
            screenSize.Height = 256;
            IntPtr playProgramPtr = InterfaceImport.NP_CreatePlayProgram(fileName, screenSize);
            if (playProgramPtr.ToInt32() == 0)
            {
                MessageBox.Show("Create Play-program failed!");
                return;
            }
            else
            {
                MessageBox.Show("Create Play-program succeed,and handler is:" 
                                + playProgramPtr.ToString());
            }
            _playProgramPtrList.Add(playProgramPtr);
            listBox_Playlist.Items.Add(saveFileDia.FileName);
            if (listBox_Playlist.Items.Count > 0)
            {
                listBox_Playlist.SetSelected(listBox_Playlist.Items.Count - 1, true);
            }
        }
        private void button_OpenPlayProgram_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            IntPtr playProgramPtr = InterfaceImport.NP_OpenPlayProgram(openFileDia.FileName);
            if (playProgramPtr.ToInt32() == 0)
            {
                MessageBox.Show("Open Play-program failed!");
                return;
            }
            else
            {
                MessageBox.Show("Open Play-program succeed,and handler is:"
                                + playProgramPtr.ToString());
            }
            _playProgramPtrList.Add(playProgramPtr);
            listBox_Playlist.Items.Add(openFileDia.FileName);
            if (listBox_Playlist.Items.Count > 0)
            {
                listBox_Playlist.SetSelected(listBox_Playlist.Items.Count - 1, true);
            }
        }

        private void button_SavePlayProgram_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            if (!InterfaceImport.NP_SavePlayProgram(_playProgramPtrList[index]))
            {
                MessageBox.Show("Save Play-program failed!");
            }
            else
            {
                MessageBox.Show("Save Play-program Succeed!");
            }
        }
        private void button_ClosePlayProgram_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            if (!InterfaceImport.NP_ClosePlayProgram(_playProgramPtrList[index]))
            {
                MessageBox.Show("Close Play-program failed!");
            }
            else
            {
                MessageBox.Show("Close Play-program Succeed!");
                _playProgramPtrList.RemoveAt(index);
                listBox_Playlist.Items.RemoveAt(index);
                if (listBox_Playlist.Items.Count > 0)
                {
                    listBox_Playlist.SetSelected(listBox_Playlist.Items.Count - 1, true);
                }
            }
        }

        private void button_AddTimeSegment_Click(object sender, EventArgs e)
        {       
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            } 
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_TIMESEGMENT_INFO timeSegmentInfo = new NP_TIMESEGMENT_INFO();
            timeSegmentInfo.Name = "Segment1";
            timeSegmentInfo.StartDate = new NP_DATE();
            timeSegmentInfo.StartDate.Year = 2013;
            timeSegmentInfo.StartDate.Month = 1;
            timeSegmentInfo.StartDate.Day = 1;

            timeSegmentInfo.StopDate.Year = 2014;
            timeSegmentInfo.StopDate.Month = 1;
            timeSegmentInfo.StopDate.Day = 1;
            int size = Marshal.SizeOf(typeof(NP_ANALOGCLOCK_INFO));
            timeSegmentInfo.IsSpecificDate = false;
            timeSegmentInfo.IsSpecificDayOfWeek = FALSE;
            timeSegmentInfo.IsWholeDayPlay = true;
            timeSegmentInfo.WeekDayIsValid
                        = new bool[7] { true,true,true,true,true,true,true };
            timeSegmentInfo.StartTimeOfDay.Hour = 0;
            timeSegmentInfo.StartTimeOfDay.Minute = 0;
            timeSegmentInfo.StartTimeOfDay.Second = 1;
            timeSegmentInfo.StartTimeOfDay.MilliSeconds = 0;

            timeSegmentInfo.StopTimeOfDay.Hour = 0;
            timeSegmentInfo.StopTimeOfDay.Minute = 1;
            timeSegmentInfo.StopTimeOfDay.Second = 1;
            timeSegmentInfo.StopTimeOfDay.MilliSeconds = 0;
            if (!InterfaceImport.NP_AddTimeSegment(_playProgramPtrList[index], timeSegmentInfo))
            {
                MessageBox.Show("Add time segment to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add time segment to play-program succeed!");
            }
        }
        private void button_RemoveTimeSegment_Click(object sender, EventArgs e)
        {           
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            if (!InterfaceImport.NP_RemoveTimeSegment(_playProgramPtrList[index], 0))
            {
                MessageBox.Show("Remove time segment from play-program failed!");
            }
            else
            {
                MessageBox.Show("Remove time segment from play-program Succeed!");
            }
        }

        private void button_AddPage_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_PAGE_INFO pageInfo = new NP_PAGE_INFO();
            pageInfo.Name = "page1";
            pageInfo.PlayType = 1;
            pageInfo.PlayTimes = 100;
            if (!InterfaceImport.NP_AddBasicPage(_playProgramPtrList[index], 0, pageInfo))
            {
                MessageBox.Show("Add page to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add page to play-program succeed!");
            }
        }
        private void button_RemovePage_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            if (!InterfaceImport.NP_RemovePage(_playProgramPtrList[index], 0, 0))
            {
                MessageBox.Show("Remove page from play-program failed!");
            }
            else
            {
                MessageBox.Show("Remove page from play-program Succeed!");
            }
        }

        private void button_AddWindow_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_WINDOW_INFO windowInfo = new NP_WINDOW_INFO();
            windowInfo.Name = "W";
            windowInfo.WindowRect = new NP_RECTANGLE();
            windowInfo.WindowRect.X = 0;
            windowInfo.WindowRect.Y = 0;
            windowInfo.WindowRect.Width = 256;
            windowInfo.WindowRect.Height = 256;
            if (!InterfaceImport.NP_AddWindowToPage(_playProgramPtrList[index], 0, 0, windowInfo))
            {
                MessageBox.Show("Add window to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add window to play-program succeed!");
            }
        }
        private void button_RemoveWindow_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            if (!InterfaceImport.NP_RemoveWindow(_playProgramPtrList[index], 0, 0, 0))
            {
                MessageBox.Show("Remove window from play-program failed!");
            }
            else
            {
                MessageBox.Show("Remove window from play-program Succeed!");
            }
        }

        private void button_AddVideo_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            NP_VIDEOFIEL_INFO videoInfo = new NP_VIDEOFIEL_INFO();
            videoInfo.PlayScale = 1;
            videoInfo.Volume = 10;
            if (!InterfaceImport.NP_AddVideoFile(_playProgramPtrList[index], 0, 0, 0, openFileDia.FileName, videoInfo))
            {
                MessageBox.Show("Add video to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add video to play-program succeed!");
            }
        }
        private void button_AddFlash_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            NP_FLASH_INFO flashInfo = new NP_FLASH_INFO();
            flashInfo.PlayScale = 1;
            if (!InterfaceImport.NP_AddFlashFile(_playProgramPtrList[index], 0, 0, 0, openFileDia.FileName, flashInfo))
            {
                MessageBox.Show("Add flash to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add flash to play-program succeed!");
            }
        }
        private void button_AddImage_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            NP_IMAGEFILE_INFO imageInfo = new NP_IMAGEFILE_INFO();
            imageInfo.BackMusicFileName = null;
            imageInfo.InEffectInfo.IsHasEffect = false;
            imageInfo.InEffectInfo.EffectValue = 0;
            imageInfo.InEffectInfo.EffectSpeed = 1;
            imageInfo.OutEffectInfo.IsHasEffect = false;
            imageInfo.OutEffectInfo.EffectValue = 0;
            imageInfo.OutEffectInfo.EffectSpeed = 1;
            imageInfo.PlayScale = 1;
            imageInfo.StayTime = 9;
            if (!InterfaceImport.NP_AddImageFile(_playProgramPtrList[index], 0, 0, 0, openFileDia.FileName, imageInfo))
            {
                MessageBox.Show("Add image to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add image to play-program succeed!");
            }
        }
        private void button_AddTxt_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            NP_TXTFILE_INFO txtInfo = new NP_TXTFILE_INFO();
            txtInfo.BackColor = 0xff00ffff;
            txtInfo.BackMusicFileName = null;
            txtInfo.InEffectInfo.IsHasEffect = false;
            txtInfo.InEffectInfo.EffectValue = 0;
            txtInfo.InEffectInfo.EffectSpeed = 1;
            txtInfo.OutEffectInfo.IsHasEffect = false;
            txtInfo.OutEffectInfo.EffectValue = 0;
            txtInfo.OutEffectInfo.EffectSpeed = 1;
            txtInfo.PlayScale = 1;
            txtInfo.StayTime = 10;
            txtInfo.ColorInverseType = 0;
            if (!InterfaceImport.NP_AddTxtFile(_playProgramPtrList[index], 0, 0, 0, openFileDia.FileName, txtInfo))
            {
                MessageBox.Show("Add txt to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add txt to play-program succeed!");
            }
        }
        private void button_AddScrollingText_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_SCROLLTEXT_INFO scrollTxtInfo = new NP_SCROLLTEXT_INFO();
            scrollTxtInfo.Text = "NovaPlutoSDKTest";
            scrollTxtInfo.BackColor = 0xffffffff;
            scrollTxtInfo.IsScroll = true;
            scrollTxtInfo.TextEffect.EffectType = 0;
            scrollTxtInfo.ScrollIntervalPixel = 10;
            scrollTxtInfo.ScrollSpeed = 0;
            scrollTxtInfo.ScrollDirection = 0;
            scrollTxtInfo.PlayDuration.Hour = 0;
            scrollTxtInfo.PlayDuration.Minute = 0;
            scrollTxtInfo.PlayDuration.Second = 20;
            scrollTxtInfo.PlayDuration.MilliSeconds = 0;
            scrollTxtInfo.TextFont.TextFont = GetDefaultLOGFONT();
            if (!InterfaceImport.NP_AddScrollingText(_playProgramPtrList[index], 0, 0, 0, scrollTxtInfo))
            {
                MessageBox.Show("Add scrollingText to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add scrollingText to play-program succeed!");
            }
        }
        private void button_AddStaticText_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_STATICTEXT_INFO staticTxtInfo = new NP_STATICTEXT_INFO();
            staticTxtInfo.Text = "NovaPlutoSDKTest";
            staticTxtInfo.TextFont.TextFont = GetDefaultLOGFONT();
            staticTxtInfo.BackColor = 0xffffffff;
            staticTxtInfo.TextEffect.EffectType = 0;
            staticTxtInfo.RowSpacing = 10;
            staticTxtInfo.CharacterSpacing = 1;
            staticTxtInfo.AlignmentType = 2;
            staticTxtInfo.PlayDuration.Hour = 0;
            staticTxtInfo.PlayDuration.Minute = 0;
            staticTxtInfo.PlayDuration.Second = 20;
            staticTxtInfo.PlayDuration.MilliSeconds = 0;
            if (!InterfaceImport.NP_AddStaticText(_playProgramPtrList[index], 0, 0, 0, staticTxtInfo))
            {
                MessageBox.Show("Add static txt to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add static txt play-program succeed!");
            }
        }
        private void button_AddSingleText_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_SINGLELINETEXT_INFO singleTxtInfo = new NP_SINGLELINETEXT_INFO();
            singleTxtInfo.Text = "NovaPlutoSDKTest";
            singleTxtInfo.TextFont.TextFont = GetDefaultLOGFONT();
            singleTxtInfo.BackColor = 0xffffffff;
            singleTxtInfo.TextEffect.EffectType = 0;
            singleTxtInfo.InEffectInfo.EffectValue = 0;
            singleTxtInfo.InEffectInfo.EffectSpeed = 1;
            singleTxtInfo.OutEffectInfo.EffectValue = 0;
            singleTxtInfo.OutEffectInfo.EffectSpeed = 1;
            singleTxtInfo.StayTime = 9;
            if (!InterfaceImport.NP_AddSingleLineText(_playProgramPtrList[index], 0, 0, 0, singleTxtInfo))
            {
                MessageBox.Show("Add single txt to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add single txt to play-program succeed!");
            }
        }
        private void button_AddAnalogClock_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_ANALOGCLOCK_INFO anaClockInfo = new NP_ANALOGCLOCK_INFO();
            anaClockInfo.BackColor = 0xff000000;
            anaClockInfo.HourScaleColor = 0xff00ff00;//
            anaClockInfo.HourScaleWidth = 6;
            anaClockInfo.HourScaleHeight = 6;
            anaClockInfo.HourScaleShape = 0;
            anaClockInfo.HourScaleFont = GetDefaultLOGFONT();
            anaClockInfo.MinuteScaleColor = 0xfffc0386;
            anaClockInfo.MinuteScaleWidth = 4;
            anaClockInfo.MinuteScaleHeight = 4;
            anaClockInfo.MinuteScaleShape = 1;
            anaClockInfo.Content = "北京";
            anaClockInfo.ContentFont.TextFont = GetDefaultLOGFONT();
            anaClockInfo.ContentFont.TextForeColor = 0xff0000ff;
            anaClockInfo.IsShowDate = true;
            anaClockInfo.DateShowType = 1;
            anaClockInfo.DateFont.TextFont = GetDefaultLOGFONT();
            anaClockInfo.DateFont.TextForeColor = 0xffffffff;
            anaClockInfo.IsShowWeekDay = true;
            anaClockInfo.WeekDayFont.TextFont = GetDefaultLOGFONT();
            anaClockInfo.WeekDayFont.TextForeColor = 0xfff200ff;
            anaClockInfo.HourHandColor = 0xfff200ff;
            anaClockInfo.MinuteHandColor = 0xff00ff00;
            anaClockInfo.SecondHandColor = 0xff0000ff;
            anaClockInfo.PlayDuration.Hour = 0;
            anaClockInfo.PlayDuration.Minute = 0;
            anaClockInfo.PlayDuration.Second = 40;
            anaClockInfo.PlayDuration.MilliSeconds = 0;

            int logfont = Marshal.SizeOf(typeof(LOGFONTW));
            int npfont = Marshal.SizeOf(typeof(NP_FONT));
            int nptimespan = Marshal.SizeOf(typeof(NP_TIMESPAN));
            int npfonteffe = Marshal.SizeOf(typeof(NP_FONT_EFFECT));
            int size = Marshal.SizeOf(typeof(NP_DIGITALCLOCK_INFO));
            if (!InterfaceImport.NP_AddAnalogClock(_playProgramPtrList[index], 0, 0, 0, anaClockInfo))
            {
                MessageBox.Show("Add analog clock to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add analog clock to play-program succeed!");
            }
        }
        private void button_AddDigitalClock_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_DIGITALCLOCK_INFO digClockInfo = new NP_DIGITALCLOCK_INFO();
            digClockInfo.DateStyle = 0;
            digClockInfo.IsShowYear = true;
            digClockInfo.IsShowMonth = true;
            digClockInfo.IsShowDay = true;
            digClockInfo.IsShowAmOrPm = true;
            digClockInfo.IsShowHour = true;
            digClockInfo.IsShowMinute = true;
            digClockInfo.IsShowSecond = true;
            digClockInfo.IsShowWeekDay = true;
            digClockInfo.YearStyle = 0;
            digClockInfo.HourStyle = 0;
            digClockInfo.IsMultiLine = true;
            digClockInfo.FixedContent = "北京";
            digClockInfo.TextEffect.EffectType = 0;
            digClockInfo.TextEffect.EffectColor = 0xff00ff00;
            digClockInfo.PlayDuration.Hour = 0;
            digClockInfo.PlayDuration.Minute = 0;
            digClockInfo.PlayDuration.Second = 40;
            digClockInfo.PlayDuration.MilliSeconds = 0;
            digClockInfo.TextFont.TextFont = GetDefaultLOGFONT();
            digClockInfo.TextFont.TextForeColor = 0xfff200ff;
            if (!InterfaceImport.NP_AddDigitalClock(_playProgramPtrList[index], 0, 0, 0, digClockInfo))
            {
                MessageBox.Show("Add digital clock to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add digital clock to play-program succeed!");
            }
        }
        private void button_AddWeather_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            NP_WEATHER_INFO weatherInfo = new NP_WEATHER_INFO();
            weatherInfo.ShowType = 0;
            weatherInfo.ScrollSpeed = 2;
            weatherInfo.IsShowWeather = true;
            weatherInfo.IsShowTemperature = true;
            weatherInfo.IsShowWind = true;
            weatherInfo.IsShowHumidity = true;
            weatherInfo.IsShowCurTemperature = true;
            weatherInfo.WeatherConstText = "天气";
            weatherInfo.TempConstText = "温度";
            weatherInfo.WindConstText = "风力";
            weatherInfo.CurTempConstText = "当前温度";
            weatherInfo.HumiConstText = "当前湿度";
            weatherInfo.CountryName = "中国";
            weatherInfo.ProvinceName = "直辖市";
            weatherInfo.CityName = "北京";
            weatherInfo.BackColor = 0xff000000;
            weatherInfo.UpdateInterval = 30;
            weatherInfo.TextEffect.EffectType = 0;
            weatherInfo.TextEffect.EffectColor = 0xff00ff00;
            weatherInfo.PlayDuration.Hour = 0;
            weatherInfo.PlayDuration.Minute = 0;
            weatherInfo.PlayDuration.Second = 40;
            weatherInfo.PlayDuration.MilliSeconds = 0;
            weatherInfo.TextFont.TextFont = GetDefaultLOGFONT();
            weatherInfo.TextFont.TextForeColor = 0xfff200ff;
            if (!InterfaceImport.NP_AddWeather(_playProgramPtrList[index], 0, 0, 0, weatherInfo))
            {
                MessageBox.Show("Add weather to play-program failed!");
            }
            else
            {
                MessageBox.Show("Add weather to play-program succeed!");
            }
        }

        private void button_RemoveMedia_Click(object sender, EventArgs e)
        {
            if (_playProgramPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-program opened!");
                return;
            }
            int index = listBox_Playlist.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select the item in Play_program list!");
            }
            if (!InterfaceImport.NP_RemoveMedia(_playProgramPtrList[index], 0, 0, 0, 0))
            {
                MessageBox.Show("Remove media from play-program failed!");
            }
            else
            {
                MessageBox.Show("Remove media from play-program Succeed!");
            }
        }
        #endregion

        #region PlayLog
        private void button_GetPlayLog_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_GetLogCardID.Text))
            {
                MessageBox.Show("Please input card ID!");
                return;
            }

            NP_DATE getDate = new NP_DATE();
            getDate.Year = dateTimePicker1.Value.Year;
            getDate.Month = dateTimePicker1.Value.Month;
            getDate.Day = dateTimePicker1.Value.Day;
            if (!InterfaceImport.NP_GetCardPlayLog(textBox_GetLogCardID.Text, getDate))
            {
                MessageBox.Show("Send Commend failed!");
            }
            else
            {
                EnableGetInfoPercent(true, Color.Green, "Getting Playlog...");
            }
        }
        private void button_OpenPlayLog_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            IntPtr playLogPtr = InterfaceImport.NP_OpenPlayLogFile(openFileDia.FileName);
            if (playLogPtr.ToInt32() == 0)
            {
                MessageBox.Show("Open Play-log failed!");
                return;
            }
            else
            {
                MessageBox.Show("Open Play-log succeed,and handler is:"
                                + playLogPtr.ToString());
            }
            _playLogPtrList.Add(playLogPtr);
        }

        private void button_GetLogItemCnt_Click(object sender, EventArgs e)
        {
            if (_playLogPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-log opened!");
                return;
            }
            int itemCnt = InterfaceImport.NP_GetPlayLogItemCount(_playLogPtrList[0]);
            MessageBox.Show("Item count of play-log is:" + itemCnt.ToString());
        }
        private void button_GetLogItemInfo_Click(object sender, EventArgs e)
        {
            if (_playLogPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-log opened!");
                return;
            }
            NP_PLAYLOG_ITEM itemInfo;
            if (!InterfaceImport.NP_GetPlayLogItemInfo(_playLogPtrList[0], 0, out itemInfo))
            {
                MessageBox.Show("Get item info failed!");
            }
            else
            {
                string playDuration = itemInfo.MediaDuration.Hour.ToString() + ":"
                                      + itemInfo.MediaDuration.Minute.ToString() + ":"
                                      + itemInfo.MediaDuration.Second.ToString() + ":"
                                      + itemInfo.MediaDuration.MilliSeconds.ToString();
                string startTime = itemInfo.StartTime.wYear.ToString() + "-"
                                   + itemInfo.StartTime.wMonth.ToString() + "-"
                                   + itemInfo.StartTime.wDay.ToString() + "  "
                                   + itemInfo.StartTime.wHour.ToString() + ":"
                                   + itemInfo.StartTime.wMinute.ToString() + ":"
                                   + itemInfo.StartTime.wSecond.ToString() + ":"
                                   + itemInfo.StartTime.wMilliseconds.ToString();
                string stopTime = itemInfo.StopTime.wYear.ToString() + "-"
                                   + itemInfo.StopTime.wMonth.ToString() + "-"
                                   + itemInfo.StopTime.wDay.ToString() + "  "
                                   + itemInfo.StopTime.wHour.ToString() + ":"
                                   + itemInfo.StopTime.wMinute.ToString() + ":"
                                   + itemInfo.StopTime.wSecond.ToString() + ":"
                                   + itemInfo.StopTime.wMilliseconds.ToString();
                string playResult = "OK";
                switch (itemInfo.PlayResType)
                {
                    case 0:
                        playResult = "OK";
                        break;
                    case 1:
                        playResult = "File not exist";
                        break;
                    case 2:
                        playResult = "Load failed";
                        break;
                    case 3:
                        playResult = "Play failed";
                        break;
                }
                string info = "Media name:" + itemInfo.MediaName + "\r\n"
                             + "Play duration:" + playDuration + "\r\n"
                             + "Start time:" + startTime + "\r\n"
                             + "Stop time:" + stopTime + "\r\n"
                             + "Play result:" + playResult;
                MessageBox.Show("Get item info succeed,details:" + "\r\n" + info);
            }
        }

        private void button_ClosePlayLog_Click(object sender, EventArgs e)
        {
            if (_playLogPtrList.Count <= 0)
            {
                MessageBox.Show("There is no play-log opened!");
                return;
            }
            if (!InterfaceImport.NP_ClosePlayLogFile(_playLogPtrList[0]))
            {
                MessageBox.Show("Close Play-program failed!");
            }
            else
            {
                MessageBox.Show("Close Play-program Succeed!");
                _playLogPtrList.RemoveAt(0);
            }
        }
        #endregion

        #region SendPlayFile
        private void button_SendPlayProgram_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_SendFileCardID.Text))
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (!InterfaceImport.NP_SendPlayProgram(textBox_SendFileCardID.Text, 0, openFileDia.FileName))
            {
                MessageBox.Show("SendPlayProgram failed!");
            }
            else 
            {
                EnableGetInfoPercent(true, Color.Green, "PlayProgram is setting...");
                MessageBox.Show("SendPlayProgram success!");
            }
        }
        private void button_GetPlayProgramSendInfo_Click(object sender, EventArgs e)
        {
            if (textBox_SendFileCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_SENDPLAYFILE_INFO sendInfo = new NP_SENDPLAYFILE_INFO();
            if (!InterfaceImport.NP_GetPlayFileSendInfo(textBox_SendFileCardID.Text, 0, out sendInfo))
            {
                label_GetPlayProgramSendInfo.ForeColor = Color.Red;
                label_GetPlayProgramSendInfo.Text = "Get send info failed!";
                MessageBox.Show("Get send info failed!");
            }
            else
            {
                label_GetPlayProgramSendInfo.ForeColor = Color.Green;
                label_GetPlayProgramSendInfo.Text = "Send file: " + sendInfo.SendFileName
                                                    + "， Current percent: " + sendInfo.CurFilePercent
                                                    + "， Total percent: " + sendInfo.TotalPercent;
                MessageBox.Show("Get send info succeed!");
            }
        }

        private void button_SendVideoInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_SendFileCardID.Text))
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            NP_SIZE windowSize = new NP_SIZE();
            windowSize.Width = 320;
            windowSize.Height = 280;
            NP_TIMESPAN playDuration = new NP_TIMESPAN();
            playDuration.Hour = 0;
            playDuration.Minute = 1;
            playDuration.Second = 5;
            playDuration.MilliSeconds = 0;
            NP_VIDEOFIEL_INFO videoInfo = new NP_VIDEOFIEL_INFO();
            videoInfo.PlayScale = 4;
            videoInfo.Volume = 10;
            if (!InterfaceImport.NP_SendVideoInsertPlay(textBox_SendFileCardID.Text, openFileDia.FileName, 
                                                        windowSize, playDuration, videoInfo ))
            {
                MessageBox.Show("SendVideo failed!");
            }
            else
            {
                EnableGetInfoPercent(true, Color.Green, "Video is setting...");
                MessageBox.Show("SendVideo success!");
            }
        }
        private void button_SendImageInsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_SendFileCardID.Text))
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            NP_SIZE windowSize = new NP_SIZE();
            windowSize.Width = 320;
            windowSize.Height = 280;
            NP_TIMESPAN playDuration = new NP_TIMESPAN();
            playDuration.Hour = 0;
            playDuration.Minute = 1;
            playDuration.Second = 5;
            playDuration.MilliSeconds = 0;
            NP_IMAGEFILE_INFO imageInfo = new NP_IMAGEFILE_INFO();
            imageInfo.BackMusicFileName = null;
            imageInfo.InEffectInfo.IsHasEffect = true;
            imageInfo.InEffectInfo.EffectValue = 0;
            imageInfo.InEffectInfo.EffectSpeed = 3;
            imageInfo.OutEffectInfo.IsHasEffect = true;
            imageInfo.OutEffectInfo.EffectValue = 0;
            imageInfo.OutEffectInfo.EffectSpeed = 3;
            imageInfo.PlayScale = 0;
            imageInfo.StayTime = 9;
            if (!InterfaceImport.NP_SendImageInsertPlay(textBox_SendFileCardID.Text, 
                                                        openFileDia.FileName,
                                                        windowSize, playDuration, 
                                                        imageInfo))
            {
                MessageBox.Show("SendImage failed!");
            }
            else
            {
                EnableGetInfoPercent(true, Color.Green, "Image is setting...");
                MessageBox.Show("SendImage success!");
            }
        }
        private void button_GetInsertPlaySendInfo_Click(object sender, EventArgs e)
        {
            if (textBox_SendFileCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_SENDPLAYFILE_INFO sendInfo;
            if (!InterfaceImport.NP_GetPlayFileSendInfo(textBox_SendFileCardID.Text, 1, out sendInfo))
            {
                label_GetInsertPlaySendInfo.ForeColor = Color.Red;
                label_GetInsertPlaySendInfo.Text = "Get send info failed!";
                MessageBox.Show("Get send info failed!");
            }
            else
            {
                label_GetInsertPlaySendInfo.ForeColor = Color.Green;
                label_GetInsertPlaySendInfo.Text = "Send file: " + sendInfo.SendFileName
                                                    + "， Current percent: " + sendInfo.CurFilePercent
                                                    + "， Total percent: " + sendInfo.TotalPercent;
                MessageBox.Show("Get send info succeed!");
            }
        }

        private void button_SendScrollingNotify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_SendFileCardID.Text))
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_RECTANGLE windowRect = new NP_RECTANGLE();
            windowRect.X = 10;
            windowRect.Y = 50;
            windowRect.Width = 320;
            windowRect.Height = 280;
            uint playTimes = 10;
            NP_SCROLLTEXT_INFO scrollTxtInfo = new NP_SCROLLTEXT_INFO();
            scrollTxtInfo.Text = "NovaPlutoSDKTest";
            scrollTxtInfo.BackColor = 0xffffffff;
            scrollTxtInfo.IsScroll = true;
            scrollTxtInfo.TextEffect.EffectType = 0;
            scrollTxtInfo.ScrollIntervalPixel = 10;
            scrollTxtInfo.ScrollSpeed = 0;
            scrollTxtInfo.ScrollDirection = 0;
            scrollTxtInfo.PlayDuration.Hour = 0;
            scrollTxtInfo.PlayDuration.Minute = 5;
            scrollTxtInfo.PlayDuration.Second = 1;
            scrollTxtInfo.PlayDuration.MilliSeconds = 0;
            scrollTxtInfo.TextFont.TextFont = GetDefaultLOGFONT();
            if (!InterfaceImport.NP_SendSrollingTextNotify(textBox_SendFileCardID.Text, windowRect,
                                                           playTimes, scrollTxtInfo))
            {
                MessageBox.Show("SendScrollingText failed!");
            }
            else
            {
                EnableGetInfoPercent(true, Color.Green, "ScrollingText is setting...");
                MessageBox.Show("SendScrollingText success!");
            }
        }
        private void button_SendStaticNotify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_SendFileCardID.Text))
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_RECTANGLE windowRect = new NP_RECTANGLE();
            windowRect.X = 20;
            windowRect.Y = 50;
            windowRect.Width = 320;
            windowRect.Height = 280;
            uint playTimes = 10;
            NP_STATICTEXT_INFO staticTxtInfo = new NP_STATICTEXT_INFO();
            staticTxtInfo.Text = "NovaPlutoSDKTest";
            staticTxtInfo.TextFont.TextFont = GetDefaultLOGFONT();
            staticTxtInfo.BackColor = 0xffffffff;
            staticTxtInfo.TextEffect.EffectType = 0;
            staticTxtInfo.RowSpacing = 10;
            staticTxtInfo.CharacterSpacing = 1;
            staticTxtInfo.AlignmentType = 2;
            staticTxtInfo.PlayDuration.Hour = 0;
            staticTxtInfo.PlayDuration.Minute = 5;
            staticTxtInfo.PlayDuration.Second = 1;
            staticTxtInfo.PlayDuration.MilliSeconds = 0;
            if (!InterfaceImport.NP_SendStaticTextNotify(textBox_SendFileCardID.Text, windowRect,
                                                         playTimes, staticTxtInfo))
            {
                MessageBox.Show("SendStaticText failed!");
            }
            else
            {
                EnableGetInfoPercent(true, Color.Green, "StaticText is setting...");
                MessageBox.Show("SendStaticText success!");
            }
        }
        private void button_SendSingleNotify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_SendFileCardID.Text))
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_RECTANGLE windowRect = new NP_RECTANGLE();
            windowRect.X = 20;
            windowRect.Y = 50;
            windowRect.Width = 320;
            windowRect.Height = 280;
            uint playTimes = 10;
            NP_SINGLELINETEXT_INFO singleTxtInfo = new NP_SINGLELINETEXT_INFO();
            singleTxtInfo.Text = "NovaPlutoSDKTest";
            singleTxtInfo.TextFont.TextFont = GetDefaultLOGFONT();
            singleTxtInfo.BackColor = 0xffffffff;
            singleTxtInfo.TextEffect.EffectType = 0;
            singleTxtInfo.InEffectInfo.EffectValue = 0;
            singleTxtInfo.InEffectInfo.EffectSpeed = 1;
            singleTxtInfo.OutEffectInfo.EffectValue = 0;
            singleTxtInfo.OutEffectInfo.EffectSpeed = 1;
            singleTxtInfo.StayTime = 120;
            if (!InterfaceImport.NP_SendSingleLineTextNotify(textBox_SendFileCardID.Text, windowRect,
                                                             playTimes, singleTxtInfo))
            {
                MessageBox.Show("SendSingleLineText failed!");
            }
            else
            {
                EnableGetInfoPercent(true, Color.Green, "SingleLineText is setting...");
                MessageBox.Show("SendSingleLineText success!");
            }
        }
        private void button_GetNotificationSendInfo_Click(object sender, EventArgs e)
        {
            if (textBox_SendFileCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_SENDPLAYFILE_INFO sendInfo;
            if (!InterfaceImport.NP_GetPlayFileSendInfo(textBox_SendFileCardID.Text, 2, out sendInfo))
            {
                label_GetNotificationSendInfo.ForeColor = Color.Red;
                label_GetNotificationSendInfo.Text = "Get send info failed!";
                MessageBox.Show("Get send info failed!");
            }
            else
            {
                label_GetNotificationSendInfo.ForeColor = Color.Green;
                label_GetNotificationSendInfo.Text = "Send file: " + sendInfo.SendFileName
                                                    + "， Current percent: " + sendInfo.CurFilePercent
                                                    + "， Total percent: " + sendInfo.TotalPercent;
                MessageBox.Show("Get send info succeed!");
            }
        }
        #endregion

        #region PlayStatus
        private void button_ControlPlay_Click(object sender, EventArgs e)
        {
            if (textBox_CtrlPlayCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (comboBox_CardPlayStatus.Text == "")
            {
                MessageBox.Show("Please choose play status!");
                return;
            }
            if (!InterfaceImport.NP_ControlCardPlay(textBox_CtrlPlayCardID.Text, 
                                    (ushort)comboBox_CardPlayStatus.SelectedIndex))
            {
                MessageBox.Show("Control failed!");
            }
            else
            {
                MessageBox.Show("Control succeed!");
            }
        }

        private void button_GetScreenShotPicture_Click(object sender, EventArgs e)
        {
            if (textBox_CtrlPlayCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            SaveFileDialog saveFileDia = new SaveFileDialog();
            if (saveFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = saveFileDia.FileName;
            fileName = Path.GetDirectoryName(fileName) + @"\"
                       + Path.GetFileNameWithoutExtension(fileName)
                       + ".jpg";
            if (pictureBox_Image.BackgroundImage != null)
            {
                pictureBox_Image.BackgroundImage.Dispose();
                pictureBox_Image.BackgroundImage = null;
            }
            if (!InterfaceImport.NP_GetCardScreenShotPicture(textBox_CtrlPlayCardID.Text, fileName))
            {
                label_GetPictureResult.ForeColor = Color.Red;
                label_GetPictureResult.Text = "Get  failed!";
                MessageBox.Show("Get cycle monitor config failed!");
            }
            else
            {
                if (File.Exists(fileName))
                {
                    pictureBox_Image.BackgroundImage = Image.FromFile(fileName);
                }

                label_GetPictureResult.ForeColor = Color.Green;
                label_GetPictureResult.Text = "Get succeed!";
                MessageBox.Show("Get screen shot succeed!");
            }
        }
        #endregion

        #region CardControl
        private void button_SetScreenStatus_Click(object sender, EventArgs e)
        {
            if (textBox_CtrlCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (comboBox_SetScreenStatus.Text == "")
            {
                MessageBox.Show("Please choose screen status!");
                return;
            }
            if (!InterfaceImport.NP_SetCardSreenStatus(textBox_CtrlCardID.Text,
                                                       (ushort)comboBox_SetScreenStatus.SelectedIndex))
            {
                MessageBox.Show("Control failed!");
            }
            else
            {
                MessageBox.Show("Control succeed!");
            }
        }

        private void button_SetSystemTime_Click(object sender, EventArgs e)
        {
            if (textBox_CtrlCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (dateTimePicker_SetSystemTime.Text == "")
            {
                MessageBox.Show("Please input time!");
                return;
            }
            DateTime tempTime = DateTime.ParseExact(dateTimePicker_SetSystemTime.Text,
                                                    "yyyy-MM-dd  HH:mm:ss", null);
            SYSTEMTIME time = new SYSTEMTIME();
            time.wYear = (ushort)tempTime.Year;
            time.wMonth = (ushort)tempTime.Month;
            time.wDay = (ushort)tempTime.Day;
            time.wHour = (ushort)tempTime.Hour;
            time.wMinute = (ushort)tempTime.Minute;
            time.wSecond = (ushort)tempTime.Second;
            if (!InterfaceImport.NP_SetCardSystemTime(textBox_CtrlCardID.Text, time))
            {
                MessageBox.Show("Set time failed!");
            }
            else
            {
                MessageBox.Show("Set time succeed!");
            }
        }
        private void button_GetSystemTime_Click(object sender, EventArgs e)
        {
            if (textBox_CtrlCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            SYSTEMTIME time;
            if (!InterfaceImport.NP_GetCardSystemTime(textBox_CtrlCardID.Text, out time))
            {
                label_GetSystemTime.ForeColor = Color.Red;
                label_GetSystemTime.Text = "Get time failed!";
                MessageBox.Show("Get time failed!");
            }
            else
            {
                label_GetSystemTime.ForeColor = Color.Green;
                label_GetSystemTime.Text = time.wYear.ToString("0000")
                                           + "-" + time.wMonth.ToString("00")
                                           + "-" + time.wDay.ToString("00")
                                           + "  " + time.wHour.ToString("00")
                                           + ":" + time.wMinute.ToString("00")
                                           + ":" + time.wSecond.ToString("00")
                                           + ":" + time.wMilliseconds.ToString("000");
                MessageBox.Show("Get time succeed!");
            }
        }

        private void button_RestartCard_Click(object sender, EventArgs e)
        {
            if (textBox_CtrlCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (!InterfaceImport.NP_RestartCard(textBox_CtrlCardID.Text))
            {
                label_RestartCardRes.ForeColor = Color.Red;
                label_RestartCardRes.Text = "Restart card failed!";
                MessageBox.Show("Restart card failed!");
            }
            else
            {
                label_RestartCardRes.ForeColor = Color.Green;
                label_RestartCardRes.Text = "Restart card succeed!";
                MessageBox.Show("Restart card succeed!");
            }
        }

        private void button_SaveHWParameter_Click(object sender, EventArgs e)
        {
            if (textBox_CtrlCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (!InterfaceImport.NP_SaveHardwareParameter(textBox_CtrlCardID.Text))
            {
                label_SaveHWParameterRes.ForeColor = Color.Red;
                label_SaveHWParameterRes.Text = "Save parameter failed!";
                MessageBox.Show("Save parameter failed!");
            }
            else
            {
                label_SaveHWParameterRes.ForeColor = Color.Green;
                label_SaveHWParameterRes.Text = "Save parameter succeed!";
                MessageBox.Show("Save parameter succeed!");
            }
        }
        #endregion

        #region DetectPoint
        private void button_GetCabinetCount_Click(object sender, EventArgs e)
        {
            if (textBox_DetectCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            ushort cabinetCnt;
            if (!InterfaceImport.NP_GetCabinetCount(textBox_DetectCardID.Text, out cabinetCnt))
            {
                label_CabinetCnt.ForeColor = Color.Red;
                label_CabinetCnt.Text = "Get cabinet count failed!";
                MessageBox.Show("Get cabinet count failed!");
            }
            else
            {
                label_CabinetCnt.ForeColor = Color.Green;
                label_CabinetCnt.Text = "Cabinet count:" + cabinetCnt.ToString();
                MessageBox.Show("Get cabinet count succeed!");
            }
        }

        private void checkBox_IsUseCurrentGain_CheckedChanged(object sender, EventArgs e)
        {
            panel_DetectPointGain.Enabled = checkBox_IsUseCurrentGain.Checked;
        }
        private void button_BeginDetectPoint_Click(object sender, EventArgs e)
        {
            if (textBox_DetectCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            treeView_DetectPointResult.Nodes.Clear();

            string cardID = textBox_DetectCardID.Text;
            ushort cabinetIndex = Convert.ToUInt16(numericUpDown_PointDetectCabinetIndex.Value);

            NP_DETECTPOINT_PARA detectPointPara = new NP_DETECTPOINT_PARA();
            detectPointPara.PointDetectType = (byte)comboBox_DetectPointType.SelectedIndex;
            detectPointPara.Threshold = Convert.ToByte(numericUpDown_DetectPointThreshold.Value);
            detectPointPara.IsUseCurrentGain = checkBox_IsUseCurrentGain.Checked;
            detectPointPara.RedGain = Convert.ToByte(numericUpDown_DetectPointRedGain.Value);
            detectPointPara.GreenGain = Convert.ToByte(numericUpDown_DetectPointGreenGain.Value);
            detectPointPara.BlutGain = Convert.ToByte(numericUpDown_DetectPointBlueGain.Value);
            if (!InterfaceImport.NP_BeginDetectPoint(cardID, cabinetIndex, detectPointPara))
            {
                label_DetectPointResult.ForeColor = Color.Red;
                label_DetectPointResult.Text = "Send point detect command failed!";
                MessageBox.Show("Send point detect command failed!");
            }
            else
            {
                label_DetectPointResult.ForeColor = Color.Black;
                label_DetectPointResult.Text = "Detecting point......";
                EnableGetInfoPercent(true, Color.Black, "Detecting point......");
            }
        }
        #endregion

        #region Monitor
        private void button_RefreshCardMonitor_Click(object sender, EventArgs e)
        {
            if (textBox_MonitorCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            treeView_MonitorInfo.Nodes.Clear();
            if (!InterfaceImport.NP_RefreshCardMonitorInfo(textBox_MonitorCardID.Text, 
                                                           s_MonitorInfoSaveFolder))
            {
                label_MonitorResult.ForeColor = Color.Red;
                label_MonitorResult.Text = "Send monitor command failed!";
                MessageBox.Show("Send monitor command failed!");
            }
            else
            {
                label_MonitorResult.ForeColor = Color.Black;
                label_MonitorResult.Text = "Refreshing monitor info......";
                EnableGetInfoPercent(true, Color.Black, "Refreshing monitor info......");
            }
        }

        private void button_OpenMonitorFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDia = new OpenFileDialog();
            if (openFileDia.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            IntPtr monitorFilePtr = InterfaceImport.NP_OpenMonitorInfoFile(openFileDia.FileName);
            if (monitorFilePtr.ToInt32() == 0)
            {
                label_OpenMonitorFileRes.ForeColor = Color.Red;
                label_OpenMonitorFileRes.Text = "Open file failed!";
                MessageBox.Show("Open file failed!");
            }
            else
            {
                if (!_monitorFilePtrList.Contains(monitorFilePtr))
                {
                    _monitorFilePtrList.Add(monitorFilePtr);
                }
                label_OpenMonitorFileRes.ForeColor = Color.Green;
                label_OpenMonitorFileRes.Text = "Monitor file ptr: " + monitorFilePtr.ToInt32();
                MessageBox.Show("Open file succeed, and handler is:" + monitorFilePtr.ToString());
            }
        }
        private void button_GetMonitorInfoCnt_Click(object sender, EventArgs e)
        {
            if (_monitorFilePtrList.Count <= 0)
            {
                MessageBox.Show("There is no monitor file opened!");
                return;
            }
            int infoCnt = InterfaceImport.NP_GetInfoCntFromFile(_monitorFilePtrList[0]);
            label_GetMonitorCntRes.Text = "Info count: " + infoCnt.ToString();
            MessageBox.Show("Info count: " + infoCnt.ToString());
        }
        private void button_GetMonitorInfo_Click(object sender, EventArgs e)
        {
            if (_monitorFilePtrList.Count <= 0)
            {
                MessageBox.Show("There is no monitor file opened!");
                return;
            }
            if (string.IsNullOrEmpty(numericUpDown_MonitorInfoIndex.Text))
            {
                MessageBox.Show("Please input info index!");
                return;
            }
            treeView_MonitorInfo.Nodes.Clear();
            NP_MONITOR_INFO monitorInfo;
            int infoIndex = Convert.ToInt32(numericUpDown_MonitorInfoIndex.Value);
            if (InterfaceImport.NP_GetMonitorInfoFromFile(_monitorFilePtrList[0], infoIndex, out monitorInfo))
            {
                UpdateMonitorInfo(monitorInfo);
                MessageBox.Show("Get monitor info succeed!");
            }
            else
            {
                MessageBox.Show("Get monitor info failed!");
            }
        }
        private void button_CloseMonitorFile_Click(object sender, EventArgs e)
        {
            if (_monitorFilePtrList.Count <= 0)
            {
                MessageBox.Show("There is no monitor file opened!");
                return;
            }
            if (InterfaceImport.NP_CloseMonitoInfoFile(_monitorFilePtrList[0]))
            {
                _monitorFilePtrList.RemoveAt(0);
                MessageBox.Show("Close monitor file succeed!");
            }
            else
            {
                MessageBox.Show("Close monitor file failed!");
            }
        }

        private void checkBox_EnableCycleMonitor_CheckedChanged(object sender, EventArgs e)
        {
            panel_CycleMonitor.Enabled = checkBox_EnableCycleMonitor.Checked;
        }
        private void button_SetCycleMonitor_Click(object sender, EventArgs e)
        {
            if (textBox_MonitorCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            ushort nCycleValue = 60;
            if (checkBox_EnableCycleMonitor.Checked)
            {
                nCycleValue = Convert.ToUInt16(numericUpDown_CycleValue.Value);
            }
            if (!InterfaceImport.NP_SetCycleMontorConfig(textBox_MonitorCardID.Text,
                                                         checkBox_EnableCycleMonitor.Checked,
                                                         nCycleValue))
            {
                MessageBox.Show("Set cycle monitor config failed!");
            }
            else
            {
                MessageBox.Show("Set cycle monitor config succeed!");
            }
        }
        private void button_GetCycleMonitor_Click(object sender, EventArgs e)
        {
            if (textBox_MonitorCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            bool isEnable = true;
            UInt16 cycleValue = 0;
            if (!InterfaceImport.NP_GetCycleMontorConfig(textBox_MonitorCardID.Text,
                                                         ref isEnable,
                                                         out cycleValue))
            {
                label_GetCycleMonitorResult.ForeColor = Color.Red;
                label_GetCycleMonitorResult.Text = "Get cycle monitor config failed!";
                MessageBox.Show("Get cycle monitor config failed!");
            }
            else
            {
                label_GetCycleMonitorResult.ForeColor = Color.Green;
                label_GetCycleMonitorResult.Text = "Is enable: " + isEnable.ToString()
                                                   + ", Cycle value: " + cycleValue.ToString();
                MessageBox.Show("Get cycle monitor config succeed!");
            }
        }

        private void checkBox_EnableSelfMonitorCtrl_CheckedChanged(object sender, EventArgs e)
        {
            panel_SelfMonitorCtrl.Enabled = checkBox_EnableSelfMonitorCtrl.Checked;
        }
        private void button_SetSelfMonitorCtrl_Click(object sender, EventArgs e)
        {
            if (textBox_MonitorCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (comboBox_RowLineErrorCtrlType.Text == "")
            {
                MessageBox.Show("Please choose row line ctrl type!");
                return;
            }
            NP_SELFMONITORCTRL_PARA selfMonitorPara = new NP_SELFMONITORCTRL_PARA();
            selfMonitorPara.IsCycleSelfMonitor = checkBox_EnableSelfMonitorCtrl.Checked;
            
            if (checkBox_EnableSelfMonitorCtrl.Checked)
            {
                selfMonitorPara.SelfMonitorPeriod = Convert.ToUInt16(numericUpDown_SelfMonitorCtrlPeriod.Value);
            }
            else
            {
                selfMonitorPara.SelfMonitorPeriod = 30;
            }
            selfMonitorPara.RowLineErrorCtrlType = (byte)comboBox_RowLineErrorCtrlType.SelectedIndex;
            if (!InterfaceImport.NP_SetSelfMonitorCtrlPara(textBox_MonitorCardID.Text, selfMonitorPara))
            {
                MessageBox.Show("Set self monitor parameter failed!");
            }
            else
            {
                MessageBox.Show("Set self monitor parameter succeed!");
            }
        }
        private void button_GetSelfMonitorCtrl_Click(object sender, EventArgs e)
        {
            if (textBox_MonitorCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_SELFMONITORCTRL_PARA selfMonitorPara = new NP_SELFMONITORCTRL_PARA();

            if (!InterfaceImport.NP_GetSelfMonitorCtrlPara(textBox_MonitorCardID.Text,
                                                           out selfMonitorPara))
            {
                label_GetSelfMonitorCtrlResult.ForeColor = Color.Red;
                label_GetSelfMonitorCtrlResult.Text = "Get self monitor parameter failed!";
                MessageBox.Show("Get self monitor parameter failed!");
            }
            else
            {
                label_GetSelfMonitorCtrlResult.ForeColor = Color.Green;
                label_GetSelfMonitorCtrlResult.Text = "Is enable: " + selfMonitorPara.IsCycleSelfMonitor.ToString()
                                                      + ", Cycle value: " + selfMonitorPara.SelfMonitorPeriod.ToString()
                                                      + ", RowLineErrorCtrlType: "
                                                      + comboBox_RowLineErrorCtrlType.Items[selfMonitorPara.RowLineErrorCtrlType] ;
                MessageBox.Show("Get self monitor parameter succeed!");
            }
        }

        private void button_GetGlobalMonitorInfo_Click(object sender, EventArgs e)
        {
            if (textBox_MonitorCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_GLOBALMONITOR_INFO globalMonitorInfo;
            if (!InterfaceImport.NP_GetGlobalMonitorInfo(textBox_MonitorCardID.Text, out globalMonitorInfo))
            {
                label_GetGlobalMonitorInfoRes.ForeColor = Color.Red;
                label_GetGlobalMonitorInfoRes.Text = "Get global monitor info failed!";
                MessageBox.Show("Get global monitor info failed!");
            }
            else
            {
                UpdateGlobalMonitorInfo(globalMonitorInfo);
                label_GetGlobalMonitorInfoRes.ForeColor = Color.Green;
                label_GetGlobalMonitorInfoRes.Text = "Get global monitor info succeed!";
                MessageBox.Show("Get global monitor info succeed!");
            }
        }
        #endregion

        #region OnBoardPower
        private void button_SetOnBoardPowerStatus_Click(object sender, EventArgs e)
        {
            if (textBox_OnBoardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (comboBox_OnBoardPowerState.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose power state!");
                return;
            }

            byte powerState = (byte)comboBox_OnBoardPowerState.SelectedIndex;
            if (!InterfaceImport.NP_SetOnBoardPowerState(textBox_OnBoardPowerCardID.Text, powerState))
            {
                MessageBox.Show("Set power state failed!");
            }
            else
            {
                MessageBox.Show("Set power state succeed!");
            }
        }
        private void button_GetOnBoardPowerStatus_Click(object sender, EventArgs e)
        {
            if (textBox_OnBoardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            byte powerState;
            if (!InterfaceImport.NP_GetOnBoardPowerState(textBox_OnBoardPowerCardID.Text, out powerState))
            {
                label_GetOnBoardPowerStatusRes.ForeColor = Color.Red;
                label_GetOnBoardPowerStatusRes.Text = "Get power state failed!";
                MessageBox.Show("Get power state failed!");
            }
            else
            {
                if (powerState >= 0
                    && powerState < 2)
                {
                    label_GetOnBoardPowerStatusRes.ForeColor = Color.Green;
                    label_GetOnBoardPowerStatusRes.Text = comboBox_OnBoardPowerState.Items[powerState].ToString();
                }
                else
                {
                    label_GetOnBoardPowerStatusRes.ForeColor = Color.Red;
                    label_GetOnBoardPowerStatusRes.Text = "Unknown";
                }
                MessageBox.Show("Get power state succeed!");
            }
        }

        private void button_AddPowerAutoInfo_Click(object sender, EventArgs e)
        {
            Frm_EditBDPowerAutoInfo addCtrlInfoFrm = new Frm_EditBDPowerAutoInfo(new NP_BDPOWER_AUTOTIME_INFO());
            if (addCtrlInfoFrm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            NP_BDPOWER_AUTOTIME_INFO ctrlInfo = addCtrlInfoFrm.CtrlInfo;
            UpdateOneBDPowerAutoInfo(dataGridView_OnBoardPowerAutoInfo.RowCount, ctrlInfo);
        }
        private void button_EditPowerAutoInfo_Click(object sender, EventArgs e)
        {
            if (dataGridView_OnBoardPowerAutoInfo.SelectedRows.Count <= 0)
            {
                return;
            }
            int nIndex = dataGridView_OnBoardPowerAutoInfo.SelectedRows[0].Index;
            NP_BDPOWER_AUTOTIME_INFO tempInfo = (NP_BDPOWER_AUTOTIME_INFO)dataGridView_OnBoardPowerAutoInfo.Rows[nIndex].Tag;

            Frm_EditBDPowerAutoInfo editCtrlInfoFrm = new Frm_EditBDPowerAutoInfo(tempInfo);
            if (editCtrlInfoFrm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            tempInfo = editCtrlInfoFrm.CtrlInfo;
            UpdateOneBDPowerAutoInfo(nIndex, tempInfo);
        }
        private void button_DeletePowerAutoInfo_Click(object sender, EventArgs e)
        {
            if (dataGridView_OnBoardPowerAutoInfo.SelectedRows.Count <= 0)
            {
                return;
            }
            int nIndex = dataGridView_OnBoardPowerAutoInfo.SelectedRows[0].Index;
            dataGridView_OnBoardPowerAutoInfo.Rows.RemoveAt(nIndex);
        }
        private void button_GetOnBoardPowerAutoInfo_Click(object sender, EventArgs e)
        {
            if (textBox_OnBoardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_BDPOWER_AUTOCTRL_INFO ctrlInfo = new NP_BDPOWER_AUTOCTRL_INFO();
            if (!InterfaceImport.NP_GetOnBoardPowerAutoInfo(textBox_OnBoardPowerCardID.Text, out ctrlInfo))
            {
                label_GetOnBoardPowerAutoInfoRes.ForeColor = Color.Red;
                label_GetOnBoardPowerAutoInfoRes.Text = "Get power auto ctrl info failed!";
                MessageBox.Show("Get power auto ctrl info failed!");
            }
            else
            {
                dataGridView_OnBoardPowerAutoInfo.Rows.Clear();
                IntPtr ptr;
                NP_BDPOWER_AUTOTIME_INFO tempInfo;
                int valueSize = Marshal.SizeOf(new NP_BDPOWER_AUTOTIME_INFO());
                for (int i = 0; i < ctrlInfo.CtrlInfoCount; i++)
                {
                    ptr = (IntPtr)((UInt32)ctrlInfo.CtrlInfoArrayPtr + valueSize * i);
                    tempInfo = (NP_BDPOWER_AUTOTIME_INFO)Marshal.PtrToStructure(ptr, typeof(NP_BDPOWER_AUTOTIME_INFO));

                    UpdateOneBDPowerAutoInfo(i, tempInfo);
                }
                label_GetOnBoardPowerAutoInfoRes.ForeColor = Color.Green;
                label_GetOnBoardPowerAutoInfoRes.Text = "Get power auto ctrl info succeed!";
                MessageBox.Show("Get power auto ctrl info succeed!");
            }
        }
        private void button_SetOnBoardPowerAutoInfo_Click(object sender, EventArgs e)
        {
            if (textBox_OnBoardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            int infoCnt = dataGridView_OnBoardPowerAutoInfo.RowCount;
            NP_BDPOWER_AUTOTIME_INFO[] ctrlInfoArray = new NP_BDPOWER_AUTOTIME_INFO[infoCnt];
            for (int i = 0; i < infoCnt; i++)
            {
                ctrlInfoArray[i] = (NP_BDPOWER_AUTOTIME_INFO)dataGridView_OnBoardPowerAutoInfo.Rows[i].Tag;
            }

            NP_BDPOWER_AUTOCTRL_INFO ctrlInfo = new NP_BDPOWER_AUTOCTRL_INFO();
            ctrlInfo.CtrlInfoCount = (ushort)infoCnt;
            ctrlInfo.CtrlInfoArrayPtr = GetPtrFromOnBoardCtrlArray(ctrlInfoArray);
            if (!InterfaceImport.NP_SetOnBoardPowerAutoInfo(textBox_OnBoardPowerCardID.Text, ctrlInfo))
            {
                MessageBox.Show("Set power auto ctrl info failed!");
            }
            else
            {
                MessageBox.Show("Set power auto ctrl info succeed!");
            }
        }
        #endregion

        #region FunCardPower
        private void button_SetFunPowerMode_Click(object sender, EventArgs e)
        {
            if (textBox_FunCardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (comboBox_FunPowerMode.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose power mode!");
                return;
            }

            byte powerMode = (byte)comboBox_FunPowerMode.SelectedIndex;
            if (!InterfaceImport.NP_SetFunPowerAdjustMode(textBox_FunCardPowerCardID.Text, 
                                                          Convert.ToUInt16(numericUpDown_FunCardIndex.Value),
                                                          powerMode))
            {
                MessageBox.Show("Set power mode failed!");
            }
            else
            {
                MessageBox.Show("Set power mode succeed!");
            }
        }
        private void button_GetFunPowerMode_Click(object sender, EventArgs e)
        {
            if (textBox_FunCardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            byte powerState;
            if (!InterfaceImport.NP_GetFunPowerAdjustMode(textBox_FunCardPowerCardID.Text, 
                                                          Convert.ToUInt16(numericUpDown_FunCardIndex.Value),
                                                          out powerState))
            {
                label_GetFunPowerMode.ForeColor = Color.Red;
                label_GetFunPowerMode.Text = "Get power mode failed!";
                MessageBox.Show("Get power mode failed!");
            }
            else
            {
                if (powerState >= 0
                    && powerState < 2)
                {
                    label_GetFunPowerMode.ForeColor = Color.Green;
                    label_GetFunPowerMode.Text = comboBox_FunPowerMode.Items[powerState].ToString();
                }
                else
                {
                    label_GetFunPowerMode.ForeColor = Color.Red;
                    label_GetFunPowerMode.Text = "Unknown";
                }
                MessageBox.Show("Get power mode succeed!");
            }
        }

        private void button_SetFunPowerState_Click(object sender, EventArgs e)
        {
            if (textBox_FunCardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (comboBox_SetFunPowerState.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose power mode!");
                return;
            }

            byte powerState = (byte)comboBox_SetFunPowerState.SelectedIndex;
            ushort funIndex = Convert.ToUInt16(numericUpDown_FunCardIndex.Value);
            byte powerIndex = Convert.ToByte(numericUpDown_FunPowerIndex.Value);
            if (!InterfaceImport.NP_SetFunPowerState(textBox_FunCardPowerCardID.Text, funIndex, powerIndex, powerState))
            {
                MessageBox.Show("Set power state failed!");
            }
            else
            {
                MessageBox.Show("Set power state succeed!");
            }
        }
        private void button_GetFunPowerState_Click(object sender, EventArgs e)
        {
            if (textBox_FunCardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            ushort funIndex = Convert.ToUInt16(numericUpDown_FunCardIndex.Value);
            byte powerIndex = Convert.ToByte(numericUpDown_FunPowerIndex.Value);
            byte powerState;
            if (!InterfaceImport.NP_GetFunPowerState(textBox_FunCardPowerCardID.Text, funIndex, powerIndex, out powerState))
            {
                label_GetFunPowerStateRes.ForeColor = Color.Red;
                label_GetFunPowerStateRes.Text = "Get power state failed!";
                MessageBox.Show("Get power state failed!");
            }
            else
            {
                if (powerState >= 0
                    && powerState < 2)
                {
                    label_GetFunPowerStateRes.ForeColor = Color.Green;
                    label_GetFunPowerStateRes.Text = comboBox_SetFunPowerState.Items[powerState].ToString();
                }
                else
                {
                    label_GetFunPowerStateRes.ForeColor = Color.Red;
                    label_GetFunPowerStateRes.Text = "Unknown";
                }
                MessageBox.Show("Get power state succeed!");
            }
        }

        private void button_SetFunPowerAutoInfo_Click(object sender, EventArgs e)
        {
            if (textBox_FunCardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_FUNPOWER_AUTOCTRL_INFO autoCtrlInfo = new NP_FUNPOWER_AUTOCTRL_INFO();
            autoCtrlInfo.CtrlInfoArray = new NP_FUNPOWER_AUTOTIME_INFO[8];

            #region 获取电源自动控制信息
            autoCtrlInfo.CtrlInfoArray[0] = new NP_FUNPOWER_AUTOTIME_INFO();
            autoCtrlInfo.CtrlInfoArray[0].OpenTime.Hour = dateTimePicker_Power1StartTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[0].OpenTime.Minute = dateTimePicker_Power1StartTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[0].OpenTime.Second = dateTimePicker_Power1StartTime.Value.Second;
            autoCtrlInfo.CtrlInfoArray[0].CloseTime.Hour = dateTimePicker_Power1StopTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[0].CloseTime.Minute = dateTimePicker_Power1StopTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[0].CloseTime.Second = dateTimePicker_Power1StopTime.Value.Minute;

            autoCtrlInfo.CtrlInfoArray[1] = new NP_FUNPOWER_AUTOTIME_INFO();
            autoCtrlInfo.CtrlInfoArray[1].OpenTime.Hour = dateTimePicker_Power2StartTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[1].OpenTime.Minute = dateTimePicker_Power2StartTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[1].OpenTime.Second = dateTimePicker_Power2StartTime.Value.Second;
            autoCtrlInfo.CtrlInfoArray[1].CloseTime.Hour = dateTimePicker_Power2StopTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[1].CloseTime.Minute = dateTimePicker_Power2StopTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[1].CloseTime.Second = dateTimePicker_Power2StopTime.Value.Minute;

            autoCtrlInfo.CtrlInfoArray[2] = new NP_FUNPOWER_AUTOTIME_INFO();
            autoCtrlInfo.CtrlInfoArray[2].OpenTime.Hour = dateTimePicker_Power3StartTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[2].OpenTime.Minute = dateTimePicker_Power3StartTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[2].OpenTime.Second = dateTimePicker_Power3StartTime.Value.Second;
            autoCtrlInfo.CtrlInfoArray[2].CloseTime.Hour = dateTimePicker_Power3StopTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[2].CloseTime.Minute = dateTimePicker_Power3StopTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[2].CloseTime.Second = dateTimePicker_Power3StopTime.Value.Minute;

            autoCtrlInfo.CtrlInfoArray[3] = new NP_FUNPOWER_AUTOTIME_INFO();
            autoCtrlInfo.CtrlInfoArray[3].OpenTime.Hour = dateTimePicker_Power4StartTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[3].OpenTime.Minute = dateTimePicker_Power4StartTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[3].OpenTime.Second = dateTimePicker_Power4StartTime.Value.Second;
            autoCtrlInfo.CtrlInfoArray[3].CloseTime.Hour = dateTimePicker_Power4StopTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[3].CloseTime.Minute = dateTimePicker_Power4StopTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[3].CloseTime.Second = dateTimePicker_Power4StopTime.Value.Minute;

            autoCtrlInfo.CtrlInfoArray[4] = new NP_FUNPOWER_AUTOTIME_INFO();
            autoCtrlInfo.CtrlInfoArray[4].OpenTime.Hour = dateTimePicker_Power5StartTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[4].OpenTime.Minute = dateTimePicker_Power5StartTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[4].OpenTime.Second = dateTimePicker_Power5StartTime.Value.Second;
            autoCtrlInfo.CtrlInfoArray[4].CloseTime.Hour = dateTimePicker_Power5StopTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[4].CloseTime.Minute = dateTimePicker_Power5StopTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[4].CloseTime.Second = dateTimePicker_Power5StopTime.Value.Minute;

            autoCtrlInfo.CtrlInfoArray[5] = new NP_FUNPOWER_AUTOTIME_INFO();
            autoCtrlInfo.CtrlInfoArray[5].OpenTime.Hour = dateTimePicker_Power6StartTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[5].OpenTime.Minute = dateTimePicker_Power6StartTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[5].OpenTime.Second = dateTimePicker_Power6StartTime.Value.Second;
            autoCtrlInfo.CtrlInfoArray[5].CloseTime.Hour = dateTimePicker_Power6StopTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[5].CloseTime.Minute = dateTimePicker_Power6StopTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[5].CloseTime.Second = dateTimePicker_Power6StopTime.Value.Minute;

            autoCtrlInfo.CtrlInfoArray[6] = new NP_FUNPOWER_AUTOTIME_INFO();
            autoCtrlInfo.CtrlInfoArray[6].OpenTime.Hour = dateTimePicker_Power7StartTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[6].OpenTime.Minute = dateTimePicker_Power7StartTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[6].OpenTime.Second = dateTimePicker_Power7StartTime.Value.Second;
            autoCtrlInfo.CtrlInfoArray[6].CloseTime.Hour = dateTimePicker_Power7StopTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[6].CloseTime.Minute = dateTimePicker_Power7StopTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[6].CloseTime.Second = dateTimePicker_Power7StopTime.Value.Minute;

            autoCtrlInfo.CtrlInfoArray[7] = new NP_FUNPOWER_AUTOTIME_INFO();
            autoCtrlInfo.CtrlInfoArray[7].OpenTime.Hour = dateTimePicker_Power8StartTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[7].OpenTime.Minute = dateTimePicker_Power8StartTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[7].OpenTime.Second = dateTimePicker_Power8StartTime.Value.Second;
            autoCtrlInfo.CtrlInfoArray[7].CloseTime.Hour = dateTimePicker_Power8StopTime.Value.Hour;
            autoCtrlInfo.CtrlInfoArray[7].CloseTime.Minute = dateTimePicker_Power8StopTime.Value.Minute;
            autoCtrlInfo.CtrlInfoArray[7].CloseTime.Second = dateTimePicker_Power8StopTime.Value.Minute;
            #endregion

            if (!InterfaceImport.NP_SetFunPowerAutoInfo(textBox_FunCardPowerCardID.Text,
                                                        Convert.ToUInt16(numericUpDown_FunCardIndex.Value),
                                                        autoCtrlInfo))
            {
                MessageBox.Show("Set power auto ctrl info failed!");
            }
            else
            {
                MessageBox.Show("Set power auto ctrl info succeed!");
            }
        }
        private void button_GetFunPowerAutoInfo_Click(object sender, EventArgs e)
        {
            if (textBox_FunCardPowerCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            ushort funIndex = Convert.ToUInt16(numericUpDown_FunCardIndex.Value);
            NP_FUNPOWER_AUTOCTRL_INFO autoCtrlInfo = new NP_FUNPOWER_AUTOCTRL_INFO();
            if (!InterfaceImport.NP_GetFunPowerAutoInfo(textBox_FunCardPowerCardID.Text, funIndex, out autoCtrlInfo))
            {
                label_GetPowerAutoInfoRes.ForeColor = Color.Red;
                label_GetPowerAutoInfoRes.Text = "Get failed!";
                MessageBox.Show("Get power auto ctrl info failed!");
            }
            else
            {
                #region 更新电源自动控制时间
                label_GetPowerAutoInfoRes.ForeColor = Color.Green;
                label_GetPowerAutoInfoRes.Text = "Get succeed!";

                dateTimePicker_Power1StartTime.Value = new DateTime(2013, 12, 24,
                                                                    autoCtrlInfo.CtrlInfoArray[0].OpenTime.Hour,
                                                                    autoCtrlInfo.CtrlInfoArray[0].OpenTime.Minute,
                                                                    autoCtrlInfo.CtrlInfoArray[0].OpenTime.Second);
                dateTimePicker_Power1StopTime.Value = new DateTime(2013, 12, 24,
                                                                   autoCtrlInfo.CtrlInfoArray[0].CloseTime.Hour,
                                                                   autoCtrlInfo.CtrlInfoArray[0].CloseTime.Minute,
                                                                   autoCtrlInfo.CtrlInfoArray[0].CloseTime.Second);

                dateTimePicker_Power2StartTime.Value = new DateTime(2013, 12, 24,
                                                                    autoCtrlInfo.CtrlInfoArray[1].OpenTime.Hour,
                                                                    autoCtrlInfo.CtrlInfoArray[1].OpenTime.Minute,
                                                                    autoCtrlInfo.CtrlInfoArray[1].OpenTime.Second);
                dateTimePicker_Power2StopTime.Value = new DateTime(2013, 12, 24,
                                                                   autoCtrlInfo.CtrlInfoArray[1].CloseTime.Hour,
                                                                   autoCtrlInfo.CtrlInfoArray[1].CloseTime.Minute,
                                                                   autoCtrlInfo.CtrlInfoArray[1].CloseTime.Second);

                dateTimePicker_Power3StartTime.Value = new DateTime(2013, 12, 24,
                                                                    autoCtrlInfo.CtrlInfoArray[2].OpenTime.Hour,
                                                                    autoCtrlInfo.CtrlInfoArray[2].OpenTime.Minute,
                                                                    autoCtrlInfo.CtrlInfoArray[2].OpenTime.Second);
                dateTimePicker_Power3StopTime.Value = new DateTime(2013, 12, 24,
                                                                   autoCtrlInfo.CtrlInfoArray[2].CloseTime.Hour,
                                                                   autoCtrlInfo.CtrlInfoArray[2].CloseTime.Minute,
                                                                   autoCtrlInfo.CtrlInfoArray[2].CloseTime.Second);

                dateTimePicker_Power4StartTime.Value = new DateTime(2013, 12, 24,
                                                                    autoCtrlInfo.CtrlInfoArray[3].OpenTime.Hour,
                                                                    autoCtrlInfo.CtrlInfoArray[3].OpenTime.Minute,
                                                                    autoCtrlInfo.CtrlInfoArray[3].OpenTime.Second);
                dateTimePicker_Power4StopTime.Value = new DateTime(2013, 12, 24,
                                                                   autoCtrlInfo.CtrlInfoArray[3].CloseTime.Hour,
                                                                   autoCtrlInfo.CtrlInfoArray[3].CloseTime.Minute,
                                                                   autoCtrlInfo.CtrlInfoArray[3].CloseTime.Second);

                dateTimePicker_Power5StartTime.Value = new DateTime(2013, 12, 24,
                                                                    autoCtrlInfo.CtrlInfoArray[4].OpenTime.Hour,
                                                                    autoCtrlInfo.CtrlInfoArray[4].OpenTime.Minute,
                                                                    autoCtrlInfo.CtrlInfoArray[4].OpenTime.Second);
                dateTimePicker_Power5StopTime.Value = new DateTime(2013, 12, 24,
                                                                   autoCtrlInfo.CtrlInfoArray[4].CloseTime.Hour,
                                                                   autoCtrlInfo.CtrlInfoArray[4].CloseTime.Minute,
                                                                   autoCtrlInfo.CtrlInfoArray[4].CloseTime.Second);

                dateTimePicker_Power6StartTime.Value = new DateTime(2013, 12, 24,
                                                                    autoCtrlInfo.CtrlInfoArray[5].OpenTime.Hour,
                                                                    autoCtrlInfo.CtrlInfoArray[5].OpenTime.Minute,
                                                                    autoCtrlInfo.CtrlInfoArray[5].OpenTime.Second);
                dateTimePicker_Power6StopTime.Value = new DateTime(2013, 12, 24,
                                                                   autoCtrlInfo.CtrlInfoArray[5].CloseTime.Hour,
                                                                   autoCtrlInfo.CtrlInfoArray[5].CloseTime.Minute,
                                                                   autoCtrlInfo.CtrlInfoArray[5].CloseTime.Second);

                dateTimePicker_Power7StartTime.Value = new DateTime(2013, 12, 24,
                                                                    autoCtrlInfo.CtrlInfoArray[6].OpenTime.Hour,
                                                                    autoCtrlInfo.CtrlInfoArray[6].OpenTime.Minute,
                                                                    autoCtrlInfo.CtrlInfoArray[6].OpenTime.Second);
                dateTimePicker_Power7StopTime.Value = new DateTime(2013, 12, 24,
                                                                   autoCtrlInfo.CtrlInfoArray[6].CloseTime.Hour,
                                                                   autoCtrlInfo.CtrlInfoArray[6].CloseTime.Minute,
                                                                   autoCtrlInfo.CtrlInfoArray[6].CloseTime.Second);

                dateTimePicker_Power8StartTime.Value = new DateTime(2013, 12, 24,
                                                                    autoCtrlInfo.CtrlInfoArray[7].OpenTime.Hour,
                                                                    autoCtrlInfo.CtrlInfoArray[7].OpenTime.Minute,
                                                                    autoCtrlInfo.CtrlInfoArray[7].OpenTime.Second);
                dateTimePicker_Power8StopTime.Value = new DateTime(2013, 12, 24,
                                                                   autoCtrlInfo.CtrlInfoArray[7].CloseTime.Hour,
                                                                   autoCtrlInfo.CtrlInfoArray[7].CloseTime.Minute,
                                                                   autoCtrlInfo.CtrlInfoArray[7].CloseTime.Second);
                #endregion

                MessageBox.Show("Get power auto ctrl info succeed!");
            }
        }
        #endregion

        #region Bright
        private void hScrollBar_SetBrightValue_Scroll(object sender, ScrollEventArgs e)
        {
            label_SetBrightValue.Text = hScrollBar_SetBrightValue.Value.ToString();
        }
        private void button_SetBrightValue_Click(object sender, EventArgs e)
        {
            if (textBox_BrightCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            byte value = Convert.ToByte(hScrollBar_SetBrightValue.Value);
            if (!InterfaceImport.NP_SetCardBrightValue(textBox_BrightCardID.Text, value))
            {
                MessageBox.Show("Set bright value failed!");
            }
            else
            {
                MessageBox.Show("Set bright value succeed!");
            }
        }
        private void button_GetBrightValue_Click(object sender, EventArgs e)
        {
            if (textBox_BrightCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            byte brightValue;
            if (!InterfaceImport.NP_GetCardBrightValue(textBox_BrightCardID.Text, out brightValue))
            {
                label_GetBrightValue.ForeColor = Color.Red;
                label_GetBrightValue.Text = "Get bright value failed!";
                MessageBox.Show("Get bright value failed!");
            }
            else
            {
                label_GetBrightValue.ForeColor = Color.Green;
                label_GetBrightValue.Text = brightValue.ToString();
                MessageBox.Show("Get bright value succeed!");
            }
        }

        private void button_SetBrightMode_Click(object sender, EventArgs e)
        {
            if (textBox_BrightCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            ushort brightMode = (ushort)comboBox_SetBrightMode.SelectedIndex;
            if (!InterfaceImport.NP_SetCardBrightMode(textBox_BrightCardID.Text, brightMode))
            {
                MessageBox.Show("Set bright mode failed!");
            }
            else
            {
                MessageBox.Show("Set bright mode succeed!");
            }
        }
        private void button_GetBrightMode_Click(object sender, EventArgs e)
        {
            if (textBox_BrightCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            ushort brightMode;
            if (!InterfaceImport.NP_GetCardBrightMode(textBox_BrightCardID.Text, out brightMode))
            {
                label_GetBrightMode.ForeColor = Color.Red;
                label_GetBrightMode.Text = "Get bright mode failed!";
                MessageBox.Show("Get bright mode failed!");
            }
            else
            {
                if (brightMode >= 0
                    && brightMode <= 2)
                {
                    label_GetBrightMode.ForeColor = Color.Green;
                    label_GetBrightMode.Text = comboBox_SetBrightMode.Items[brightMode].ToString();
                }
                else
                {
                    label_GetBrightMode.ForeColor = Color.Red;
                    label_GetBrightMode.Text = "Unknown";
                }
                MessageBox.Show("Get bright mode succeed!");
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            Frm_EditSchedualBright addAdjustFrm = new Frm_EditSchedualBright(new NP_BRIGHTADJUST_INFO());
            if (addAdjustFrm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            NP_BRIGHTADJUST_INFO adjustInfo = addAdjustFrm.BrightInfo;
            UpdateOneSchedualBirghtInfo(dataGridView_SchedualBright.RowCount, adjustInfo);
        }
        private void button_Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView_SchedualBright.SelectedRows.Count <= 0)
            {
                return;
            }
            int nIndex = dataGridView_SchedualBright.SelectedRows[0].Index;
            NP_BRIGHTADJUST_INFO adjustInfo = (NP_BRIGHTADJUST_INFO)dataGridView_SchedualBright.Rows[nIndex].Tag;

            Frm_EditSchedualBright editAdjustFrm = new Frm_EditSchedualBright(adjustInfo);
            if (editAdjustFrm.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            adjustInfo = editAdjustFrm.BrightInfo;
            UpdateOneSchedualBirghtInfo(nIndex, adjustInfo);
        }
        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView_SchedualBright.SelectedRows.Count <= 0)
            {
                return;
            }
            int nIndex = dataGridView_SchedualBright.SelectedRows[0].Index;
            dataGridView_SchedualBright.Rows.RemoveAt(nIndex);
        }
        private void button_GetSchedualBright_Click(object sender, EventArgs e)
        {
            if (textBox_BrightCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_SCHEDUALBRIGHT_INFO adjustInfo;
            if (!InterfaceImport.NP_GetSchedualBrightInfo(textBox_BrightCardID.Text, out adjustInfo))
            {
                label_GetSchedualBrightRes.ForeColor = Color.Red;
                label_GetSchedualBrightRes.Text = "Get schedual bright info failed!";
                MessageBox.Show("Get schedual bright info failed!");
            }
            else
            {
                dataGridView_SchedualBright.Rows.Clear();
                IntPtr ptr;
                NP_BRIGHTADJUST_INFO tempInfo;
                int valueSize = Marshal.SizeOf(new NP_BRIGHTADJUST_INFO());
                for (int i = 0; i < adjustInfo.AdjustInfoCount; i++)
                {
                    ptr = (IntPtr)((UInt32)adjustInfo.AdjustInfoArrayPtr + valueSize * i);
                    tempInfo = (NP_BRIGHTADJUST_INFO)Marshal.PtrToStructure(ptr, typeof(NP_BRIGHTADJUST_INFO));

                    UpdateOneSchedualBirghtInfo(i, tempInfo);
                }
                label_GetSchedualBrightRes.ForeColor = Color.Green;
                label_GetSchedualBrightRes.Text = "Get schedual bright info succeed!";
                MessageBox.Show("Get schedual bright info succeed!");
            }
        }
        private void button_SetSchedualBright_Click(object sender, EventArgs e)
        {
            if (textBox_BrightCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            int infoCnt = dataGridView_SchedualBright.RowCount;
            NP_BRIGHTADJUST_INFO[] ctrlInfoArray = new NP_BRIGHTADJUST_INFO[infoCnt];
            for (int i = 0; i < infoCnt; i++)
            {
                ctrlInfoArray[i] = (NP_BRIGHTADJUST_INFO)dataGridView_SchedualBright.Rows[i].Tag;
            }

            NP_SCHEDUALBRIGHT_INFO adjustInfo = new NP_SCHEDUALBRIGHT_INFO();
            adjustInfo.AdjustInfoCount = (ushort)infoCnt;
            adjustInfo.AdjustInfoArrayPtr = GetPtrFromBrightAjustArray(ctrlInfoArray);

            if (!InterfaceImport.NP_SetSchedualBrightInfo(textBox_BrightCardID.Text, adjustInfo))
            {
                MessageBox.Show("Set schedual bright info failed!");
            }
            else
            {
                MessageBox.Show("Set schedual bright info succeed!");
            }
        }
        #endregion

        #region StorageInfo
        private void button_GetStorageDeviceInfo_Click(object sender, EventArgs e)
        {
            if (textBox_StorageInfoCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            NP_STORAGEDEVICE_INFO storageDeviceInfo;
            if (!InterfaceImport.NP_GetStorageDeviceInfo(textBox_StorageInfoCardID.Text, out storageDeviceInfo))
            {
                MessageBox.Show("Get storage device info failed!");
            }
            else
            {
                UpdateStorageDeviceInfo(storageDeviceInfo);
                MessageBox.Show("Get storage device info succeed!");
            }
        }

        private void button_DeleteAllMedia_Click(object sender, EventArgs e)
        {
            if (textBox_StorageInfoCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (!InterfaceImport.NP_DeleteMedia(textBox_StorageInfoCardID.Text, 0))
            {
                label_DeleteAllMediaResult.ForeColor = Color.Red;
                label_DeleteAllMediaResult.Text = "Delete all media failed!";
                MessageBox.Show("Delete all media failed!");
            }
            else
            {
                label_DeleteAllMediaResult.ForeColor = Color.Green;
                label_DeleteAllMediaResult.Text = "Delete all media succeed!";
                MessageBox.Show("Delete all media succeed!");
            }
        }
        private void button_DeleteInvalidMedia_Click(object sender, EventArgs e)
        {
            if (textBox_StorageInfoCardID.Text == "")
            {
                MessageBox.Show("Please input card ID!");
                return;
            }
            if (!InterfaceImport.NP_DeleteMedia(textBox_StorageInfoCardID.Text, 1))
            {
                label_DeleteInvalidMedia.ForeColor = Color.Red;
                label_DeleteInvalidMedia.Text = "Delte invalid media failed!";
                MessageBox.Show("Delte invalid media failed!");
            }
            else
            {
                label_DeleteInvalidMedia.ForeColor = Color.Green;
                label_DeleteInvalidMedia.Text = "Delte invalid media succeed!";
                MessageBox.Show("Delte invalid media succeed!");
            }
        }
        #endregion

        private void timer_GetPercent_Tick(object sender, EventArgs e)
        {
            if (toolStripProgressBar_Percent.Value >= toolStripProgressBar_Percent.Maximum)
            {
                toolStripProgressBar_Percent.Value = 0;
            }
            else
            {
                toolStripProgressBar_Percent.Value += toolStripProgressBar_Percent.Step;
            }
        }

        private void NovaPlutoSDKTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            InterfaceImport.NP_UnInitialize();
        }

        private string GetAvaliableIPAddress()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            string strIp;
            foreach (IPAddress ip in ips)
            {
                strIp = ip.ToString();
                #region 判断IP格式是否正确（排除win7和vista下的乱地址IP
                string[] ipPart = strIp.Split('.');
                if (ipPart.Length == 4)
                {
                    int partValue;
                    if (int.TryParse(ipPart[0], out partValue)
                        && int.TryParse(ipPart[1], out partValue)
                        && int.TryParse(ipPart[2], out partValue)
                        && int.TryParse(ipPart[3], out partValue))
                    {
                        return strIp;
                    }
                }
                #endregion
            }
            return "";
        }

        private LOGFONTW GetDefaultLOGFONT()
        {
            Font font = new Font("宋体", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            LOGFONTW logFont = new LOGFONTW();
            logFont.lfCharSet = font.GdiCharSet;
            logFont.lfFaceName = font.Name;
            logFont.lfHeight = font.Height;
            logFont.lfWidth = 15;
            logFont.lfWeight = 25;
            logFont.lfUnderline = 1;
            return logFont;
        }

        private delegate void CompleteConnectCardListDelegate(NP_CARD_INFO cardInfo);
        private void CompleteConnectCardList(NP_CARD_INFO cardInfo)
        {
            if (!this.InvokeRequired)
            {
                int cardExistIndex = -1;
                for (int i = 0; i < _cardInfoList.Count; i++)
                {
                    if (_cardInfoList[i].ID == cardInfo.ID)
                    {
                        cardExistIndex = i;
                        break;
                    }
                }
                string cardInfoStr = cardInfo.Name + "(ID: " + cardInfo.ID +")"
                                     + "  " + cardInfo.IP + "  - Connect";
                if (cardExistIndex == -1)
                {
                    listBox_CardList.Items.Add(cardInfoStr);
                    _cardInfoList.Add(cardInfo);
                }
                else
                {
                    listBox_CardList.Items[cardExistIndex] = cardInfoStr;
                }
            }
            else
            {
                CompleteConnectCardListDelegate uc = new CompleteConnectCardListDelegate(CompleteConnectCardList);
                this.Invoke(uc, new object[] { cardInfo });
            }
        }

        private delegate void DisconnectCardListDelegate(string cardID);
        private void DisconnectCardList(string cardID)
        {
            if (!this.InvokeRequired)
            {
                for (int i = 0; i < _cardInfoList.Count; i++)
                {
                    if (_cardInfoList[i].ID == cardID)
                    {
                        string cardInfoStr = _cardInfoList[i].Name + "(ID: " 
                                             + _cardInfoList[i].ID + ")"
                                             + "  " + _cardInfoList[i].IP
                                             + "  - DisConnect";;
                        listBox_CardList.Items[i] = cardInfoStr;
                        break;
                    }
                }
            }
            else
            {
                DisconnectCardListDelegate uc = new DisconnectCardListDelegate(DisconnectCardList);
                this.Invoke(uc, new object[] { cardID });
            }
        }

        private delegate void GetCardInfoDelegate(NP_CARD_INFO cardInfo);
        private void GetCardInfo(NP_CARD_INFO cardInfo)
        {
            if (!this.InvokeRequired)
            {
                string cardInfoStr = "Name:" + cardInfo.Name +
                                     " ID:" + cardInfo.ID +
                                     " IP:" + cardInfo.IP +
                                     " Screen:" + cardInfo.ScreenWidth +
                                     "*" + cardInfo.ScreenHeight;
                listBox_CardInfo.Items.Add(cardInfoStr);
            }
            else
            {
                GetCardInfoDelegate uc = new GetCardInfoDelegate(GetCardInfo);
                this.Invoke(uc, new object[] { cardInfo });
            }
        }

        private delegate void EnableGetInfoPercentDelegate(bool isEnable, Color infoClr, string info);
        private void EnableGetInfoPercent(bool isEnable, Color infoClr, string info)
        {
            if (this.InvokeRequired)
            {
                EnableGetInfoPercentDelegate eg = new EnableGetInfoPercentDelegate(EnableGetInfoPercent);
                this.Invoke(eg, new object[] { isEnable, infoClr, info });
            }
            else
            {
                timer_GetPercent.Enabled = isEnable;
                toolStripProgressBar_Percent.Visible = isEnable;
                toolStripStatusLabel_Info.Text = info;
                toolStripStatusLabel_Info.ForeColor = infoClr;
            }
        }

        private delegate void UpdateDetectPointResultDelegate(bool isOK, NP_DETECTPOINT_RESULT result);
        private void UpdateDetectPointResult(bool isOK, NP_DETECTPOINT_RESULT result)
        {
            if (!this.InvokeRequired)
            {
                treeView_DetectPointResult.Nodes.Clear();
                if (!isOK)
                {
                    label_DetectPointResult.ForeColor = Color.Red;
                    label_DetectPointResult.Text = "Detect point failed!";
                    return;
                }
                label_DetectPointResult.ForeColor = Color.Green;
                label_DetectPointResult.Text = "Detect point ok!";

                string info = "Cabinet index:" + result.CabinetIndex.ToString();
                TreeNode node = new TreeNode(info);
                treeView_DetectPointResult.Nodes.Add(node);

                info = "Cabinet region(x,y,w,h):("
                              + result.CabinetRegion.X.ToString() + ","
                              + result.CabinetRegion.Y.ToString() + ","
                              + result.CabinetRegion.Width.ToString() + ","
                              + result.CabinetRegion.Height.ToString() + ")";
                node = new TreeNode(info);
                treeView_DetectPointResult.Nodes.Add(node);

                info = "Cabinet Total pixel:" + result.PixelTotalCount.ToString();
                node = new TreeNode(info);
                treeView_DetectPointResult.Nodes.Add(node);

                info = "Detect Error point:" + result.ErrorPointInfo.ErrorTotalCount.ToString();
                node = new TreeNode(info);
                treeView_DetectPointResult.Nodes.Add(node);

                NP_POINT point;
                IntPtr ptr;
                int pointSize = Marshal.SizeOf(new NP_POINT());

                #region Red
            	info = "Red error point(Count:" + result.ErrorPointInfo.ErrorRedCount.ToString() + ")";
                node = new TreeNode(info);
                treeView_DetectPointResult.Nodes[3].Nodes.Add(node);
                for(int i = 0; i < result.ErrorPointInfo.ErrorRedCount; i++)
                {
                    ptr = (IntPtr)((UInt32)result.ErrorPointInfo.ErrorRedPoint + pointSize * i);
                    point = (NP_POINT)Marshal.PtrToStructure(ptr, typeof(NP_POINT));

                    info = "(x,y) : (" + point.X.ToString() 
                           + ", " + point.Y.ToString() + ")";
                    node = new TreeNode(info);
                    treeView_DetectPointResult.Nodes[3].Nodes[0].Nodes.Add(node);
                }
	            #endregion

                #region Green
            	info = "Green error point(Count:" + result.ErrorPointInfo.ErrorGreenCount.ToString() + ")";
                node = new TreeNode(info);
                treeView_DetectPointResult.Nodes[3].Nodes.Add(node);
                for(int i = 0; i < result.ErrorPointInfo.ErrorGreenCount; i++)
                {
                    ptr = (IntPtr)((UInt32)result.ErrorPointInfo.ErrorGreenPoint + pointSize * i);
                    point = (NP_POINT)Marshal.PtrToStructure(ptr, typeof(NP_POINT));

                    info = "(x,y) : (" + point.X.ToString() 
                           + ", " + point.Y.ToString() + ")";
                    node = new TreeNode(info);
                    treeView_DetectPointResult.Nodes[3].Nodes[1].Nodes.Add(node);
                }
	            #endregion

                #region Blue
            	info = "Blue error point(Count:" + result.ErrorPointInfo.ErrorBlueCount.ToString() + ")";
                node = new TreeNode(info);
                treeView_DetectPointResult.Nodes[3].Nodes.Add(node);
                for(int i = 0; i < result.ErrorPointInfo.ErrorBlueCount; i++)
                {
                    ptr = (IntPtr)((UInt32)result.ErrorPointInfo.ErrorBluePoint + pointSize * i);

                    point = (NP_POINT)Marshal.PtrToStructure(ptr, typeof(NP_POINT));

                    info = "(x,y) : (" + point.X.ToString() 
                           + ", " + point.Y.ToString() + ")";
                    node = new TreeNode(info);
                    treeView_DetectPointResult.Nodes[3].Nodes[2].Nodes.Add(node);
                }
	            #endregion

                #region VRed
            	info = "virtual red error point(Count:" + result.ErrorPointInfo.ErrorVRedCount.ToString() + ")";
                node = new TreeNode(info);
                treeView_DetectPointResult.Nodes[3].Nodes.Add(node);
                for(int i = 0; i < result.ErrorPointInfo.ErrorVRedCount; i++)
                {
                    ptr = (IntPtr)((UInt32)result.ErrorPointInfo.ErrorVRedPoint + pointSize * i);
                    point = (NP_POINT)Marshal.PtrToStructure(ptr, typeof(NP_POINT));

                    info = "(x,y) : (" + point.X.ToString() 
                           + ", " + point.Y.ToString() + ")";
                    node = new TreeNode(info);
                    treeView_DetectPointResult.Nodes[3].Nodes[3].Nodes.Add(node);
                }
	            #endregion

                try
                {
                    //释放内存空间
                    Marshal.FreeHGlobal(result.ErrorPointInfo.ErrorRedPoint);
                    Marshal.FreeHGlobal(result.ErrorPointInfo.ErrorGreenPoint);
                    Marshal.FreeHGlobal(result.ErrorPointInfo.ErrorBluePoint);
                    Marshal.FreeHGlobal(result.ErrorPointInfo.ErrorVRedPoint);
                }
                catch
                {

                }
            }
            else
            {
                UpdateDetectPointResultDelegate ud = new UpdateDetectPointResultDelegate(UpdateDetectPointResult);
                this.Invoke(ud, new object[] { isOK, result });
            }
        }

        private delegate void UpdateRefreshMonitorResDelegate(Color foreClr, string res);
        private void UpdateRefreshMonitorRes(Color foreClr, string res)
        {
            if (!this.InvokeRequired)
            {
                label_MonitorResult.ForeColor = foreClr;
                label_MonitorResult.Text = res;
            }
            else
            {
                UpdateRefreshMonitorResDelegate ur = new UpdateRefreshMonitorResDelegate(UpdateRefreshMonitorRes);
                this.Invoke(ur, new object[] { foreClr, res });
            }
        }

        private void UpdateMonitorInfo(NP_MONITOR_INFO monitorInfo)
        {
            string info = "Cabinet index:" + monitorInfo.CabinetIndex.ToString();
            TreeNode node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            info = "Cabinet region(x,y,w,h):("
                          + monitorInfo.CabinetRegion.X.ToString() + ","
                          + monitorInfo.CabinetRegion.Y.ToString() + ","
                          + monitorInfo.CabinetRegion.Width.ToString() + ","
                          + monitorInfo.CabinetRegion.Height.ToString() + ")";
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            info = "CabinetStatusIsOK: " + monitorInfo.CabinetStatusIsOK.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            #region CabinetVoltage
            info = "CabinetVoltage";
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            info = "IsValid: " + monitorInfo.CabinetVoltage.IsValid.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[3].Nodes.Add(node);

            info = "Value: " + monitorInfo.CabinetVoltage.Value.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[3].Nodes.Add(node);
            #endregion

            #region TempInfo
            info = "TempInfo";
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            info = "IsValid: " + monitorInfo.TempInfo.IsValid.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[4].Nodes.Add(node);

            info = "Value: " + monitorInfo.TempInfo.Value.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[4].Nodes.Add(node);
            #endregion

            info = "IsConnectMCard: " + monitorInfo.IsConnectMCard.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            #region MCardVoltage
            info = "MCardVoltage";
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            info = "IsValid: " + monitorInfo.MCardVoltage.IsValid.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[6].Nodes.Add(node);

            info = "Value: " + monitorInfo.MCardVoltage.Value.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[6].Nodes.Add(node);
            #endregion

            #region HumidityInfo
            info = "HumidityInfo";
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            info = "IsValid: " + monitorInfo.HumidityInfo.IsValid.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[7].Nodes.Add(node);

            info = "Value: " + monitorInfo.HumidityInfo.Value.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[7].Nodes.Add(node);
            #endregion

            info = "CabinetDoorIsClose: " + monitorInfo.CabinetDoorIsClose.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            #region SmokeAlarmInfo
            info = "SmokeAlarmInfo";
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            info = "IsValid: " + monitorInfo.SmokeAlarmInfo.IsValid.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[9].Nodes.Add(node);

            info = "IsAlarm: " + monitorInfo.SmokeAlarmInfo.IsAlarm.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes[9].Nodes.Add(node);
            #endregion

            int valueSize = Marshal.SizeOf(new NP_VALUE_INFO());
            NP_VALUE_INFO valueInfo;
            IntPtr ptr;

            #region FanInfo
            info = "Fan count: " + monitorInfo.FanCount.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            for (int i = 0; i < monitorInfo.FanCount; i++)
            {
                info = "FanInfo(Index: " + i.ToString() + ")";
                node = new TreeNode(info);
                treeView_MonitorInfo.Nodes[10].Nodes.Add(node);


                ptr = (IntPtr)((UInt32)monitorInfo.FanInfoPtr + valueSize * i);
                valueInfo = (NP_VALUE_INFO)Marshal.PtrToStructure(ptr, typeof(NP_VALUE_INFO));

                info = "IsValid: " + valueInfo.IsValid.ToString();
                node = new TreeNode(info);
                treeView_MonitorInfo.Nodes[10].Nodes[i].Nodes.Add(node);

                info = "Value: " + valueInfo.Value.ToString();
                node = new TreeNode(info);
                treeView_MonitorInfo.Nodes[10].Nodes[i].Nodes.Add(node);
            }
            #endregion

            #region PowerInfo
            info = "MC power count: " + monitorInfo.MCPowerCount.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);

            for (int i = 0; i < monitorInfo.MCPowerCount; i++)
            {
                info = "VoltageInfo(Index: " + i.ToString() + ")";
                node = new TreeNode(info);
                treeView_MonitorInfo.Nodes[11].Nodes.Add(node);


                ptr = (IntPtr)((UInt32)monitorInfo.MCVoltagePtr + valueSize * i);
                valueInfo = (NP_VALUE_INFO)Marshal.PtrToStructure(ptr, typeof(NP_VALUE_INFO));

                info = "IsValid: " + valueInfo.IsValid.ToString();
                node = new TreeNode(info);
                treeView_MonitorInfo.Nodes[11].Nodes[i].Nodes.Add(node);

                info = "Value: " + valueInfo.Value.ToString();
                node = new TreeNode(info);
                treeView_MonitorInfo.Nodes[11].Nodes[i].Nodes.Add(node);
            }
            #endregion

            #region RowLine
            info = "IsRowLineOK: " + monitorInfo.IsRowLineOK.ToString();
            node = new TreeNode(info);
            treeView_MonitorInfo.Nodes.Add(node);
            #endregion

            //try
            //{
            //    //释放内存空间
            //    Marshal.FreeHGlobal(monitorInfo.FanInfoPtr);
            //    Marshal.FreeHGlobal(monitorInfo.MCVoltagePtr);
            //}
            //catch
            //{

            //}
        }
        private void UpdateGlobalMonitorInfo(NP_GLOBALMONITOR_INFO globalMonitorInfo)
        {
            treeView_GlobalMonitorInfo.Nodes.Clear();

            string info = "CabinetStatusIsOK: " + globalMonitorInfo.CabinetStatusIsOK.ToString();
            TreeNode node = new TreeNode(info);
            treeView_GlobalMonitorInfo.Nodes.Add(node);

            info = "CabinetVoltageIsOK: " + globalMonitorInfo.CabinetVoltageIsOK.ToString();
            node = new TreeNode(info);
            treeView_GlobalMonitorInfo.Nodes.Add(node);

            info = "IsConnectMonitorCard: " + globalMonitorInfo.IsConnectMonitorCard.ToString();
            node = new TreeNode(info);
            treeView_GlobalMonitorInfo.Nodes.Add(node);

            info = "MCVoltageIsOK: " + globalMonitorInfo.MCVoltageIsOK.ToString();
            node = new TreeNode(info);
            treeView_GlobalMonitorInfo.Nodes.Add(node);

            info = "CabinetDoorIsClose: " + globalMonitorInfo.CabinetDoorIsClose.ToString();
            node = new TreeNode(info);
            treeView_GlobalMonitorInfo.Nodes.Add(node);

            info = "RowLineIsOK: " + globalMonitorInfo.RowLineIsOK.ToString();
            node = new TreeNode(info);
            treeView_GlobalMonitorInfo.Nodes.Add(node);

            info = "LightSensorIsOK: " + globalMonitorInfo.LightSensorIsOK.ToString();
            node = new TreeNode(info);
            treeView_GlobalMonitorInfo.Nodes.Add(node);
        }

        private void UpdateOneBDPowerAutoInfo(int nRowIndex, NP_BDPOWER_AUTOTIME_INFO ctrlInfo)
        {
            if (nRowIndex < 0
                || nRowIndex >= dataGridView_OnBoardPowerAutoInfo.RowCount)
            {
                dataGridView_OnBoardPowerAutoInfo.RowCount++;
                nRowIndex = dataGridView_OnBoardPowerAutoInfo.RowCount - 1;
            }
            string startDateStr = "0001-01-01";
            string endDateStr = "9999-00-00";
            if (ctrlInfo.IsSpecialDate)
            {
                startDateStr = ctrlInfo.StartDate.Year + "-" + ctrlInfo.StartDate.Month + "-" + ctrlInfo.StartDate.Day;
                endDateStr = ctrlInfo.StopDate.Year + "-" + ctrlInfo.StopDate.Month + "-" + ctrlInfo.StopDate.Day;
            }
            string validWeekDayStr = "";
            if (ctrlInfo.WeekDayIsValid != null)
            {
                for (int i = 1; i < ctrlInfo.WeekDayIsValid.Length; i++)
                {
                    if (ctrlInfo.WeekDayIsValid[i])
                    {
                        validWeekDayStr += i.ToString() + ",";
                    }
                }
                if (ctrlInfo.WeekDayIsValid[0])
                {
                    validWeekDayStr += "7";
                }
            }
            string openTimeStr = ctrlInfo.OpenTime.Hour + ":" 
                                 + ctrlInfo.OpenTime.Minute;
            string closeTimeStr = ctrlInfo.CloseTime.Hour + ":"
                                 + ctrlInfo.CloseTime.Minute;
            dataGridView_OnBoardPowerAutoInfo.Rows[nRowIndex].SetValues(new object[] { startDateStr, 
                                                                                       endDateStr,
                                                                                       validWeekDayStr,
                                                                                       openTimeStr,
                                                                                       closeTimeStr
                                                                                      });
            dataGridView_OnBoardPowerAutoInfo.Rows[nRowIndex].Tag = ctrlInfo;
        }
        private IntPtr GetPtrFromOnBoardCtrlArray(NP_BDPOWER_AUTOTIME_INFO[] ctrlInfoArray)
        {
            int size = Marshal.SizeOf(new NP_BDPOWER_AUTOTIME_INFO());
            int length = 0;
            if (ctrlInfoArray != null)
            {
                length = ctrlInfoArray.Length;
            }
            IntPtr structPtr = Marshal.AllocHGlobal(size * length);

            //初始化指针指向的内存
            ZeroMemory(structPtr, size * length);

            int start = (int)structPtr;
            for (int i = 0; i < length; i++)
            {
                Marshal.StructureToPtr(ctrlInfoArray[i], (IntPtr)(start + size * i), true);
            }
            return structPtr;
        }

        private void UpdateOneSchedualBirghtInfo(int nRowIndex, NP_BRIGHTADJUST_INFO adjustInfo)
        {
            if (nRowIndex < 0
                || nRowIndex >= dataGridView_SchedualBright.RowCount)
            {
                dataGridView_SchedualBright.RowCount++;
                nRowIndex = dataGridView_SchedualBright.RowCount - 1;
            }
            string timeStr = adjustInfo.AdjustTime.Hour + ":"
                            + adjustInfo.AdjustTime.Minute;
            dataGridView_SchedualBright.Rows[nRowIndex].SetValues(new object[] { timeStr, adjustInfo.BrightValue });
            dataGridView_SchedualBright.Rows[nRowIndex].Tag = adjustInfo;
        }
        private IntPtr GetPtrFromBrightAjustArray(NP_BRIGHTADJUST_INFO[] adjustInfoArray)
        {
            int size = Marshal.SizeOf(new NP_BRIGHTADJUST_INFO());
            int length = 0;
            if (adjustInfoArray != null)
            {
                length = adjustInfoArray.Length;
            }
            IntPtr structPtr = Marshal.AllocHGlobal(size * length);

            //初始化指针指向的内存
            ZeroMemory(structPtr, size * length);

            int start = (int)structPtr;
            for (int i = 0; i < length; i++)
            {
                Marshal.StructureToPtr(adjustInfoArray[i], (IntPtr)(start + size * i), true);
            }
            return structPtr;
        }

        private void UpdateStorageDeviceInfo(NP_STORAGEDEVICE_INFO storageInfo)
        {
            if (storageInfo.CurStorageDeviceType == 0)
            {
                label_CurrentInfo.Text = "Flash";
            }
            else if (storageInfo.CurStorageDeviceType == 1)
            {
                label_CurrentInfo.Text = "SD Card";
            }
            else if (storageInfo.CurStorageDeviceType == 2)
            {
                label_CurrentInfo.Text = "U Disk";
            }
            label_FlashInfo.Text = "Total size: " + storageInfo.FlashTotalSpace.ToString() + "     "
                                   + "Free size: " + storageInfo.FlashFreeSpace.ToString();
            label_SDCardInfo.Text = "Total size: " + storageInfo.SDCardTotalSpace.ToString() + "     "
                                    + "Free size: " + storageInfo.SDCardFreeSpace.ToString();
            label_UDiskInfo.Text = "Total size: " + storageInfo.UDiskTotalSpace.ToString() + "     "
                                   + "Free size: " + storageInfo.UDiskFreeSpace.ToString();
        }
    }
}