from Case import *
from DynamicArray import *





class Engine:

    c1 = Case("1","2")
    CaseList = DynamicArray()
    PawnList = DynamicArray()

    

    def GetPawnFromPawnList(self,position):
        for pawn in self.PawnList:
            if pawn.Name == position:
                return pawn
        return None


  
   
    def __init__(self,computurColor):
        abscisse=["a","b","c","d","e","f","g","h"]
        ordonnee=["1","2","3","4","5","8","7","8"]
        for x in abscisse:
            for y in ordonnee:
                newCase = Case(x,y)
               # print(x+y)
                self.CaseList.append(newCase)

        #TODO à supprimer
        pawnTest = Pawn("SimplePawn","a1","White",engine)
        self.PawnList.append(pawnTest)
class Pawn:
    MainServer = Engine("White")
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

    def SetValue(self,x):
        return {
            "SimplePawn": 10,
            "Queen": 100,
            "Rook": 50,
            "Bishop": 40,
            "Knight": 30,
            "King": 1000
        }.get(x, 10)    # 9 is default if x not found

    def fillPossibleTripsSimplePawn(self):
        toAdd = 0
        if self.Colore == "White":
            toAdd = toAdd+1
        else:
            toAdd = toAdd-1
        xasciiCode = ord(self.X)
        intY = int(self.Y)

        tripsPosition = self.X + str((intY +(toAdd)))
        pawnInTrips = self.MainServer.GetPawnFromPawnList(tripsPosition)
        if pawnInTrips is None:
            PossibleTrips.Add(tripsPosition)
        if IsFirstMove:
            tripsPosition = X + (intY + (toAdd)).ToString();
            pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition)
        if pawnInTrips is None:
          tripsPosition = X + str(intY + (toAdd) + (toAdd));
          pawnInTrips = MainServer.GetPawnFromPawnList(tripsPosition);
        if pawnInTrips is None:
            PossibleTrips.Add(tripsPosition)
        



    def FillPossibleTrips(self):
      self.PossibleTrips = DynamicArray()
      if self.Name == "SimplePawn":
          self.fillPossibleTripsSimplePawn()







    def __init__(self,name, location, colore, server):
        self.MainServer= server
        self.Colore = colore
        self.Location = location
        self.Name=name
        self.X=location[0]
        self.Y=location[1]
        self.Value=SetValue(name)
        if  self.Name=="SimplePawn":
            self.IsFirstMove = True;
        if self.Name == "King":
            self.IsFirstMoveKing = True;
            self.IsLeftRookFirstMove = True;
            self.IsRightRookFirstMove = True;