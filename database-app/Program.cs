using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace database_app
{


    class Program
    {



        public Stream GenerateStreamFromString(String s)
        {
            Byte[] bytes = Encoding.UTF8.GetBytes(s);
            MemoryStream strm = new MemoryStream();
            strm.Write(bytes, 0, bytes.Length);
            return strm;
        }

        static void Main(string[] args)
        {



            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("C:/Users/user/Documents/desktop stuff/music_test/output/ultimate_playlist.txt");




            string[] fileArray = Directory.GetFiles(@"C:/Users/user/Documents/desktop stuff/music_test/normal_songs/");

            // Program p = new Program();



            /*


            for (int i = 0; i < fileArray.Length; i++)
            {


                var ba = p.GenerateStreamFromString(fileArray[i]);



                System.Console.WriteLine(ba);




            }


            */



            /*



            for (int i = 0; i < fileArray.Length; i++)
            {
                    //Write a line of text
                    sw.WriteLine(fileArray[i]);

            }

            //Close the file
            sw.Close();

            */

            foreach (string value in fileArray)
            {

                byte[] b = new byte[128];
                string sArtist;
                string sTitle;

                FileStream fs = new FileStream(value, FileMode.Open);
                fs.Seek(-128, SeekOrigin.End);
                fs.Read(b, 0, 128);
                bool isSet = false;
                String sFlag = System.Text.Encoding.Default.GetString(b, 0, 3);
                if (sFlag.CompareTo("TAG") == 0)
                {

                    isSet = true;
                }

                if (isSet)
                {
                    //get   artist and title and write a line of text 
                    sArtist = System.Text.Encoding.Default.GetString(b, 33, 30);
                    sTitle = System.Text.Encoding.Default.GetString(b, 3, 30);

                    sw.WriteLine(sArtist + "," + sTitle);

                }




            }


            //Close the file
            sw.Close();



            /*


            foreach (string value in fileArray)
            {

                byte[] b = new byte[128];
                string sArtist;
                string sTitle;
                
                FileStream fs = new FileStream(value, FileMode.Open);
                fs.Seek(-128, SeekOrigin.End);
                fs.Read(b, 0, 128);
                bool isSet = false;
                String sFlag = System.Text.Encoding.Default.GetString(b, 0, 3);
                if (sFlag.CompareTo("TAG") == 0)
                {

                    isSet = true;
                }

                if (isSet)
                {
                    //get   artist and title and write a line of text 
                    sArtist = System.Text.Encoding.Default.GetString(b, 33, 30);
                    sTitle = System.Text.Encoding.Default.GetString(b, 3, 30);

                    sw.WriteLine(sArtist + "," + sTitle);

                }




            }





            */




            string[] tableName = Directory.GetFiles(@"C:/Users/user/Documents/desktop stuff/music_test/output/");

            List<string> myValues = new List<string>();

            string line;

            // datasource = localhost; port = 3306; username = root; password = root;

            string MyConnectionString = "Server=localhost;port = 3306;Uid=root;Pwd=password;";
            MySqlConnection conn = new MySqlConnection(MyConnectionString);

            var lineCount = File.ReadLines(@"C:\Users\user\Documents\desktop stuff\music_test\output\ultimate_playlist.txt").Count();

            StreamReader file = new StreamReader(@"C:\Users\user\Documents\desktop stuff\music_test\output\ultimate_playlist.txt");

            MySqlCommand cmd;
            conn.Open();
            cmd = conn.CreateCommand();

            cmd.CommandText = "DROP DATABASE IF EXISTS songs";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "CREATE DATABASE songs";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "USE songs";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "CREATE TABLE ultimate_playlist (ID int unsigned not null auto_increment, artist varchar(150) not null, title varchar(150) not null, PRIMARY KEY (ID));";
            cmd.ExecuteNonQuery();


            for (int i = 0; i < lineCount; i++)
            {


                if ((line = file.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');



                    cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into ultimate_playlist (artist, title) values (@artist, @title)";
                    cmd.Parameters.AddWithValue("@artist", fields[0]);
                    cmd.Parameters.AddWithValue("@title", fields[1]);
                    cmd.ExecuteNonQuery();

                }


            }


            conn.Close();



            /*




            string[] fields = new string[lineCount];



            foreach (string value in fields)
            {



                for (int i = 0; i < lineCount; i++)
            {


                if ((line = file.ReadLine()) != null)
                {
                    fields = line.Split(',');

                }

                System.Console.WriteLine(value);

                }


            }

            System.Console.ReadLine();

            */


            // cmd.CommandText = "insert into ultimate_playlist (artist, title) values (?fields[0], ?fields[1])";



            // cmd.Parameters.AddWithValue("?artist", fields[0]);
            // cmd.Parameters.AddWithValue("?title", fields[1]);




            // cmd.CommandText = db.ultimate_playlist.insert("ID", "artist", "title").values(i, fields[0], fields[1]);
            // cmd.ExecuteNonQuery();


        }

    }

}