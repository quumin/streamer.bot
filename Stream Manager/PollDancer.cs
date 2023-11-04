using System;
using System.IO;
using System.Collections.Generic;
using QminBotDLL;

/*Poll Dancer
 * 
 *  Create a poll to select what you do next based on your currently installed games.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public void Init()
    {
        //Set Static Class in QnamicLib to active instance of CPH
        QnamicLib.CPH = CPH;
    }//Init()

    public bool Execute()
    {
        //Declarations
        Random rnd_index;
        List<string>[] list_games;
        List<string> list_opt;
        string str_question;
        int int_index, int_dur, int_cp;

        //Initializations
        rnd_index = new Random();
        list_opt = new List<string>();
        list_games = QnamicLib.LoadGameLibrary();
        str_question = "What should Q do next?";
        int_index = 0;
        int_dur = 90;
        int_cp = 0;

        //Create Baseline Poll Options.
        list_opt.Add("Keep on keepin' on.");
        list_opt.Add("Coding!");

        //Generate Games.
        for (int i = 0; i < 3; i++)
        {
            int_index = rnd_index.Next(list_games[0].Count);
            CPH.LogInfo($"『P O L L』 Polled games #{int_index}: {list_games[0][int_index]}");
            
            //If the selected index is not empty...
            if (list_games[0][int_index] != null)
            {
                //... then add to options and remove from the list to prevent duplicates.
                list_opt.Add(list_games[0][int_index]);
                list_games[0].RemoveAt(int_index);
            }//if
            else
            {
                //... else decrement index by 1 to handle an empty newline.
                list_opt.Add(list_games[0][int_index - 1]);
                list_games[0].RemoveAt(int_index - 1);
            }//else

        }//for

        CPH.TwitchPollCreate(str_question, list_opt, int_dur, int_cp);

        return true;
    }//Execute()
}//CPHInline