using System;

public class CPHInline
{
	public bool Execute()
	{
		string str_path, str_media;
		bool bool_serious = CPH.GetGlobalVar<bool>("seriousMode");

		str_path = "W:\\Streaming\\Media\\Sounds\\";
		str_media = "CoolCoolerCoolest.mp3";

		CPH.SendMessage("Pog Q is an Coolermaster Affiliate PogU https://t.co/OhdqhTMT1l <- Click the link to get some cool deals TricksterWink");

		if (!bool_serious)
		{
			CPH.PlaySound(str_path + str_media, 0.15f, true);
		}//if(!bool_serious)

		return true;
	}
}
