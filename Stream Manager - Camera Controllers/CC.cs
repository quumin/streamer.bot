using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		int int_pos;
        string str_scene, str_filter, str_postfix;

        //Initializations
        int_pos = CPH.GetGlobalVar<int>("globalMove");
		str_scene = "SS_KiyoPro_FancyCam";
		str_filter = "";
		str_postfix = "_Busta";

		//Check Position
		switch (int_pos)
		{
			//	Top Left
			case 1:
				str_filter = "TL";
				break;
			//	Top Middle
			case 2:
				str_filter = "TM";
				break;
            //	Top Right
            case 3:
				str_filter = "TR";
				break;
            //	Middle Right
            case 4:
				str_filter = "MR";
				break;
            //	Bottom Right
            case 5:
				str_filter = "BR";
				break;
            //	Bottom Middle
            case 6:
				str_filter = "BM";
				break;
            //	Bottom Left
            case 7:
				str_filter = "BL";
				break;
			//	Middle Left
			case 8:
				str_filter = "ML";
				break;
		}//switch

		str_filter += str_postfix;

		//If the filter exists...
		if (str_filter != str_postfix)
		{
			//... show it.
			CPH.ObsShowFilter(str_scene, str_filter);
			//... run the Spotify checker to move Now Playing.
			CPH.RunAction("Cam Controller - Now Playing");
		}//if

		return true;
	}//Execute()
}//CPHInline