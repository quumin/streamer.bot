using System;

public class CPHInline
{
	public bool Execute()
	{

		int camState = CPH.GetGlobalVar<int>("globalMove");
		string sceneName = "SS_KiyoPro_FancyCam";
		string filterName = "";
		string postFix = "_Busta";

		switch (camState)
		{
			case 1:
				//TL
				filterName = "TL";
				break;
			case 2:
				//TM
				filterName = "TM";
				break;
			case 3:
				//TR
				filterName = "TR";
				break;
			case 4:
				//MR
				filterName = "MR";
				break;
			case 5:
				//BR
				filterName = "BR";
				break;
			case 6:
				//BM
				filterName = "BM";
				break;
			case 7:
				//BL
				filterName = "BL";
				break;
			case 8:
				//ML
				filterName = "ML";
				break;
		}

		filterName += postFix;

		if (filterName != postFix)
		{
			CPH.ObsShowFilter(sceneName, filterName);
			//Run check for Spotify Banner
			CPH.RunActionById("cdb415f2-6a4f-4f4f-954c-f6046fa0e003");
		}

		return true;
	}
}