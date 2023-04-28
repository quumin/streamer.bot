using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		int int_pos, int_ln;
		string[] str_scene, str_filter, str_postfix;
		
		//Initializations
		int_pos = CPH.GetGlobalVar<int>("globalMove");
		int_ln = Int32.Parse(args["lineCount"].ToString());
		str_scene = new string[]
		{
			"SS_KiyoPro_FancyCam",
			"SS_NowPlaying"
		};
        str_scene = new string[]
		{
			"SS_KiyoPro_FancyCam",
			"SS_NowPlaying"
		};
		str_filter = new string[]
		{
			"",
			""
		};
        str_filter = new string[]
		{
            "_NP",
            ""
		};

        //Check Position
        switch (int_pos)
        {
            //	Top Left
            case 1:
				str_filter[0] = "TL";
                str_filter[1] = "TL";
                break;
            //	Top Middle
            case 2:
                str_filter[0] = "TM";
                break;
            //	Top Right
            case 3:
                str_filter[0] = "TR";
                str_filter[1] = "TR";
                break;
            //	Middle Right
            case 4:
                str_filter[0] = "MR";
                break;
            //	Bottom Right
            case 5:
                str_filter[0] = "BR";
                str_filter[1] = "BR";
                break;
            //	Bottom Middle
            case 6:
                str_filter[0] = "BM";
                break;
            //	Bottom Left
            case 7:
                str_filter[0] = "BL";
                str_filter[1] = "BL";
                break;
            //	Middle Left
            case 8:
                str_filter[0] = "ML";
                break;
        }//switch

        //Adjust Now Playing postfix
        str_filter[0] += str_postfix[0];

		//If NP Filter is not just the postfix...
		if (str_filter[0] != str_postfix[0])
		{
			//... update the filter.
			CPH.ObsShowFilter(str_scene[0], str_filter[0]);
		}//if

		//If Snip.txt is empty...
		if (int_ln == 0)
		{
			//... hide Now Playing.
			CPH.ObsHideSource(str_scene[0], str_scene[1]);
			//... move Alerts_Text away from cam.
			str_postfix[1] = "_NP-Gone";
		}//if
		else
		{
			//... show Now Playing.
			CPH.ObsShowSource(str_scene[0], str_scene[1]);
			//... bring Alerts_Text closer to cam.
			str_postfix[1] = "_AT-NP";
		}//else

		//Adjust Alerts Text Postfix
		str_filter[1] += str_postfix[1];

		//If AT Filter is not just the postfix...
		if (str_filter[1] != str_postfix[1])
		{
			//... update the filter.
			CPH.ObsShowFilter(str_scene[0], str_filter[1]);
		}//if

        return true;
    }//Execute()
}//CPHInline