using System;

public class CPHInline
{
	public bool Execute()
	{
		string str_path, str_media;
		bool bool_serious = CPH.GetGlobalVar<bool>("seriousMode");

		str_path = "W:\\Streaming\\Media\\Sounds\\";
		str_media = "Whomp.mp3";

		CPH.SendMessage("@bobotucci has been Q's best bud for 16 years. Than-Q for coming by, Q always appreciates it quuminL");

		if (!bool_serious)
		{
			CPH.PlaySound(str_path + str_media, 0.15f, true);
		}//if(!bool_serious)

		CPH.DisableAction("bobotucci");
		return true;
	}
}
