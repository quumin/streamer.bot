using System;

public class CPHInline
{
	public bool Execute()
	{
		string str_path, str_media;
		bool bool_serious = CPH.GetGlobalVar<bool>("seriousMode");

		str_path = "W:\\Streaming\\Media\\Sounds\\";
		str_media = "WolfHowl.mp3";

		CPH.SendMessage("dataHuh Yo whaddup @gryze_wolf! Thanks for including Q in the wolf pack brother-man quuminL");

		if (!bool_serious)
		{
			CPH.PlaySound(str_path + str_media, 0.15f, true);
		}//if(!bool_serious)

		CPH.DisableAction("gryze_wolf");
		return true;
	}
}
