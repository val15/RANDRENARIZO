using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ficherToSQLite
{
  class Program
  {
    static void Main(string[] args)
    {
      //System.Console.WriteLine("test");
      //  SQLiteConnection.CreateFile("words.db");
      SQLiteConnection maConnexion;
      maConnexion = new SQLiteConnection("Data Source=words.db;Version=3;");
      maConnexion.Open();

      //creation du table
    /*  var sql = "DROP TABLE IF EXISTS words";//word FROM words
      SQLiteCommand commande = new SQLiteCommand(sql, maConnexion);
      commande.ExecuteNonQuery();

      sql = "create table words (word text)";//word FROM words
      commande = new SQLiteCommand(sql, maConnexion);
      commande.ExecuteNonQuery();

      var f = new Fichier("dicofr.txt");

      if (f.NbLinge == 0)
      {
        System.Console.WriteLine("fichier vide ou n'existe pas");
      }
      else
      {
        var allWord = f.ReadAllLinges();
        var n = 0;
        foreach (var word in allWord)
        {
          //  MessageBox.Show(f.lireUneLinge(c), "info");

          n++;
          var formatedWord = word.Replace("'", "");
          sql = "insert into words (word) values ('" + formatedWord + "')";
          commande = new SQLiteCommand(sql, maConnexion);
          commande.ExecuteNonQuery();
          System.Console.WriteLine($"n {n} : { formatedWord}");
        }
       
      }
    */

      //lecture de la db
      var sql = "SELECT word FROM words ";
     var commande = new SQLiteCommand(sql, maConnexion);
      SQLiteDataReader reader = commande.ExecuteReader();
      var i = 0;
      while (reader.Read())
      {
        //*  MessageBox.Show();
        // motTrouver = reader["word"].ToString();
        i++;
        System.Console.WriteLine($"numero {i}  mot: {reader["word"]}");

      }





      //eliminatein des doublans

      System.Console.WriteLine("FINI");
      maConnexion.Close();

      Console.ReadKey();
    }
  }
}
