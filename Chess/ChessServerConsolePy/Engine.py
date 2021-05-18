from Pawn import *

from DynamicArray import *

class Engine:



    PawnList = DynamicArray()

    def GeneratePawnList(self,enterStringList):
        #print(len(enterStringList))
        for line in enterStringList:
            if ";" not in line:
                break
            datas = line.split(";")
            pawn = Pawn(datas[0],datas[1],datas[2])
            pawn.IsFirstMove = datas[3]
            pawn.IsFirstMoveKing = datas[4]
            pawn.IsLeftRookFirstMove = datas[5]
            pawn.IsRightRookFirstMove = datas[6]
            self.PawnList.append(pawn)
            #print(len(self.PawnList))
        self.FillAllPossibleTips()
            


    def FillAllPossibleTips(self):
        c=0
        while c < len(self.PawnList):
            self.PawnList[c].FillPossibleTrips(self.PawnList)
            c = c+1
        c=0


        while c < len(self.PawnList):
            i=0
            print("--"+self.PawnList[c].Name +" "+self.PawnList[c].Colore+" "+self.PawnList[c].Location +"--")
            while i < len(self.PawnList[c].PossibleTrips):
                print(self.PawnList[c].PossibleTrips[i])
                i=i+1
            c = c+1


    

    


  
   
    def __init__(self,computurColor):


        #TODO à supprimer
        
        """ 
        simplePawnWhite0 = Pawn("SimplePawn","d6","White")
        simplePawnWhite1 = Pawn("SimplePawn","c7","White")
        simplePawnBlack0 = Pawn("SimplePawn","b7","Black")
        simplePawnBlack1 = Pawn("SimplePawn","f1","Black")
        simpleKnightWhite0 = Pawn("Knight","g1","White")
        simpleKnightBlack0 = Pawn("Knight","g8","Black")
        self.PawnList.append(simplePawnWhite0)
        self.PawnList.append(simplePawnWhite1)
        self.PawnList.append(simplePawnBlack0)
        self.PawnList.append(simplePawnBlack1)
        self.PawnList.append(simpleKnightWhite0)
        self.PawnList.append(simpleKnightBlack0) """
        #rookWhite0 = Pawn("Rook","a1","White")
        #self.PawnList.append(rookWhite0)
        #self.PawnList.append(simplePawnWhite0)
        #rookBlack0 = Pawn("Rook","d5","Black")
        #self.PawnList.append(rookBlack0)
        #bishopWhite0 = Pawn("Bishop","a1","White")
        #self.PawnList.append(bishopWhite0)
        #bishopWhite0 = Pawn("Bishop","h8","White")
        #self.PawnList.append(bishopWhite0)
        #bishopWhite0 = Pawn("Bishop","h8","White")
        #self.PawnList.append(bishopWhite0)
        #bishopBlack0 = Pawn("Bishop","h1","Black")
        #self.PawnList.append(bishopBlack0)
        #bishopBlack1 = Pawn("Queen","d4","Black")
        #self.PawnList.append(bishopBlack1)
        #kingBlack0 = Pawn("King","d4","Black")
        #self.PawnList.append(kingBlack0)
        #self.FillAllPossibleTips()
        
        
        
      

            
