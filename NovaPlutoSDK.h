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

//��ʼ��ϵͳ
//appHwnd�� Ӧ�ó���Ĵ��ھ��
//recvMsgID�� Ӧ�ó�������첽���첽��֪ͨ��Ϣ������ϢID
//serverIP: Ӧ�ó������ڼ������IP
//serverPort: Ӧ�ó������ڼ�������罻���Ķ˿�
//recvSavePath��Ӧ�ó������й����л�ȡ���첽������Ϣ�洢·��
//����ֵ��ϵͳ��ʼ���Ľ��
extern "C" __declspec(dllexport)bool __stdcall NP_Initialize(HWND appHwnd, int recvMsgID, 
															 char* serverIP, int serverPort, 
															 char* recvSavePath);
//����ʼ�����ͷ���Դ
extern "C" __declspec(dllexport)void __stdcall NP_UnInitialize();
//���Ӿ��������첽��
//cardIP����Ҫ���ӵ��첽��IP������ֵΪ255.255.255.255ʱ��ʾ�㲥���ӵ�ǰ�������������նˣ�
//        ��ʱ����isSync��True����False�������첽֪ͨ���ӵ����첽����
//isSync���Ƿ�ͬ��֪ͨ���ӽ��
//cardInfo��isSyncΪTrueʱ�����ӵ����첽����Ϣ
//����ֵ����isSyncΪTrue�����ʾ�����첽���Ľ������ʱ�������True����cardInfoΪ���ӵ����첽����Ϣ�����������첽��ʧ��
//        ��isSyncΪFalse�����ʾ���������첽��������������ʱ�������True�����ʾ��������ɹ��������ʾ��������ʧ�ܡ�
//��ע���첽֪ͨʱ��Ӧ�ó�����յ�WM_CARDINFO����Ϣ����ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_CARDINFO��
//      LPARAM����Ϊ���ӵ����첽����Ϣָ��
extern "C" __declspec(dllexport)bool __stdcall NP_ConnectCardOfLocalNet(char* cardIP, bool isSync,
																		NP_CARD_INFO* cardInfo);
//��ȡ�첽������Ϣ
//cardID����Ҫ��ȡ��Ϣ���첽��ID
//isSync���Ƿ�ͬ��֪ͨ��ȡ���Ľ��
//cardInfo��isSyncΪTrueʱ����ȡ�����첽����Ϣ
//����ֵ����isSyncΪTrue�����ʾ��ȡ�첽����Ϣ�������ʱ�������True����cardInfoΪ��ȡ�����첽����Ϣ�������ȡ�첽����Ϣʧ��
//        ��isSyncΪFalse�����ʾ���ͻ�ȡ�첽����Ϣ������������ʱ�������True�����ʾ��������ɹ��������ʾ��������ʧ�ܡ�
//��ע���첽֪ͨʱ��Ӧ�ó�����յ�WM_CARDINFO����Ϣ����ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_CARDINFO��
//      LPARAM����Ϊ��ȡ�����첽����Ϣָ��
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardInfo(char* cardID, bool isSync,
															  NP_CARD_INFO* cardInfo);
//�������ŷ���
//fileName����Ҫ�����Ĳ��ŷ����ļ�ȫ·��
//screenSize�������Ĳ��ŷ�������ʾ���Ĵ�С��������ߣ�
//����ֵ�������Ĳ��ŷ������������������ʧ����ΪNULL
extern "C" __declspec(dllexport)HANDLE __stdcall NP_CreatePlayProgram(char* fileName,
																	  NP_SIZE screenSize);
//��ָ��·���Ĳ��ŷ���
//fileName����Ҫ�򿪵Ĳ��ŷ���ȫ·��
//����ֵ���򿪵Ĳ��ŷ�����������������ļ�ʧ����ΪNULL
extern "C" __declspec(dllexport)HANDLE __stdcall NP_OpenPlayProgram(char* fileName);
//��ָ�����ŷ������ʱ���
//programHwnd����Ҫ���ʱ��εĲ��ŷ���������
//propInfo����ӵ�ʱ�����Ϣ
//����ֵ�����ʱ��εĽ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddTimeSegment(HANDLE programHwnd,
																 NP_TIMESEGMENT_INFO propInfo);
//��ָ�����ŷ�����ָ��ʱ�����ӳ���ҳ��
//programHwnd����Ҫ���ҳ��Ĳ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ���ҳ���ʱ�����������0��ʼ��
//propInfo����ӵ�ҳ����Ϣ
//����ֵ�����ҳ��Ľ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddBasicPage(HANDLE programHwnd,
															   unsigned short timeSegmentIndex, 
															   NP_PAGE_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ��ε�ָ��ҳ����Ӵ���
//programHwnd����Ҫ��Ӵ��ڵĲ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ��Ӵ��ڵ�ҳ�����ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ��Ӵ��ڵ�ҳ����������0��ʼ��
//propInfo����ӵĴ�����Ϣ
//����ֵ����Ӵ��ڵĽ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddWindowToPage(HANDLE programHwnd,
																  unsigned short timeSegmentIndex,
																  unsigned short pageIndex,
																  NP_WINDOW_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ�����������Ƶ�ļ�
//programHwnd����Ҫ�����Ƶ�ļ��Ĳ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ�����Ƶ�ļ��Ĵ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ�����Ƶ�ļ��Ĵ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ�����Ƶ�ļ��Ĵ�����������0��ʼ��
//fileName����ӵ���Ƶ�ļ�ȫ·��
//propInfo����Ƶ�ļ��Ĳ��Ų���
//����ֵ�������Ƶ�ļ��Ľ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddVideoFile(HANDLE programHwnd,
															   unsigned short timeSegmentIndex,
															   unsigned short pageIndex,
															   unsigned int windowIndex,
															   char* fileName,
															   NP_VIDEOFIEL_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ���������ͼƬ�ļ�
//programHwnd����Ҫ���ͼƬ�ļ��Ĳ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ���ͼƬ�ļ��Ĵ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ���ͼƬ�ļ��Ĵ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ���ͼƬ�ļ��Ĵ�����������0��ʼ��
//fileName����ӵ�ͼƬ�ļ�ȫ·��
//propInfo��ͼƬ�ļ��Ĳ��Ų���
//����ֵ�����ͼƬ�ļ��Ľ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddImageFile(HANDLE programHwnd,
															   unsigned short timeSegmentIndex,
															   unsigned short pageIndex,
															   unsigned int windowIndex,
															   char* fileName,
															   NP_IMAGEFILE_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ��������Ӷ����ļ�
//programHwnd����Ҫ��Ӷ����ļ��Ĳ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ��Ӷ����ļ��Ĵ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ��Ӷ����ļ��Ĵ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ��Ӷ����ļ��Ĵ�����������0��ʼ��
//fileName����ӵĶ����ļ�ȫ·��
//propInfo�������ļ��Ĳ��Ų���
//����ֵ����Ӷ����ļ��Ľ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddFlashFile(HANDLE programHwnd,
															   unsigned short timeSegmentIndex,
															   unsigned short pageIndex,
															   unsigned int windowIndex,
															   char* fileName,
															   NP_FLASH_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ����������ı��ļ���txt�ļ���
//programHwnd����Ҫ����ı��ļ���txt�ļ����Ĳ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ����ı��ļ���txt�ļ����Ĵ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ����ı��ļ���txt�ļ����Ĵ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ����ı��ļ���txt�ļ����Ĵ�����������0��ʼ��
//fileName����ӵ��ı��ļ���txt�ļ���ȫ·��
//propInfo���ı��ļ���txt�ļ����Ĳ��Ų���
//����ֵ������ı��ļ���txt�ļ����Ľ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddTxtFile(HANDLE programHwnd,
															 unsigned short timeSegmentIndex,
															 unsigned short pageIndex,
															 unsigned int windowIndex,
															 char* fileName,
															 NP_TXTFILE_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ���������ģ��ʱ��
//programHwnd����Ҫ���ģ��ʱ�ӵĲ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ���ģ��ʱ�ӵĴ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ���ģ��ʱ�ӵĴ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ���ģ��ʱ�ӵĴ�����������0��ʼ��
//propInfo��ģ��ʱ�ӵĲ��Ų���
//����ֵ�����ģ��ʱ�ӵĽ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddAnalogClock(HANDLE programHwnd,
																 unsigned short timeSegmentIndex,
																 unsigned short pageIndex,
																 unsigned int windowIndex,
																 NP_ANALOGCLOCK_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ�������������ʱ��
//programHwnd����Ҫ�������ʱ�ӵĲ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ�������ʱ�ӵĴ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ�������ʱ�ӵĴ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ�������ʱ�ӵĴ�����������0��ʼ��
//propInfo������ʱ�ӵĲ��Ų���
//����ֵ���������ʱ�ӵĽ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddDigitalClock(HANDLE programHwnd,
																  unsigned short timeSegmentIndex,
																  unsigned short pageIndex,
																  unsigned int windowIndex,
																  NP_DIGITALCLOCK_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ��������ӵ����ı�
//programHwnd����Ҫ��ӵ����ı��Ĳ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ��ӵ����ı��Ĵ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ��ӵ����ı��Ĵ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ��ӵ����ı��Ĵ�����������0��ʼ��
//propInfo�������ı��Ĳ��Ų���
//����ֵ����ӵ����ı��Ľ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddSingleLineText(HANDLE programHwnd,
																	unsigned short timeSegmentIndex,
																	unsigned short pageIndex,
																	unsigned int windowIndex,
																	NP_SINGLELINETEXT_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ��������������
//programHwnd����Ҫ�������ƵĲ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ�������ƵĴ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ�������ƵĴ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ�������ƵĴ�����������0��ʼ��
//propInfo������ƵĲ��Ų���
//����ֵ���������ƵĽ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddScrollingText(HANDLE programHwnd,
																   unsigned short timeSegmentIndex,
																   unsigned short pageIndex,
																   unsigned int windowIndex,
																   NP_SCROLLTEXT_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ��������Ӿ�̬�ı�
//programHwnd����Ҫ��Ӿ�̬�ı��Ĳ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ��Ӿ�̬�ı��Ĵ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ��Ӿ�̬�ı��Ĵ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ��Ӿ�̬�ı��Ĵ�����������0��ʼ��
//propInfo����̬�ı��Ĳ��Ų���
//����ֵ����Ӿ�̬�ı��Ľ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddStaticText(HANDLE programHwnd,
																unsigned short timeSegmentIndex,
																unsigned short pageIndex,
																unsigned int windowIndex,
																NP_STATICTEXT_INFO propInfo);
//��ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ�������������Ԥ��
//programHwnd����Ҫ�������Ԥ���Ĳ��ŷ���������
//timeSegmentIndex�����ŷ�������Ҫ�������Ԥ���Ĵ������ڵ�ʱ�����������0��ʼ��
//pageIndex�����ŷ�������Ҫ�������Ԥ���Ĵ������ڵ�ҳ����������0��ʼ��
//windowIndex�����ŷ�������Ҫ�������Ԥ���Ĵ�����������0��ʼ��
//propInfo������Ԥ���Ĳ��Ų���
//����ֵ���������Ԥ���Ľ�������ΪTrue���ʾ��ӳɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_AddWeather(HANDLE programHwnd,
															 unsigned short timeSegmentIndex,
															 unsigned short pageIndex,
															 unsigned int windowIndex,
															 NP_WEATHER_INFO propInfo);
//�Ƴ�ָ�����ŷ���ָ��ʱ���ָ��ҳ��ָ�����ڵ�ָ��ý��
//programHwnd����Ҫ�Ƴ�ý��Ĳ��ŷ���������
//timeSegmentIndex����Ҫ�Ƴ���ý�����ڵ�ʱ�����������0��ʼ��
//pageIndex����Ҫ�Ƴ���ý�����ڵ�ҳ����������0��ʼ��
//windowIndex����Ҫ�Ƴ���ý�����ڵĴ�����������0��ʼ��
//mediaIndex����Ҫ�Ƴ���ý���ڵ�ǰ�����е�����
//����ֵ���Ƴ�ý��Ľ�������ΪTrue���ʾ�Ƴ��ɹ��������Ƴ�ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_RemoveMedia(HANDLE programHwnd,
															  unsigned short timeSegmentIndex,
															  unsigned short pageIndex,
															  unsigned int windowIndex,
															  unsigned int mediaIndex);
//�Ƴ�ָ�����ŷ���ָ��ʱ���ָ��ҳ���ָ������
//programHwnd����Ҫ�Ƴ����ڵĲ��ŷ���������
//timeSegmentIndex����Ҫ�Ƴ��Ĵ������ڵ�ʱ�����������0��ʼ��
//pageIndex����Ҫ�Ƴ��Ĵ������ڵ�ҳ����������0��ʼ��
//windowIndex����Ҫ�Ƴ��Ĵ�����������0��ʼ��
//����ֵ���Ƴ����ڵĽ�������ΪTrue���ʾ�Ƴ��ɹ��������Ƴ�ʧ��
//��ע������Ƴ����ڣ���ô�������ӵ�ý��Ҳ�ᱻ�Ƴ���
extern "C" __declspec(dllexport)bool __stdcall NP_RemoveWindow(HANDLE programHwnd,
															   unsigned short timeSegmentIndex,
															   unsigned short pageIndex,
															   unsigned int windowIndex);
//�Ƴ�ָ�����ŷ���ָ��ʱ��ε�ָ��ҳ��
//programHwnd����Ҫ�Ƴ�ҳ��Ĳ��ŷ���������
//timeSegmentIndex����Ҫ�Ƴ���ҳ�����ڵ�ʱ�����������0��ʼ��
//pageIndex����Ҫ�Ƴ���ҳ����������0��ʼ��
//����ֵ���Ƴ�ҳ��Ľ�������ΪTrue���ʾ�Ƴ��ɹ��������Ƴ�ʧ��
//��ע������Ƴ�ҳ�棬���ҳ���ϵĴ��ڼ��䴰������ӵ�ý��Ҳ�ᱻ�Ƴ���
extern "C" __declspec(dllexport)bool __stdcall NP_RemovePage(HANDLE programHwnd,
															 unsigned short timeSegmentIndex,
															 unsigned short pageIndex);
//�Ƴ�ָ�����ŷ�����ָ��ʱ���
//programHwnd����Ҫ�Ƴ�ʱ��εĲ��ŷ���������
//timeSegmentIndex����Ҫ�Ƴ���ʱ�����������0��ʼ��
//����ֵ���Ƴ�ʱ��εĽ�������ΪTrue���ʾ�Ƴ��ɹ��������Ƴ�ʧ��
//��ע������Ƴ�ʱ��Σ����ʱ��ε�ҳ�桢ҳ���ϵĴ��ڼ��䴰������ӵ�ý��Ҳ�ᱻ�Ƴ���
extern "C" __declspec(dllexport)bool __stdcall NP_RemoveTimeSegment(HANDLE programHwnd,
																	unsigned short timeSegmentIndex);
//����ָ�����ŷ���
//programHwnd����Ҫ����Ĳ��ŷ���������
//����ֵ�����沥�ŷ����Ľ�������ΪTrue���ʾ����ɹ������򱣴�ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_SavePlayProgram(HANDLE programHwnd);
//�رմ򿪵Ĳ��ŷ���
//programHwnd����Ҫ�رյĲ��ŷ���������
//����ֵ���رղ��ŷ����Ľ�������ΪTrue���ʾ�رճɹ�������ر�ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_ClosePlayProgram(HANDLE programHwnd);
//���Ͳ��ŷ������첽��
//cardID����Ҫ���ղ��ŷ������첽��ID
//saveDevice�����ŷ������͵��첽����Ĵ洢�豸��0��ʾ�洢��Flash��1��ʾ�洢��SD����2��ʾ�洢��U��
//playProgramPath����Ҫ���͵Ĳ��ŷ���ȫ·��
//����ֵ�����÷��Ͳ��ŷ����������������ΪTrue���ʾ���óɹ����������ʧ��
//��ע���ӿڷ���True��Ӧ�ó�����ڲ��ŷ���������ɺ��յ�WM_SENDPLAYPROGRAM����Ϣ��
//      ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_SENDPLAYPROGRAM��LPARAM����Ϊ���͵Ľ��
extern "C" __declspec(dllexport)bool __stdcall NP_SendPlayProgram(char* cardID, 
																  unsigned short saveDevice,
																  char* playProgramPath);
//������Ƶ�Ĳ岥���첽��
//cardID����Ҫ���ղ岥�ļ����첽��ID
//fileName����Ҫ�岥����Ƶ�ļ�ȫ·��
//windowSize���岥���ڵĴ�С���������ߣ�
//windowSize���岥���ڵĴ�С���������ߣ�
//playDuration���岥��Ƶ�Ĳ���ʱ��
//����ֵ�����÷��Ͳ岥�ļ�����Ľ�������True���ʾ������óɹ��������������ʧ��
//��ע���ӿڷ���True��Ӧ�ó�����ڲ岥�ļ�������ɺ��յ�WM_SENDINSERTPLAY����Ϣ��
//      ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_SENDINSERTPLAY��LPARAM����Ϊ���͵Ľ��
extern "C" __declspec(dllexport)bool __stdcall NP_SendVideoInsertPlay(char* cardID, 
																	  char* fileName,
																	  NP_SIZE windowSize,
																	  NP_TIMESPAN playDuration,
																	  NP_VIDEOFIEL_INFO propInfo);
//����ͼƬ�Ĳ岥���첽��
//cardID����Ҫ���ղ岥�ļ����첽��ID
//fileName����Ҫ�岥��ͼƬ�ļ�ȫ·��
//windowSize���岥���ڵĴ�С���������ߣ�
//playDuration���岥ͼƬ�Ĳ���ʱ��
//propInfo���岥ͼƬ�Ĳ��Ų���
//����ֵ�����÷��Ͳ岥�ļ�����Ľ�������True���ʾ������óɹ��������������ʧ��
//��ע���ӿڷ���True��Ӧ�ó�����ڲ岥�ļ�������ɺ��յ�WM_SENDINSERTPLAY����Ϣ��
//      ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_SENDINSERTPLAY��LPARAM����Ϊ���͵Ľ��
extern "C" __declspec(dllexport)bool __stdcall NP_SendImageInsertPlay(char* cardID,
																	  char* fileName,
																	  NP_SIZE windowSize,
																	  NP_TIMESPAN playDuration,
																	  NP_IMAGEFILE_INFO propInfo);
//���;�̬�ı���֪ͨ���첽��
//cardID����Ҫ����֪ͨ�ļ����첽��ID
//windowRect��֪ͨ������ʾ�Ĵ������򣨰���λ�úʹ�С��
//playTimes����̬�ı��Ĳ��Ŵ���
//propInfo����̬�ı��Ĳ��Ų���
//����ֵ�����÷���֪ͨ�ļ�����Ľ�������True���ʾ������óɹ��������������ʧ��
//��ע���ӿڷ���True��Ӧ�ó������֪ͨ�ļ�������ɺ��յ�WM_SENDNOTIFICATION����Ϣ��
//      ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_SENDNOTIFICATION��LPARAM����Ϊ���͵Ľ��
extern "C" __declspec(dllexport)bool __stdcall NP_SendStaticTextNotify(char* cardID,
																	   NP_RECTANGLE windowRect,
																	   unsigned int playTimes,
																	   NP_STATICTEXT_INFO propInfo);
//��������Ƶ�֪ͨ���첽��
//cardID����Ҫ����֪ͨ�ļ����첽��ID
//windowRect��֪ͨ������ʾ�Ĵ������򣨰���λ�úʹ�С��
//playTimes������ƵĲ��Ŵ���
//propInfo������ƵĲ��Ų���
//����ֵ�����÷���֪ͨ�ļ�����Ľ�������True���ʾ������óɹ��������������ʧ��
//��ע���ӿڷ���True��Ӧ�ó������֪ͨ�ļ�������ɺ��յ�WM_SENDNOTIFICATION����Ϣ��
//      ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_SENDNOTIFICATION��LPARAM����Ϊ���͵Ľ��
extern "C" __declspec(dllexport)bool __stdcall NP_SendSrollingTextNotify(char* cardID,
																		 NP_RECTANGLE windowRect,
																		 unsigned int playTimes,
																		 NP_SCROLLTEXT_INFO propInfo);
//���͵����ı���֪ͨ���첽��
//cardID����Ҫ����֪ͨ�ļ����첽��ID
//windowRect��֪ͨ������ʾ�Ĵ������򣨰���λ�úʹ�С��
//playTimes�������ı��Ĳ��Ŵ���
//propInfo�������ı��Ĳ��Ų���
//����ֵ�����÷���֪ͨ�ļ�����Ľ�������True���ʾ������óɹ��������������ʧ��
//��ע���ӿڷ���True��Ӧ�ó������֪ͨ�ļ�������ɺ��յ�WM_SENDNOTIFICATION����Ϣ��
//      ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_SENDNOTIFICATION��LPARAM����Ϊ���͵Ľ��
extern "C" __declspec(dllexport)bool __stdcall NP_SendSingleLineTextNotify(char* cardID, 
																		   NP_RECTANGLE windowRect,
																		   unsigned int playTimes,
																		   NP_SINGLELINETEXT_INFO propInfo);
//�����첽���Ĳ���
//cardID����Ҫ���Ƶ��첽��ID
//ctrlMode���������ͣ�0��ʾ���ţ�Ϊ1��ʾ��ͣ��Ϊ2��ʾֹͣ
//����ֵ�����Ʋ��ŵĽ�������True���ʾ���Ƴɹ����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_ControlCardPlay(char* cardID, unsigned short ctrlMode);
//��ȡ�첽��������־
//cardID����Ҫ��ȡ��־���첽��ID
//getDate����Ҫ��ȡ����־������
//����ֵ�����û�ȡ��־����Ľ�������True���ʾ���óɹ����������ʧ��
//��ע��1.�ӿڷ���True��Ӧ�ó��������־��ȡ�ɹ����յ�WM_GETPLAYLOGOK����Ϣ��
//        ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_GETPLAYLOGOK��
//        LPARAM����Ϊ��ȡ������־�洢��ȫ·����
//      2.�ӿڷ���True��Ӧ�ó��������־��ȡʧ�ܺ��յ�WM_GETPLAYLOGERROR����Ϣ��
//        ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_GETPLAYLOGERROR��
//        LPARAM����Ϊ��ȡ��־ʧ�ܵ�ԭ��
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardPlayLog(char* cardID, NP_DATE getDate);
//��ָ����־
//logFileName����Ҫ�򿪵���־��ȫ·��
//����ֵ���򿪵���־���������������־ʧ�ܣ��򷵻�NULL
extern "C" __declspec(dllexport)HANDLE __stdcall NP_OpenPlayLogFile(char* logFileName);
//��ȡָ����־����Ϣ�ĸ���
//logHandle����Ҫ��ȡ��Ϣ��������־������
//����ֵ����ȡ������Ϣ�����������ȡʧ����Ϊ0
extern "C" __declspec(dllexport)int __stdcall NP_GetPlayLogItemCount(HANDLE logHandle);
///��ȡָ����־��ָ������Ϣ
//logHandle����Ҫ��ȡ��Ϣ����־������
//itemIndex����Ҫ��ȡ����Ϣ����Ϣ�б��е���������0��ʼ��
//logInfo����ȡ������־��Ϣָ��
//����ֵ����ȡ��־��Ϣ�Ľ���������ȡ�ɹ����򷵻�True����logInfoΪ��ȡ����Ϣ��ָ�룬���򷵻�False��
//��ע�����ָ����־��ָ����������Ϣ�����ڣ��򷵻�False
extern "C" __declspec(dllexport)bool __stdcall NP_GetPlayLogItemInfo(HANDLE logHandle, 
																	 int itemIndex,
																	 NP_PLAYLOG_ITEM* logInfo);
//�رմ򿪵���־�ļ�
//logHandle����Ҫ�رյ���־������
//����ֵ���ر���־�Ľ�����������True���ʾ�رճɹ�������ر�ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_ClosePlayLogFile(HANDLE logHandle);


//�����첽����ϵͳʱ��
//cardID����Ҫ����ʱ����첽��ID
//time����Ҫ���õ�ʱ��
//����ֵ�������첽��ʱ��Ľ�����������True���ʾ���óɹ�����������ʧ��
//��ע���ýӿڵ�ִ����Ҫ�ϳ�ʱ�䣨1-2�룩��
extern "C" __declspec(dllexport)bool __stdcall NP_SetCardSystemTime(char* cardID, SYSTEMTIME time);
//��ȡ�첽����ϵͳʱ��
//cardID����Ҫ��ȡʱ����첽��ID
//time����ȡ�����첽��ʱ��
//����ֵ����ȡ�첽��ʱ��Ľ�����������True���ʾ��ȡ�ɹ��������ȡʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardSystemTime(char* cardID, SYSTEMTIME* time);

//�����첽��������ֵ
//cardID����Ҫ��������ֵ���첽��ID
//brightValue����Ҫ���õ�����ֵ(0-255)
//����ֵ�������첽������ֵ�Ľ�����������True���ʾ���óɹ�����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_SetCardBrightValue(char* cardID, BYTE brightValue);
//��ȡ�첽��������ֵ
//cardID����Ҫ��ȡ����ֵ���첽��ID
//brightValue����ȡ�����첽������ֵ(0-255)
//����ֵ�������ȡ�ɹ����򷵻�True����brightValueΪ��ȡ��������ֵ�����򷵻�False��
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardBrightValue(char* cardID, BYTE* brightValue);

//�����첽��������ģʽ
//cardID����Ҫ��������ģʽ���첽��ID
//brightMode����Ҫ���õ�����ģʽ��0��ʾ�Զ����ȣ�1��ʾ�ֶ����ȣ�2��ʾ��ʱ
//����ֵ�������첽������ģʽ�Ľ�����������True���ʾ���óɹ�����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_SetCardBrightMode(char* cardID, unsigned char brightMode);
//��ȡ�첽��������ģʽ
//cardID����Ҫ��ȡ����ģʽ���첽��ID
//brightMode����ȡ�����첽������ģʽ
//����ֵ�������ȡ�ɹ����򷵻�True����brightModeΪ��ȡ��������ģʽ
//       ��0��ʾ�Զ����ȣ�1��ʾ�ֶ����ȣ�2��ʾ��ʱ���ȣ������򷵻�False��
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardBrightMode(char* cardID, unsigned char* brightMode);

//�����첽������Ļ״̬
//cardID����Ҫ������Ļ״̬���첽��ID
//screenStatus����Ҫ���õ���Ļ״̬��0��ʾ������ʾ��1��ʾ������
//����ֵ�������첽����Ļ״̬�Ľ�����������True���ʾ���óɹ�����������ʧ��
extern "C" __declspec(dllexport)bool __stdcall NP_SetCardSreenStatus(char* cardID, unsigned char screenStatus);

//��ȡ��ʾ���е��������
//cardID����Ҫ��ȡ����������첽��ID
//cabinetCnt����ȡ�����������
//����ֵ����ȡ��������Ľ���������ȡ�ɹ����򷵻�True����cabinetCntΪ��ȡ����������������򷵻�False��
extern "C" __declspec(dllexport)bool __stdcall NP_GetCabinetCount(char* cardID, unsigned short* cabinetCnt);
//��ʼ���
//cardID����Ҫ�����첽��ID
//cabinetIndex����Ҫ����������������0��ʼ��
//detectPointPara�����ʱ��Ҫ�Ĳ���
//����ֵ�����͵������Ľ�����������True�����ʾ���͵������ɹ��������ʾ���͵������ʧ�ܡ�
//��ע��1.�ӿڷ���True��Ӧ�ó�����ڵ��ɹ����յ�WM_DETECTPOINTOK����Ϣ��
//        ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_DETECTPOINTOK��
//        LPARAM����Ϊ���������ָ�롣
//      2.�ӿڷ���True��Ӧ�ó�����ڵ��ʧ�ܺ��յ�WM_DETECTPOINTFAILED����Ϣ��
//        ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_DETECTPOINTFAILED��
//        LPARAM����Ϊ���ʧ�ܵ�ԭ��
extern "C" __declspec(dllexport)bool __stdcall NP_BeginDetectPoint(char* cardID, 
																   unsigned short cabinetIndex, 
																   NP_DETECTPOINT_PARA detectPointPara);
//�����첽��
//cardID����Ҫ�������첽��ID
//����ֵ�������ն˵Ľ�����������True�����ʾ�����ɹ��������ʾ����ʧ�ܡ�
extern "C" __declspec(dllexport)bool __stdcall NP_RestartCard(char* cardID);
//ˢ���첽���ļ����Ϣ
//cardID����Ҫˢ�¼����Ϣ���첽��ID
//monitorInfoSavePath:ˢ�µ��ļ����Ϣ��PC���ϵĴ洢·�����洢�ļ��У���
//����ֵ������ˢ�¼����Ϣ����Ľ�����������True�����ʾ��������ɹ��������ʾ����ˢ�¼����Ϣ����ʧ�ܡ�
//��ע��1.�ӿڷ���True��Ӧ�ó������ˢ�¼�سɹ����յ�WM_REFRESHMONITOROK����Ϣ��
//        ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_REFRESHMONITOROK��
//        LPARAM����Ϊ�����Ϣ�洢�ļ�ȫ·����ָ�롣
//      2.�ӿڷ���True��Ӧ�ó������ˢ�¼��ʧ�ܺ��յ�WM_REFRESHMONITORFAILED����Ϣ��
//        ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_REFRESHMONITORFAILED��
//        LPARAM����Ϊˢ�¼������ʧ�ܵ�ԭ��
extern "C" __declspec(dllexport)bool __stdcall NP_RefreshCardMonitorInfo(char* cardID, char* monitorInfoSavePath);
//�򿪼����Ϣ�洢�ļ�
//monitoInfoFileName�������Ϣ�洢�ļ���ȫ·��
//����ֵ���򿪵ļ����Ϣ�ļ������������ļ�ʧ�ܣ��򷵻�NULL
extern "C" __declspec(dllexport)HANDLE __stdcall NP_OpenMonitorInfoFile(char* monitoInfoFileName);
//��ȡָ�������Ϣ�洢�ļ��е���Ϣ����
//hMonitorFile����Ҫ��ȡ��Ϣ�����ļ����Ϣ�ļ�������
//����ֵ����ȡ������Ϣ�����������ȡʧ����Ϊ0
extern "C" __declspec(dllexport)int __stdcall NP_GetInfoCntFromFile(HANDLE hMonitorFile);
///��ȡָ�������Ϣ�洢�ļ���ָ���ļ����Ϣ
//hMonitorFile����Ҫ��ȡ��Ϣ�ļ����Ϣ�ļ�������
//infoIndex����Ҫ��ȡ�ļ����Ϣ����Ϣ�б��е���������0��ʼ��
//monitorInfo����ȡ���ļ����Ϣָ��
//����ֵ����ȡ�����Ϣ�Ľ���������ȡ�ɹ����򷵻�True����monitorInfoΪ��ȡ����Ϣ��ָ�룬���򷵻�False��
//��ע�����ָ����������Ϣ�����ڣ��򷵻�False
extern "C" __declspec(dllexport)bool __stdcall NP_GetMonitorInfoFromFile(HANDLE hMonitorFile, 
																		 int infoIndex,
																		 NP_MONITOR_INFO* monitorInfo);
//�رռ����Ϣ�洢�ļ�
//hMonitorFile����Ҫ�رյļ����Ϣ�洢�ļ�������
//����ֵ���رռ����Ϣ�洢�ļ��Ľ��������رճɹ����򷵻�True�����򷵻�False��
extern "C" __declspec(dllexport)bool __stdcall NP_CloseMonitoInfoFile(HANDLE hMonitorFile);
//�����첽��ͨѶ״̬��⹦���Ƿ���
//cardID�� ��Ҫ����ͨѶ״̬��⹦���Ƿ������첽��ID
//isEanble���Ƿ���ͨѶ״̬��⹦�ܣ����������ΪTrue������ΪFalse
//����ֵ������ͨѶ״̬��⹦���Ƿ����Ľ����������óɹ����򷵻�True�����򷵻�False��
//��ע���첽��ͨѶ״̬��⹦�ܿ���������첽���Ͽ������˵����ӣ�����ʾ��������
//      ����첽���ָ������˵����ӣ�����ʾ���ָ�������ʾ��
extern "C" __declspec(dllexport)bool __stdcall NP_SetConnectDetectEnable(char* cardID, bool isEanble);
//��ȡ�첽��ͨѶ״̬��⹦���Ƿ���
//cardID����Ҫ��ȡͨѶ״̬��⹦���Ƿ������첽��ID
//isEanble����ȡ����ͨѶ״̬��⹦���Ƿ����ı�־
//����ֵ����ȡͨѶ״̬��⹦���Ƿ����Ľ���������ȡ�ɹ����򷵻�True����isEanbleΪ�Ƿ��������򷵻�False��
extern "C" __declspec(dllexport)bool __stdcall NP_GetConnectDetectEnable(char* cardID, bool* isEanble);
//�������ڻ㱨���״̬���ܵĲ���
//cardID�� ��Ҫ�������ڻ㱨���״̬���첽��ID
//isEanble���Ƿ������ڻ㱨���״̬�����������ΪTrue������ΪFalse
//cycleValue�����ڻ㱨���״̬�����ڣ���λΪ�룬��СֵΪ60�룬Ĭ��60��
//����ֵ���������ڻ㱨���״̬�����Ƿ����Ľ����������óɹ����򷵻�True�����򷵻�False��
//��ע��1.�ӿڷ���True��Ӧ�ó������ˢ�¼�سɹ����յ�WM_REFRESHMONITOROK����Ϣ��
//        ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_REFRESHMONITOROK��
//        LPARAM����Ϊ�����Ϣ�洢�ļ�ȫ·����ָ�롣
//      2.�ӿڷ���True��Ӧ�ó������ˢ�¼��ʧ�ܺ��յ�WM_REFRESHMONITORFAILED����Ϣ��
//        ��ϢIDΪ��ʼ��ʱ���õ���ϢID��WPARAM����ΪWM_REFRESHMONITORFAILED��
//        LPARAM����Ϊˢ�¼������ʧ�ܵ�ԭ��
extern "C" __declspec(dllexport)bool __stdcall NP_SetCycleMontorConfig(char* cardID, bool isEanble, unsigned short cycleValue);
//��ȡ���ڻ㱨���״̬���ܵĲ���
//cardID����Ҫ��ȡ���ڻ㱨���״̬�����Ƿ������첽��ID
//isEanble����ȡ�������ڻ㱨���״̬�Ƿ����ı�־
//cycleValue����ȡ�������ڻ㱨���״̬������
//����ֵ����ȡ���ڻ㱨���״̬���ܲ����Ľ����������óɹ����򷵻�True�����򷵻�False��
extern "C" __declspec(dllexport)bool __stdcall NP_GetCycleMontorConfig(char* cardID, bool* isEanble, unsigned short* cycleValue);
//��ȡ�첽���Ľ�ͼ
//cardID����Ҫ��ȡ��ͼ���첽��ID
//picFileName: ��ȡ�����첽����ͼ��PC���ϵĴ洢·�����ļ�ȫ·������׺Ϊ.jpg��
//����ֵ����ȡ�첽����ͼ�Ľ���������ȡ�ɹ����򷵻�True����picFileNameΪ��ȡ����ͼƬ�ļ�·�������򷵻�False��
extern "C" __declspec(dllexport)bool __stdcall NP_GetCardScreenShotPicture(char* cardID, char* picFileName);

//�����첽�������Դ��״̬
//cardID����Ҫ���õ�Դ״̬���첽��ID
//powerState�����õı����Դ״̬��0���رգ� 1��������
//����ֵ�������첽����Դ״̬�Ľ�����������True���ʾ���óɹ��������ʾʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_SetOnBoardPowerState(char* cardID, unsigned char powerState);
//��ȡ�첽�������Դ��״̬
//cardID����Ҫ��ȡ��Դ״̬���첽��ID
//powerState����ȡ���ı����Դ״̬��0���رգ� 1��������
//����ֵ����ȡ�첽����Դ״̬�Ľ�����������True���ʾ��ȡ�ɹ�����powerStateΪ��ȡ���ĵ�Դ״̬�������ʾ��ȡʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_GetOnBoardPowerState(char* cardID, unsigned char* powerState);
//���ñ����Դ�Զ����Ƶ���Ϣ���������Ҫ�Զ����ƣ���Ҫ���Զ�������Ϣ����Ϊ�գ�
//cardID����Ҫ�����Զ�������Ϣ���첽��ID
//autoCtrlInfo�����õı����Դ�Զ�������Ϣ
//����ֵ�����ñ����Դ�Զ�������Ϣ�Ľ�����������True���ʾ���óɹ��������ʾʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_SetOnBoardPowerAutoInfo(char* cardID, 
																		 NP_BDPOWER_AUTOCTRL_INFO autoCtrlInfo);
//��ȡ�����Դ�Զ����Ƶ���Ϣ
//cardID����Ҫ��ȡ��Դ�Զ�������Ϣ���첽��ID
//autoCtrlInfo����ȡ���ı����Դ�Զ�������Ϣ
//����ֵ����ȡ�����Դ�Զ�������Ϣ�Ľ��
//        �������True���ʾ��ȡ�ɹ�����autoCtrlInfoΪ��ȡ�ı����Դ�Զ�������Ϣ;
//        �������False���ʾ��ȡʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_GetOnBoardPowerAutoInfo(char* cardID,
																		 NP_BDPOWER_AUTOCTRL_INFO* autoCtrlInfo);

//�����������첽���ϵĶ๦�ܿ��ĵ�Դ����ģʽ
//cardID����Ҫ���õ�Դ����ģʽ�Ķ๦�ܿ����ӵ��첽��ID
//funCardIndex����Ҫ���õ�Դ����ģʽ�Ķ๦�ܿ���������0��ʼ��
//adjustMode�����õĵ�Դ����ģʽ��0���ֶ��� 1���Զ���
//����ֵ�����ö๦�ܿ���Դ����ģʽ�Ľ�����������True���ʾ���óɹ��������ʾʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_SetFunPowerAdjustMode(char* cardID, 
																	   unsigned short funCardIndex,
																	   unsigned char adjustMode);
//��ȡ�������첽���ϵĶ๦�ܿ��ĵ�Դ����ģʽ
//cardID����Ҫ��ȡ��Դ����ģʽ�Ķ๦�ܿ����ӵ��첽��ID
//funCardIndex����Ҫ��ȡ��Դ����ģʽ�Ķ๦�ܿ���������0��ʼ��
//adjustMode����ȡ���ĵ�Դ����ģʽ��0���ֶ��� 1���Զ���
//����ֵ����ȡ�๦�ܿ���Դ����ģʽ�Ľ��
//        �������True���ʾ��ȡ�ɹ�����adjustModeΪ��ȡ���ĵ�Դ����ģʽ;
//        �������False���ʾ��ȡʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_GetFunPowerAdjustMode(char* cardID, 
																	   unsigned short funCardIndex,
																	   unsigned char* adjustMode);
//�����������첽���ϵĶ๦�ܿ��ĵ�Դ״̬
//cardID����Ҫ���õ�Դ״̬�Ķ๦�ܿ����ӵ��첽��ID
//funCardIndex����Ҫ���õ�Դ״̬�Ķ๦�ܿ���������0��ʼ��
//powerIndex����Ҫ����״̬�ĵ�Դ����(0~7)
//powerState�����õĵ�Դ״̬��0���رգ�1��������
//����ֵ�����ö๦�ܿ���Դ״̬�Ľ�����������True���ʾ���óɹ�������ʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_SetFunPowerState(char* cardID, 
																  unsigned short funCardIndex,
																  BYTE powerIndex,
																  unsigned char powerState);
//��ȡ�������첽���ϵĶ๦�ܿ��ĵ�Դ״̬
//cardID����Ҫ��ȡ��Դ״̬�Ķ๦�ܿ����ӵ��첽��ID
//funCardIndex����Ҫ��ȡ��Դ״̬�Ķ๦�ܿ���������0��ʼ��
//powerIndex����Ҫ��ȡ״̬�ĵ�Դ����(0~7)
//powerState����ȡ���ĵ�Դ״̬
//����ֵ����ȡ�๦�ܿ���Դ״̬�Ľ��
//        �������True���ʾ��ȡ�ɹ�����powerStateΪ��ȡ���ĵ�Դ״̬��Ϣ��
//        �������False���ʾ��ȡʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_GetFunPowerState(char* cardID, 
																  unsigned short funCardIndex,
																  BYTE powerIndex,
																  unsigned char* powerState);
//�����������첽���ϵĶ๦�ܿ��ĵ�Դ�Զ�������Ϣ
//cardID����Ҫ���õ�Դ�Զ�������Ϣ�Ķ๦�ܿ����ӵ��첽��ID
//funCardIndex����Ҫ�����õ�Դ�Զ�������Ϣ�Ķ๦�ܿ���������0��ʼ��
//autoCtrlInfo�����õĵ�Դ�Զ�������Ϣ
//����ֵ�����ö๦�ܿ���Դ�Զ�������Ϣ�Ľ�����������True���ʾ���óɹ�������ʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_SetFunPowerAutoInfo(char* cardID, 
																	 unsigned short funCardIndex,
																	 NP_FUNPOWER_AUTOCTRL_INFO autoCtrlInfo);
//��ȡ�������첽���ϵĶ๦�ܿ��ĵ�Դ�Զ�������Ϣ
//cardID����Ҫ��ȡ��Դ�Զ�������Ϣ�Ķ๦�ܿ����ӵ��첽��ID
//funCardIndex����Ҫ��ȡ��Դ�Զ�������Ϣ�Ķ๦�ܿ���������0��ʼ��
//autoCtrlInfo����ȡ���ĵ�Դ�Զ�������Ϣ
//����ֵ����ȡ�๦�ܿ���Դ�Զ�������Ϣ�Ľ��
//        �������True���ʾ��ȡ�ɹ�����autoCtrlInfoΪ��ȡ���ĵ�Դ�Զ�������Ϣ��
//        �������False���ʾ��ȡʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_GetFunPowerAutoInfo(char* cardID, 
																	 unsigned short funCardIndex,
																	 NP_FUNPOWER_AUTOCTRL_INFO* autoCtrlInfo);

//�����첽��ͨѶ״̬���Ĳ���
//cardID����Ҫ����ͨѶ״̬���������첽��ID
//detectPara����Ҫ���õ�ͨѶ״̬������
//����ֵ������ͨѶ״̬�������Ľ�����������True���ʾ���óɹ�������ʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_SetConnectDetectPara(char* cardID,
																	  NP_CONNECTDETECT_PARA detectPara);
//��ȡ�첽��ͨѶ״̬���Ĳ���
//cardID����Ҫ��ȡͨѶ״̬���������첽��ID
//detectPara����ȡ����ͨѶ״̬������
//����ֵ����ȡͨѶ״̬�������Ľ��
//        �������True���ʾ��ȡ�ɹ�����detectParaΪ��ȡ����ͨѶ״̬��������
//        �������False���ʾ��ȡʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_GetConnectDetectPara(char* cardID, 
																	  NP_CONNECTDETECT_PARA* detectPara);

//��ȡ�첽����ȫ�ּ����Ϣ
//cardID����Ҫ��ȡ�첽��ȫ�ּ����Ϣ���첽��ID
//monitorInfo����ȡ����ȫ�ּ����Ϣ
//����ֵ����ȡȫ�ּ����Ϣ�Ľ��
//        �������True���ʾ��ȡ�ɹ�����monitorInfoΪ��ȡ���ļ����Ϣ
//        �������False���ʾ��ȡʧ�ܡ�
//��ע��ֻ��ʹ�ýӿ�NP_SetSelfMonitorPara�����첽�����Լ칦�ܣ�������ÿ�λ�ȡȫ�ּ����Ϣʱ��õ��µ���Ϣ��
extern "C" __declspec(dllexport)bool _stdcall NP_GetGlobalMonitorInfo(char* cardID, 
																	  NP_GLOBALMONITOR_INFO* monitorInfo);
//����Ӳ������
//cardID����Ҫ����Ӳ���������첽��ID
//����ֵ������Ӳ�������Ľ�����������True���ʾ����ɹ�������ʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_SaveHardwareParameter(char* cardID);
//��ȡ�����ļ�������Ϣ
//cardID����Ҫ��ȡ�ļ�������Ϣ���첽��ID
//playFileType����Ҫ��ȡ������Ϣ�Ĳ����ļ����ͣ�0��ʾ���ŷ�����1��ʾ�岥�ļ���2��ʾ֪ͨ
//fileSendInfo����ȡ�����ļ�������Ϣ
//����ֵ����ȡ�ļ�������Ϣ�Ľ��
//        �������True���ʾ��ȡ�ɹ�����fileSendInfoΪ��ȡ�����ļ�������Ϣ
//        �������False���ʾ��ȡʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_GetPlayFileSendInfo(char* cardID, 
																	 unsigned char playFileType,
																	 NP_SENDPLAYFILE_INFO* fileSendInfo);

//�����첽�����Լ����
//cardID����Ҫ�����Լ�������첽��ID
//selfCtrlPara����Ҫ���õ��Լ����
//����ֵ�������Լ�����Ľ�����������True���ʾ���óɹ�������ʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_SetSelfMonitorCtrlPara(char* cardID, 
																		NP_SELFMONITORCTRL_PARA selfCtrlPara);
//��ȡ�첽�����Լ����
//cardID����Ҫ��ȡ�Լ�������첽��ID
//selfCtrlPara����ȡ�����Լ����
//����ֵ����ȡ�Լ�����Ľ��
//        �������True���ʾ��ȡ�ɹ�����selfCtrlParaΪ��ȡ�����Լ����
//        �������False���ʾ��ȡʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_GetSelfMonitorCtrlPara(char* cardID, 
																		NP_SELFMONITORCTRL_PARA* selfCtrlPara);
//���ö�ʱ���ȵ�����Ϣ
//cardID����Ҫ���ö�ʱ���ȵ�����Ϣ���첽��ID
//adjustInfo����Ҫ���õĶ�ʱ���ȵ�����Ϣ
//����ֵ�����ö�ʱ���ȵ�����Ϣ�Ľ�����������True���ʾ���óɹ�������ʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_SetSchedualBrightInfo(char* cardID, 
																	   NP_SCHEDUALBRIGHT_INFO adjustInfo);
//��ȡ��ʱ���ȵ�����Ϣ
//cardID����Ҫ��ȡ��ʱ���ȵ�����Ϣ���첽��ID
//adjustInfo����ȡ���Ķ�ʱ���ȵ�����Ϣ
//����ֵ����ȡ��ʱ���ȵ�����Ϣ�Ľ��
//        �������True���ʾ��ȡ�ɹ�����adjustInfoΪ��ȡ���Ķ�ʱ���ȵ�����Ϣ
//        �������False���ʾ��ȡʧ�ܡ�
extern "C" __declspec(dllexport)bool _stdcall NP_GetSchedualBrightInfo(char* cardID, 
																	   NP_SCHEDUALBRIGHT_INFO* adjustInfo);

//ɾ���첽����ǰ�洢�豸��ý��
//cardID����Ҫɾ��ý����첽��ID
//deleteType����Ҫɾ�������ͣ�0��ʾ��ǰ�洢�豸������ý�壬1��ʾ��ǰ�洢�豸�Ĺ���ý��
//����ֵ��ɾ��ý��Ľ�����������True���ʾɾ���ɹ�������ʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_DeleteMedia(char* cardID, unsigned char deleteType);
//��ȡ�첽���Ĵ洢�豸��Ϣ
//cardID����Ҫ��ȡ�洢�豸��Ϣ���첽��ID
//storageDeviceInfo����ȡ���Ĵ洢�豸��Ϣ
//����ֵ����ȡ�洢�豸��Ϣ�Ľ��
//        �������True���ʾ��ȡ�ɹ�����storageDeviceInfoΪ��ȡ���Ĵ洢�豸��Ϣ
//        �������False���ʾ��ȡʧ��
extern "C" __declspec(dllexport)bool _stdcall NP_GetStorageDeviceInfo(char* cardID, 
																	  NP_STORAGEDEVICE_INFO* storageDeviceInfo);