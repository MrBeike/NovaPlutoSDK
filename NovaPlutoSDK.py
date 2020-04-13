from ctypes import *
from ctypes.wintypes import HWND
from CommonProperty import *

try:
    dll = windll.LoadLibrary('NovaPlutoManager.dll')
except Exception as e:
    print(e)


def NP_Initialize(appHwnd,recvMsgID,serverIP,serverPotr,recvSavePath):
    initialize = dll.NP_Initialize
    initialize.argtypes = (HWND,c_int,c_char_p,c_int,c_char_p)
    initialize.restype = c_bool
    return initialize(appHwnd,recvMsgID,serverIP,serverPotr,recvSavePath)

def NP_Unitialize():
    uninitialize = dll.NP_UnInitialize
    return uninitialize

def NP_ConnectCardofLocalNet(cardIP,isSync,cardInfo):
    connect_card_of_localnet = dll.NP_ConnectCardofLocalNet
    connect_card_of_localnet.argtypes = (c_char_p,c_bool,POINTER(NP_CARD_INFO))
    connect_card_of_localnet.restype = c_bool
    return connect_card_of_localnet(cardIP,isSync,cardInfo)

def NP_CreatePlayProgram(fileName,screenSize):
    create_play_program = dll.NP_CreatePlayProgram
    create_play_program.argtypes = (c_char_p,NP_SIZE)
    # 0表示失败，否则表示成功并返回文件句柄
    create_play_program.restype = c_int
    return create_play_program(fileName,screenSize)

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

def NP_GetCardInfo(cardID,isSync,cardInfo):
    '''
    :param cardID: string 需要获取信息的异步卡ID
    :param isSync: bool 消息是否同步显示
    :param cardInfo 储存消息的消息体
    '''
    get_card_info = dll.NP_GetCardInfo
    get_card_info.argtypes = (c_char_p,c_bool,POINTER(NP_CARD_INFO))
    get_card_info.restype = c_bool
    return get_card_info(cardID,isSync,cardInfo)