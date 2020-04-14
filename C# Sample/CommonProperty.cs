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
        /// //ˮƽ��ʼλ��
        /// </summary>
        public int X;
        /// <summary>
        /// ��ֱ��ʼλ��
        /// </summary>
        public int Y;
    }
    //Size
    public struct NP_SIZE
    {
        //���
        public int Width;
        //�߶�
        public int Height;
    };
    //Rectangle
    public struct NP_RECTANGLE
    {
        //x
        public int X;
        //y
        public int Y;
        //���
        public int Width;
        //�߶�
        public int Height;
    };
    //����
    public struct NP_DATE
    {
        //��
        public int Year;
        //��
        public int Month;
        //��
        public int Day;
    };
    //ʱ��
    public struct NP_TIMESPAN
    {
        // ʱ
        public int Hour;
        // ��
        public int Minute;
        // ��
        public int Second;
        // ����
        public int MilliSeconds;
    };
    //����
    public struct NP_FONT
    {
        //����
        public LOGFONTW TextFont;
        //������ɫ
        public uint TextForeColor;
    };
    //������Ч
    public struct NP_FONT_EFFECT
    {
        // ������ʾ��Ч��0��ʾ����Ч��1��ʾ������2��ʾ��ɫ
        public byte EffectType;
        // ������Ч����ɫ
        public uint EffectColor;
    };
    //ý����Ч
    public struct NP_MEDIA_EFFECT
    {
        // �Ƿ�����Ч
        public bool IsHasEffect;
        // ��Ч��ֵ��0��ʾ�����
        public ushort EffectValue;
        // ��Ч�ٶȣ���λΪ��0.1S��
        public uint EffectSpeed;
    };
    //�첽������Ϣ
    public struct NP_CARD_INFO
    {
        // �ն�IP
        public string IP;
        // �ն�ID
        public string ID;
        // �ն�����
        public string Name;
        // ��ʾ�����
        public uint ScreenWidth;
        // ��ʾ���߶�
        public uint ScreenHeight;
        // Ԥ��
        public string Reserved1;
        // Ԥ��
        public string Reserved2;
        // Ԥ��
        public string Reserved3;
    };
    //һ��������־��Ϣ
    public struct NP_PLAYLOG_ITEM
    {
        // ý������
        public string MediaName;
        //ý��ʱ��
        public NP_TIMESPAN MediaDuration;
        // ���Ž����0��ʾ�ɹ���1��ʾ�ļ������ڣ�2��ʾ����ʧ�ܣ�3��ʾ����ʧ��
        public byte PlayResType;
        //�������ŵ�ʱ��
        public SYSTEMTIME StartTime;
        //ֹͣ���ŵ�ʱ��
        public SYSTEMTIME StopTime;
    };
    //���ŷ�����ʱ��ε���Ϣ
    public struct NP_TIMESEGMENT_INFO
    {
        // �Ƿ�ָ�����ڲ���
        public bool IsSpecificDate;
        // �Ƿ�ָ��һ�ܵ�ĳһ��
        public bool IsSpecificDayOfWeek;
        // �Ƿ�ȫ�첥��
        public bool IsWholeDayPlay;
        //ʱ��ε�����
        public string Name;
        //һ�ܵ�ÿһ���Ƿ񲥷ţ�ֻ�е�IsSpecificDayOfWeekΪTrueʱ����Ч��
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.Bool)]
        public bool[] WeekDayIsValid;
        // ��������
        public NP_DATE StartDate;
        // ֹͣ����
        public NP_DATE StopDate;
        //      
        // ��һ���е�����ʱ�䣨ֻ�е�IsWholeDayPlayΪFalseʱ����Ч��
        public NP_TIMESPAN StartTimeOfDay;
        // ��һ���е�ֹͣʱ�䣨ֻ�е�IsWholeDayPlayΪFalseʱ����Ч��
        public NP_TIMESPAN StopTimeOfDay;
    };
    //����ҳ�����Ϣ
    public struct NP_PAGE_INFO
    {
        //ҳ������
        public string Name;
        // ���ŷ�ʽ��0��ʾָ��ʱ���� 1��ʾָ��������2��ʾѭ������
        public byte PlayType;
        // ���Ŵ�������PlayTypeΪ1ʱ��������Ч
        public byte PlayTimes;
        // ����ʱ������PlayTypeΪ0ʱ��������Ч
        public NP_TIMESPAN PlayDuration;
        // ������ɫ
        public uint BackColor;
    };
    //���ڵ���Ϣ
    public struct NP_WINDOW_INFO
    {
        //ҳ������
        public string Name;
        //���ڵ����򣨰���λ�úʹ�С��
        public NP_RECTANGLE WindowRect;
    };
    //��Ƶ�ļ�����Ϣ
    public struct NP_VIDEOFIEL_INFO
    {
        // ������ɫ
        public uint BackColor;
        // ���ű�����0��ʾ������1��ʾԭʼ����
        public byte PlayScale;
        // ������С(0~100)
        public byte Volume;
    };
    //ͼƬ�ļ���Ϣ
    public struct NP_IMAGEFILE_INFO
    {
        // ������ɫ
        public uint BackColor;
        // ��������·��������ޱ������֣��ò���������Ϊ�գ�
        public string BackMusicFileName;

        //�볡��Ч��Ϣ
        public NP_MEDIA_EFFECT InEffectInfo;

        //������Ч��Ϣ
        public NP_MEDIA_EFFECT OutEffectInfo;

        // ���ű�����0��ʾ������1��ʾԭʼ����
        public byte PlayScale;
        // ͣ��ʱ�䣨��λ���룩
        public uint StayTime;
    };
    //Flash����Ϣ
    public struct NP_FLASH_INFO
    {
        // ������ɫ
        public uint BackColor;
        // ���ű�����0��ʾ������1��ʾԭʼ����
        public byte PlayScale;
    };
    //Txt�ļ�����Ϣ
    public struct NP_TXTFILE_INFO
    {
        // ������ɫ
        public uint BackColor;
        // ��������·��������ޱ������֣��ò���������Ϊ�գ�
        public string BackMusicFileName;

        //�볡��Ч��Ϣ
        public NP_MEDIA_EFFECT InEffectInfo;

        //������Ч��Ϣ
        public NP_MEDIA_EFFECT OutEffectInfo;

        // ���ű�����0��ʾ������1��ʾԭʼ����
        public byte PlayScale;
        //  ͣ��ʱ�䣨��λ���룩
        public uint StayTime;

        // ��ɫ��ת���ͣ�0��ʾ������ת��1��ʾ������ɫ��ת��2��ʾ�ڰ׷�ת
        public byte ColorInverseType;
    };
    //����Ƶ���Ϣ
    public struct NP_SCROLLTEXT_INFO
    {
        //�������ʾ������
        public string Text;
        //����
        public NP_FONT TextFont;

        // ������ɫ
        public uint BackColor;

        //������Ч
        public NP_FONT_EFFECT TextEffect;

        // �Ƿ����
        public bool IsScroll;
        // ���������������
        public uint ScrollIntervalPixel;
        // �����ٶȣ�0��ʾ������1��ʾ��֮��2��ʾ���٣�3��ʾ�ȳ��ٿ죬4��ʾ���
        public byte ScrollSpeed;
        // ��������0��ʾ���ҵ���1��ʾ�����ң�2��ʾ���µ��ϣ�3��ʾ���ϵ���
        public byte ScrollDirection;

        // ����ʱ��
        public NP_TIMESPAN PlayDuration;
    };
    //�����ı�����Ϣ
    public struct NP_SINGLELINETEXT_INFO
    {
        //�����ı���ʾ������
        public string Text;
        //����
        public NP_FONT TextFont;

        // ������ɫ
        public uint BackColor;

        //������Ч
        public NP_FONT_EFFECT TextEffect;

        //�볡��Ч��Ϣ
        public NP_MEDIA_EFFECT InEffectInfo;

        //������Ч��Ϣ
        public NP_MEDIA_EFFECT OutEffectInfo;

        //  ͣ��ʱ�䣨��λ���룩
        public uint StayTime;
    };
    //��̬�ı�����Ϣ
    public struct NP_STATICTEXT_INFO
    {
        //��̬�ı���ʾ������
        public string Text;
        //����
        public NP_FONT TextFont;

        // ������ɫ
        public uint BackColor;

        //������Ч
        public NP_FONT_EFFECT TextEffect;

        // �о�
        public uint RowSpacing;
        // �־�
        public uint CharacterSpacing;

        // ���뷽ʽ��0��ʾ����1��ʾ���ң�2��ʾ����
        public byte AlignmentType;

        // ����ʱ��
        public NP_TIMESPAN PlayDuration;
    };
    //ģ��ʱ�ӵ���Ϣ
    public struct NP_ANALOGCLOCK_INFO
    {
        // ������ɫ
        public uint BackColor;
        // ʱ����ɫ
        public uint HourScaleColor;
        // ʱ����
        public ushort HourScaleWidth;
        // ʱ��߶�
        public ushort HourScaleHeight;
        // ʱ����״��0��ʾ���Σ�1��ʾԲ�Σ�2��ʾ����
        public byte HourScaleShape;
        //ʱ������壨��HourScaleShapeΪ2ʱ��ֵ��Ч��
        public LOGFONTW HourScaleFont;
        // �ֱ���ɫ
        public uint MinuteScaleColor;
        // �ֱ���
        public ushort MinuteScaleWidth;
        // �ֱ�߶�
        public ushort MinuteScaleHeight;
        // �ֱ����״��0��ʾ���Σ�1��ʾԲ��
        public byte MinuteScaleShape;

        // ��ʾ����������
        public string Content;
        // ���ֵ�����
        public NP_FONT ContentFont;

        // �Ƿ���ʾ����
        public bool IsShowDate;
        //���ڵ���ʾ���ͣ�0��ʾ����ǰ���ں�1��ʾ����ǰ���ں�
        public byte DateShowType;
        //���ڵ�����
        public NP_FONT DateFont;

        // �Ƿ���ʾ����
        public bool IsShowWeekDay;
        //���ڵ�����
        public NP_FONT WeekDayFont;

        // ʱ�����ɫ
        public uint HourHandColor;
        // �������ɫ
        public uint MinuteHandColor;
        // �������ɫ
        public uint SecondHandColor;
        // ����ʱ��
        public NP_TIMESPAN PlayDuration;
    };
    //����ʱ�ӵ���Ϣ
    public struct NP_DIGITALCLOCK_INFO
    {
        // �̶��ַ���
        public string FixedContent;

        // ���ڵ���ʾ���0��ʾ�����գ�1��ʾ�����꣬2��ʾ������
        public byte DateStyle;

        //���ֵ�����
        public NP_FONT TextFont;

        //������Ч
        public NP_FONT_EFFECT TextEffect;

        //�Ƿ���ʾ��
        public bool IsShowYear;
        // �Ƿ���ʾ��
        public bool IsShowMonth;
        // �Ƿ���ʾ��
        public bool IsShowDay;
        // �Ƿ���ʾ���磨���磩
        public bool IsShowAmOrPm;
        // �Ƿ���ʾʱ
        public bool IsShowHour;
        // �Ƿ���ʾ��
        public bool IsShowMinute;
        // �Ƿ���ʾ��
        public bool IsShowSecond;
        // �Ƿ���ʾ����
        public bool IsShowWeekDay;

        // �����ʾ���0��ʾ��λ������ʾ��1��ʾֻ��ʾ��ݵĺ���λ
        public byte YearStyle;
        // ʱ����ʾ���0��ʾ24Сʱ�ƣ�1��ʾ12Сʱ��
        public byte HourStyle;
        // �Ƿ�֧�ֶ�����ʾ
        public bool IsMultiLine;

        // ����ʱ��
        public NP_TIMESPAN PlayDuration;
    };
    //��������Ϣ
    public struct NP_WEATHER_INFO
    {
        // ������ɫ
        public uint BackColor;

        //���ֵ�����
        public NP_FONT TextFont;

        //������Ч
        public NP_FONT_EFFECT TextEffect;

        // �������µ����ڣ���λ�����ӣ�
        public uint UpdateInterval;

        // ��ʾ���ͣ�0��ʾ�ֶ�����ʾ��1��ʾ���о�ֹ��2��ʾ���й���
        public byte ShowType;
        // �����ٶȣ�ShowTypeΪ2ʱ��Ч��0��ʾ������1��ʾ��֮��2��ʾ���٣�3��ʾ�ȳ��ٿ죬4��ʾ���
        public byte ScrollSpeed;

        // �Ƿ���ʾ����
        public bool IsShowWeather;
        // �Ƿ���ʾ�¶�
        public bool IsShowTemperature;
        // �Ƿ���ʾ����
        public bool IsShowWind;
        // �Ƿ���ʾʪ��
        public bool IsShowHumidity;
        // �Ƿ���ʾ��ǰ�¶�
        public bool IsShowCurTemperature;

        // ������ProvinceName�̶���ʾ����
        public string WeatherConstText;
        // �¶ȵĹ̶���ʾ����
        public string TempConstText;
        // �����Ĺ̶���ʾ����
        public string WindConstText;
        // ��ǰ�¶ȵĹ̶���ʾ����
        public string CurTempConstText;
        // ʪ�ȵĹ̶���ʾ����
        public string HumiConstText;

        //��������
        public string CountryName;
        //ʡ������
        public string ProvinceName;
        //��������
        public string CityName;

        // ����ʱ��
        public NP_TIMESPAN PlayDuration;
    };
    /// <summary>
    /// ������
    /// </summary>
    public struct NP_DETECTPOINT_PARA
    {
        //�����ֵ(��ͬ����оƬ��ֵ��Χ��ͬ)
        public byte Threshold;
        //������ͣ���ͬ����оƬ֧�ֵĵ�����Ͳ�ͬ����0��ʾ��·��죬1��ʾ��·���
        public byte PointDetectType;
        //�Ƿ�ʹ�õ�ǰ���õĵ�������
        public Boolean IsUseCurrentGain;
        //������
        public byte RedGain;
        //������
        public byte GreenGain;
        //������
        public byte BlutGain;
    };
    /// <summary>
    /// ���ϵƵ���Ϣ
    /// </summary>
    public struct NP_ERRORPOINT_INFO
    {
        /// <summary>
        /// �����ܵ���
        /// </summary>
        public uint ErrorTotalCount;

        /// <summary>
        /// ��ɫ���ϵƵ����
        /// </summary>
        public uint ErrorRedCount;
        /// <summary>
        /// ��ɫ���ϵƵ������б��ָ��
        /// </summary>
        public IntPtr ErrorRedPoint;

        /// <summary>
        /// ��ɫ���ϵƵ����
        /// </summary>
        public uint ErrorGreenCount;
        /// <summary>
        /// ��ɫ���ϵƵ������б��ָ��
        /// </summary>
        public IntPtr ErrorGreenPoint;

        /// <summary>
        /// ��ɫ���ϵƵ����
        /// </summary>
        public uint ErrorBlueCount;
        /// <summary>
        /// ��ɫ���ϵƵ������б��ָ��
        /// </summary>
        public IntPtr ErrorBluePoint;

        /// <summary>
        /// �������ϵƵ����
        /// </summary>
        public uint ErrorVRedCount;
        /// <summary>
        /// �������ϵƵ������б��ָ��
        /// </summary>
        public IntPtr ErrorVRedPoint;
    };
    /// <summary>
    /// �����
    /// </summary>
    public struct NP_DETECTPOINT_RESULT
    {
        /// <summary>
        /// ��������
        /// </summary>
        public ushort CabinetIndex;
        /// <summary>
        /// ��������ʾ���е�λ�úʹ�С
        /// </summary>
        public NP_RECTANGLE CabinetRegion;

        /// <summary>
        /// �����ܵ���
        /// </summary>
        public uint PixelTotalCount;
        /// <summary>
        /// ���Ĺ��ϵƵ���Ϣ
        /// </summary>
        public NP_ERRORPOINT_INFO ErrorPointInfo;
    };
    /// <summary>
    /// ֵ��Ϣ
    /// </summary>
    public struct NP_VALUE_INFO
    {     
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsValid;
        /// <summary>
        /// ֵ��ֻ�е�IsValidΪTrueʱ����Ч��
        /// </summary>
        public float Value;
    };
    /// <summary>
    /// �澯��Ϣ
    /// </summary>
    public struct NP_ALARM_INFO
    {
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsValid;
        /// <summary>
        /// �Ƿ�澯��ֻ��IsValidΪTrueʱ����Ч��
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsAlarm;
    };
    /// <summary>
    /// ����ļ������
    /// </summary>
    public struct NP_MONITOR_INFO
    {
        /// <summary>
        /// ��������
        /// </summary>
        public ushort CabinetIndex;
        /// <summary>
        /// ��������ʾ���е�λ�úʹ�С
        /// </summary>
        public NP_RECTANGLE CabinetRegion;

        /// <summary>
        /// ����״̬�Ƿ�����
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetStatusIsOK;
        /// <summary>
        /// ������ص�ѹ��Ϣ��ֻ��CabinetStatusIsOKΪTrueʱ����Ч
        /// </summary>
        public NP_VALUE_INFO CabinetVoltage;
        /// <summary>
        /// �¶���Ϣ��ֻ��CabinetStatusIsOKΪTrueʱ����Ч
        /// </summary>
        public NP_VALUE_INFO TempInfo;

        /// <summary>
        /// �Ƿ����Ӽ�ؿ���ֻ��CabinetStatusIsOKΪTrueʱ����Ч
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsConnectMCard;
        /// <summary>
        /// ��ؿ��İ��ص�ѹ��ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        public NP_VALUE_INFO MCardVoltage;
        /// <summary>
        /// ʪ����Ϣ��ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        public NP_VALUE_INFO HumidityInfo;
        /// <summary>
        /// �����Ƿ�رգ�ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetDoorIsClose;
        /// <summary>
        /// ����澯��Ϣ��ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        public NP_ALARM_INFO SmokeAlarmInfo;
        /// <summary>
        /// ���ȸ�����ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        public byte FanCount;
        /// <summary>
        /// ������Ϣ�б��ָ�룬ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        public IntPtr FanInfoPtr;
        /// <summary>
        /// ��Դ������ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        public byte MCPowerCount;
        /// <summary>
        /// ��Դ��Ϣ�б��ָ�룬ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        public IntPtr MCVoltagePtr;
        /// <summary>
        /// ����״̬�Ƿ�OK,ֻ�е�CabinetStatusIsOK��IsConnectMCard��ΪTrueʱ����Ч
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsRowLineOK;
    };
    /// <summary>
    /// �����Դ�Զ����Ƶ�ʱ����Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_BDPOWER_AUTOTIME_INFO
    {
	    /// <summary>
	    /// �Ƿ���ָ�����ڿ��Ƶ�Դ
	    /// </summary>
	    public Boolean IsSpecialDate;
	    /// <summary>
	    /// ָ�����ڵ���ʼ���ڣ�ֻ�е�IsSpecialDateΪTrueʱ����Ч
	    /// </summary>
	    public NP_DATE StartDate;
	    /// <summary>
	    /// ָ�����ڵĽ������ڣ�ֻ�е�IsSpecialDateΪTrueʱ����Ч
	    /// </summary>
	    public NP_DATE StopDate;
	    /// <summary>
	    /// һ���ڵ�ÿһ���Ƿ���ƣ������ָ�����ڣ����Ӧ��ֵ����ΪFalse,˳������Ϊ�����쵽��������
	    /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.Bool)]
        public Boolean[] WeekDayIsValid;
	    /// <summary>
	    /// ��Դ����ʱ��
	    /// </summary>
	    public NP_TIMESPAN OpenTime;
	    /// <summary>
	    /// ��Դ�ر�ʱ��
	    /// </summary>
	    public NP_TIMESPAN CloseTime;
    };
    /// <summary>
    /// �����Դ�Զ�������Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_BDPOWER_AUTOCTRL_INFO
    {
	    /// <summary>
	    /// �����Դ�Զ�������Ϣ��ĸ���
	    /// </summary>
	    public ushort CtrlInfoCount;
	    /// <summary>
	    /// �����Դ�Զ�������Ϣ�б�
	    /// </summary>
	    public IntPtr CtrlInfoArrayPtr;
    };
    /// <summary>
    /// �๦�E���ϵ�һ·��Դ�Զ�������Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_FUNPOWER_AUTOTIME_INFO
    {
	    /// <summary>
	    /// ��Դ����ʱ��
	    /// </summary>
	    public NP_TIMESPAN OpenTime;
	    /// <summary>
	    /// ��Դ�ر�ʱ��
	    /// </summary>
	    public NP_TIMESPAN CloseTime;
    };
    /// <summary>
    /// �๦�ܿ��ϵĵ�Դ�Զ�������Ϣ
    /// </summary>
    public struct NP_FUNPOWER_AUTOCTRL_INFO
    {
        /// <summary>
        /// ��Դ�Զ�������Ϣ�б�
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8)]
        public NP_FUNPOWER_AUTOTIME_INFO[] CtrlInfoArray;
    };
    /// <summary>
    /// ���Ӽ��Ĳ���
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_CONNECTDETECT_INFO
    {
        /// <summary>
        /// ����״̬��������
	    /// 0����ʾ���������״̬;
	    /// 1����ʾ����������Ƿ��������ӣ������ù��ܺ�����첽���Ͽ������˵����ӣ�����ʾ��������
	    /// 2����ʾ����첽������������״̬�������ù��ܺ������ͨѶ�����δ�յ��κ��������ʾ��������
        /// </summary>
	    public byte DetectType;
	    /// <summary>
        /// ������״̬���ʱ����СͨѶ�����ֻ�е�DetectTypeΪ2ʱ����Ч,��λΪ�룬Ĭ��60�룬��Сֵ60��
	    /// </summary>
	    public ushort VirConnectMinInterval;
    };
    /// <summary>
    /// ȫ�ּ����Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_GLOBALMONITOR_INFO
    {
        /// <summary>
        /// ����״̬
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetStatusIsOK;
        /// <summary>
        /// �����ѹ�Ƿ�OK
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetVoltageIsOK;
        /// <summary>
        /// �Ƿ����Ӽ�ؿ�
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean IsConnectMonitorCard;
        /// <summary>
        /// ��ؿ���ѹ�Ƿ�OK
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean MCVoltageIsOK;
        /// <summary>
        /// �����Ƿ�ر�
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean CabinetDoorIsClose;
        /// <summary>
        /// �����Ƿ�OK
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean RowLineIsOK;
        /// <summary>
        /// ��̽ͷ�Ƿ�OK
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public Boolean LightSensorIsOK;
    };
    /// <summary>
    /// ���Ͳ����ļ��Ľ�����Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_SENDPLAYFILE_INFO
    {
	    /// <summary>
        /// ��ǰ���͵��ļ�����
	    /// </summary>
	    public string SendFileName;
	    /// <summary>
        /// ��ǰ���ڷ��͵��ļ��ķ��ͽ���
	    /// </summary>
	    public float CurFilePercent;
	    /// <summary>
        /// �����ܽ���
	    /// </summary>
	    public float TotalPercent;
    };
    /// <summary>
    /// �ն������Լ����
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_SELFMONITORCTRL_PARA
    {
	    /// <summary>
        /// �Ƿ����������Լ칦��
	    /// </summary>
        [MarshalAs(UnmanagedType.U1)]
	    public Boolean IsCycleSelfMonitor;
	    /// <summary>
        /// �Լ����ڣ���λΪ���ӣ�Ĭ��Ϊ30���ӣ���СֵΪ5����
	    /// </summary>
	    public int SelfMonitorPeriod;
        /// <summary>
        /// ���߹���ʱ�Ŀ������ͣ�0��ʾ�����ƣ�1��ʾ���߹���ʱ����
        /// </summary>
        public byte RowLineErrorCtrlType;
    };
    /// <summary>
    /// ���ȵ�����Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_BRIGHTADJUST_INFO
    {
	    /// <summary>
        /// ����ʱ��
	    /// </summary>
	    public NP_TIMESPAN AdjustTime;
	    /// <summary>
        /// ��������ֵ(0%~100%)
	    /// </summary>
	    public Byte BrightValue;
    };
    /// <summary>
    /// ��ʱ���ȵ�����Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_SCHEDUALBRIGHT_INFO
    {
	    /// <summary>
        /// ������Ϣ������
	    /// </summary>
	    public ushort AdjustInfoCount;
	    /// <summary>
        /// ������Ϣ�б�
	    /// </summary>
	    public IntPtr AdjustInfoArrayPtr;
    };
    /// <summary>
    /// �洢�豸��Ϣ
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NP_STORAGEDEVICE_INFO
    {
	    /// <summary>
        /// ��ǰ�洢�豸���ͣ�0��ʾFlash��1��ʾSD����2��ʾUSB�ڽ����Ӳ��
	    /// </summary>
	    public byte CurStorageDeviceType;
	    /// <summary>
        /// Flash��ʣ��ռ��С����λΪ�ֽ�
	    /// </summary>
	    public long FlashFreeSpace;
	    /// <summary>
        /// Flash���ܿռ��С����λΪ�ֽ�
	    /// </summary>
	    public long FlashTotalSpace;
	    /// <summary>
        /// SD����ʣ��ռ��С����λΪ�ֽ�
	    /// </summary>
	    public long SDCardFreeSpace;
	    /// <summary>
        /// SD�����ܿռ��С����λΪ�ֽ�
	    /// </summary>
	    public long SDCardTotalSpace;
	    /// <summary>
        /// USB�ڽӿ��豸��ʣ��ռ��С����λΪ�ֽ�
	    /// </summary>
	    public long UDiskFreeSpace;
	    /// <summary>
        /// USB�ڽӿ��豸���ܿռ��С����λΪ�ֽ�
	    /// </summary>
	    public long UDiskTotalSpace;
    };
    /// <summary>
    /// ������Ϣ����
    /// </summary>
    public enum CustomMessageType
    {
        /// <summary>
        /// �õ�����Ϣ����Ϣ
        /// </summary>
        WM_CardInfo = 0x0510,
        /// <summary>
        /// �첽���Ͽ�����Ϣ
        /// </summary>
        WM_CardDisconnect = 0x0511,
        /// <summary>
        /// ���Ͳ��ŷ�����ɵ���Ϣ
        /// </summary>
        WM_SendPlayProgram = 0x0512,
        /// <summary>
        /// ���Ͳ岥�ļ���ɵ���Ϣ
        /// </summary>
        WM_SendInsertPlay = 0x0513,
        /// <summary>
        /// ����֪ͨ��ɵ���Ϣ
        /// </summary>
        WM_SendNotify = 0x0514,
        /// <summary>
        /// ��ȡ��־�ɹ�����Ϣ
        /// </summary>
        WM_GetPlayLogOK = 0x0515,
        /// <summary>
        /// ��ȡ��־ʧ�ܵ���Ϣ
        /// </summary>
        WM_GetPlayLogError = 0x0516,
        /// <summary>
        /// ���ɹ�
        /// </summary>
        WM_DetectPointOK = 0x0517,
        /// <summary>
        /// ���ʧ��
        /// </summary>
        WM_DetectPointFailed = 0x0518,
        /// <summary>
        /// ˢ�¼������OK
        /// </summary>
        WM_RefreshMonitorOK = 0x0519,
        /// <summary>
        /// ˢ�¼������ʧ��
        /// </summary>
        WM_RefreshMonitorFailed = 0x0520
    }
}
