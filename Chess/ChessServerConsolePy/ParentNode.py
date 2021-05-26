from Pawn import *
from Node import *

class Node:
    CurrentLocalPawnList =DynamicArray()
    Level=0
    Weight=0
    Location=""
    OldPositionName=""
    Colore =""
    Parent = Node()
    ChildList=DynamicArray()
    BestChildPosition=""
    AssociatePawn=Pawn()

    def GetCurrentLocalPawnListAllier(self):
        resultList =  DynamicArray()
        c =0
        while c < len(self.CurrentLocalPawnList):
            if(self.CurrentLocalPawnList[c].Colore == self.Colore):
                resultList.append(self.CurrentLocalPawnList[c])
            c = c+1
        return resultList

    def __init__(self):
        return

    def __init__(self,currentLocalPawnList):
        self.CurrentLocalPawnList = currentLocalPawnList