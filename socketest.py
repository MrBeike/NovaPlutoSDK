import socket
import idna

name = socket.gethostname()
newname = idna.encode(name)

print(newname)

ip = socket.gethostbyname(newname)
print(ip)