from ctypes import *
from CommonProperty import *
from NovaPlutoSDK import *

# appHwnd = c_int(0x00450888)
# recvMsgID = c_int(0x800)
# serverIP = c_char_p(bytes("172.22.178.2","utf-8"))
# serverPort = c_int(25000)
# recvSavePath = c_char_p(bytes("D:\\NovaPluto",'utf-8'))

appHwnd = 51544                 
recvMsgID = 2048
serverIP = b"172.22.178.2"
serverPort = 25000
recvSavePath =b"D:\\NovaPluto"




result = NP_Initialize(appHwnd, recvMsgID,serverIP, serverPort, recvSavePath)
print(result)
# screenSize = NP_SIZE(800,600)
screenSize =NP_SIZE()
screenSize.Width = 800
screenSize.Height = 600
fileName = b'D:\NovaPluto\A.plym'
result = NP_CreatePlayProgram(fileName,screenSize)
# print(result)




k = NP_SendPlayProgram()