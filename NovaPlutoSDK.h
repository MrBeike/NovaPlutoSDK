#define WM_CARDINFO = 0x0510;
#define WM_CARDDISCONNECT = 0x0511;
#define WM_SENDPLAYPROGRAM = 0x0512;
#define WM_SENDINSERTPLAY = 0x0513;
#define WM_SENDNOTIFICATION = 0x0514;
#define WM_GETPLAYLOGOK = 0x0515;
#define WM_GETPLAYLOGERROR = 0x0516;
#define WM_DETECTPOINTOK = 0x0517;
#define WM_DETECTPOINTFAILED = 0x0518;
#define WM_REFRESHMONITOROK = 0x0519;
#define WM_REFRESHMONITORFAILED = 0x0520;

//初始化系统
//appHwnd： 应用程序的窗口句柄
//recvMsgID： 应用程序接收异步卡异步卡通知信息的主消息ID
//serverIP: 应用程序所在计算机的IP
//serverPort: 应用程序所在计算机网络交互的端口
//recvSavePath：应用程序运行过程中获取到异步卡的信息存储路径
//返回值：系统初始化的结果
extern "C" __declspec(dllexport)bool __stdcall NP_Initialize(HWND appHwnd, int recvMsgID, 
															 char* serverIP, int serverPort, 
															 char* recvSavePath);
//反初始化并释放资源
extern "C" __declspec(dllexport)void __stdcall NP_UnInitialize();
//连接局域网的异步卡
//cardIP：需要连接的异步卡IP，当该值为255.255.255.255时表示广播连接当前局域网的所有终端，
//        此时无论isSync是True还是False，均是异步通知连接到的异步卡。
//isSync：是否同步通知连接结果
//cardInfo：isSync为True时，连接到的异步卡信息
//返回值：当isSync为True，则表示连接异步卡的结果，此时如果返回True，则cardInfo为连接到的异步卡信息，否则连接异步卡失败
//        当isSync为False，则表示发送连接异步卡的命令结果，此时如果返回True，则表示发送命令成功，否则表示发送命令失败。
//备注：异步通知时，应用程序会收到WM_CARDINFO的消息，消息ID为初始化时设置的消息ID，WPARAM参数为WM_CARDINFO，
//      LPARAM参数为连接到的异步卡信息指针
extern "C" __declspec(dllexport)bool __stdcall NP_ConnectCardOfLocalNet(char* cardIP, bool isSync,
																		NP_CARD_INFO* cardInfo);
//获取异步卡的信息
//cardID：需要获取信息的异步卡ID
//isSync：是否同步通知获取到的结果
//cardInfo：isSync为True时，获取到的异步卡信息
//返回值：当isSync为True，则表示获取异步卡信息结果，此时如果返回True，则cardInfo为获取到的异步卡信息，否则获取异步卡信息失败
//        当isSync为False，则表示发送获取异步卡信息的命令结果，此时如果返回True，则表示发送命令成功，否则表示发送命令失败。
//备注：异步通知时，应用程序会收到WM_CARDINFO的消息，消息ID为初始化时设置的消息ID，WPARAM参数为WM_CARDINFO，
//      LPARAM参数为获取到的异步卡信息指针
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardInfo(char* cardID, bool isSync,
															  NP_CARD_INFO* cardInfo);
//创建播放方案
//fileName：需要创建的播放方案文件全路径
//screenSize：创建的播放方案中显示屏的大小（包含宽高）
//返回值：创建的播放方案对象句柄，如果创建失败则为NULL
extern "C" __declspec(dllexport)HANDLE __stdcall NP_CreatePlayProgram(char* fileName,
																	  NP_SIZE screenSize);
//打开指定路径的播放方案
//fileName：需要打开的播放方案全路径
//返回值：打开的播放方案对象句柄，如果打开文件失败则为NULL
extern "C" __declspec(dllexport)HANDLE __stdcall NP_OpenPlayProgram(char* fileName);
//给指定播放方案添加时间段
//programHwnd：需要添加时间段的播放方案对象句柄
//propInfo：添加的时间段信息
//返回值：添加时间段的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddTimeSegment(HANDLE programHwnd,
																 NP_TIMESEGMENT_INFO propInfo);
//给指定播放方案的指定时间段添加常规页面
//programHwnd：需要添加页面的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加页面的时间段索引（从0开始）
//propInfo：添加的页面信息
//返回值：添加页面的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddBasicPage(HANDLE programHwnd,
															   unsigned short timeSegmentIndex, 
															   NP_PAGE_INFO propInfo);
//给指定播放方案指定时间段的指定页面添加窗口
//programHwnd：需要添加窗口的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加窗口的页面所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加窗口的页面索引（从0开始）
//propInfo：添加的窗口信息
//返回值：添加窗口的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddWindowToPage(HANDLE programHwnd,
																  unsigned short timeSegmentIndex,
																  unsigned short pageIndex,
																  NP_WINDOW_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加视频文件
//programHwnd：需要添加视频文件的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加视频文件的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加视频文件的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加视频文件的窗口索引（从0开始）
//fileName：添加的视频文件全路径
//propInfo：视频文件的播放参数
//返回值：添加视频文件的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddVideoFile(HANDLE programHwnd,
															   unsigned short timeSegmentIndex,
															   unsigned short pageIndex,
															   unsigned int windowIndex,
															   char* fileName,
															   NP_VIDEOFIEL_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加图片文件
//programHwnd：需要添加图片文件的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加图片文件的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加图片文件的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加图片文件的窗口索引（从0开始）
//fileName：添加的图片文件全路径
//propInfo：图片文件的播放参数
//返回值：添加图片文件的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddImageFile(HANDLE programHwnd,
															   unsigned short timeSegmentIndex,
															   unsigned short pageIndex,
															   unsigned int windowIndex,
															   char* fileName,
															   NP_IMAGEFILE_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加动画文件
//programHwnd：需要添加动画文件的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加动画文件的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加动画文件的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加动画文件的窗口索引（从0开始）
//fileName：添加的动画文件全路径
//propInfo：动画文件的播放参数
//返回值：添加动画文件的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddFlashFile(HANDLE programHwnd,
															   unsigned short timeSegmentIndex,
															   unsigned short pageIndex,
															   unsigned int windowIndex,
															   char* fileName,
															   NP_FLASH_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加文本文件（txt文件）
//programHwnd：需要添加文本文件（txt文件）的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加文本文件（txt文件）的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加文本文件（txt文件）的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加文本文件（txt文件）的窗口索引（从0开始）
//fileName：添加的文本文件（txt文件）全路径
//propInfo：文本文件（txt文件）的播放参数
//返回值：添加文本文件（txt文件）的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddTxtFile(HANDLE programHwnd,
															 unsigned short timeSegmentIndex,
															 unsigned short pageIndex,
															 unsigned int windowIndex,
															 char* fileName,
															 NP_TXTFILE_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加模拟时钟
//programHwnd：需要添加模拟时钟的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加模拟时钟的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加模拟时钟的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加模拟时钟的窗口索引（从0开始）
//propInfo：模拟时钟的播放参数
//返回值：添加模拟时钟的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddAnalogClock(HANDLE programHwnd,
																 unsigned short timeSegmentIndex,
																 unsigned short pageIndex,
																 unsigned int windowIndex,
																 NP_ANALOGCLOCK_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加数字时钟
//programHwnd：需要添加数字时钟的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加数字时钟的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加数字时钟的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加数字时钟的窗口索引（从0开始）
//propInfo：数字时钟的播放参数
//返回值：添加数字时钟的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddDigitalClock(HANDLE programHwnd,
																  unsigned short timeSegmentIndex,
																  unsigned short pageIndex,
																  unsigned int windowIndex,
																  NP_DIGITALCLOCK_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加单行文本
//programHwnd：需要添加单行文本的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加单行文本的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加单行文本的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加单行文本的窗口索引（从0开始）
//propInfo：单行文本的播放参数
//返回值：添加单行文本的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddSingleLineText(HANDLE programHwnd,
																	unsigned short timeSegmentIndex,
																	unsigned short pageIndex,
																	unsigned int windowIndex,
																	NP_SINGLELINETEXT_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加走马灯
//programHwnd：需要添加走马灯的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加走马灯的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加走马灯的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加走马灯的窗口索引（从0开始）
//propInfo：走马灯的播放参数
//返回值：添加走马灯的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddScrollingText(HANDLE programHwnd,
																   unsigned short timeSegmentIndex,
																   unsigned short pageIndex,
																   unsigned int windowIndex,
																   NP_SCROLLTEXT_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加静态文本
//programHwnd：需要添加静态文本的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加静态文本的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加静态文本的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加静态文本的窗口索引（从0开始）
//propInfo：静态文本的播放参数
//返回值：添加静态文本的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddStaticText(HANDLE programHwnd,
																unsigned short timeSegmentIndex,
																unsigned short pageIndex,
																unsigned int windowIndex,
																NP_STATICTEXT_INFO propInfo);
//给指定播放方案指定时间段指定页面的指定窗口添加天气预报
//programHwnd：需要添加天气预报的播放方案对象句柄
//timeSegmentIndex：播放方案中需要添加天气预报的窗口所在的时间段索引（从0开始）
//pageIndex：播放方案中需要添加天气预报的窗口所在的页面索引（从0开始）
//windowIndex：播放方案中需要添加天气预报的窗口索引（从0开始）
//propInfo：天气预报的播放参数
//返回值：添加天气预报的结果，如果为True则表示添加成功，否则添加失败
extern "C" __declspec(dllexport)bool __stdcall NP_AddWeather(HANDLE programHwnd,
															 unsigned short timeSegmentIndex,
															 unsigned short pageIndex,
															 unsigned int windowIndex,
															 NP_WEATHER_INFO propInfo);
//移除指定播放方案指定时间段指定页面指定窗口的指定媒体
//programHwnd：需要移除媒体的播放方案对象句柄
//timeSegmentIndex：需要移除的媒体所在的时间段索引（从0开始）
//pageIndex：需要移除的媒体所在的页面索引（从0开始）
//windowIndex：需要移除的媒体所在的窗口索引（从0开始）
//mediaIndex：需要移除的媒体在当前窗口中的索引
//返回值：移除媒体的结果，如果为True则表示移除成功，否则移除失败
extern "C" __declspec(dllexport)bool __stdcall NP_RemoveMedia(HANDLE programHwnd,
															  unsigned short timeSegmentIndex,
															  unsigned short pageIndex,
															  unsigned int windowIndex,
															  unsigned int mediaIndex);
//移除指定播放方案指定时间段指定页面的指定窗口
//programHwnd：需要移除窗口的播放方案对象句柄
//timeSegmentIndex：需要移除的窗口所在的时间段索引（从0开始）
//pageIndex：需要移除的窗口所在的页面索引（从0开始）
//windowIndex：需要移除的窗口索引（从0开始）
//返回值：移除窗口的结果，如果为True则表示移除成功，否则移除失败
//备注：如果移除窗口，则该窗口上添加的媒体也会被移除。
extern "C" __declspec(dllexport)bool __stdcall NP_RemoveWindow(HANDLE programHwnd,
															   unsigned short timeSegmentIndex,
															   unsigned short pageIndex,
															   unsigned int windowIndex);
//移除指定播放方案指定时间段的指定页面
//programHwnd：需要移除页面的播放方案对象句柄
//timeSegmentIndex：需要移除的页面所在的时间段索引（从0开始）
//pageIndex：需要移除的页面索引（从0开始）
//返回值：移除页面的结果，如果为True则表示移除成功，否则移除失败
//备注：如果移除页面，则该页面上的窗口及其窗口上添加的媒体也会被移除。
extern "C" __declspec(dllexport)bool __stdcall NP_RemovePage(HANDLE programHwnd,
															 unsigned short timeSegmentIndex,
															 unsigned short pageIndex);
//移除指定播放方案的指定时间段
//programHwnd：需要移除时间段的播放方案对象句柄
//timeSegmentIndex：需要移除的时间段索引（从0开始）
//返回值：移除时间段的结果，如果为True则表示移除成功，否则移除失败
//备注：如果移除时间段，则该时间段的页面、页面上的窗口及其窗口上添加的媒体也会被移除。
extern "C" __declspec(dllexport)bool __stdcall NP_RemoveTimeSegment(HANDLE programHwnd,
																	unsigned short timeSegmentIndex);
//保存指定播放方案
//programHwnd：需要保存的播放方案对象句柄
//返回值：保存播放方案的结果，如果为True则表示保存成功，否则保存失败
extern "C" __declspec(dllexport)bool __stdcall NP_SavePlayProgram(HANDLE programHwnd);
//关闭打开的播放方案
//programHwnd：需要关闭的播放方案对象句柄
//返回值：关闭播放方案的结果，如果为True则表示关闭成功，否则关闭失败
extern "C" __declspec(dllexport)bool __stdcall NP_ClosePlayProgram(HANDLE programHwnd);
//发送播放方案到异步卡
//cardID：需要接收播放方案的异步卡ID
//saveDevice：播放方案发送到异步卡后的存储设备，0表示存储到Flash，1表示存储到SD卡，2表示存储到U盘
//playProgramPath：需要发送的播放方案全路径
//返回值：调用发送播放方案的命令结果，如果为True则表示调用成功，否则调用失败
//备注：接口返回True后，应用程序会在播放方案发送完成后收到WM_SENDPLAYPROGRAM的消息，
//      消息ID为初始化时设置的消息ID，WPARAM参数为WM_SENDPLAYPROGRAM，LPARAM参数为发送的结果
extern "C" __declspec(dllexport)bool __stdcall NP_SendPlayProgram(char* cardID, 
																  unsigned short saveDevice,
																  char* playProgramPath);
//发送视频的插播到异步卡
//cardID：需要接收插播文件的异步卡ID
//fileName：需要插播的视频文件全路径
//windowSize：插播窗口的大小（包含宽、高）
//windowSize：插播窗口的大小（包含宽、高）
//playDuration：插播视频的播放时长
//返回值：调用发送插播文件命令的结果，如果True则表示命令调用成功，否则调用命令失败
//备注：接口返回True后，应用程序会在插播文件发送完成后收到WM_SENDINSERTPLAY的消息，
//      消息ID为初始化时设置的消息ID，WPARAM参数为WM_SENDINSERTPLAY，LPARAM参数为发送的结果
extern "C" __declspec(dllexport)bool __stdcall NP_SendVideoInsertPlay(char* cardID, 
																	  char* fileName,
																	  NP_SIZE windowSize,
																	  NP_TIMESPAN playDuration,
																	  NP_VIDEOFIEL_INFO propInfo);
//发送图片的插播到异步卡
//cardID：需要接收插播文件的异步卡ID
//fileName：需要插播的图片文件全路径
//windowSize：插播窗口的大小（包含宽、高）
//playDuration：插播图片的播放时长
//propInfo：插播图片的播放参数
//返回值：调用发送插播文件命令的结果，如果True则表示命令调用成功，否则调用命令失败
//备注：接口返回True后，应用程序会在插播文件发送完成后收到WM_SENDINSERTPLAY的消息，
//      消息ID为初始化时设置的消息ID，WPARAM参数为WM_SENDINSERTPLAY，LPARAM参数为发送的结果
extern "C" __declspec(dllexport)bool __stdcall NP_SendImageInsertPlay(char* cardID,
																	  char* fileName,
																	  NP_SIZE windowSize,
																	  NP_TIMESPAN playDuration,
																	  NP_IMAGEFILE_INFO propInfo);
//发送静态文本的通知到异步卡
//cardID：需要接收通知文件的异步卡ID
//windowRect：通知内容显示的窗口区域（包括位置和大小）
//playTimes：静态文本的播放次数
//propInfo：静态文本的播放参数
//返回值：调用发送通知文件命令的结果，如果True则表示命令调用成功，否则调用命令失败
//备注：接口返回True后，应用程序会在通知文件发送完成后收到WM_SENDNOTIFICATION的消息，
//      消息ID为初始化时设置的消息ID，WPARAM参数为WM_SENDNOTIFICATION，LPARAM参数为发送的结果
extern "C" __declspec(dllexport)bool __stdcall NP_SendStaticTextNotify(char* cardID,
																	   NP_RECTANGLE windowRect,
																	   unsigned int playTimes,
																	   NP_STATICTEXT_INFO propInfo);
//发送走马灯的通知到异步卡
//cardID：需要接收通知文件的异步卡ID
//windowRect：通知内容显示的窗口区域（包括位置和大小）
//playTimes：走马灯的播放次数
//propInfo：走马灯的播放参数
//返回值：调用发送通知文件命令的结果，如果True则表示命令调用成功，否则调用命令失败
//备注：接口返回True后，应用程序会在通知文件发送完成后收到WM_SENDNOTIFICATION的消息，
//      消息ID为初始化时设置的消息ID，WPARAM参数为WM_SENDNOTIFICATION，LPARAM参数为发送的结果
extern "C" __declspec(dllexport)bool __stdcall NP_SendSrollingTextNotify(char* cardID,
																		 NP_RECTANGLE windowRect,
																		 unsigned int playTimes,
																		 NP_SCROLLTEXT_INFO propInfo);
//发送单行文本的通知到异步卡
//cardID：需要接收通知文件的异步卡ID
//windowRect：通知内容显示的窗口区域（包括位置和大小）
//playTimes：单行文本的播放次数
//propInfo：单行文本的播放参数
//返回值：调用发送通知文件命令的结果，如果True则表示命令调用成功，否则调用命令失败
//备注：接口返回True后，应用程序会在通知文件发送完成后收到WM_SENDNOTIFICATION的消息，
//      消息ID为初始化时设置的消息ID，WPARAM参数为WM_SENDNOTIFICATION，LPARAM参数为发送的结果
extern "C" __declspec(dllexport)bool __stdcall NP_SendSingleLineTextNotify(char* cardID, 
																		   NP_RECTANGLE windowRect,
																		   unsigned int playTimes,
																		   NP_SINGLELINETEXT_INFO propInfo);
//控制异步卡的播放
//cardID：需要控制的异步卡ID
//ctrlMode：控制类型，0表示播放，为1表示暂停，为2表示停止
//返回值：控制播放的结果，如果True则表示控制成功，否则控制失败
extern "C" __declspec(dllexport)bool __stdcall NP_ControlCardPlay(char* cardID, unsigned short ctrlMode);
//获取异步卡播放日志
//cardID：需要获取日志的异步卡ID
//getDate：需要获取的日志的日期
//返回值：调用获取日志命令的结果，如果True则表示调用成功，否则调用失败
//备注：1.接口返回True后，应用程序会在日志获取成功后收到WM_GETPLAYLOGOK的消息，
//        消息ID为初始化时设置的消息ID，WPARAM参数为WM_GETPLAYLOGOK，
//        LPARAM参数为获取到的日志存储的全路径。
//      2.接口返回True后，应用程序会在日志获取失败后收到WM_GETPLAYLOGERROR的消息，
//        消息ID为初始化时设置的消息ID，WPARAM参数为WM_GETPLAYLOGERROR，
//        LPARAM参数为获取日志失败的原因
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardPlayLog(char* cardID, NP_DATE getDate);
//打开指定日志
//logFileName：需要打开的日志的全路径
//返回值：打开的日志对象句柄，如果打开日志失败，则返回NULL
extern "C" __declspec(dllexport)HANDLE __stdcall NP_OpenPlayLogFile(char* logFileName);
//获取指定日志中信息的个数
//logHandle：需要获取信息个数的日志对象句柄
//返回值：获取到的信息个数，如果获取失败则为0
extern "C" __declspec(dllexport)int __stdcall NP_GetPlayLogItemCount(HANDLE logHandle);
///获取指定日志中指定的信息
//logHandle：需要获取信息的日志对象句柄
//itemIndex：需要获取的信息在信息列表中的索引（从0开始）
//logInfo：获取到的日志信息指针
//返回值：获取日志信息的结果，如果获取成功，则返回True，且logInfo为获取到信息的指针，否则返回False。
//备注：如果指定日志或指定索引的信息不存在，则返回False
extern "C" __declspec(dllexport)bool __stdcall NP_GetPlayLogItemInfo(HANDLE logHandle, 
																	 int itemIndex,
																	 NP_PLAYLOG_ITEM* logInfo);
//关闭打开的日志文件
//logHandle：需要关闭的日志对象句柄
//返回值：关闭日志的结果，如果返回True则表示关闭成功，否则关闭失败
extern "C" __declspec(dllexport)bool __stdcall NP_ClosePlayLogFile(HANDLE logHandle);


//设置异步卡的系统时间
//cardID：需要设置时间的异步卡ID
//time：需要设置的时间
//返回值：设置异步卡时间的结果，如果返回True则表示设置成功，否则设置失败
//备注：该接口的执行需要较长时间（1-2秒）。
extern "C" __declspec(dllexport)bool __stdcall NP_SetCardSystemTime(char* cardID, SYSTEMTIME time);
//获取异步卡的系统时间
//cardID：需要获取时间的异步卡ID
//time：获取到的异步卡时间
//返回值：获取异步卡时间的结果，如果返回True则表示获取成功，否则获取失败
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardSystemTime(char* cardID, SYSTEMTIME* time);

//设置异步卡的亮度值
//cardID：需要设置亮度值的异步卡ID
//brightValue：需要设置的亮度值(0-255)
//返回值：设置异步卡亮度值的结果，如果返回True则表示设置成功，否则设置失败
extern "C" __declspec(dllexport)bool __stdcall NP_SetCardBrightValue(char* cardID, BYTE brightValue);
//获取异步卡的亮度值
//cardID：需要获取亮度值的异步卡ID
//brightValue：获取到的异步卡亮度值(0-255)
//返回值：如果获取成功，则返回True，且brightValue为获取到的亮度值，否则返回False。
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardBrightValue(char* cardID, BYTE* brightValue);

//设置异步卡的亮度模式
//cardID：需要设置亮度模式的异步卡ID
//brightMode：需要设置的亮度模式：0表示自动亮度，1表示手动亮度，2表示定时
//返回值：设置异步卡亮度模式的结果，如果返回True则表示设置成功，否则设置失败
extern "C" __declspec(dllexport)bool __stdcall NP_SetCardBrightMode(char* cardID, unsigned char brightMode);
//获取异步卡的亮度模式
//cardID：需要获取亮度模式的异步卡ID
//brightMode：获取到的异步卡亮度模式
//返回值：如果获取成功，则返回True，且brightMode为获取到的亮度模式
//       （0表示自动亮度，1表示手动亮度，2表示定时亮度），否则返回False。
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardBrightMode(char* cardID, unsigned char* brightMode);

//设置异步卡的屏幕状态
//cardID：需要设置屏幕状态的异步卡ID
//screenStatus：需要设置的屏幕状态：0表示正常显示，1表示黑屏。
//返回值：设置异步卡屏幕状态的结果，如果返回True则表示设置成功，否则设置失败
extern "C" __declspec(dllexport)bool __stdcall NP_SetCardSreenStatus(char* cardID, unsigned char screenStatus);

//获取显示屏中的箱体个数
//cardID：需要获取箱体个数的异步卡ID
//cabinetCnt：获取到的箱体个数
//返回值：获取箱体个数的结果，如果获取成功，则返回True，且cabinetCnt为获取到的箱体个数，否则返回False。
extern "C" __declspec(dllexport)bool __stdcall NP_GetCabinetCount(char* cardID, unsigned short* cabinetCnt);
//开始点检
//cardID：需要点检的异步卡ID
//cabinetIndex：需要点检的箱体索引（从0开始）
//detectPointPara：点检时需要的参数
//返回值：发送点检命令的结果，如果返回True，则表示发送点检命令成功，否则表示发送点检命令失败。
//备注：1.接口返回True后，应用程序会在点检成功后收到WM_DETECTPOINTOK的消息，
//        消息ID为初始化时设置的消息ID，WPARAM参数为WM_DETECTPOINTOK，
//        LPARAM参数为点检结果数据指针。
//      2.接口返回True后，应用程序会在点检失败后收到WM_DETECTPOINTFAILED的消息，
//        消息ID为初始化时设置的消息ID，WPARAM参数为WM_DETECTPOINTFAILED，
//        LPARAM参数为点检失败的原因
extern "C" __declspec(dllexport)bool __stdcall NP_BeginDetectPoint(char* cardID, 
																   unsigned short cabinetIndex, 
																   NP_DETECTPOINT_PARA detectPointPara);
//重启异步卡
//cardID：需要重启的异步卡ID
//返回值：重启终端的结果，如果返回True，则表示重启成功，否则表示重启失败。
extern "C" __declspec(dllexport)bool __stdcall NP_RestartCard(char* cardID);
//刷新异步卡的监控信息
//cardID：需要刷新监控信息的异步卡ID
//monitorInfoSavePath:刷新到的监控信息在PC机上的存储路径（存储文件夹）。
//返回值：发送刷新监控信息命令的结果。如果返回True，则表示发送命令成功，否则表示发送刷新监控信息命令失败。
//备注：1.接口返回True后，应用程序会在刷新监控成功后收到WM_REFRESHMONITOROK的消息，
//        消息ID为初始化时设置的消息ID，WPARAM参数为WM_REFRESHMONITOROK，
//        LPARAM参数为监控信息存储文件全路径的指针。
//      2.接口返回True后，应用程序会在刷新监控失败后收到WM_REFRESHMONITORFAILED的消息，
//        消息ID为初始化时设置的消息ID，WPARAM参数为WM_REFRESHMONITORFAILED，
//        LPARAM参数为刷新监控数据失败的原因
extern "C" __declspec(dllexport)bool __stdcall NP_RefreshCardMonitorInfo(char* cardID, char* monitorInfoSavePath);
//打开监控信息存储文件
//monitoInfoFileName：监控信息存储文件的全路径
//返回值：打开的监控信息文件句柄，如果打开文件失败，则返回NULL
extern "C" __declspec(dllexport)HANDLE __stdcall NP_OpenMonitorInfoFile(char* monitoInfoFileName);
//获取指定监控信息存储文件中的信息个数
//hMonitorFile：需要获取信息个数的监控信息文件对象句柄
//返回值：获取到的信息个数，如果获取失败则为0
extern "C" __declspec(dllexport)int __stdcall NP_GetInfoCntFromFile(HANDLE hMonitorFile);
///获取指定监控信息存储文件中指定的监控信息
//hMonitorFile：需要获取信息的监控信息文件对象句柄
//infoIndex：需要获取的监控信息在信息列表中的索引（从0开始）
//monitorInfo：获取到的监控信息指针
//返回值：获取监控信息的结果，如果获取成功，则返回True，且monitorInfo为获取到信息的指针，否则返回False。
//备注：如果指定索引的信息不存在，则返回False
extern "C" __declspec(dllexport)bool __stdcall NP_GetMonitorInfoFromFile(HANDLE hMonitorFile, 
																		 int infoIndex,
																		 NP_MONITOR_INFO* monitorInfo);
//关闭监控信息存储文件
//hMonitorFile：需要关闭的监控信息存储文件对象句柄
//返回值：关闭监控信息存储文件的结果，如果关闭成功，则返回True，否则返回False。
extern "C" __declspec(dllexport)bool __stdcall NP_CloseMonitoInfoFile(HANDLE hMonitorFile);
//设置异步卡通讯状态检测功能是否开启
//cardID： 需要设置通讯状态检测功能是否开启的异步卡ID
//isEanble：是否开启通讯状态检测功能，如果开启则为True，否则为False
//返回值：设置通讯状态检测功能是否开启的结果，如果设置成功，则返回True，否则返回False。
//备注：异步卡通讯状态检测功能开启后，如果异步卡断开与管理端的连接，则显示屏黑屏，
//      如果异步卡恢复与管理端的连接，则显示屏恢复正常显示。
extern "C" __declspec(dllexport)bool __stdcall NP_SetConnectDetectEnable(char* cardID, bool isEanble);
//获取异步卡通讯状态检测功能是否开启
//cardID：需要获取通讯状态检测功能是否开启的异步卡ID
//isEanble：获取到的通讯状态检测功能是否开启的标志
//返回值：获取通讯状态检测功能是否开启的结果，如果获取成功，则返回True，且isEanble为是否开启，否则返回False。
extern "C" __declspec(dllexport)bool __stdcall NP_GetConnectDetectEnable(char* cardID, bool* isEanble);
//设置周期汇报监控状态功能的参数
//cardID： 需要开启周期汇报监控状态的异步卡ID
//isEanble：是否开启周期汇报监控状态，如果开启则为True，否则为False
//cycleValue：周期汇报监控状态的周期，单位为秒，最小值为60秒，默认60秒
//返回值：设置周期汇报监控状态功能是否开启的结果，如果设置成功，则返回True，否则返回False。
//备注：1.接口返回True后，应用程序会在刷新监控成功后收到WM_REFRESHMONITOROK的消息，
//        消息ID为初始化时设置的消息ID，WPARAM参数为WM_REFRESHMONITOROK，
//        LPARAM参数为监控信息存储文件全路径的指针。
//      2.接口返回True后，应用程序会在刷新监控失败后收到WM_REFRESHMONITORFAILED的消息，
//        消息ID为初始化时设置的消息ID，WPARAM参数为WM_REFRESHMONITORFAILED，
//        LPARAM参数为刷新监控数据失败的原因
extern "C" __declspec(dllexport)bool __stdcall NP_SetCycleMontorConfig(char* cardID, bool isEanble, unsigned short cycleValue);
//获取周期汇报监控状态功能的参数
//cardID：需要获取周期汇报监控状态功能是否开启的异步卡ID
//isEanble：获取到的周期汇报监控状态是否开启的标志
//cycleValue：获取到的周期汇报监控状态的周期
//返回值：获取周期汇报监控状态功能参数的结果，如果设置成功，则返回True，否则返回False。
extern "C" __declspec(dllexport)bool __stdcall NP_GetCycleMontorConfig(char* cardID, bool* isEanble, unsigned short* cycleValue);
//获取异步卡的截图
//cardID：需要获取截图的异步卡ID
//picFileName: 获取到的异步卡截图在PC机上的存储路径（文件全路径，后缀为.jpg）
//返回值：获取异步卡截图的结果，如果获取成功，则返回True，且picFileName为获取到的图片文件路径，否则返回False。
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardScreenShotPicture(char* cardID, char* picFileName);

//设置异步卡本板电源的状态
//cardID：需要设置电源状态的异步卡ID
//powerState：设置的本板电源状态（0：关闭， 1：开启）
//返回值：设置异步卡电源状态的结果，如果返回True则表示设置成功，否则表示失败
extern "C" __declspec(dllexport)bool _stdcall NP_SetOnBoardPowerState(char* cardID, unsigned char powerState);
//获取异步卡本板电源的状态
//cardID：需要获取电源状态的异步卡ID
//powerState：获取到的本板电源状态（0：关闭， 1：开启）
//返回值：获取异步卡电源状态的结果，如果返回True则表示获取成功，且powerState为获取到的电源状态，否则表示获取失败
extern "C" __declspec(dllexport)bool _stdcall NP_GetOnBoardPowerState(char* cardID, unsigned char* powerState);
//设置本板电源自动控制的信息（如果不需要自动控制，需要将自动控制信息设置为空）
//cardID：需要设置自动控制信息的异步卡ID
//autoCtrlInfo：设置的本板电源自动控制信息
//返回值：设置本板电源自动控制信息的结果，如果返回True则表示设置成功，否则表示失败。
extern "C" __declspec(dllexport)bool _stdcall NP_SetOnBoardPowerAutoInfo(char* cardID, 
																		 NP_BDPOWER_AUTOCTRL_INFO autoCtrlInfo);
//获取本板电源自动控制的信息
//cardID：需要获取电源自动控制信息的异步卡ID
//autoCtrlInfo：获取到的本板电源自动控制信息
//返回值：获取本板电源自动控制信息的结果
//        如果返回True则表示获取成功，且autoCtrlInfo为获取的本板电源自动控制信息;
//        如果返回False则表示获取失败。
extern "C" __declspec(dllexport)bool _stdcall NP_GetOnBoardPowerAutoInfo(char* cardID,
																		 NP_BDPOWER_AUTOCTRL_INFO* autoCtrlInfo);

//设置连接在异步卡上的多功能卡的电源调节模式
//cardID：需要设置电源调节模式的多功能卡连接的异步卡ID
//funCardIndex：需要设置电源调节模式的多功能卡索引（从0开始）
//adjustMode：设置的电源调节模式（0：手动， 1：自动）
//返回值：设置多功能卡电源调节模式的结果，如果返回True则表示设置成功，否则表示失败
extern "C" __declspec(dllexport)bool _stdcall NP_SetFunPowerAdjustMode(char* cardID, 
																	   unsigned short funCardIndex,
																	   unsigned char adjustMode);
//获取连接在异步卡上的多功能卡的电源调节模式
//cardID：需要获取电源调节模式的多功能卡连接的异步卡ID
//funCardIndex：需要获取电源调节模式的多功能卡索引（从0开始）
//adjustMode：获取到的电源调节模式（0：手动， 1：自动）
//返回值：获取多功能卡电源调节模式的结果
//        如果返回True则表示获取成功，且adjustMode为获取到的电源调节模式;
//        如果返回False则表示获取失败。
extern "C" __declspec(dllexport)bool _stdcall NP_GetFunPowerAdjustMode(char* cardID, 
																	   unsigned short funCardIndex,
																	   unsigned char* adjustMode);
//设置连接在异步卡上的多功能卡的电源状态
//cardID：需要设置电源状态的多功能卡连接的异步卡ID
//funCardIndex：需要设置电源状态的多功能卡索引（从0开始）
//powerIndex：需要设置状态的电源索引(0~7)
//powerState：设置的电源状态（0：关闭，1：开启）
//返回值：设置多功能卡电源状态的结果，如果返回True则表示设置成功，否则失败
extern "C" __declspec(dllexport)bool _stdcall NP_SetFunPowerState(char* cardID, 
																  unsigned short funCardIndex,
																  BYTE powerIndex,
																  unsigned char powerState);
//获取连接在异步卡上的多功能卡的电源状态
//cardID：需要获取电源状态的多功能卡连接的异步卡ID
//funCardIndex：需要获取电源状态的多功能卡索引（从0开始）
//powerIndex：需要获取状态的电源索引(0~7)
//powerState：获取到的电源状态
//返回值：获取多功能卡电源状态的结果
//        如果返回True则表示获取成功，且powerState为获取到的电源状态信息；
//        如果返回False则表示获取失败。
extern "C" __declspec(dllexport)bool _stdcall NP_GetFunPowerState(char* cardID, 
																  unsigned short funCardIndex,
																  BYTE powerIndex,
																  unsigned char* powerState);
//设置连接在异步卡上的多功能卡的电源自动控制信息
//cardID：需要设置电源自动控制信息的多功能卡连接的异步卡ID
//funCardIndex：需要获设置电源自动控制信息的多功能卡索引（从0开始）
//autoCtrlInfo：设置的电源自动控制信息
//返回值：设置多功能卡电源自动控制信息的结果，如果返回True则表示设置成功，否则失败
extern "C" __declspec(dllexport)bool _stdcall NP_SetFunPowerAutoInfo(char* cardID, 
																	 unsigned short funCardIndex,
																	 NP_FUNPOWER_AUTOCTRL_INFO autoCtrlInfo);
//获取连接在异步卡上的多功能卡的电源自动控制信息
//cardID：需要获取电源自动控制信息的多功能卡连接的异步卡ID
//funCardIndex：需要获取电源自动控制信息的多功能卡索引（从0开始）
//autoCtrlInfo：获取到的电源自动控制信息
//返回值：获取多功能卡电源自动控制信息的结果
//        如果返回True则表示获取成功，且autoCtrlInfo为获取到的电源自动控制信息；
//        如果返回False则表示获取失败。
extern "C" __declspec(dllexport)bool _stdcall NP_GetFunPowerAutoInfo(char* cardID, 
																	 unsigned short funCardIndex,
																	 NP_FUNPOWER_AUTOCTRL_INFO* autoCtrlInfo);

//设置异步卡通讯状态检测的参数
//cardID：需要设置通讯状态检测参数的异步卡ID
//detectPara：需要设置的通讯状态检测参数
//返回值：设置通讯状态检测参数的结果，如果返回True则表示设置成功，否则失败
extern "C" __declspec(dllexport)bool _stdcall NP_SetConnectDetectPara(char* cardID,
																	  NP_CONNECTDETECT_PARA detectPara);
//获取异步卡通讯状态检测的参数
//cardID：需要获取通讯状态检测参数的异步卡ID
//detectPara：获取到的通讯状态检测参数
//返回值：获取通讯状态检测参数的结果
//        如果返回True则表示获取成功，且detectPara为获取到的通讯状态检测参数；
//        如果返回False则表示获取失败。
extern "C" __declspec(dllexport)bool _stdcall NP_GetConnectDetectPara(char* cardID, 
																	  NP_CONNECTDETECT_PARA* detectPara);

//获取异步卡的全局监控信息
//cardID：需要获取异步卡全局监控信息的异步卡ID
//monitorInfo：获取到的全局监控信息
//返回值：获取全局监控信息的结果
//        如果返回True则表示获取成功，且monitorInfo为获取到的监控信息
//        如果返回False则表示获取失败。
//备注：只有使用接口NP_SetSelfMonitorPara开启异步卡的自检功能，才能在每次获取全局监控信息时获得到新的信息。
extern "C" __declspec(dllexport)bool _stdcall NP_GetGlobalMonitorInfo(char* cardID, 
																	  NP_GLOBALMONITOR_INFO* monitorInfo);
//保存硬件参数
//cardID：需要保存硬件参数的异步卡ID
//返回值：保存硬件参数的结果，如果返回True则表示保存成功，否则失败
extern "C" __declspec(dllexport)bool _stdcall NP_SaveHardwareParameter(char* cardID);
//获取播放文件发送信息
//cardID：需要获取文件发送信息的异步卡ID
//playFileType：需要获取发送信息的播放文件类型：0表示播放方案，1表示插播文件，2表示通知
//fileSendInfo：获取到的文件发送信息
//返回值：获取文件发送信息的结果
//        如果返回True则表示获取成功，且fileSendInfo为获取到的文件发送信息
//        如果返回False则表示获取失败。
extern "C" __declspec(dllexport)bool _stdcall NP_GetPlayFileSendInfo(char* cardID, 
																	 unsigned char playFileType,
																	 NP_SENDPLAYFILE_INFO* fileSendInfo);

//设置异步卡的自检参数
//cardID：需要设置自检参数的异步卡ID
//selfCtrlPara：需要设置的自检参数
//返回值：设置自检参数的结果，如果返回True则表示设置成功，否则失败
extern "C" __declspec(dllexport)bool _stdcall NP_SetSelfMonitorCtrlPara(char* cardID, 
																		NP_SELFMONITORCTRL_PARA selfCtrlPara);
//获取异步卡的自检参数
//cardID：需要获取自检参数的异步卡ID
//selfCtrlPara：获取到的自检参数
//返回值：获取自检参数的结果
//        如果返回True则表示获取成功，且selfCtrlPara为获取到的自检参数
//        如果返回False则表示获取失败。
extern "C" __declspec(dllexport)bool _stdcall NP_GetSelfMonitorCtrlPara(char* cardID, 
																		NP_SELFMONITORCTRL_PARA* selfCtrlPara);
//设置定时亮度调节信息
//cardID：需要设置定时亮度调节信息的异步卡ID
//adjustInfo：需要设置的定时亮度调节信息
//返回值：设置定时亮度调节信息的结果，如果返回True则表示设置成功，否则失败
extern "C" __declspec(dllexport)bool _stdcall NP_SetSchedualBrightInfo(char* cardID, 
																	   NP_SCHEDUALBRIGHT_INFO adjustInfo);
//获取定时亮度调节信息
//cardID：需要获取定时亮度调节信息的异步卡ID
//adjustInfo：获取到的定时亮度调节信息
//返回值：获取定时亮度调节信息的结果
//        如果返回True则表示获取成功，且adjustInfo为获取到的定时亮度调节信息
//        如果返回False则表示获取失败。
extern "C" __declspec(dllexport)bool _stdcall NP_GetSchedualBrightInfo(char* cardID, 
																	   NP_SCHEDUALBRIGHT_INFO* adjustInfo);

//删除异步卡当前存储设备的媒体
//cardID：需要删除媒体的异步卡ID
//deleteType：需要删除的类型：0表示当前存储设备的所有媒体，1表示当前存储设备的过期媒体
//返回值：删除媒体的结果，如果返回True则表示删除成功，否则失败
extern "C" __declspec(dllexport)bool _stdcall NP_DeleteMedia(char* cardID, unsigned char deleteType);
//获取异步卡的存储设备信息
//cardID：需要获取存储设备信息的异步卡ID
//storageDeviceInfo：获取到的存储设备信息
//返回值：获取存储设备信息的结果
//        如果返回True则表示获取成功，且storageDeviceInfo为获取到的存储设备信息
//        如果返回False则表示获取失败
extern "C" __declspec(dllexport)bool _stdcall NP_GetStorageDeviceInfo(char* cardID, 
																	  NP_STORAGEDEVICE_INFO* storageDeviceInfo);