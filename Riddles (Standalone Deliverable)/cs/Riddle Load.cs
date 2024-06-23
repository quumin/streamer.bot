using System;
using System.IO;
using System.Collections.Generic;

/*Riddle Load
 * 
 *  Load riddles from ./external_files/riddles.csv.
 *  LU: 23-jun-2024
 *
 */

public class CPHInline
{
    public bool Execute()
    {
        //Decalarations
        string[] usedGlobals;
        List<string>[] riddleLists;
        string msgOut;

        //Initializations
        // Global List
        usedGlobals = new string[]
        {
            "qminRiddleLineOne",
            "qminRiddleLineTwo",
            "qminRiddleLineThree",
            "qminRiddleLineFour",
            "qminRiddleLineFive",
            "qminRiddleLineSix",
            "qminRiddleLineSeven",
            "qminRiddleAnswers",
        };
        // Specific
        riddleLists = new List<string>[8];
        for (int i = 0; i < riddleLists.Length; i++)
            riddleLists[i] = new List<string>();
        msgOut = "/me ";

        try
        {
            using (var reader = new StreamReader(@".\\external_files\\riddles.csv"))
            {
                //Try to find the file
                //Populate the Lists
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    for (int i = 0; i < riddleLists.Length; i++)
                    {
                        riddleLists[i].Add(values[i]);
                        CPH.SetGlobalVar(usedGlobals[i], riddleLists[0]);
                    }

                }//while

                //Feedback
                CPH.LogInfo("『R I D D L E S』 Riddles Loaded Successfully.");
                msgOut += "Riddles loaded successfully Q-Mander dataMask";
            }//using
        }//try
        catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
        {
            //Catch when the directory and/or file is incorrect.
            CPH.LogWarn("『R I D D L E S』 Riddle file failed to load! Is the directory correctly set?");
            msgOut += "dataHuh The Riddles file could not be found, sir.";
        }//catch

        //Send message
        CPH.SendMessage(msgOut);

        return true;
    }//Execute()
}//CPHInline