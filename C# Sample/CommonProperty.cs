using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CSharpTest
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct LOGFONTW
    {
        public int lfHeight;
        public int lfWidth;
        public int lfEscapement;
        public int lfOrientation;
        public int lfWeight;
        public byte lfItalic;
        public byte lfUnderline;
        public byte lfStrikeOut;
        public byte lfCharSet;
        public byte lfOutPrecision;
        public byte lfClipPrecision;
        public byte lfQuality;
        public byte lfPitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string lfFaceName;
    }
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
    /// <summary>
    /// Point
    /// </summary>
    public struct NP_POINT
    {
        /// <summary>
        /// //水平起始位置
        /// </summary>
        public int X;
        /// <summary>
        /// 垂直起始位置
        /// </summary>
        public int Y;
    }
    //Size
    public struct NP_SIZE
    {
        //宽度
        public int Width;
        //高度
        public int Height;
    };
    //Rectangle
    public struct NP_RECTANGLE
    {
        //x
        public int X;
        //y
        public int Y;
        //宽度
        public int Width;
        //高度
        public int Height;
    };
    //日期
    public struct NP_DATE
    {
        //年
        public int Year;
        //月
        public int Month;
        //日
        public int Day;
    };
    //时长
    public struct NP_TIMESPAN
    {
        // 时
        public int Hour;
        // 分
        public int Minute;
        // 秒
        public int Second;
        // 毫秒
        public int MilliSeconds;
    };
    //字体
    public struct NP_FONT
    {
        //字体
        public LOGFONTW TextFont;
        //字体颜色
        public uint TextForeColor;
    };
    //字体特效
    public struct NP_FONT_EFFECT
    {
        // 文字显示特效：0表示无特效，1表示悬浮，2表示套色
        public byte EffectType;
        // 文字特效的颜色
        public uint EffectColor;
    };
    //媒体特效
    public struct NP_MEDIA_EFFECT
    {
        // 是否有特效
        public bool IsHasEffect;
        // 特效的值（0表示随机）
        public ushort EffectValue;
        // 特效速度（单位为：0.1S）
        public uint EffectSpeed;
    };
    //异步卡的信息
    public struct NP_CARD_INFO
    {
        // 终端IP
        public string IP;
        // 终端ID
        public string ID;
        // 终端名称
        public string Name;
        // 显示屏宽度
        public uint ScreenWidth;
        // 显示屏高度
        public uint ScreenHeight;
        // 预留
        public string Reserved1;
        // 预留
        public string Reserved2;
        // 预留
        public string Reserved3;
    };
    //一条播放日志信息
    public struct NP_PLAYLOG_ITEM
    {
        // 媒体名称
        public string MediaName;
        //媒体时长
        public NP_TIMESPAN MediaDuration;
        // 播放结果：0表示成功，1表示文件不存在，2表示加载失败，3表示播放失败
        public byte PlayResType;
        //启动播放的时间
        public SYSTEMTIME StartTime;
        //停止播放的时间
        public SYSTEMTIME StopTime;
    };
    //播放方案中时间段的信息
    public struct NP_TIMESEGMENT_INFO
    {
        // 是否指定日期播放
        public bool IsSpecificDate;
        // 是否指定一周的某一天
        public bool IsSpecificDayOfWeek;
        // 是否全天播放
        public bool IsWholeDayPlay;
        //时间段的名称
        public string Name;
        //一周的每一天是否播放（只有当IsSpecificDayOfWeek为True时才有效）
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.Bool)]
        public bool[] WeekDayIsValid;
        // 启动日期
        public NP_DATE StartDate;
        // 停止日期
        public NP_DATE StopDate;
        //      
        // 在一天中的启动时间（只有当IsWholeDayPlay为False时才有效）
        public NP_TIMESPAN StartTimeOfDay;
        // 在一天中的停止时间（只有当IsWholeDayPlay为False时才有效）
        public NP_TIMESPAN StopTimeOfDay;
    };
    //常规页面的信息
    public struct NP_PAGE_INFO
    {
        //页的名称
        public string Name;
        // 播放方式：0表示指定时长， 1表示指定次数，2表示循环播放
        public byte PlayType;
        // 播放次数，当PlayType为1时该属性有效
        public byte PlayTimes;
        // 播放时长，当PlayType为0时该属性有效
        public NP_TIMESPAN PlayDuration;
        // 背景颜色
        public uint BackColor;
    };
    //窗口的信息
    public struct NP_WINDOW_INFO
    {
        //页的名称
        public string Name;
        //窗口的区域（包括位置和大小）
        public NP_RECTANGLE WindowRect;
    };
    //视频文件的信息
    public struct NP_VIDEOFIEL_INFO
    {
        // 背景颜色
        public uint BackColor;
        // 播放比例：0表示铺满，1表示原始比例
        public byte PlayScale;
        // 音量大小(0~100)
        public byte Volume;
    };
    //图片文件信息
    public struct NP_IMAGEFILE_INFO
    {
        // 背景颜色
        public uint BackColor;
        // 背景音乐路径（如果无背景音乐，该参数需设置为空）
        public string BackMusicFileName;

        //入场特效信息
        public NP_MEDIA_EFFECT InEffectInfo;

        //出场特效信息
        public NP_MEDIA_EFFECT OutEffectInfo;

        // 播放比例：0表示铺满，1表示原始比例
        public byte PlayScale;
        // 停留时间（单位：秒）
        public uint StayTime;
    };
    //Flash的信息
    public struct NP_FLASH_INFO
    {
        // 背景颜色
        public uint BackColor;
        // 播放比例：0表示铺满，1表示原始比例
        public byte PlayScale;
    };
    //Txt文件的信息
    public struct NP_TXTFILE_INFO
    {
        // 背景颜色
        public uint BackColor;
        // 背景音乐路径（如果无背景音乐，该参数需设置为空）
        public string BackMusicFileName;

        //入场特效信息
        public NP_MEDIA_EFFECT InEffectInfo;

        //出场特效信息
        public NP_MEDIA_EFFECT OutEffectInfo;

        // 播放比例：0表示铺满，1表示原始比例
        public byte PlayScale;
        //  停留时间（单位：秒）
        public uint StayTime;

        // 颜色反转类型：0表示不作反转，1表示所有颜色反转，2表示黑白反转
        public byte ColorInverseType;
    };
    //走马灯的信息
    public struct NP_SCROLLTEXT_INFO
    {
        //走马灯显示的内容
        public string Text;
        //字体
        public NP_FONT TextFont;

        // 背景颜色
        public uint BackColor;

        //文字特效
        public NP_FONT_EFFECT TextEffect;

        // 是否滚动
        public bool IsScroll;
        // 滚动间隔的像素数
        public uint ScrollIntervalPixel;
        // 滚动速度：0表示最慢，1表示次之，2表示常速，3表示比常速快，4表示最快
        public byte ScrollSpeed;
        // 滚动方向：0表示从右到左，1表示从左到右，2表示从下到上，3表示从上到下
        public byte ScrollDirection;

        // 播放时长
        public NP_TIMESPAN PlayDuration;
    };
    //单行文本的信息
    public struct NP_SINGLELINETEXT_INFO
    {
        //单行文本显示的内容
        public string Text;
        //字体
        public NP_FONT TextFont;

        // 背景颜色
        public uint BackColor;

        //文字特效
        public NP_FONT_EFFECT TextEffect;

        //入场特效信息
        public NP_MEDIA_EFFECT InEffectInfo;

        //出场特效信息
        public NP_MEDIA_EFFECT OutEffectInfo;

        //  停留时间（单位：秒）
        public uint StayTime;
    };
    //静态文本的信息
    public struct NP_STATICTEXT_INFO
    {
        //静态文本显示的内容
        public string Text;
        //字体
        public NP_FONT TextFont;

        // 背景颜色
        public uint BackColor;

        //文字特效
        public NP_FONT_EFFECT TextEffect;

        // 行距
        public uint RowSpacing;
        // 字距
        public uint CharacterSpacing;

        // 对齐方式：0表示靠左，1表示靠右，2表示居中
        public byte AlignmentType;

        // 播放时长
        public NP_TIMESPAN PlayDuration;
    };
    //模拟时钟的信息
    public struct NP_ANALOGCLOCK_INFO
    {
        // 背景颜色
        public uint BackColor;
        // 时标颜色
        public uint HourScaleColor;
        // 时标宽度
        public ushort HourScaleWidth;
        // 时标高度
        public ushort HourScaleHeight;
        // 时标形状：0表示矩形，1表示圆形，2表示数字
        public byte HourScaleShape;
        //时标的字体（当HourScaleShape为2时该值有效）
        public LOGFONTW HourScaleFont;
        // 分标颜色
        public uint MinuteScaleColor;
        // 分标宽度
        public ushort MinuteScaleWidth;
        // 分标高度
        public ushort MinuteScaleHeight;
        // 分标的形状：0表示矩形，1表示圆形
        public byte MinuteScaleShape;

        // 显示的文字内容
        public string Content;
        // 文字的字体
        public NP_FONT ContentFont;

        // 是否显示日期
        public bool IsShowDate;
        //日期的显示类型：0表示月在前日在后，1表示日在前月在后
        public byte DateShowType;
        //日期的字体
        public NP_FONT DateFont;

        // 是否显示星期
        public bool IsShowWeekDay;
        //星期的字体
        public NP_FONT WeekDayFont;

        // 时针的颜色
        public uint HourHandColor;
        // 分针的颜色
        public uint MinuteHandColor;
        // 秒针的颜色
        public uint SecondHandColor;
        // 播放时长
        public NP_TIMESPAN PlayDuration;
    };
    //数字时钟的信息
    public struct NP_DIGITALCLOCK_INFO
    {
        // 固定字符串
        public string FixedContent;

        // 日期的显示风格：0表示年月日，1表示日月年，2表示月日年
        public byte DateStyle;

        //文字的字体
        public NP_FONT TextFont;

        //文字特效
        public NP_FONT_EFFECT TextEffect;

        //是否显示年
        public bool IsShowYear;
        // 是否显示月
        public bool IsShowMonth;
        // 是否显示日
        public bool IsShowDay;
        // 是否显示上午（下午）
        public bool IsShowAmOrPm;
        // 是否显示时
        public bool IsShowHour;
        // 是否显示分
        public bool IsShowMinute;
        // 是否显示秒
        public bool IsShowSecond;
        // 是否显示星期
        public bool IsShowWeekDay;

        // 年的显示风格：0表示四位数字显示，1表示只显示年份的后两位
        public byte YearStyle;
        // 时的显示风格：0表示24小时制，1表示12小时制
        public byte HourStyle;
        // 是否支持多行显示
        public bool IsMultiLine;

        // 播放时长
        public NP_TIMESPAN PlayDuration;
    };
    //天气的信息
    public struct NP_WEATHER_INFO
    {
        // 背景颜色
        public uint BackColor;

        //文字的字体
        public NP_FONT TextFont;

        //文字特效
        public NP_FONT_EFFECT TextEffect;

        // 天气更新的周期（单位：分钟）
        public uint UpdateInterval;

        // 显示类型：0表示分多行显示，1表示单行静止，2表示单行滚动
        public byte ShowType;
        // 滚动速度，ShowType为2时有效，0表示最慢，1表示次之，2表示常速，3表示比常速快，4表示最快
        public byte ScrollSpeed;

        // 是否显示天气
        public bool IsShowWeather;
        // 是否显示温度
        public bool IsShowTemperature;
        // 是否显示风力
        public bool IsShowWind;
        // 是否显示湿度
        public bool IsShowHumidity;
        // 是否显示当前温度
        public bool IsShowCurTemperature;

        // 天气的ProvinceName固定显示内容
        public string WeatherConstText;
        // 温度的固定显示内容
        public string TempConstText;
        // 风力的固定显示内容
        public string WindConstText;
        // 当前温度的固定显示内容
        public string CurTempConstText;
        // 湿度的固定显示内容
        public string HumiConstText;

        //国家名称
        public string CountryName;
        //省份名称
        public string ProvinceName;
        //城市名称
        public string CityName;

        // 播放时长
        public NP_TIMESPAN PlayDuration;
    };
    /// <summary>
    /// 点检参数
    /// </summary>
    public struct NP_DETECTPOINT_PARA
    {
        //点检阈值(不同驱动芯片阈值范围不同)
        public byte Threshold;
        //点检类型（不同驱动芯片支持的点检类型不同）：0表示开路点检，1表示短路点检
        public byte PointDetectType;
        //是否使用当前设置的电流增益
        public Boolean IsUseCurrentGain;
        //红增益
        public byte RedGain;
        //绿增益
        public byte GreenGain;
        //蓝增益
        public byte BlutGain;
    };
    /// <summary>
    /// 故障灯点信息
    /// </summary>
    public struct NP_ERRORPOINT_INFO
    {
        /// <summary>
        /// 故障总点数
        /// </summary>
        public uint ErrorTotalCount;

        /// <summary>
        /// 红色故障灯点个数
        /// </summary>
        public uint ErrorRedCount;
        /// <summary>
        /// 红色故障灯点坐标列表的指针
        /// </summary>
        public IntPtr ErrorRedPoint;

        /// <summary>
        /// 绿色故障灯点个数
        /// </summary>
        public uint ErrorGreenCount;
        /// <summary>
        /// 绿色故障灯点坐标列表的指针
        /// </summary>
        public IntPtr ErrorGreenPoint;

        /// <summary>
        /// 蓝色故障灯点个数
        /// </summary>
        public uint ErrorBlueCount;
        /// <summary>
        /// 蓝色故障灯点坐标列表的指针
        /// </summary>
        public IntPtr ErrorBluePoint;

        /// <summary>
        /// 虚拟红故障灯点个数
        /// </summary>
        public uint ErrorVRedCount;
        /// <summary>
        /// 虚拟红故障灯点坐标列表的指针
        /// </summary>
        public IntPtr ErrorVRedPoint;
    };
    /// <summary>
    /// 点检结果
    /// </summary>
    public struct NP_DETECTPOINT_RESULT
    {
        /// <summary>
        /// 箱体索引
        /// </summary>
        public ushort CabinetIndex;
        /// <summary>
        /// 箱体在显示屏中的位置和大小
        /// </summary>
        public NP_RECTANGLE CabinetRegion;

        /// <summary>
        /// 像素总点数
        /// </summary>
        public uint PixelTotalCount;
        /// <summary>
        /// 点检的故障灯点信息
        /// </summary>
        public NP_ERRORPOINT_INFO ErrorPointInfo;
    };
    /// <summary>
    /// 值信息
    /// </summary>
    public struct NP_VALUE_INFO
    {     
        /// <summary>
        /// 是否有效
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsValid;
        /// <summary>
        /// 值，只有当IsValid为True时才有效。
        /// </summary>
        public float Value;
    };
    /// <summary>
    /// 告警信息
    /// </summary>
    public struct NP_ALARM_INFO
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsValid;
        /// <summary>
        /// 是否告警，只有IsValid为True时才有效。
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsAlarm;
    };
    /// <summary>
    /// 箱体的监控数据
    /// </summary>
    public struct NP_MONITOR_INFO
    {
        /// <summary>
        /// 箱体索引
        /// </summary>
        public ushort CabinetIndex;
        /// <summary>
        /// 箱体在显示屏中的位置和大小
        /// </summary>
        public NP_RECTANGLE CabinetRegion;

        /// <summary>
        /// 箱体状态是否正常
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetStatusIsOK;
        /// <summary>
        /// 箱体板载电压信息，只有CabinetStatusIsOK为True时才有效
        /// </summary>
        public NP_VALUE_INFO CabinetVoltage;
        /// <summary>
        /// 温度信息，只有CabinetStatusIsOK为True时才有效
        /// </summary>
        public NP_VALUE_INFO TempInfo;

        /// <summary>
        /// 是否连接监控卡，只有CabinetStatusIsOK为True时才有效
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsConnectMCard;
        /// <summary>
        /// 监控卡的板载电压，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        public NP_VALUE_INFO MCardVoltage;
        /// <summary>
        /// 湿度信息，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        public NP_VALUE_INFO HumidityInfo;
        /// <summary>
        /// 箱门是否关闭，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetDoorIsClose;
        /// <summary>
        /// 烟雾告警信息，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        public NP_ALARM_INFO SmokeAlarmInfo;
        /// <summary>
        /// 风扇个数，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        public byte FanCount;
        /// <summary>
        /// 风扇信息列表的指针，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        public IntPtr FanInfoPtr;
        /// <summary>
        /// 电源个数，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        public byte MCPowerCount;
        /// <summary>
        /// 电源信息列表的指针，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        public IntPtr MCVoltagePtr;
        /// <summary>
        /// 排线状态是否OK,只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsRowLineOK;
    };
    /// <summary>
    /// 本板电源自动控制的时间信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_BDPOWER_AUTOTIME_INFO
    {
	    /// <summary>
	    /// 是否在指定日期控制电源
	    /// </summary>
	    public Boolean IsSpecialDate;
	    /// <summary>
	    /// 指定日期的起始日期，只有当IsSpecialDate为True时才有效
	    /// </summary>
	    public NP_DATE StartDate;
	    /// <summary>
	    /// 指定日期的结束日期，只有当IsSpecialDate为True时才有效
	    /// </summary>
	    public NP_DATE StopDate;
	    /// <summary>
	    /// 一周内的每一天是否控制，如果不指定星期，则对应的值设置为False,顺序依次为星期天到星期六。
	    /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.Bool)]
        public Boolean[] WeekDayIsValid;
	    /// <summary>
	    /// 电源开启时间
	    /// </summary>
	    public NP_TIMESPAN OpenTime;
	    /// <summary>
	    /// 电源关闭时间
	    /// </summary>
	    public NP_TIMESPAN CloseTime;
    };
    /// <summary>
    /// 本板电源自动控制信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_BDPOWER_AUTOCTRL_INFO
    {
	    /// <summary>
	    /// 本板电源自动控制信息项的个数
	    /// </summary>
	    public ushort CtrlInfoCount;
	    /// <summary>
	    /// 本板电源自动控制信息列表
	    /// </summary>
	    public IntPtr CtrlInfoArrayPtr;
    };
    /// <summary>
    /// 多功鞥卡上的一路电源自动控制信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_FUNPOWER_AUTOTIME_INFO
    {
	    /// <summary>
	    /// 电源开启时间
	    /// </summary>
	    public NP_TIMESPAN OpenTime;
	    /// <summary>
	    /// 电源关闭时间
	    /// </summary>
	    public NP_TIMESPAN CloseTime;
    };
    /// <summary>
    /// 多功能卡上的电源自动控制信息
    /// </summary>
    public struct NP_FUNPOWER_AUTOCTRL_INFO
    {
        /// <summary>
        /// 电源自动控制信息列表
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
        public NP_FUNPOWER_AUTOTIME_INFO[] CtrlInfoArray;
    };
    /// <summary>
    /// 连接检测的参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_CONNECTDETECT_INFO
    {
        /// <summary>
        /// 连接状态检测的类型
	    /// 0：表示不检测连接状态;
	    /// 1：表示检测与管理端是否正常连接，开启该功能后，如果异步卡断开与管理端的连接，则显示屏黑屏；
	    /// 2：表示检测异步卡处于虚连接状态，开启该功能后，如果在通讯间隔内未收到任何命令，则显示屏黑屏；
        /// </summary>
	    public byte DetectType;
	    /// <summary>
        /// 虚连接状态检测时的最小通讯间隔，只有当DetectType为2时才有效,单位为秒，默认60秒，最小值60秒
	    /// </summary>
	    public ushort VirConnectMinInterval;
    };
    /// <summary>
    /// 全局监控信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_GLOBALMONITOR_INFO
    {
        /// <summary>
        /// 箱体状态
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetStatusIsOK;
        /// <summary>
        /// 箱体电压是否OK
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetVoltageIsOK;
        /// <summary>
        /// 是否连接监控卡
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsConnectMonitorCard;
        /// <summary>
        /// 监控卡电压是否OK
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean MCVoltageIsOK;
        /// <summary>
        /// 箱门是否关闭
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetDoorIsClose;
        /// <summary>
        /// 排线是否OK
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean RowLineIsOK;
        /// <summary>
        /// 光探头是否OK
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean LightSensorIsOK;
    };
    /// <summary>
    /// 发送播放文件的进度信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_SENDPLAYFILE_INFO
    {
	    /// <summary>
        /// 当前发送的文件名称
	    /// </summary>
	    public string SendFileName;
	    /// <summary>
        /// 当前正在发送的文件的发送进度
	    /// </summary>
	    public float CurFilePercent;
	    /// <summary>
        /// 发送总进度
	    /// </summary>
	    public float TotalPercent;
    };
    /// <summary>
    /// 终端周期自检参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_SELFMONITORCTRL_PARA
    {
	    /// <summary>
        /// 是否启用周期自检功能
	    /// </summary>
        [MarshalAs(UnmanagedType.U1)]
	    public Boolean IsCycleSelfMonitor;
	    /// <summary>
        /// 自检周期，单位为分钟，默认为30分钟，最小值为5分钟
	    /// </summary>
	    public int SelfMonitorPeriod;
        /// <summary>
        /// 排线故障时的控制类型：0表示不控制，1表示排线故障时黑屏
        /// </summary>
        public byte RowLineErrorCtrlType;
    };
    /// <summary>
    /// 亮度调节信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_BRIGHTADJUST_INFO
    {
	    /// <summary>
        /// 调节时间
	    /// </summary>
	    public NP_TIMESPAN AdjustTime;
	    /// <summary>
        /// 调节亮度值(0%~100%)
	    /// </summary>
	    public Byte BrightValue;
    };
    /// <summary>
    /// 定时亮度调节信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_SCHEDUALBRIGHT_INFO
    {
	    /// <summary>
        /// 调节信息饿个数
	    /// </summary>
	    public ushort AdjustInfoCount;
	    /// <summary>
        /// 调节信息列表
	    /// </summary>
	    public IntPtr AdjustInfoArrayPtr;
    };
    /// <summary>
    /// 存储设备信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_STORAGEDEVICE_INFO
    {
	    /// <summary>
        /// 当前存储设备类型：0表示Flash，1表示SD卡，2表示USB口接入的硬件
	    /// </summary>
	    public byte CurStorageDeviceType;
	    /// <summary>
        /// Flash的剩余空间大小：单位为字节
	    /// </summary>
	    public long FlashFreeSpace;
	    /// <summary>
        /// Flash的总空间大小：单位为字节
	    /// </summary>
	    public long FlashTotalSpace;
	    /// <summary>
        /// SD卡的剩余空间大小：单位为字节
	    /// </summary>
	    public long SDCardFreeSpace;
	    /// <summary>
        /// SD卡的总空间大小：单位为字节
	    /// </summary>
	    public long SDCardTotalSpace;
	    /// <summary>
        /// USB口接口设备的剩余空间大小：单位为字节
	    /// </summary>
	    public long UDiskFreeSpace;
	    /// <summary>
        /// USB口接口设备的总空间大小：单位为字节
	    /// </summary>
	    public long UDiskTotalSpace;
    };
    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum CustomMessageType
    {
        /// <summary>
        /// 得到卡信息的消息
        /// </summary>
        WM_CardInfo = 0x0510,
        /// <summary>
        /// 异步卡断开的消息
        /// </summary>
        WM_CardDisconnect = 0x0511,
        /// <summary>
        /// 发送播放方案完成的消息
        /// </summary>
        WM_SendPlayProgram = 0x0512,
        /// <summary>
        /// 发送插播文件完成的消息
        /// </summary>
        WM_SendInsertPlay = 0x0513,
        /// <summary>
        /// 发送通知完成的消息
        /// </summary>
        WM_SendNotify = 0x0514,
        /// <summary>
        /// 获取日志成功的消息
        /// </summary>
        WM_GetPlayLogOK = 0x0515,
        /// <summary>
        /// 获取日志失败的消息
        /// </summary>
        WM_GetPlayLogError = 0x0516,
        /// <summary>
        /// 点检成功
        /// </summary>
        WM_DetectPointOK = 0x0517,
        /// <summary>
        /// 点检失败
        /// </summary>
        WM_DetectPointFailed = 0x0518,
        /// <summary>
        /// 刷新监控数据OK
        /// </summary>
        WM_RefreshMonitorOK = 0x0519,
        /// <summary>
        /// 刷新监控数据失败
        /// </summary>
        WM_RefreshMonitorFailed = 0x0520
    }
}
