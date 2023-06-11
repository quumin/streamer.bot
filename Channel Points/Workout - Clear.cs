using System;
using System.IO;

/*Workout Clear
 * 
 *  Clear the workout files and variables.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_path;
        FileStream fs_open;

        //Initializations 
        str_path = new string[]
        {
            "W:\\Streaming\\Exercise.txt",
            "W:\\Streaming\\Exercise.csv"
        };

        //Clear .txt
        fs_open = File.Open(str_path[0], FileMode.Open);
        fs_open.SetLength(0);
        fs_open.Close();
        
        //Clear .csv
        fs_open = File.Open(str_path[1], FileMode.Open);
        fs_open.SetLength(0);
        fs_open.Close();

        CPH.SendMessage("/me Workout Files cleared Q-Mander NODDERS");

        return true;
    }//Execute()
}//CPHInline