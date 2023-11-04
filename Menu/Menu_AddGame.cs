using System;
using System.IO;

/*Menu - Add Game to Library
 * 
 *  Add the game with the selected settings to the Library.
 *  LU: 31-oct-2023
 *  
 */

public class CPHInline
{

    public bool Execute()
    {
        //Declarations
        string[] str_uG, str_game;
        string str_delim, str_path, str_file;

        //Initializations
        // Global List
        str_uG = new string[]
        {
            "qminCurrentGame"
        };
        str_game = CPH.GetGlobalVar<string[]>(str_uG[0]);
        // Specific
        str_delim = ";";
        str_path = @".\\external_files\\";
        str_file = "GamesList.csv";

        File.AppendAllText($"{str_path}{str_file}", string.Join(str_delim, str_game) + Environment.NewLine);
        return true;
    }//Execute()
}//CPHInline