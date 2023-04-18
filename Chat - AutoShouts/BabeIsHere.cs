using System;

public class CPHInline
{
	public bool Execute()
	{
		string str_ss, str_media;
		int int_wait = 2300;
		bool bool_serious = CPH.GetGlobalVar<bool>("seriousMode");

		str_ss = "SS_Alerts";
		str_media = "BabeHerePog";

		CPH.SendMessage("Hey it's @whymusticryy! Thank-Q for coming by you sexy human quuminL you are Q's thiqq or die quuminL");

		if (!bool_serious)
		{
			CPH.ObsShowSource(str_ss, str_media);
			CPH.Wait(int_wait);
			CPH.ObsHideSource(str_ss, str_media);
		}//if(!bool_serious)

		CPH.DisableAction("whymusticryy");
		return true;
	}
}