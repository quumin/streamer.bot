using System;

public class CPHInline
{
	public bool Execute()
	{

		int camState = CPH.GetGlobalVar<int>("globalMove");
		int lines = Int32.Parse(args["lineCount"].ToString());
		string cam_sceneName = "SS_KiyoPro_FancyCam";
		string np_sceneName = "SS_NowPlaying";
		string currentScene = CPH.ObsGetCurrentScene();
		string filterName = "";
		string filterName_AT = "";
		string postFix_AT = "";
		string postFix = "_NP";

		//Adjust Now Playing prefix
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

		//Adjust Now Playing postfix
		filterName += postFix;

		//If filterName is not just the postFix...
		if (filterName != postFix)
		{
			//... update the filter.
			CPH.ObsShowFilter(cam_sceneName, filterName);
		}

		//Adjust Alerts_Text prefix
		switch (camState)
		{
			case 1:
				//TL
				filterName_AT = "TL";
				break;
			case 3:
				//TR
				filterName_AT = "TR";
				break;
			case 5:
				//BR
				filterName_AT = "BR";
				break;
			case 7:
				//BL
				filterName_AT = "BL";
				break;
		}


		//If Snip.txt is empty...
		if (lines == 0)
		{
			//... hide Now Playing.
			CPH.ObsHideSource(cam_sceneName, np_sceneName);
			//... move Alerts_Text away from cam.
			postFix_AT = "_NP-Gone";
		}
		else
		{
			//... show Now Playing.
			CPH.ObsShowSource(cam_sceneName, np_sceneName);
			//... bring Alerts_Text closer to cam.
			postFix_AT = "_AT-NP";
		}

		filterName_AT += postFix_AT;

		//If filterName is not just the postFix...
		if (filterName_AT != postFix_AT)
		{
			//... update the filter.
			CPH.ObsShowFilter(cam_sceneName, filterName_AT);
		}

		return true;
	}
}