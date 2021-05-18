from Engine import *
from DynamicArray import *



class Pawn:
    Name =""
    Location=""
    Colore=""
    X=""
    Y=""
    Value=0
    PossibleTrips = DynamicArray()
    IsFirstMove = False
    IsFirstMoveKing = False
    IsLeftRookFirstMove = False
    IsRightRookFirstMove = False

    

    def GetPawnFromPawnList(self,position,pawnList):
        #first =next(filter(lambda pawn: pawn.Location == "a2", self.PawnList), None)
        c =0
        while c < len(pawnList):
            if(pawnList[c].Location == position):
                return pawnList[c]
            c = c+1
        return None
    
    def GetOpignonPawnList(self,colore,pawnList):
        resultList =  DynamicArray()
        c =0
        while c < len(pawnList):
            if(pawnList[c].Colore == colore):
                resultList.append(pawnList[c])
            c = c+1
        return resultList
        
      

    def SetValue(self,x):
        return {
            "SimplePawn": 10,
            "Queen": 100,
            "Rook": 50,
            "Bishop": 40,
            "Knight": 30,
            "King": 1000
        }.get(x, 10)    # 9 is default if x not found

    def fillPossibleTripsSimplePawn(self,pawnList):
        toAdd = 0
        if self.Colore == "White":
            toAdd = toAdd+1
        else:
            toAdd = toAdd-1
        xasciiCode = ord(self.X)
        intY = int(self.Y)

        tripsPosition = self.X + str((intY +(toAdd)))
        print("retY"+tripsPosition)
        if(tripsPosition[1].isnumeric() == False):
            return
        if(int(tripsPosition[1])>8 or int(tripsPosition[1])<1):
            return

        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if pawnInTrips is None:
            self.PossibleTrips.append(tripsPosition)
        if self.IsFirstMove:
            tripsPosition = self.X + str(intY + (toAdd))
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if pawnInTrips is None:
                tripsPosition = self.X + str(intY + (toAdd) + (toAdd))
                if(tripsPosition[1].isnumeric() == False):
                    return
                if(int(tripsPosition[1])>8 or int(tripsPosition[1])<1):
                    return
                pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
                if pawnInTrips is None:
                    self.PossibleTrips.append(tripsPosition)
        
        #pour les attaques des pions
        tripsPosition = chr(xasciiCode -1) + str(intY +toAdd)
        print("ATTAQUE " + tripsPosition)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if pawnInTrips is not None:
            if pawnInTrips.Colore != self.Colore:
                self.PossibleTrips.append(tripsPosition)
        tripsPosition = chr(xasciiCode + 1) + str(intY + toAdd)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition, pawnList)
        if pawnInTrips is not None:
            if pawnInTrips.Colore != self.Colore:
                self.PossibleTrips.append(tripsPosition)



    
    def fillPossibleTripsKnight(self,pawnList):
        avalablesPositionList = DynamicArray()
        xasciiCode = ord(self.X)
        intY = int(self.Y)

        tripsPosition = chr(xasciiCode - 1)+str(intY + 2)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)

        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)):
            if  pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)
        
        tripsPosition = chr(xasciiCode + 1) + str(intY + 2)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)):
            if pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)
        
        tripsPosition = chr(xasciiCode + 2) + str(intY -1)
        pawnInTrips = pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)):
            if pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)
        
        tripsPosition = chr(xasciiCode + 2) + str(intY + 1)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)):
            if pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)

        tripsPosition = chr(xasciiCode - 1) + str(intY -2 )
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)):
            if pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)
        
        tripsPosition = chr(xasciiCode + 1) + str(intY - 2)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)):
            if pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)

        tripsPosition = chr(xasciiCode - 2) + str(intY - 1)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)):
            if pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)

        tripsPosition = chr(xasciiCode - 2) + str(intY + 1)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)):
            if pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)


        c=0
        while c < len(avalablesPositionList):
            self.PossibleTrips.append(avalablesPositionList[c])
            c = c+1

    def fillPossibleTripsRook(self,pawnList):
        avalablesPositionList = DynamicArray()
        intY = int(self.Y)
        i=intY
        while(i<=8):
            i = i+1
            tripsPosition = self.X + str(i)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition)
                continue
            if(pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break;
            

        i=intY
        while(i>1):
            i = i-1
            tripsPosition = self.X + str(i)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition)
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
            
      

        xasciiCode = ord(self.X)
        i = 1-1
        while(i <= 97+8 and xasciiCode < 104):
            i=i+1
            tripsPosition = chr(xasciiCode + i) + self.Y
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition)
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
            

        i = xasciiCode-1+1
        while(i >= 97):
            i =i-1
            tripsPosition = chr(i) + self.Y
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition)
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break

            
      


        c=0
        while c < len(avalablesPositionList):
            self.PossibleTrips.append(avalablesPositionList[c])
            c = c+1
    
    def fillPossibleTripsBishop(self,pawnList):
        avalablesPositionList = DynamicArray()
        xasciiCode = ord(self.X)
        intY = int(self.Y)
        
        i=xasciiCode
        j=intY-1
        while(i<97+8 and j < 8):
            i = i+1
            j = j+1
            tripsPosition = chr(i)+str(j+1)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition);
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
        
        i=xasciiCode+1
        j=intY-1+2
        while(i > 97 and j > 1):
            i=i-1
            j=j-1
            tripsPosition = chr(i-1) + str(j - 1)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition);
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break

        i=xasciiCode-1+1
        j = intY-1
        while(i >= 97 and j <= 8):
            i = i-1
            j = j+1
            tripsPosition = chr(i) + str(j + 1)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition);
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
        
        i = xasciiCode
        j = intY
        
        while(i < 97+7 and j > 1):
            i = i+1
            j = j-1
            tripsPosition = chr(i) + str(j)
            print("j = " + str(j))
            print("tripsPosition = " + tripsPosition)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition);
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
    
        c=0
        while c < len(avalablesPositionList):
            self.PossibleTrips.append(avalablesPositionList[c])
            c = c+1


    def fillPossibleTripsQueen(self,pawnList):
        avalablesPositionList = DynamicArray()
        intY = int(self.Y)
        i=intY
        xasciiCode = ord(self.X)
        while(i<=8):
            i = i+1
            tripsPosition = self.X + str(i)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition)
                continue
            if(pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
            

        i=intY
        while(i>1):
            i = i-1
            tripsPosition = self.X + str(i)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition)
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
            
      

        
        i = 1-1
        while(i <= 97+8 and xasciiCode < 104):
            i=i+1
            tripsPosition = chr(xasciiCode + i) + self.Y
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition)
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
            

        i = xasciiCode-1+1
        while(i >= 97):
            i =i-1
            tripsPosition = chr(i) + self.Y
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition)
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break

        i=xasciiCode
        j=intY-1
        while(i<97+8 and j < 8):
            i = i+1
            j = j+1
            tripsPosition = chr(i)+str(j+1)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition);
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
        
        i=xasciiCode+1
        j=intY-1+2
        while(i > 97 and j > 1):
            i=i-1
            j=j-1
            tripsPosition = chr(i-1) + str(j - 1)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition);
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break

        i=xasciiCode-1+1
        j = intY-1
        while(i >= 97 and j <= 8):
            i = i-1
            j = j+1
            tripsPosition = chr(i) + str(j + 1)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition);
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
        
        i = xasciiCode
        j = intY
        
        while(i < 97+7 and j > 1):
            i = i+1
            j = j-1
            tripsPosition = chr(i) + str(j)
            print("j = " + str(j))
            print("tripsPosition = " + tripsPosition)
            pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
            if((len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8)) == False ):
                break
            if (pawnInTrips is None):
                avalablesPositionList.append(tripsPosition);
                continue
            if (pawnInTrips.Colore == self.Colore):
                break
            else:
                avalablesPositionList.append(tripsPosition)
                break
        c=0
        while c < len(avalablesPositionList):
            self.PossibleTrips.append(avalablesPositionList[c])
            c = c+1
    
    def fillPossibleTripsKing(self,pawnList):
        avalablesPositionList = DynamicArray()
        intY = int(self.Y)
        i=intY
        xasciiCode = ord(self.X)

     
        tripsPosition = self.X + str(intY + 1)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8) ):
            if  pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)
        
        tripsPosition = self.X + str(intY - 1)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8) ):
            if  pawnInTrips is None:
                avalablesPositionList.append(tripsPosition)
            else:
                if pawnInTrips.Colore != self.Colore:
                    avalablesPositionList.append(tripsPosition)
      
        tripsPosition = chr(xasciiCode + 1)+ self.Y
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8) ):
                if  pawnInTrips is None:
                    avalablesPositionList.append(tripsPosition)
                else:
                    if pawnInTrips.Colore != self.Colore:
                        avalablesPositionList.append(tripsPosition)

        tripsPosition = chr(xasciiCode - 1) +self.Y
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8) ):
                if  pawnInTrips is None:
                    avalablesPositionList.append(tripsPosition)
                else:
                    if pawnInTrips.Colore != self.Colore:
                        avalablesPositionList.append(tripsPosition)


        tripsPosition = chr(xasciiCode + 1) + str(intY + 1)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8) ):
                if  pawnInTrips is None:
                    avalablesPositionList.append(tripsPosition)
                else:
                    if pawnInTrips.Colore != self.Colore:
                        avalablesPositionList.append(tripsPosition)

        tripsPosition = chr(xasciiCode - 1) + str(intY + 1)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8) ):
                if  pawnInTrips is None:
                    avalablesPositionList.append(tripsPosition)
                else:
                    if pawnInTrips.Colore != self.Colore:
                        avalablesPositionList.append(tripsPosition)


        tripsPosition = chr(xasciiCode + 1) + str(intY - 1)
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8) ):
                if  pawnInTrips is None:
                    avalablesPositionList.append(tripsPosition)
                else:
                    if pawnInTrips.Colore != self.Colore:
                        avalablesPositionList.append(tripsPosition)

        tripsPosition = chr(xasciiCode - 1)+ str(intY - 1 )
        pawnInTrips = self.GetPawnFromPawnList(tripsPosition,pawnList)
        if(len(tripsPosition) ==2 and tripsPosition[1].isnumeric() and (ord(tripsPosition[0])>=ord('a') and ord(tripsPosition[0])<=ord('h')) and (int(tripsPosition[1])>=1 and int(tripsPosition[1])<=8) ):
                if  pawnInTrips is None:
                    avalablesPositionList.append(tripsPosition)
                else:
                    if pawnInTrips.Colore != self.Colore:
                        avalablesPositionList.append(tripsPosition)

        opignonPawnList = self.GetOpignonPawnList(self.Colore,pawnList)
        #Ajout du rook TODO
        c=0
        while c < len(avalablesPositionList):
            self.PossibleTrips.append(avalablesPositionList[c])
            c = c+1
   


    def FillPossibleTrips(self,pawnList):
        self.PossibleTrips = DynamicArray()
        if self.Name == "SimplePawn":
            self.fillPossibleTripsSimplePawn(pawnList)
        if self.Name == "Knight":
            self.fillPossibleTripsKnight(pawnList)
        if self.Name == "Rook":
            self.fillPossibleTripsRook(pawnList)
        if (self.Name == "Bishop"):
            self.fillPossibleTripsBishop(pawnList)
        if (self.Name == "Queen"):
            self.fillPossibleTripsQueen(pawnList)
        if (self.Name == "King"):
            self.fillPossibleTripsKing(pawnList)
    







    def __init__(self,name, location, colore):
        self.Colore = colore
        self.Location = location
        self.Name=name
        self.X=location[0]
        self.Y=location[1]
        self.Value=self.SetValue(name)
        if  self.Name=="SimplePawn":
            self.IsFirstMove = True;
        if self.Name == "King":
            self.IsFirstMoveKing = True;
            self.IsLeftRookFirstMove = True;
            self.IsRightRookFirstMove = True;

        