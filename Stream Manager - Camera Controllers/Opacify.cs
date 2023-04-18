using System;

public class CPHInline
{
	public bool Execute()
	{

		int opState = CPH.GetGlobalVar<int>("globalOpacity");
		string sceneName = "SS_KiyoPro_FancyCam";
		string sourceName = "Camera";
		string sourceName_SB = "Streamboss";
		string filterName = "";
		string postFix = "_Opacity";

		CPH.ObsHideFilter(sceneName, sourceName, "25" + postFix);
		CPH.ObsHideFilter(sceneName, sourceName, "50" + postFix);
		CPH.ObsHideFilter(sceneName, sourceName, "75" + postFix);
		CPH.ObsHideFilter(sceneName, sourceName_SB, "25" + postFix);
		CPH.ObsHideFilter(sceneName, sourceName_SB, "50" + postFix);
		CPH.ObsHideFilter(sceneName, sourceName_SB, "75" + postFix);

		switch (opState)
		{
			case 1:
				//TL
				filterName = "25";
				break;
			case 2:
				//TM
				filterName = "50";
				break;
			case 3:
				//TR
				filterName = "75";
				break;
		}

		filterName += postFix;

		if (filterName != postFix)
		{
			CPH.ObsShowFilter(sceneName, sourceName, filterName);
			CPH.ObsShowFilter(sceneName, sourceName_SB, filterName);
		}

		return true;
	}
}