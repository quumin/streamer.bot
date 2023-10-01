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
            @".\\external_files\\Exercise.txt",
            @".\\external_files\\Exercise.csv"
        };

        CPH.UnsetGlobalVar("deathCounter");

        //Clear .txt
        fs_open = File.Open(str_path[0], FileMode.Open);
        fs_open.SetLength(0);
        fs_open.Close();

        //Clear .csv
        fs_open = File.Open(str_path[1], FileMode.Open);
        fs_open.SetLength(0);
        fs_open.Close();

        CPH.SendMessage("/me Workout Files cleared Q-Mander NODDERS");

        CPH.DisableCommand("69e5a7a8-8d90-4064-b8dd-68a7a63268d1");
        CPH.DisableCommand("d3422d01-3041-49ac-bed8-1d72c9d12dec");

        return true;
    }//Execute()
}//CPHInline