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