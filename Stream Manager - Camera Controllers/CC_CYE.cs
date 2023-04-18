using System;

public class CPHInline
{
	public bool Execute()
	{

		int camState = CPH.GetGlobalVar<int>("globalMove");
		string sceneName = "SS_KiyoPro_FancyCam";
		string filterName = "";
		string postFix = "_CYE";

		switch (camState)
		{
			case 1:
				//TL
				filterName = "L";
				break;
			case 2:
				//TM
				filterName = "L";
				break;
			case 3:
				//TR
				filterName = "R";
				break;
			case 4:
				//MR
				filterName = "R";
				break;
			case 5:
				//BR
				filterName = "R";
				break;
			case 6:
				//BM
				filterName = "R";
				break;
			case 7:
				//BL
				filterName = "L";
				break;
			case 8:
				//ML
				filterName = "L";
				break;
		}

		filterName += postFix;

		if (filterName != postFix)
		{
			CPH.ObsShowFilter(sceneName, filterName);
		}

		return true;
	}
}