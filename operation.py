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
serverIP = "172.22.178.2".encode("utf-8")
serverPort = 25000
recvSavePath ="D:\\NovaPluto".endcode('utf-8')


result = NP_Initialize(appHwnd, recvMsgID,serverIP, serverPort, recvSavePath)
print(result)
