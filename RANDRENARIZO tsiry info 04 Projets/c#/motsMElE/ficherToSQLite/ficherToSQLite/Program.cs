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
            /*string sql = "create table words (word text)";//word FROM words
            SQLiteCommand commande = new SQLiteCommand(sql, maConnexion);
            commande.ExecuteNonQuery();*/

         /*   Fichier f = new Fichier("dicofr.txt");

            if (f.NbLinge == 0)
            {
                System.Console.WriteLine("fichier vide ou n'existe pas");
            }
            else
            {
                for (int c = 0; c < f.NbLinge; c++)
                {
                    //  MessageBox.Show(f.lireUneLinge(c), "info");
                    string sql = "insert into words (word) values ('" + f.lireUneLinge(c).ToUpper() + "')";
                    SQLiteCommand commande = new SQLiteCommand(sql, maConnexion);
                    commande.ExecuteNonQuery();
                    System.Console.WriteLine(f.lireUneLinge(c).ToUpper());

                    

                }
            }*/
      
            
            //lecture de la db
            string sql = "SELECT word FROM words ";
            SQLiteCommand commande = new SQLiteCommand(sql, maConnexion);
            SQLiteDataReader reader = commande.ExecuteReader();
            while (reader.Read())
            {
                //*  MessageBox.Show();
                // motTrouver = reader["word"].ToString();
                System.Console.WriteLine("mot: " + reader["word"]);
            }
            
            
            

            
            //eliminatein des doublans

            System.Console.WriteLine("FINI");
            maConnexion.Close();
           
            Console.ReadKey();
        }
    }
}
