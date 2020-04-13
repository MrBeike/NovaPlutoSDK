//Point
struct NP_POINT
{
public:
	//ˮƽ��ʼλ��
	unsigned int X;
	//��ֱ��ʼλ��
	unsigned int Y;
};
//Size
struct NP_SIZE
{
public:
	//���
	int Width;
	//�߶�
	int Height;
};
//Rectangle
struct NP_RECTANGLE
{
public:
	//ˮƽ��ʼλ��
	int X;
	//��ֱ��ʼλ��
	int Y;
	//���
	int Width;
	//�߶�
	int Height;
};
//����
struct NP_DATE
{
public:
	//��
	int Year;
	//��
	int Month;
	//��
	int Day;
};
//ʱ��
struct NP_TIMESPAN
{
public:
	// ʱ
	int Hour;
	// ��
	int Minute;
	// ��
	int Second;
	// ����
	int MilliSeconds;
};
//����
struct NP_FONT
{
public:
	//����
	LOGFONTW TextFont;
	//������ɫ
	COLORREF TextForeColor;
};
//������Ч
struct NP_FONT_EFFECT
{
public:
	// ������ʾ��Ч��0��ʾ����Ч��1��ʾ������2��ʾ��ɫ
	unsigned char EffectType;
	// ������Ч����ɫ
	COLORREF EffectColor;
};
//ý����Ч
struct NP_MEDIA_EFFECT
{
public:
	// �Ƿ�����Ч
	BOOL IsHasEffect;
	// ��Ч��ֵ��0��ʾ�����
	unsigned short EffectValue;
	// ��Ч�ٶȣ���λΪ��0.1S��
	unsigned int EffectSpeed;
};
//�첽������Ϣ
struct NP_CARD_INFO
{
public:
	// �ն�IP
	char* IP;
	// �ն�ID
	char* ID;
	// �ն�����
	char* Name;
	// ��ʾ�����
	unsigned int ScreenWidth;
	// ��ʾ���߶�
	unsigned int ScreenHeight;
	// Ԥ��
	char*  Reserved1;
	// Ԥ��
	char*  Reserved2;
	// Ԥ��
	char*  Reserved3;
};
//һ��������־��Ϣ
struct NP_PLAYLOG_ITEM
{
public:
	// ý������
	char* MediaName;
	//ý��ʱ��
	NP_TIMESPAN MediaDuration;
	// ���Ž����0��ʾ�ɹ���1��ʾ�ļ������ڣ�2��ʾ����ʧ�ܣ�3��ʾ����ʧ��
	unsigned char PlayResType;
	//�������ŵ�ʱ��
	SYSTEMTIME StartTime;
	//ֹͣ���ŵ�ʱ��
	SYSTEMTIME StopTime;
};
//���ŷ�����ʱ��ε���Ϣ
struct NP_TIMESEGMENT_INFO
{
public:
	// �Ƿ�ָ�����ڲ���
	BOOL IsSpecificDate;
	// �Ƿ�ָ��һ�ܵ�ĳһ��
	BOOL IsSpecificDayOfWeek;
	// �Ƿ�ȫ�첥��
	BOOL IsWholeDayPlay;
	//ʱ��ε�����
	char* Name;
	//һ�ܵ�ÿһ���Ƿ񲥷ţ�ֻ�е�IsSpecificDayOfWeekΪTrueʱ����Ч��
	BOOL WeekDayIsValid[7];
	// ��������
	NP_DATE StartDate;
	// ֹͣ����
	NP_DATE StopDate;

	// ��һ���е�����ʱ�䣨ֻ�е�IsWholeDayPlayΪFalseʱ����Ч��
	NP_TIMESPAN StartTimeOfDay;
	// ��һ���е�ֹͣʱ�䣨ֻ�е�IsWholeDayPlayΪFalseʱ����Ч��
	NP_TIMESPAN StopTimeOfDay;
};
//����ҳ�����Ϣ
struct NP_PAGE_INFO
{
public:
	//ҳ������
	char* Name;
	// ���ŷ�ʽ��0��ʾָ��ʱ���� 1��ʾָ��������2��ʾѭ������
	unsigned char PlayType;
	// ���Ŵ�������PlayTypeΪ1ʱ��������Ч
	unsigned char PlayTimes;
	// ����ʱ������PlayTypeΪ0ʱ��������Ч
	NP_TIMESPAN PlayDuration;
	// ������ɫ
	COLORREF BackColor;
};
//���ڵ���Ϣ
struct NP_WINDOW_INFO
{
public:
	//ҳ������
	char* Name;
	//���ڵ����򣨰���λ�úʹ�С��
	NP_RECTANGLE WindowRect;
};
//��Ƶ�ļ�����Ϣ
struct NP_VIDEOFIEL_INFO
{
public:
	// ������ɫ
	COLORREF BackColor;
	// ���ű�����0��ʾ������1��ʾԭʼ����
	unsigned char PlayScale;
	// ������С(0~100)
	unsigned char Volume;
};
//ͼƬ�ļ���Ϣ
struct NP_IMAGEFILE_INFO
{
public:
	// ������ɫ
	COLORREF BackColor;
	// ��������·��������ޱ������֣��ò���������Ϊ�գ�
	char* BackMusicFileName;

	//�볡��Ч��Ϣ
	NP_MEDIA_EFFECT InEffectInfo;

	//������Ч��Ϣ
	NP_MEDIA_EFFECT OutEffectInfo;

	// ���ű�����0��ʾ������1��ʾԭʼ����
	unsigned char PlayScale;
	// ͣ��ʱ�䣨��λ���룩
	unsigned int StayTime;
};
//Flash����Ϣ
struct NP_FLASH_INFO
{
public:
	// ������ɫ
	COLORREF BackColor;
	// ���ű�����0��ʾ������1��ʾԭʼ����
	unsigned char PlayScale;
};
//Txt�ļ�����Ϣ
struct NP_TXTFILE_INFO
{
public:
	// ������ɫ
	COLORREF BackColor;
	// ��������·��������ޱ������֣��ò���������Ϊ�գ�
	char* BackMusicFileName;

	//�볡��Ч��Ϣ
	NP_MEDIA_EFFECT InEffectInfo;

	//������Ч��Ϣ
	NP_MEDIA_EFFECT OutEffectInfo;

	// ���ű�����0��ʾ������1��ʾԭʼ����
	unsigned char PlayScale;
	//  ͣ��ʱ�䣨��λ���룩
	unsigned int StayTime;

	// ��ɫ��ת���ͣ�0��ʾ������ת��1��ʾ������ɫ��ת��2��ʾ�ڰ׷�ת
	unsigned char ColorInverseType;
};
//����Ƶ���Ϣ
struct NP_SCROLLTEXT_INFO
{
public:
	//�������ʾ������
	char* Text;
	//����
	NP_FONT TextFont;

	// ������ɫ
	COLORREF BackColor;

	//������Ч
	NP_FONT_EFFECT TextEffect;

	// �Ƿ����
	BOOL IsScroll;
	// ���������������
	unsigned int ScrollIntervalPixel;
	// �����ٶȣ�0��ʾ������1��ʾ��֮��2��ʾ���٣�3��ʾ�ȳ��ٿ죬4��ʾ���
	unsigned char ScrollSpeed;
	// ��������0��ʾ���ҵ���1��ʾ�����ң�2��ʾ���µ��ϣ�3��ʾ���ϵ���
	unsigned char ScrollDirection;

	// ����ʱ��
	NP_TIMESPAN PlayDuration;
};
//�����ı�����Ϣ
struct NP_SINGLELINETEXT_INFO
{
public:
	//�����ı���ʾ������
	char* Text;
	//����
	NP_FONT TextFont;

	// ������ɫ
	COLORREF BackColor;

	//������Ч
	NP_FONT_EFFECT TextEffect;

	//�볡��Ч��Ϣ
	NP_MEDIA_EFFECT InEffectInfo;

	//������Ч��Ϣ
	NP_MEDIA_EFFECT OutEffectInfo;

	//  ͣ��ʱ�䣨��λ���룩
	unsigned int StayTime;
};
//��̬�ı�����Ϣ
struct NP_STATICTEXT_INFO
{
public:
	//��̬�ı���ʾ������
	char* Text;
	//����
	NP_FONT TextFont;

	// ������ɫ
	COLORREF BackColor;

	//������Ч
	NP_FONT_EFFECT TextEffect;

	// �о�
	unsigned int RowSpacing;
	// �־�
	unsigned int CharacterSpacing;

	// ���뷽ʽ��0��ʾ����1��ʾ���ң�2��ʾ����
	unsigned char AlignmentType;

	// ����ʱ��
	NP_TIMESPAN PlayDuration;
};
//ģ��ʱ�ӵ���Ϣ
struct NP_ANALOGCLOCK_INFO
{
public:
	// ������ɫ
	COLORREF BackColor;

	// ʱ����ɫ
	COLORREF HourScaleColor;
	// ʱ����
	unsigned short HourScaleWidth;
	// ʱ��߶�
	unsigned short HourScaleHeight;
	// ʱ����״��0��ʾ���Σ�1��ʾԲ�Σ�2��ʾ����
	unsigned char HourScaleShape;
	//ʱ������壨��HourScaleShapeΪ2ʱ��ֵ��Ч��
	LOGFONTW HourScaleFont;

	// �ֱ���ɫ
	COLORREF MinuteScaleColor;
	// �ֱ���
	unsigned short MinuteScaleWidth;
	// �ֱ�߶�
	unsigned short MinuteScaleHeight;
	// �ֱ����״��0��ʾ���Σ�1��ʾԲ��
	unsigned char MinuteScaleShape;

	// ��ʾ����������
	char* Content;
	// ���ֵ�����
	NP_FONT ContentFont;

	// �Ƿ���ʾ����
	BOOL IsShowDate;
	// ���ڵ���ʾ���ͣ�0��ʾ����ǰ���ں�1��ʾ����ǰ���ں�
	unsigned char DateShowType;
	//���ڵ�����
	NP_FONT DateFont;

	// �Ƿ���ʾ����
	BOOL IsShowWeekDay;
	//���ڵ�����
	NP_FONT WeekDayFont;

	// ʱ�����ɫ
	COLORREF HourHandColor;
	// �������ɫ
	COLORREF MinuteHandColor;
	// �������ɫ
	COLORREF SecondHandColor;

	// ����ʱ��
	NP_TIMESPAN PlayDuration;
};
//����ʱ�ӵ���Ϣ
struct NP_DIGITALCLOCK_INFO
{
public:
	// �̶��ַ���
	char* FixedContent;

	// ���ڵ���ʾ���0��ʾ�����գ�1��ʾ�����꣬2��ʾ������
	unsigned char DateStyle;

	//���ֵ�����
	NP_FONT TextFont;

	//������Ч
	NP_FONT_EFFECT TextEffect;

	//�Ƿ���ʾ��
	BOOL IsShowYear;
	// �Ƿ���ʾ��
	BOOL IsShowMonth;
	// �Ƿ���ʾ��
	BOOL IsShowDay;
	// �Ƿ���ʾ���磨���磩
	BOOL IsShowAmOrPm;
	// �Ƿ���ʾʱ
	BOOL IsShowHour;
	// �Ƿ���ʾ��
	BOOL IsShowMinute;
	// �Ƿ���ʾ��
	BOOL IsShowSecond;
	// �Ƿ���ʾ����
	BOOL IsShowWeekDay;

	// �����ʾ���0��ʾ��λ������ʾ��1��ʾֻ��ʾ��ݵĺ���λ
	unsigned char YearStyle;
	// ʱ����ʾ���0��ʾ24Сʱ�ƣ�1��ʾ12Сʱ��
	unsigned char HourStyle;
	// �Ƿ�֧�ֶ�����ʾ
	BOOL IsMultiLine;

	// ����ʱ��
	NP_TIMESPAN PlayDuration;
};
//��������Ϣ
struct NP_WEATHER_INFO
{
public:
	// ������ɫ
	COLORREF BackColor;

	//���ֵ�����
	NP_FONT TextFont;

	//������Ч
	NP_FONT_EFFECT TextEffect;

	// �������µ����ڣ���λ�����ӣ�
	unsigned int UpdateInterval;

	// ��ʾ���ͣ�0��ʾ�ֶ�����ʾ��1��ʾ���о�ֹ��2��ʾ���й���
	unsigned char ShowType;
	// �����ٶȣ�ShowTypeΪ2ʱ��Ч��0��ʾ������1��ʾ��֮��2��ʾ���٣�3��ʾ�ȳ��ٿ죬4��ʾ���
	unsigned char ScrollSpeed;

	// �Ƿ���ʾ����
	BOOL IsShowWeather;
	// �Ƿ���ʾ�¶�
	BOOL IsShowTemperature;
	// �Ƿ���ʾ����
	BOOL IsShowWind;
	// �Ƿ���ʾʪ��
	BOOL IsShowHumidity;
	// �Ƿ���ʾ��ǰ�¶�
	BOOL IsShowCurTemperature;

	// ������ProvinceName�̶���ʾ����
	char* WeatherConstText;
	// �¶ȵĹ̶���ʾ����
	char* TempConstText;
	// �����Ĺ̶���ʾ����
	char* WindConstText;
	// ��ǰ�¶ȵĹ̶���ʾ����
	char* CurTempConstText;
	// ʪ�ȵĹ̶���ʾ����
	char* HumiConstText;

	//��������
	char* CountryName;
	//ʡ������
	char* ProvinceName;
	//��������
	char* CityName;

	// ����ʱ��
	NP_TIMESPAN PlayDuration;
};
//������
struct NP_DETECTPOINT_PARA
{
public:
	//�����ֵ(��ͬ����оƬ��ֵ��Χ��ͬ)
	BYTE Threshold;
	//������ͣ���ͬ����оƬ֧�ֵĵ�����Ͳ�ͬ����0��ʾ��·��죬1��ʾ��·���
	BYTE PointDetectType;
	//�Ƿ�ʹ�õ�ǰ���õĵ�������
	BOOLEAN IsUseCurrentGain;
	//������
	BYTE RedGain;
	//������
	BYTE GreenGain;
	//������
	BYTE BlutGain;
};
//���ϵƵ���Ϣ
struct NP_ERRORPOINT_INFO
{
public:
	//�����ܵ���
	unsigned int ErrorTotalCount;

	//��ɫ���ϵƵ����
	unsigned int ErrorRedCount;
	//��ɫ���ϵƵ������б��ָ��
	NP_POINT* ErrorRedPoint;
	
	//��ɫ���ϵƵ����
	unsigned int ErrorGreenCount;
	//��ɫ���ϵƵ������б��ָ��
	NP_POINT* ErrorGreenPoint;

	//��ɫ���ϵƵ����
	unsigned int ErrorBlueCount;
	//��ɫ���ϵƵ������б��ָ��
	NP_POINT* ErrorBluePoint;

	//�������ϵƵ����
	unsigned int ErrorVRedCount;
	//�������ϵƵ������б��ָ��
	NP_POINT* ErrorVRedPoint;
};
//�����
struct NP_DETECTPOINT_RESULT
{
public:
	//��������
	unsigned short CabinetIndex;
	//��������ʾ���е�λ�úʹ�С
	NP_RECTANGLE CabinetRegion;

	//�����ܵ���
	unsigned int PixelTotalCount;
	//���Ĺ��ϵƵ���Ϣ
	NP_ERRORPOINT_INFO ErrorPointInfo;
};
//ֵ��Ϣ
struct NP_VALUE_INFO
{
public:
	//�Ƿ���Ч
	BOOLEAN IsValid;
	//ֵ��ֻ�е�IsValidΪTrueʱ����Ч��
	float Value;
};
//�澯��Ϣ
struct NP_ALARM_INFO
{
public:
	//�Ƿ���Ч
	BOOLEAN IsValid;
	//�Ƿ�澯��ֻ��IsValidΪTrueʱ����Ч��
	BOOLEAN IsAlarm;
};
//����ļ������
struct NP_MONITOR_INFO
{
public:
	//��������
	unsigned short CabinetIndex;
	//��������ʾ���е�λ�úʹ�С
	NP_RECTANGLE CabinetRegion;

	//����״̬
	BOOLEAN CabinetStatusIsOK;
	//������ص�ѹ��Ϣ��ֻ��CabinetStatusIsOKΪTrueʱ����Ч
	NP_VALUE_INFO CabinetVoltage;
	//�¶���Ϣ��ֻ��CabinetStatusIsOKΪTrueʱ����Ч
	NP_VALUE_INFO TempInfo;

	//�Ƿ����Ӽ�ؿ���ֻ��CabinetStatusIsOKΪTrueʱ����Ч
	BOOLEAN IsConnectMCard;
	//��ؿ��İ��ص�ѹ��ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	NP_VALUE_INFO MCardVoltage;
	//ʪ����Ϣ��ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	NP_VALUE_INFO HumidityInfo;
	//�����Ƿ�رգ�ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	BOOLEAN CabinetDoorIsClose;
	//����澯��Ϣ��ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	NP_ALARM_INFO SmokeAlarmInfo;
	////���ȸ�����ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	BYTE FanCount;
	//������Ϣ�б��ָ�룬ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	NP_VALUE_INFO* FanInfoPtr;
	//��Դ������ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	BYTE MCPowerCount;
	//��Դ��Ϣ�б��ָ�룬ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	NP_VALUE_INFO* MCVoltagePtr;

	//����״̬�Ƿ�OK,ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
	BOOLEAN IsRowLineOK;
};
#pragma pack(1)
//�����Դ�Զ����Ƶ�ʱ����Ϣ
struct NP_BDPOWER_AUTOTIME_INFO
{
public:
	//�Ƿ���ָ�����ڿ��Ƶ�Դ
	BOOL IsSpecialDate;
	//ָ�����ڵ���ʼ���ڣ�ֻ�е�IsSpecialDateΪTrueʱ����Ч
	NP_DATE StartDate;
	//ָ�����ڵĽ������ڣ�ֻ�е�IsSpecialDateΪTrueʱ����Ч
	NP_DATE StopDate;
	//һ���ڵ�ÿһ���Ƿ���ƣ������ָ�����ڣ����Ӧ��ֵ����ΪFalse,˳������Ϊ�����쵽��������
	BOOL WeekDayIsValid[7];
	//��Դ����ʱ��
	NP_TIMESPAN OpenTime;
	//��Դ�ر�ʱ��
	NP_TIMESPAN CloseTime;
};
//�����Դ�Զ�������Ϣ
struct NP_BDPOWER_AUTOCTRL_INFO
{
public:
	//�����Դ�Զ�������Ϣ��ĸ���
	unsigned short CtrlInfoCount;
	//�����Դ�Զ�������Ϣ�б�
	NP_BDPOWER_AUTOTIME_INFO* CtrlInfoArray;
};
//�๦�E���ϵ�һ·��Դ�Զ�������Ϣ
struct NP_FUNPOWER_AUTOTIME_INFO
{
public:
	//��Դ����ʱ��
	NP_TIMESPAN OpenTime;
	//��Դ�ر�ʱ��
	NP_TIMESPAN CloseTime;
};
//�๦�ܿ��ϵĵ�Դ�Զ�������Ϣ
struct NP_FUNPOWER_AUTOCTRL_INFO
{
public:
	//��Դ�Զ�������Ϣ�б�,�๦�ܿ�8·��Դͳһ����
	NP_FUNPOWER_AUTOTIME_INFO CtrlInfoArray[8];
};
//���Ӽ��Ĳ���
struct NP_CONNECTDETECT_PARA
{
public:
	//����״̬��������
	//0����ʾ���������״̬;
	//1����ʾ����������Ƿ��������ӣ������ù��ܺ�����첽���Ͽ������˵����ӣ�����ʾ��������
	//2����ʾ����첽������������״̬�������ù��ܺ������ͨѶ�����δ�յ��κ��������ʾ��������
	unsigned char DetectType;
	//������״̬���ʱ����СͨѶ�����ֻ�е�DetectTypeΪ2ʱ����Ч,��λΪ�룬Ĭ��60�룬��Сֵ60��
	unsigned short VirConnectMinInterval;
};
//ȫ�ּ����Ϣ
struct NP_GLOBALMONITOR_INFO
{
public:
	//����״̬
	BOOLEAN CabinetStatusIsOK;
	//�����ѹ�Ƿ�OK
	BOOLEAN CabinetVoltageIsOK;
	//�Ƿ����Ӽ�ؿ�
	BOOLEAN IsConnectMonitorCard;
	//��ؿ���ѹ�Ƿ�OK
	BOOLEAN MCVoltageIsOK;
	//�����Ƿ�ر�
	BOOLEAN CabinetDoorIsClose;
	//�����Ƿ�OK
	BOOLEAN RowLineIsOK;
	//��̽ͷ�Ƿ�OK
	BOOLEAN LightSensorIsOK;
};
//���Ͳ����ļ��Ľ�����Ϣ
struct NP_SENDPLAYFILE_INFO
{
public:
	//��ǰ���͵��ļ�����
	char* SendFileName;
	//��ǰ���ڷ��͵��ļ��ķ��ͽ���
	float CurFilePercent;
	//�����ܽ���
	float TotalPercent;
};
//�ն������Լ����
struct NP_SELFMONITORCTRL_PARA
{
public:
	//�Ƿ����������Լ칦��
	BOOLEAN IsCycleSelfMonitor;
	//�Լ����ڣ���λΪ���ӣ�Ĭ��Ϊ30���ӣ���СֵΪ5����
	int SelfMonitorPeriod;
	//���߹���ʱ�Ŀ������ͣ�0��ʾ�����ƣ�1��ʾ���߹���ʱ����
	unsigned char RowLineErrorCtrlType;
};
//���ȵ�����Ϣ
struct NP_BRIGHTADJUST_INFO
{
public:
	//����ʱ��
	NP_TIMESPAN AdjustTime;
	//��������ֵ(0%~100%)
	BYTE BrightValue;
};
//��ʱ���ȵ�����Ϣ
struct NP_SCHEDUALBRIGHT_INFO
{
public:
	//������Ϣ������
	unsigned short AdjustInfoCount;
	//������Ϣ�б�
	NP_BRIGHTADJUST_INFO* AdjustInfoArray;
};
//�洢�豸��Ϣ
struct NP_STORAGEDEVICE_INFO
{
public:
	//��ǰ�洢�豸���ͣ�0��ʾFlash��1��ʾSD����2��ʾUSB�ڽ����Ӳ��
	unsigned char CurStorageDeviceType;
	//Flash��ʣ��ռ��С����λΪ�ֽ�
	INT64 FlashFreeSpace;
	//Flash���ܿռ��С����λΪ�ֽ�
	INT64 FlashTotalSpace;
	//SD����ʣ��ռ��С����λΪ�ֽ�
	INT64 SDCardFreeSpace;
	//SD�����ܿռ��С����λΪ�ֽ�
	INT64 SDCardTotalSpace;
	//USB�ڽӿ��豸��ʣ��ռ��С����λΪ�ֽ�
	INT64 UDiskFreeSpace;
	//USB�ڽӿ��豸���ܿռ��С����λΪ�ֽ�
	INT64 UDiskTotalSpace;
};
#pragma pack