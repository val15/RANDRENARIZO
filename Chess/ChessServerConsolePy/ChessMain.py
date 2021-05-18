from Engine import *
from Pawn import *



computerColore = "White"

print("Chess main")

def Load():
    f = open("D:\\tsiry\RANDRENARIZO\Chess\ChessServerConsolePy\WHITEListOld.txt", "r")
    contentList  = f.read()

    f = open("D:\\tsiry\RANDRENARIZO\Chess\ChessServerConsolePy\BLACKListOld.txt", "r")
    contentList += f.read()

    return contentList

result = Load()
resultList=result.split("\n")


#print(result)

engine = Engine(computerColore)

engine.GeneratePawnList(resultList)

#l = len(engine.CaseList)
#print(l)
#c=0
#while c < l:
#    print(engine.CaseList[c].caseName)
#    c = c+1


"""
PawnList = DynamicArray()
pawnTest = Pawn("SimplePawn","a1","White",PawnList)
print(len(PawnList))
PawnList.append(pawnTest)

print(len(PawnList))
pawnTest.FillPossibleTrips()
print(pawnTest.Name)
print(pawnTest.Value)
print(pawnTest.X)
print(pawnTest.Y)
print(pawnTest.IsLeftRookFirstMove)
print(len(pawnTest.PossibleTrips)) """



