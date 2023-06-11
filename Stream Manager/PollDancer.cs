using System;
using System.IO;
using System.Collections.Generic;

/*Poll Dancer
 * 
 *  Create a poll to select what yo do next based on your currently installed games.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        Random rnd_index;
        List<string> list_opt, list_games;
        string str_question;
        int int_index, int_bits, int_dur, int_cp;

        //Initializations
        rnd_index = new Random();
        list_opt = new List<string>();
        list_games = gameLoad();
        str_question = "What should Q do next?";
        int_index = 0;
        int_dur = 60;
        int_cp = 0;

        //Create Baseline Poll Options.
        list_opt.Add("Keep on keepin' on.");
        list_opt.Add("Code (& React)");

        //Generate Games.
        for (int i = 0; i < 3; i++)
        {
            int_index = rnd_index.Next(list_games.Count);
            list_opt.Add(list_games[int_index]);
            list_games.RemoveAt(int_index);
        }//for

        CPH.TwitchPollCreate(str_question, list_opt, int_dur, int_cp);

        return true;
    }//Execute()
    List<string> gameLoad()
    {
        //Declarations
        List<string> list_games;

        //Initializations
        list_games = new List<string>();

        try
        {
            //Try to find the file and read from it...
            using (var reader = new StreamReader(@".\\external_files\\GamesList.csv"))
            {
                //Populate the List
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    list_games.Add(line);
                }//while

                CPH.LogInfo("『G A M E S』 Loaded Successfully.");
            }//using
        }//try
        catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
        {
            //Catch when the directory and/or file is incorrect.
            CPH.LogWarn("『G A M E S』 Games File failed to load!");
            CPH.SendMessage("/me dataHuh The Games file could not be found, sir.");
        }//catch
        return list_games;
    }//gameLoad()
}//CPHInline