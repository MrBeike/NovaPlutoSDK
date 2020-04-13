from ctypes import *
from ctypes.wintypes import HWND
from CommonProperty import NP_CARD_INFO

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
    connect_card_of_localnet.argtypes = (c_char_p,c_bool,NP_CARD_INFO)
    connect_card_of_localnet.restype = c_bool
    return connect_card_of_localnet(cardIP,isSync,cardInfo)
