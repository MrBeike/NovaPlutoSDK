from ctypes import *

class NovaPlutoSDK:
    def __init__(self):
        self.dll = self.load_dll()

    @classmethod    
    def load_dll(cls):
        try:
            dll = windll.LoadLibrary('NovaPlutoManager.dll')
        except Exception as e:
            dll = e 
        return dll

    def NP_Initialize(self,appHwnd,recvMsgID,serverIP,serverPotr,recvSavePath):
        initialize = self.NP_Initialize
        initialize.argstype = [HWND,c_int,c_char_p,c_int,c_char_p]
        initialize.restype = c_bool
        return initialize(appHwnd,recvMsgID,serverIP,serverPotr,recvSavePath)

