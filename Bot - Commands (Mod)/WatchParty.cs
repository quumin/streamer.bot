using System;

/*Watch Party
 * 
 *  Upload YT videos as links to Stream.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_ri, str_url, str_id;
        string[] str_rawUrl, str_src;


        //Initializations
        str_ri = args["rawInput"].ToString();
        str_rawUrl = str_ri.Split(new[] { "?v=" }, StringSplitOptions.None);
        str_src = new string[]
        {
            "SS_MidScreen",
            "WatchParty"
        };
        str_id = str_rawUrl[1];
        str_url = "https://www.youtube.com/embed/" + str_id + "?autostart=1&controls=0";


        //Update Browser Source and Show
        CPH.ObsSetBrowserSource(str_src[0], str_src[1], str_url);
        CPH.ObsShowSource(str_src[0], str_src[1]);
        CPH.SetGlobalVar("ytNow", str_ri);
        return true;
    }//Execute()
}//CPHInline