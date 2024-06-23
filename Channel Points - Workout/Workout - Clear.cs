using System;
using System.IO;

/*Workout Clear
 * 
 *  Clear the workout files and variables.
 *  LU: 22-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] filePath;
        FileStream workoutFile;

        //Initializations 
        filePath = new string[]
        {
            @".\\external_files\\Exercise.txt",
            @".\\external_files\\Exercise.csv"
        };

        CPH.UnsetGlobalVar("deathCounter");

        //Clear .txt
        workoutFile = File.Open(filePath[0], FileMode.Open);
        workoutFile.SetLength(0);
        workoutFile.Close();

        //Clear .csv
        workoutFile = File.Open(filePath[1], FileMode.Open);
        workoutFile.SetLength(0);
        workoutFile.Close();

        CPH.SendMessage("/me Workout Files cleared Q-Mander NODDERS");

        CPH.DisableCommand("69e5a7a8-8d90-4064-b8dd-68a7a63268d1");
        CPH.DisableCommand("d3422d01-3041-49ac-bed8-1d72c9d12dec");

        return true;
    }//Execute()
}//CPHInline