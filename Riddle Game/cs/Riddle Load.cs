using System;
using System.IO;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
		try {
			using(var reader = new StreamReader(@".\\external_files\\riddles.csv"))
			{
			//Try to find the file
				//Decalarations
				List<string>[] question = new List<string>[7];
				List<string> ans = new List<string>();
				question[0] = new List<string>();
				question[1] = new List<string>();
				question[2] = new List<string>();
				question[3] = new List<string>();
				question[4] = new List<string>();
				question[5] = new List<string>();
				question[6] = new List<string>();
				
				//Populate the Lists
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					var values = line.Split(';');

					question[0].Add(values[0]);
					question[1].Add(values[1]);
					question[2].Add(values[2]);
					question[3].Add(values[3]);
					question[4].Add(values[4]);
					question[5].Add(values[5]);
					question[6].Add(values[6]);
					ans.Add(values[7]);
				}
			
				//Store the Globals
				CPH.SetGlobalVar("questionsOne", question[0]);
				CPH.SetGlobalVar("questionsTwo", question[1]);
				CPH.SetGlobalVar("questionsThr", question[2]);
				CPH.SetGlobalVar("questionsFou", question[3]);
				CPH.SetGlobalVar("questionsFiv", question[4]);
				CPH.SetGlobalVar("questionsSix", question[5]);
				CPH.SetGlobalVar("questionsSev", question[6]);
				CPH.SetGlobalVar("ansWer", ans);
				CPH.LogInfo("『R I D D L E S』 Riddles Loaded Successfully.");
				//Announcement
				CPH.TwitchAnnounce("Riddles loaded successfully Q-Mander dataMask", 
					color: "orange");
			}
		}
		catch (Exception ex) when(ex is FileNotFoundException || ex is DirectoryNotFoundException){
		//Catch when the directory and/or file is incorrect.
			CPH.LogWarn("『R I D D L E S』 Riddle file failed to load! Is the directory correctly set?");
			CPH.TwitchAnnounce("dataHuh The Riddles file could not be found, sir.", 
				color: "orange");
		}

		return true;
	}
}
