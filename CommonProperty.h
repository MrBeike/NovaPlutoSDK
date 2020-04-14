//Point
struct NP_POINT
{
public:
	//水平起始位置
	unsigned int X;
	//垂直起始位置
	unsigned int Y;
};
//Size
struct NP_SIZE
{
public:
	//宽度
	int Width;
	//高度
	int Height;
};
//Rectangle
struct NP_RECTANGLE
{
public:
	//水平起始位置
	int X;
	//垂直起始位置
	int Y;
	//宽度
	int Width;
	//高度
	int Height;
};
//日期
struct NP_DATE
{
public:
	//年
	int Year;
	//月
	int Month;
	//日
	int Day;
};
//时长
struct NP_TIMESPAN
{
public:
	// 时
	int Hour;
	// 分
	int Minute;
	// 秒
	int Second;
	// 毫秒
	int MilliSeconds;
};
//字体
struct NP_FONT
{
public:
	//字体
	LOGFONTW TextFont;
	//字体颜色
	COLORREF TextForeColor;
};
//字体特效
struct NP_FONT_EFFECT
{
public:
	// 文字显示特效：0表示无特效，1表示悬浮，2表示套色
	unsigned char EffectType;
	// 文字特效的颜色
	COLORREF EffectColor;
};
//媒体特效
struct NP_MEDIA_EFFECT
{
public:
	// 是否有特效
	BOOL IsHasEffect;
	// 特效的值（0表示随机）
	unsigned short EffectValue;
	// 特效速度（单位为：0.1S）
	unsigned int EffectSpeed;
};
//异步卡的信息
struct NP_CARD_INFO
{
public:
	// 终端IP
	char* IP;
	// 终端ID
	char* ID;
	// 终端名称
	char* Name;
	// 显示屏宽度
	unsigned int ScreenWidth;
	// 显示屏高度
	unsigned int ScreenHeight;
	// 预留
	char*  Reserved1;
	// 预留
	char*  Reserved2;
	// 预留
	char*  Reserved3;
};
//一条播放日志信息
struct NP_PLAYLOG_ITEM
{
public:
	// 媒体名称
	char* MediaName;
	//媒体时长
	NP_TIMESPAN MediaDuration;
	// 播放结果：0表示成功，1表示文件不存在，2表示加载失败，3表示播放失败
	unsigned char PlayResType;
	//启动播放的时间
	SYSTEMTIME StartTime;
	//停止播放的时间
	SYSTEMTIME StopTime;
};
//播放方案中时间段的信息
struct NP_TIMESEGMENT_INFO
{
public:
	// 是否指定日期播放
	BOOL IsSpecificDate;
	// 是否指定一周的某一天
	BOOL IsSpecificDayOfWeek;
	// 是否全天播放
	BOOL IsWholeDayPlay;
	//时间段的名称
	char* Name;
	//一周的每一天是否播放（只有当IsSpecificDayOfWeek为True时才有效）
	BOOL WeekDayIsValid[7];
	// 启动日期
	NP_DATE StartDate;
	// 停止日期
	NP_DATE StopDate;

	// 在一天中的启动时间（只有当IsWholeDayPlay为False时才有效）
	NP_TIMESPAN StartTimeOfDay;
	// 在一天中的停止时间（只有当IsWholeDayPlay为False时才有效）
	NP_TIMESPAN StopTimeOfDay;
};
//常规页面的信息
struct NP_PAGE_INFO
{
public:
	//页的名称
	char* Name;
	// 播放方式：0表示指定时长， 1表示指定次数，2表示循环播放
	unsigned char PlayType;
	// 播放次数，当PlayType为1时该属性有效
	unsigned char PlayTimes;
	// 播放时长，当PlayType为0时该属性有效
	NP_TIMESPAN PlayDuration;
	// 背景颜色
	COLORREF BackColor;
};
//窗口的信息
struct NP_WINDOW_INFO
{
public:
	//页的名称
	char* Name;
	//窗口的区域（包括位置和大小）
	NP_RECTANGLE WindowRect;
};
//视频文件的信息
struct NP_VIDEOFIEL_INFO
{
public:
	// 背景颜色
	COLORREF BackColor;
	// 播放比例：0表示铺满，1表示原始比例
	unsigned char PlayScale;
	// 音量大小(0~100)
	unsigned char Volume;
};
//图片文件信息
struct NP_IMAGEFILE_INFO
{
public:
	// 背景颜色
	COLORREF BackColor;
	// 背景音乐路径（如果无背景音乐，该参数需设置为空）
	char* BackMusicFileName;

	//入场特效信息
	NP_MEDIA_EFFECT InEffectInfo;

	//出场特效信息
	NP_MEDIA_EFFECT OutEffectInfo;

	// 播放比例：0表示铺满，1表示原始比例
	unsigned char PlayScale;
	// 停留时间（单位：秒）
	unsigned int StayTime;
};
//Flash的信息
struct NP_FLASH_INFO
{
public:
	// 背景颜色
	COLORREF BackColor;
	// 播放比例：0表示铺满，1表示原始比例
	unsigned char PlayScale;
};
//Txt文件的信息
struct NP_TXTFILE_INFO
{
public:
	// 背景颜色
	COLORREF BackColor;
	// 背景音乐路径（如果无背景音乐，该参数需设置为空）
	char* BackMusicFileName;

	//入场特效信息
	NP_MEDIA_EFFECT InEffectInfo;

	//出场特效信息
	NP_MEDIA_EFFECT OutEffectInfo;

	// 播放比例：0表示铺满，1表示原始比例
	unsigned char PlayScale;
	//  停留时间（单位：秒）
	unsigned int StayTime;

	// 颜色反转类型：0表示不作反转，1表示所有颜色反转，2表示黑白反转
	unsigned char ColorInverseType;
};
//走马灯的信息
struct NP_SCROLLTEXT_INFO
{
public:
	//走马灯显示的内容
	char* Text;
	//字体
	NP_FONT TextFont;

	// 背景颜色
	COLORREF BackColor;

	//文字特效
	NP_FONT_EFFECT TextEffect;

	// 是否滚动
	BOOL IsScroll;
	// 滚动间隔的像素数
	unsigned int ScrollIntervalPixel;
	// 滚动速度：0表示最慢，1表示次之，2表示常速，3表示比常速快，4表示最快
	unsigned char ScrollSpeed;
	// 滚动方向：0表示从右到左，1表示从左到右，2表示从下到上，3表示从上到下
	unsigned char ScrollDirection;

	// 播放时长
	NP_TIMESPAN PlayDuration;
};
//单行文本的信息
struct NP_SINGLELINETEXT_INFO
{
public:
	//单行文本显示的内容
	char* Text;
	//字体
	NP_FONT TextFont;

	// 背景颜色
	COLORREF BackColor;

	//文字特效
	NP_FONT_EFFECT TextEffect;

	//入场特效信息
	NP_MEDIA_EFFECT InEffectInfo;

	//出场特效信息
	NP_MEDIA_EFFECT OutEffectInfo;

	//  停留时间（单位：秒）
	unsigned int StayTime;
};
//静态文本的信息
struct NP_STATICTEXT_INFO
{
public:
	//静态文本显示的内容
	char* Text;
	//字体
	NP_FONT TextFont;

	// 背景颜色
	COLORREF BackColor;

	//文字特效
	NP_FONT_EFFECT TextEffect;

	// 行距
	unsigned int RowSpacing;
	// 字距
	unsigned int CharacterSpacing;

	// 对齐方式：0表示靠左，1表示靠右，2表示居中
	unsigned char AlignmentType;

	// 播放时长
	NP_TIMESPAN PlayDuration;
};
//模拟时钟的信息
struct NP_ANALOGCLOCK_INFO
{
public:
	// 背景颜色
	COLORREF BackColor;

	// 时标颜色
	COLORREF HourScaleColor;
	// 时标宽度
	unsigned short HourScaleWidth;
	// 时标高度
	unsigned short HourScaleHeight;
	// 时标形状：0表示矩形，1表示圆形，2表示数字
	unsigned char HourScaleShape;
	//时标的字体（当HourScaleShape为2时该值有效）
	LOGFONTW HourScaleFont;

	// 分标颜色
	COLORREF MinuteScaleColor;
	// 分标宽度
	unsigned short MinuteScaleWidth;
	// 分标高度
	unsigned short MinuteScaleHeight;
	// 分标的形状：0表示矩形，1表示圆形
	unsigned char MinuteScaleShape;

	// 显示的文字内容
	char* Content;
	// 文字的字体
	NP_FONT ContentFont;

	// 是否显示日期
	BOOL IsShowDate;
	// 日期的显示类型：0表示月在前日在后，1表示日在前月在后
	unsigned char DateShowType;
	//日期的字体
	NP_FONT DateFont;

	// 是否显示星期
	BOOL IsShowWeekDay;
	//星期的字体
	NP_FONT WeekDayFont;

	// 时针的颜色
	COLORREF HourHandColor;
	// 分针的颜色
	COLORREF MinuteHandColor;
	// 秒针的颜色
	COLORREF SecondHandColor;

	// 播放时长
	NP_TIMESPAN PlayDuration;
};
//数字时钟的信息
struct NP_DIGITALCLOCK_INFO
{
public:
	// 固定字符串
	char* FixedContent;

	// 日期的显示风格：0表示年月日，1表示日月年，2表示月日年
	unsigned char DateStyle;

	//文字的字体
	NP_FONT TextFont;

	//文字特效
	NP_FONT_EFFECT TextEffect;

	//是否显示年
	BOOL IsShowYear;
	// 是否显示月
	BOOL IsShowMonth;
	// 是否显示日
	BOOL IsShowDay;
	// 是否显示上午（下午）
	BOOL IsShowAmOrPm;
	// 是否显示时
	BOOL IsShowHour;
	// 是否显示分
	BOOL IsShowMinute;
	// 是否显示秒
	BOOL IsShowSecond;
	// 是否显示星期
	BOOL IsShowWeekDay;

	// 年的显示风格：0表示四位数字显示，1表示只显示年份的后两位
	unsigned char YearStyle;
	// 时的显示风格：0表示24小时制，1表示12小时制
	unsigned char HourStyle;
	// 是否支持多行显示
	BOOL IsMultiLine;

	// 播放时长
	NP_TIMESPAN PlayDuration;
};
//天气的信息
struct NP_WEATHER_INFO
{
public:
	// 背景颜色
	COLORREF BackColor;

	//文字的字体
	NP_FONT TextFont;

	//文字特效
	NP_FONT_EFFECT TextEffect;

	// 天气更新的周期（单位：分钟）
	unsigned int UpdateInterval;

	// 显示类型：0表示分多行显示，1表示单行静止，2表示单行滚动
	unsigned char ShowType;
	// 滚动速度，ShowType为2时有效，0表示最慢，1表示次之，2表示常速，3表示比常速快，4表示最快
	unsigned char ScrollSpeed;

	// 是否显示天气
	BOOL IsShowWeather;
	// 是否显示温度
	BOOL IsShowTemperature;
	// 是否显示风力
	BOOL IsShowWind;
	// 是否显示湿度
	BOOL IsShowHumidity;
	// 是否显示当前温度
	BOOL IsShowCurTemperature;

	// 天气的ProvinceName固定显示内容
	char* WeatherConstText;
	// 温度的固定显示内容
	char* TempConstText;
	// 风力的固定显示内容
	char* WindConstText;
	// 当前温度的固定显示内容
	char* CurTempConstText;
	// 湿度的固定显示内容
	char* HumiConstText;

	//国家名称
	char* CountryName;
	//省份名称
	char* ProvinceName;
	//城市名称
	char* CityName;

	// 播放时长
	NP_TIMESPAN PlayDuration;
};
//点检参数
struct NP_DETECTPOINT_PARA
{
public:
	//点检阈值(不同驱动芯片阈值范围不同)
	BYTE Threshold;
	//点检类型（不同驱动芯片支持的点检类型不同）：0表示开路点检，1表示短路点检
	BYTE PointDetectType;
	//是否使用当前设置的电流增益
	BOOLEAN IsUseCurrentGain;
	//红增益
	BYTE RedGain;
	//绿增益
	BYTE GreenGain;
	//蓝增益
	BYTE BlutGain;
};
//故障灯点信息
struct NP_ERRORPOINT_INFO
{
public:
	//故障总点数
	unsigned int ErrorTotalCount;

	//红色故障灯点个数
	unsigned int ErrorRedCount;
	//红色故障灯点坐标列表的指针
	NP_POINT* ErrorRedPoint;
	
	//绿色故障灯点个数
	unsigned int ErrorGreenCount;
	//绿色故障灯点坐标列表的指针
	NP_POINT* ErrorGreenPoint;

	//蓝色故障灯点个数
	unsigned int ErrorBlueCount;
	//蓝色故障灯点坐标列表的指针
	NP_POINT* ErrorBluePoint;

	//虚拟红故障灯点个数
	unsigned int ErrorVRedCount;
	//虚拟红故障灯点坐标列表的指针
	NP_POINT* ErrorVRedPoint;
};
//点检结果
struct NP_DETECTPOINT_RESULT
{
public:
	//箱体索引
	unsigned short CabinetIndex;
	//箱体在显示屏中的位置和大小
	NP_RECTANGLE CabinetRegion;

	//像素总点数
	unsigned int PixelTotalCount;
	//点检的故障灯点信息
	NP_ERRORPOINT_INFO ErrorPointInfo;
};
//值信息
struct NP_VALUE_INFO
{
public:
	//是否有效
	BOOLEAN IsValid;
	//值，只有当IsValid为True时才有效。
	float Value;
};
//告警信息
struct NP_ALARM_INFO
{
public:
	//是否有效
	BOOLEAN IsValid;
	//是否告警，只有IsValid为True时才有效。
	BOOLEAN IsAlarm;
};
//箱体的监控数据
struct NP_MONITOR_INFO
{
public:
	//箱体索引
	unsigned short CabinetIndex;
	//箱体在显示屏中的位置和大小
	NP_RECTANGLE CabinetRegion;

	//箱体状态
	BOOLEAN CabinetStatusIsOK;
	//箱体板载电压信息，只有CabinetStatusIsOK为True时才有效
	NP_VALUE_INFO CabinetVoltage;
	//温度信息，只有CabinetStatusIsOK为True时才有效
	NP_VALUE_INFO TempInfo;

	//是否连接监控卡，只有CabinetStatusIsOK为True时才有效
	BOOLEAN IsConnectMCard;
	//监控卡的板载电压，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	NP_VALUE_INFO MCardVoltage;
	//湿度信息，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	NP_VALUE_INFO HumidityInfo;
	//箱门是否关闭：只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	BOOLEAN CabinetDoorIsClose;
	//烟雾告警信息，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	NP_ALARM_INFO SmokeAlarmInfo;
	////风扇个数，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	BYTE FanCount;
	//风扇信息列表的指针，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	NP_VALUE_INFO* FanInfoPtr;
	//电源个数，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	BYTE MCPowerCount;
	//电源信息列表的指针，只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	NP_VALUE_INFO* MCVoltagePtr;

	//排线状态是否OK,只有当CabinetStatusIsOK和IsConnectMCard均为True时才有效
	BOOLEAN IsRowLineOK;
};
#pragma pack(1)
//本板电源自动控制的时间信息
struct NP_BDPOWER_AUTOTIME_INFO
{
public:
	//是否在指定日期控制电源
	BOOL IsSpecialDate;
	//指定日期的起始日期，只有当IsSpecialDate为True时才有效
	NP_DATE StartDate;
	//指定日期的结束日期，只有当IsSpecialDate为True时才有效
	NP_DATE StopDate;
	//一周内的每一天是否控制，如果不指定星期，则对应的值设置为False,顺序依次为星期天到星期六。
	BOOL WeekDayIsValid[7];
	//电源开启时间
	NP_TIMESPAN OpenTime;
	//电源关闭时间
	NP_TIMESPAN CloseTime;
};
//本板电源自动控制信息
struct NP_BDPOWER_AUTOCTRL_INFO
{
public:
	//本板电源自动控制信息项的个数
	unsigned short CtrlInfoCount;
	//本板电源自动控制信息列表
	NP_BDPOWER_AUTOTIME_INFO* CtrlInfoArray;
};
//多功鞥卡上的一路电源自动控制信息
struct NP_FUNPOWER_AUTOTIME_INFO
{
public:
	//电源开启时间
	NP_TIMESPAN OpenTime;
	//电源关闭时间
	NP_TIMESPAN CloseTime;
};
//多功能卡上的电源自动控制信息
struct NP_FUNPOWER_AUTOCTRL_INFO
{
public:
	//电源自动控制信息列表,多功能卡8路电源统一设置
	NP_FUNPOWER_AUTOTIME_INFO CtrlInfoArray[8];
};
//连接检测的参数
struct NP_CONNECTDETECT_PARA
{
public:
	//连接状态检测的类型
	//0：表示不检测连接状态;
	//1：表示检测与管理端是否正常连接，开启该功能后，如果异步卡断开与管理端的连接，则显示屏黑屏；
	//2：表示检测异步卡处于虚连接状态，开启该功能后，如果在通讯间隔内未收到任何命令，则显示屏黑屏；
	unsigned char DetectType;
	//虚连接状态检测时的最小通讯间隔，只有当DetectType为2时才有效,单位为秒，默认60秒，最小值60秒
	unsigned short VirConnectMinInterval;
};
//全局监控信息
struct NP_GLOBALMONITOR_INFO
{
public:
	//箱体状态
	BOOLEAN CabinetStatusIsOK;
	//箱体电压是否OK
	BOOLEAN CabinetVoltageIsOK;
	//是否连接监控卡
	BOOLEAN IsConnectMonitorCard;
	//监控卡电压是否OK
	BOOLEAN MCVoltageIsOK;
	//箱门是否关闭
	BOOLEAN CabinetDoorIsClose;
	//排线是否OK
	BOOLEAN RowLineIsOK;
	//光探头是否OK
	BOOLEAN LightSensorIsOK;
};
//发送播放文件的进度信息
struct NP_SENDPLAYFILE_INFO
{
public:
	//当前发送的文件名称
	char* SendFileName;
	//当前正在发送的文件的发送进度
	float CurFilePercent;
	//发送总进度
	float TotalPercent;
};
//终端周期自检参数
struct NP_SELFMONITORCTRL_PARA
{
public:
	//是否启用周期自检功能
	BOOLEAN IsCycleSelfMonitor;
	//自检周期，单位为分钟，默认为30分钟，最小值为5分钟
	int SelfMonitorPeriod;
	//排线故障时的控制类型：0表示不控制，1表示排线故障时黑屏
	unsigned char RowLineErrorCtrlType;
};
//亮度调节信息
struct NP_BRIGHTADJUST_INFO
{
public:
	//调节时间
	NP_TIMESPAN AdjustTime;
	//调节亮度值(0%~100%)
	BYTE BrightValue;
};
//定时亮度调节信息
struct NP_SCHEDUALBRIGHT_INFO
{
public:
	//调节信息饿个数
	unsigned short AdjustInfoCount;
	//调节信息列表
	NP_BRIGHTADJUST_INFO* AdjustInfoArray;
};
//存储设备信息
struct NP_STORAGEDEVICE_INFO
{
public:
	//当前存储设备类型：0表示Flash，1表示SD卡，2表示USB口接入的硬件
	unsigned char CurStorageDeviceType;
	//Flash的剩余空间大小：单位为字节
	INT64 FlashFreeSpace;
	//Flash的总空间大小：单位为字节
	INT64 FlashTotalSpace;
	//SD卡的剩余空间大小：单位为字节
	INT64 SDCardFreeSpace;
	//SD卡的总空间大小：单位为字节
	INT64 SDCardTotalSpace;
	//USB口接口设备的剩余空间大小：单位为字节
	INT64 UDiskFreeSpace;
	//USB口接口设备的总空间大小：单位为字节
	INT64 UDiskTotalSpace;
};
#pragma pack