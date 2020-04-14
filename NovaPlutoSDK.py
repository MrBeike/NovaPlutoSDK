from ctypes import *
from ctypes.wintypes import HWND
from CommonProperty import *

try:
    dll = windll.LoadLibrary('NovaPlutoManager.dll')
except Exception as e:
    print(e)

# 说明：初始化系统
def NP_Initialize(appHwnd,recvMsgID,serverIP,serverPort,recvSavePath):
    '''
    :param appHwnd: HWND 应用程序的窗口句柄
    :param recvMsgID: int 接收到异步通知的消息时主消息ID
    :param serverIP: string 编辑计算机 IP 
    :param serverPort: int 编辑计算机网络交互端口
    :param recvSavePath: string 应用程序运行过程中获取到异步卡的信息存储路径
    '''
    initialize = dll.NP_Initialize
    initialize.argtypes = (HWND,c_int,c_char_p,c_int,c_char_p)
    initialize.restype = c_bool
    return initialize(appHwnd,recvMsgID,serverIP,serverPort,recvSavePath)

# 说明：反初始化并释放资源
def NP_Unitialize():
    uninitialize = dll.NP_UnInitialize
    return uninitialize

# 说明：连接局域网的异步卡
def NP_ConnectCardofLocalNet(cardIP,isSync,cardInfo):
    '''
    :param cardIP: string 连接的异步卡ID
    :param isSync: bool 消息是否同步显示
    :param cardInfo: NP_CARD_INFO 储存消息的消息体
    '''
    connect_card_of_localnet = dll.NP_ConnectCardofLocalNet
    connect_card_of_localnet.argtypes = (c_char_p,c_bool,POINTER(NP_CARD_INFO))
    connect_card_of_localnet.restype = c_bool
    return connect_card_of_localnet(cardIP,isSync,cardInfo)

# 说明：获取异步卡的信息
def NP_GetCardInfo(cardID,isSync,cardInfo):
    '''
    :param cardID: string 需要获取信息的异步卡ID
    :param isSync: bool 消息是否同步显示
    :param cardInfo: NP_CARD_INFO 储存消息的消息体
    '''
    get_card_info = dll.NP_GetCardInfo
    get_card_info.argtypes = (c_char_p,c_bool,POINTER(NP_CARD_INFO))
    get_card_info.restype = c_bool
    return get_card_info(cardID,isSync,cardInfo)

# 说明：创建播放方案
# 备注：播放方案的后缀必须为.plym
def NP_CreatePlayProgram(fileName,screenSize):
    '''
    :param fileName: string 播放列表的全路径
    :param screenSize: NP_SIZE 屏幕尺寸对象(Width,Height) 
    '''
    create_play_program = dll.NP_CreatePlayProgram
    create_play_program.argtypes = (c_char_p,NP_SIZE)
    # 0表示失败，否则表示成功并返回文件句柄
    create_play_program.restype = HWND
    return create_play_program(fileName,screenSize)

# 说明：打开指定路径的播放方案
def NP_OpenPlayProgram(fileName):
    '''
    :param fileName: string 需要打开的播放方案文件全路径
    '''
    open_play_program = dll.NP_OpenPlayProgram
    open_play_program.argtypes = c_char_p
    # 0表示失败，否则表示成功并返回文件句柄
    open_play_program.restype = HWND
    return open_play_program(fileName)

# 说明：保存指定播放方案
def NP_SavePlayProgram(programHwnd):
    '''
    :param programHwnd: HWND 需要保存的播放方案对象句柄
    '''
    save_play_program = dll.NP_SavePlayProgram
    save_play_program.argtypes = HWND
    save_play_program.restype = c_bool
    return save_play_program(programHwnd)

# 说明：关闭打开的播放方案
def NP_ClosePlayProgram(programHwnd):
    '''
    :param programHwnd: HWND 需要关闭的播放方案对象句柄
    '''
    close_play_program = dll.NP_ClosePlayProgram
    close_play_program.argtypes = HWND
    close_play_program.restype = c_bool
    return close_play_program(programHwnd)

# 说明：发送播放方案到异步卡
# 备注：接口返回True后，应用程序会在播放方案发送完成后收到WM_SENDPLAYPROGRAM的消息 
def NP_SendPlayProgram(cardID,saveDevice,playProgramPath):
    '''
    :param cardID: string 需要接收播放方案的异步卡ID
    :param saveDevice: int 播放方案在异步卡中的储存位置（0表示Flash,1表示SD卡,2表示U盘）
    :param playProgramPath: string 播放方案全路径
    '''
    send_play_program = dll.NP_SendPlayProgram
    send_play_program.argtypes = (c_char_p,c_ushort,c_char_p)
    send_play_program.restype = c_bool
    return send_play_program(cardID,saveDevice,playProgramPath)

# 说明：控制异步卡的播放
def NP_ControlCardPlay(cardID,ctrlMode):
    '''
    :param cardID: string 需要控制的异步卡ID
    :param ctrlMode: int 控制类型，0表示播放，1表示暂停，2表示停止
    '''
    control_card_play = dll.NP_ControlCardPlay
    control_card_play.argtypes = (c_char_p,c_ushort)
    control_card_play.restype = c_bool
    return control_card_play(cardID,ctrlMode)

# 说明：设置异步卡的系统时间
# 备注：该接口的执行需要较长时间（1-2秒）
def NP_SetCardSystemTime(cardID,time):
    '''
    :param cardID: string 需要设置时间的异步卡ID
    :param time: SYSTEMTIME 需要设置的时间
    '''
    set_card_system_time = dll.NP_SetCardSystemTime
    set_card_system_time.argtypes = (c_char_p,SYSTEMTIME)
    set_card_system_time.restype = c_bool
    return set_card_system_time(cardID,time)

# 说明：获取异步卡的系统时间
def NP_GetCardSystemTime(cardId,time):
    '''
    :param cardId: string 需要获取时间的异步卡ID
    :param time: SYSTEMTIME 获取到的异步卡时间
    '''
    get_card_system_time = dll.NP_GetCardSystemTime
    get_card_system_time.argtypes = (c_char_p,SYSTEMTIME)
    get_card_system_time.restype = c_bool
    return get_card_system_time(cardID,time)

# 说明：设置异步卡的亮度值
def NP_SetCardBrightValue(cardID,brightValue):
    '''
    :param cardID: string 需要设置亮度值的异步卡ID
    :param brightValue: int 需要设置的亮度值(0-255)
    '''
    set_card_bright_value = dll.NP_SetCardBrightValue
    set_card_bright_value.argtypes = (c_char_p,c_byte)
    set_card_bright_value.restype = c_bool
    return set_card_bright_value(cardID,brightValue)

# 说明：获取异步卡的亮度值
def NP_GetCardBrightValue(cardID,brightValue):
    '''
    :param cardID: string 需要获取亮度值的异步卡ID
    :param brightValue: int 获取到的异步卡亮度值(0-255)
    '''
    get_card_bright_value = dll.NP_GetCardBrightValue
    get_card_bright_value.argtypes = (c_char_p,POINTER(c_byte))
    get_card_bright_value.restype = c_bool
    return get_card_bright_value(cardID,brightValue)

# 说明：重启异步卡
def NP_RestartCard(cardID):
    '''
    :param cardID: sting 需要重启的异步卡ID
    '''
    restart_card = dll.NP_RestartCard
    restart_card.argtypes = c_char_p
    restart_card.restype = c_bool
    return restart_card(cardID)

# 说明：设置异步卡通讯状态检测功能是否开启
def NP_SetConnectDetectEnable(cardID,isEnable):
    '''
    :param cardID: string 需要设置通讯状态监测功能是否开启的异步卡ID
    :param isEnable: bool 是否开启通讯状态监测功能(True,False)
    '''
    set_connect_detect_enable = dll.NP_SetConnectDetectEnable
    set_connect_detect_enable.argtypes = (c_char_p,c_bool)
    set_connect_detect_enable.restype = c_bool
    return set_connect_detect_enable(cardID,isEnable)

# 说明：获取异步卡通讯状态检测功能是否开启
def NP_GetConnectDetectEnable(cardID,isEnable):
    '''
    :param cardID: string 需要获取通讯状态监测功能是否开启的异步卡ID
    :param isEnable: bool 获取到的通讯状态监测功能是否开启的标志(True,False)
    '''
    get_connect_detect_enable = dll.NP_GetConnectDetectEnable
    get_connect_detect_enable.argtypes = (c_char_p,POINTER(c_bool))
    get_connect_detect_enable.restype = c_bool
    return get_connect_detect_enable(cardID,isEnable)

# 说明：获取异步卡的截图
# 备注：截图文件的后缀必须为.jpg
def NP_GetCardScreenShotPicture(cardID,picFileName):
    '''
    :param cardID: string 需要获取截图的异步卡ID
    :param picFileName: string 获取到的截图在PC上的储存全路径(jpg)
    '''
    get_card_screen_shot_picture = dll.NP_GetCardScreenShotPicture
    get_card_screen_shot_picture.argtypes = (c_char_p,c_char_p)
    get_card_screen_shot_picture.restype = c_bool
    return get_card_screen_shot_picture(cardID,picFileName)

# 说明：设置异步卡本板电源的状态
def NP_SetOnBoardPowerState(cardID,powerState):
    '''
    :param cardID: string 需要设置电源状态的异步卡ID
    :param powerState: int 设置的电源状态,0表示关闭，1表示开启
    '''
    set_on_board_power_state = dll.NP_SetOnBoardPowerState
    set_on_board_power_state.argtypes = (c_char_p,c_ubyte)
    set_on_board_power_state.restype = c_bool
    return set_on_board_power_state(cardID,powerState)

# 说明：获取异步卡本板电源的状态
def NP_GetOnBoardPowerState(cardID,powerState):
    '''
    :param cardID: string 需要获取电源状态的异步卡ID
    :param powerState: int 获取到的电源状态，0表示关闭，1表示开启
    '''
    get_on_board_power_state = dll.NP_GetOnBoardPowerState
    get_on_board_power_state.argtypes = (c_char_p,POINTER(c_ubyte))
    get_on_board_power_state.restype = c_bool
    return get_on_board_power_state(cardID,powerState)

# 说明：获取异步卡本板电源的状态
def NP_GetStorageDeviceInfo(cardID,storageDeviceInfo):
    '''
    :param cardID: string 需要获取存储设备信息的异步卡ID
    :param storageDeviceInfo: NP_STORAGEDEVICE_INFO 获取到的存储设备信息
    '''
    get_storage_device_info = dll.NP_GetStorageDeviceInfo
    get_storage_device_info.argtypes = (c_char_p,POINTER(NP_STORAGEDEVICE_INFO))
    get_storage_device_info.restype = c_bool
    return get_storage_device_info(cardID,storageDeviceInfo)