from ctypes import *


#异步卡信息
class NP_CARD_INFO(Structure):
     _fields_ = [
          ('IP', c_char_p), 
          ('ID', c_char_p),
          ('Name',c_char_p),
          ('ScreenWidth',c_uint),
          ('ScreenHeight',c_uint),
          ('Reserved1',c_char_p),
          ('Reserved2',c_char_p),
          ('Reserved3',c_char_p)
          ]
          
#播放方案屏幕尺寸
class NP_SIZE(Structure):
     _fields_ = [
          ('Width',c_int),
          ('Height',c_int)
     ]

# 系统时间
class SYSTEMTIME(Structure):
     _fields_ = [
          ('wYear',c_ushort),
          ('wMonth',c_ushort),
          ('wDayOfWeek',c_ushort),
          ('wDay',c_ushort),
          ('wHour',c_ushort),
          ('wMinute',c_ushort),
          ('wSecond',c_ushort),
          ('wMilliseconds',c_ushort)
     ]

# 存储设备信息
class NP_STORAGEDEVICE_INFO(Structure):
     '''
     :param CurStorageDeviceType: int 当前存储设备类型：0表示Flash，1表示SD卡，2表示USB口接入的硬件
     :param FlashFreeSpace: int Flash的剩余空间大小：单位为字节
     :param FlashTotalSpace: int Flash的总空间大小：单位为字节
     :param SDCardFreeSpace: int SD卡的剩余空间大小：单位为字节
     :param SDCardTotalSpace: int SD卡的总空间大小：单位为字节
     :param UDiskFreeSpace: int USB口接口设备的剩余空间大小：单位为字节
     :param UDiskTotalSpace: int USB口接口设备的总空间大小：单位为字节
     '''
     _fields_ = [
          ('CurStorageDeviceType',c_ubyte),
          ('FlashFreeSpace',c_int),
          ('FlashTotalSpace',c_int),
          ('SDCardFreeSpace',c_int),
          ('SDCardTotalSpace',c_int),
          ('UDiskFreeSpace',c_int),
          ('UDiskTotalSpace',c_int)
     ]