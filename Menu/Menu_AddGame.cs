using System;
using System.IO;

/*Menu - Add Game to Library
 * 
 *  Add the game with the selected settings to the Library.
 *  LU: 23-jun-2024
 *  
 */

public class CPHInline
{

    public bool Execute()
    {
        //Declarations
        string[] usedGlobals, currentGame;
        string deLim, filePath, openFile;

        //Initializations
        // Global List
        usedGlobals = new string[]
        {
            "qminCurrentGame"
        };
        currentGame = CPH.GetGlobalVar<string[]>(usedGlobals[0]);
        // Specific
        deLim = ";";
        filePath = @".\\external_files\\";
        openFile = "GamesList.csv";

        File.AppendAllText($"{filePath}{openFile}", string.Join(deLim, currentGame) + Environment.NewLine);
        CPH.LogVerbose($"『MENU』: Game {currentGame[0]} successfully added to \'{filePath}{openFile}\'!");
        return true;
    }//Execute()
}//CPHInline