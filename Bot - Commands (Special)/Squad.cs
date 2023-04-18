using System;
using System.Collections.Generic;

public class CPHInline
{
	public bool Execute()
	{
		List<string> squad = new List<string>();
		squad = CPH.GetGlobalVar<List<string>>("squadCurrent");
		if (squad.Count > 0)
		{
			string squadBuild = "hmmMeeting The Q-mander is playing with ";
			int total = squad.Count;
			for (int i = 0; i < total; i++)
			{
				if (i == 0 && total == 1)
				{
					squadBuild += squad[i] + " ";
				}
				else if (i == total - 1)
				{
					squadBuild += " & " + squad[i] + " ";
				}
				else
				{
					squadBuild += squad[i] + ", ";
				}
			}
			squadBuild += "ButtBooty";
			CPH.TwitchAnnounce(squadBuild, true, "purple");
		}
		else
		{
			CPH.TwitchAnnounce("The Q-mander did not add anybody to the squad! LULdata", true, "purple");
		}

		return true;
	}
}
