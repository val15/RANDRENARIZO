# Import object classes
from board import *
from engine import *

b=Board()
e=Engine()

import socket,os
# -*- coding: utf-8 -*-
ipAdress='192.168.15.166'
#ipAdress='127.0.0.1'
port = 65432


serv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
#serv.bind(('127.0.0.1', 65432))
serv.bind((ipAdress, port))

serv.listen(5)

def TreatMessageFromServer(message):
    dataList=message.split(";")
    header =  dataList[0]
    content = dataList[1]
    print(content)

    #b.render()
    if header == "Move":
        fromPosition = content[0:2]
        toPosition = content[2:4]
        print("fromPosition= " + fromPosition)
        print("toPosition= " + toPosition)
        # b.render()
        c = fromPosition + toPosition
        e.usermove(b,c)
        b.render()
        return ""

    if header == "Go":
        result = e.search(b)
        return result
    if header == "NewGame":
        e.newgame(b)
        b.render()
        e.setDepthStr(content)
    

    return ""

print("listen in "+ipAdress+":"+str(port))

while True:
    conn, addr = serv.accept()
    from_client = b''
    while True:
        data = conn.recv(4096)
        if not data: break
        from_client += data
        #print(from_client)
        answe = TreatMessageFromServer(from_client.decode("utf-8"))
        print(answe)
        bAnswer = bytes(answe+";.", 'utf-8')
        conn.send(bAnswer)
        if answe != "":
            b.render()
       ## b.render()
    conn.close()
    print("client disconnected")