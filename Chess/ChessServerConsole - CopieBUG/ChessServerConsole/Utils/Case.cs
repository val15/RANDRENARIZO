using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessServerConsole.Utils
{
  public class Case
  {

    public string X { get; set; } //a - h
    public string Y { get; set; } //1 - 8

    public string CaseName { get; set; }






    public Case(string x,string y)
    { 

      X = x;
      Y = y;
      CaseName = X + Y;
    }

  }
}
