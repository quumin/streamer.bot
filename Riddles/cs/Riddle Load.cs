using System;
using System.IO;
using System.Collections.Generic;

/*Riddle Load
 * 
 *  Load riddles from ./external_files/riddles.csv.
 *  LU: 31-oct-2023
 *
 */

public class CPHInline
{
    public bool Execute()
    {
        //Decalarations
        string[] str_uG;
        List<string>[] list_riddle;
        string str_msg;

        //Initializations
        // Global List
        str_uG = new string[]
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
        list_riddle = new List<string>[8];
        for (int i = 0; i < list_riddle.Length; i++)
            list_riddle[i] = new List<string>();
        str_msg = "/me ";

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

                    for (int i = 0; i < list_riddle.Length; i++)
                    {
                        list_riddle[i].Add(values[i]);
                        CPH.SetGlobalVar(str_uG[i], list_riddle[0]);
                    }

                }//while

                //Feedback
                CPH.LogInfo("『R I D D L E S』 Riddles Loaded Successfully.");
                str_msg += "Riddles loaded successfully Q-Mander dataMask";
            }//using
        }//try
        catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
        {
            //Catch when the directory and/or file is incorrect.
            CPH.LogWarn("『R I D D L E S』 Riddle file failed to load! Is the directory correctly set?");
            str_msg += "dataHuh The Riddles file could not be found, sir.";
        }//catch

        //Send message
        CPH.SendMessage(str_msg);

        return true;
    }//Execute()
}//CPHInline