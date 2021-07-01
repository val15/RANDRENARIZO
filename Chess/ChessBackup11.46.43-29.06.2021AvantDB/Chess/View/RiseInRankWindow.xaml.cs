using Chess.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess.View
{
  /// <summary>
  /// Interaction logic for RiseInRankWindow.xaml
  /// </summary>
  public partial class RiseInRankWindow : Window
  {
    private Pawn _pawnSender;
    public RiseInRankWindow(Pawn pawnSender)
    {
      InitializeComponent();
      _pawnSender = pawnSender;
    }

    private void ChoiseQueen_Click(object sender, RoutedEventArgs e)
    {
      _pawnSender.SwithTo("Queen");
      this.Close();
    }

    private void ChoiseRook_Click(object sender, RoutedEventArgs e)
    {
      _pawnSender.SwithTo("Rook");
      this.Close();
    }

    private void ChoiseKnight_Click(object sender, RoutedEventArgs e)
    {
      _pawnSender.SwithTo("Knight");
      this.Close();
    }

    private void ChoiseBishop_Click(object sender, RoutedEventArgs e)
    {
      _pawnSender.SwithTo("Bishop");
      this.Close();
    }
  }
}
