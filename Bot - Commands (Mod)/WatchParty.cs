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
        string str_ri;
        string[] str_src;


        //Initializations
        str_ri = args["rawInput"].ToString();
        str_src = new string[]
        {
            "SS_MidScreen",
            "WatchParty"
        };

        //Update Show
        CPH.SetGlobalVar("ytNow", str_ri);
        return true;
    }//Execute()
}//CPHInline