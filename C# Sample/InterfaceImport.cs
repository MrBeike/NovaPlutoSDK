using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CSharpTest
{
    public class InterfaceImport
    {
        #region 引入SDK接口
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_Initialize(IntPtr hwnd, int recvMsgID, string serverIP, int serverPort, string recvSavePath);
        [DllImport("NovaPlutoManager.dll")]
        public extern static void NP_UnInitialize();
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_ConnectCardOfLocalNet(string cardIP, bool isSync, out NP_CARD_INFO cardInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetCardInfo(string cardID, bool isSync, out NP_CARD_INFO cardInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static IntPtr NP_CreatePlayProgram(string fileName, NP_SIZE screenSize);
        [DllImport("NovaPlutoManager.dll")]
        public extern static IntPtr NP_OpenPlayProgram(string fileName);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddTimeSegment(IntPtr programHwnd, NP_TIMESEGMENT_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddBasicPage(IntPtr programHwnd, ushort timeSegmentIndex, NP_PAGE_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddWindowToPage(IntPtr programHwnd, ushort timeSegmentIndex,
                                                     ushort pageIndex, NP_WINDOW_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddVideoFile(IntPtr programHwnd, ushort timeSegmentIndex,
                                                  ushort pageIndex, uint windowIndex,
                                                  string fileName, NP_VIDEOFIEL_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddImageFile(IntPtr programHwnd, ushort timeSegmentIndex,
                                                  ushort pageIndex, uint windowIndex,
                                                  string fileName, NP_IMAGEFILE_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddFlashFile(IntPtr programHwnd, ushort timeSegmentIndex,
                                                  ushort pageIndex, uint windowIndex,
                                                  string fileName, NP_FLASH_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddTxtFile(IntPtr programHwnd, ushort timeSegmentIndex,
                                                ushort pageIndex, uint windowIndex,
                                                string fileName, NP_TXTFILE_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddAnalogClock(IntPtr programHwnd, ushort timeSegmentIndex,
                                                    ushort pageIndex, uint windowIndex,
                                                    NP_ANALOGCLOCK_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddDigitalClock(IntPtr programHwnd, ushort timeSegmentIndex,
                                                     ushort pageIndex, uint windowIndex,
                                                     NP_DIGITALCLOCK_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddSingleLineText(IntPtr programHwnd, ushort timeSegmentIndex,
                                                       ushort pageIndex, uint windowIndex,
                                                       NP_SINGLELINETEXT_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddScrollingText(IntPtr programHwnd, ushort timeSegmentIndex,
                                                      ushort pageIndex, uint windowIndex,
                                                      NP_SCROLLTEXT_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddStaticText(IntPtr programHwnd, ushort timeSegmentIndex,
                                                   ushort pageIndex, uint windowIndex,
                                                   NP_STATICTEXT_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_AddWeather(IntPtr programHwnd, ushort timeSegmentIndex,
                                                ushort pageIndex, uint windowIndex,
                                                NP_WEATHER_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_RemoveMedia(IntPtr programHwnd, ushort timeSegmentIndex,
                                                 ushort pageIndex, uint windowIndex, uint mediaIndex);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_RemoveWindow(IntPtr programHwnd, ushort timeSegmentIndex,
                                                  ushort pageIndex, uint windowIndex);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_RemovePage(IntPtr programHwnd, ushort timeSegmentIndex,
                                                ushort pageIndex);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_RemoveTimeSegment(IntPtr programHwnd, ushort timeSegmentIndex);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SavePlayProgram(IntPtr programHwnd);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_ClosePlayProgram(IntPtr programHwnd);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SendPlayProgram(string cardID, ushort saveDevice, string playProgramPath);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SendVideoInsertPlay(string cardID, string fileName,
                                                         NP_SIZE windowSize,
                                                         NP_TIMESPAN playDuration,
                                                         NP_VIDEOFIEL_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SendImageInsertPlay(string cardID, string fileName,
                                                         NP_SIZE windowSize,
                                                         NP_TIMESPAN playDuration,
                                                         NP_IMAGEFILE_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SendStaticTextNotify(string cardID, NP_RECTANGLE windowRect,
                                                          uint playTimes, NP_STATICTEXT_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SendSrollingTextNotify(string cardID, NP_RECTANGLE windowRect,
                                                            uint playTimes, NP_SCROLLTEXT_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SendSingleLineTextNotify(string cardID, NP_RECTANGLE windowRect,
                                                              uint playTimes, NP_SINGLELINETEXT_INFO propInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_ControlCardPlay(string cardID, ushort ctrlMode);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetCardPlayLog(string cardID, NP_DATE getDate);
        [DllImport("NovaPlutoManager.dll")]
        public extern static IntPtr NP_OpenPlayLogFile(string logFileName);
        [DllImport("NovaPlutoManager.dll")]
        public extern static int NP_GetPlayLogItemCount(IntPtr logHandle);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetPlayLogItemInfo(IntPtr logHandle, int itemIndex,
                                                        out NP_PLAYLOG_ITEM logInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_ClosePlayLogFile(IntPtr logHandle);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetCardSystemTime(string cardID, SYSTEMTIME time);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetCardSystemTime(string cardID, out SYSTEMTIME time);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetCardBrightValue(string cardID, Byte brightValue);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetCardBrightValue(string cardID, out Byte brightValue);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetCardBrightMode(string cardID, ushort brightMode);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetCardBrightMode(string cardID, out ushort brightMode);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetCardSreenStatus(string cardID, ushort screenStatus);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetCabinetCount(string cardID, out ushort cabinetCnt);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_BeginDetectPoint(string cardID, ushort cabinetIndex,
                                                      NP_DETECTPOINT_PARA detectPointPara);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_RestartCard(string cardID);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_RefreshCardMonitorInfo(string cardID, string monitorInfoSavePath);
        [DllImport("NovaPlutoManager.dll")]
        public extern static IntPtr NP_OpenMonitorInfoFile(string monitoInfoFileName);
        [DllImport("NovaPlutoManager.dll")]
        public extern static int NP_GetInfoCntFromFile(IntPtr hMonitorFile);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetMonitorInfoFromFile(IntPtr hMonitorFile, int infoIndex,
                                                            out NP_MONITOR_INFO monitorInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_CloseMonitoInfoFile(IntPtr hMonitorFile);

        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetConnectDetectEnable(string cardID, bool isEanble);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetConnectDetectEnable(string cardID, ref bool isEanble);

        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetCycleMontorConfig(string cardID, bool isEanble, ushort cycleValue);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetCycleMontorConfig(string cardID, ref bool isEanble, out ushort cycleValue);

        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetCardScreenShotPicture(string cardID, string picFileName);
            
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetOnBoardPowerState(string cardID, byte powerState);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetOnBoardPowerState(string cardID, out byte powerState);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetOnBoardPowerAutoInfo(string cardID, NP_BDPOWER_AUTOCTRL_INFO autoCtrlInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetOnBoardPowerAutoInfo(string cardID, out NP_BDPOWER_AUTOCTRL_INFO autoCtrlInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetFunPowerAdjustMode(string cardID, ushort funCardIndex, byte adjustMode);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetFunPowerAdjustMode(string cardID, ushort funCardIndex, out byte adjustMode);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetFunPowerState(string cardID, ushort funCardIndex,
													  byte powerIndex, byte powerState);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetFunPowerState(string cardID, ushort funCardIndex,
													  byte powerIndex,out byte powerState);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetFunPowerAutoInfo(string cardID, 
                                                         ushort funCardIndex,
														 NP_FUNPOWER_AUTOCTRL_INFO autoCtrlInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetFunPowerAutoInfo(string cardID, 
                                                         ushort funCardIndex,
			                                             out NP_FUNPOWER_AUTOCTRL_INFO autoCtrlInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetConnectDetectPara(string cardID, NP_CONNECTDETECT_INFO detectPara);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetConnectDetectPara(string cardID, out NP_CONNECTDETECT_INFO detectPara);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetGlobalMonitorInfo(string cardID, out NP_GLOBALMONITOR_INFO monitorInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SaveHardwareParameter(string cardID);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetPlayFileSendInfo(string cardID, 
                                                         byte playFileType,
                                                         out NP_SENDPLAYFILE_INFO fileSendInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetSelfMonitorCtrlPara(string cardID, NP_SELFMONITORCTRL_PARA selfCtrlPara);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetSelfMonitorCtrlPara(string cardID, out NP_SELFMONITORCTRL_PARA selfCtrlPara);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_SetSchedualBrightInfo(string cardID, NP_SCHEDUALBRIGHT_INFO adjustInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetSchedualBrightInfo(string cardID, out NP_SCHEDUALBRIGHT_INFO adjustInfo);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_DeleteMedia(string cardID, byte deleteType);
        [DllImport("NovaPlutoManager.dll")]
        public extern static bool NP_GetStorageDeviceInfo(string cardID, out NP_STORAGEDEVICE_INFO storageDeviceInfo);
        #endregion
    }
}
