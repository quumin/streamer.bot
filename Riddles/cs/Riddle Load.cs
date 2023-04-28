using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
        //Decalarations
        List<string>[] list_riddle;
		string str_msg;

        //Initializations
        list_riddle = new List<string>[8];
        for (int i = 0; i < list_riddle.Length; i++)
            list_riddle[i] = new List<string>();
		str_msg = "/me ";

        try 
		{
			using(var reader = new StreamReader(@".\\external_files\\riddles.csv"))
			{
				//Try to find the file
                //Populate the Lists
                while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(';');

                    for (int i = 0; i < list_riddle.Length; i++)
                        list_riddle[i].Add(values[i]);
				}//while
			
				//Store the Globals
				CPH.SetGlobalVar("questionsOne", list_riddle[0]);
				CPH.SetGlobalVar("questionsTwo", list_riddle[1]);
				CPH.SetGlobalVar("questionsThr", list_riddle[2]);
				CPH.SetGlobalVar("questionsFou", list_riddle[3]);
				CPH.SetGlobalVar("questionsFiv", list_riddle[4]);
				CPH.SetGlobalVar("questionsSix", list_riddle[5]);
				CPH.SetGlobalVar("questionsSev", list_riddle[6]);
				CPH.SetGlobalVar("ansWer", list_riddle[7]);

				//Feedback
				CPH.LogInfo("『R I D D L E S』 Riddles Loaded Successfully.");
				str_msg += "Riddles loaded successfully Q-Mander dataMask";
			}//using
		}//try
		catch (Exception ex) when(ex is FileNotFoundException || ex is DirectoryNotFoundException){
			//Catch when the directory and/or file is incorrect.
			CPH.LogWarn("『R I D D L E S』 Riddle file failed to load! Is the directory correctly set?");
			str_msg += "dataHuh The Riddles file could not be found, sir.";
		}//catch

		//Send message
		CPH.SendMessage(str_msg);

		return true;
	}//Execute()
}//CPHInline
