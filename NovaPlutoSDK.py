from ctypes import *
from ctypes.wintypes import HWND

try:
    dll = windll.LoadLibrary('NovaPlutoManager.dll')
except Exception as e:
    dll = e


def NP_Initialize(dll,appHwnd,recvMsgID,serverIP,serverPotr,recvSavePath):
    initialize = dll.NP_Initialize
    initialize.argstype = [HWND,c_int,c_char_p,c_int,c_char_p]
    initialize.restype = c_bool
    return initialize(appHwnd,recvMsgID,serverIP,serverPotr,recvSavePath)

