# -*- coding: utf-8 -*-
import socket
from datetime import datetime

#ipAdress='10.3.141.1'
ipAdress='127.0.0.1'
port = 65432

def SendMessageToServer(header,content):
    client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    #client.connect(('127.0.0.1', 65432))
    client.connect((ipAdress, port))
    message =""
    message =f"{header};{content}"
    client.send(message.encode("ascii"))
    from_server = client.recv(4096)
    client.close()
    print(from_server)
    return

print("Socket client")



SendMessageToServer("test","Test socket")
