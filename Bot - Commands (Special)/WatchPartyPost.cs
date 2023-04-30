using System;

/*Watch Party Post
 * 
 *  Share what I'm watching.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_src;
        string str_url, str_msg;

        //Initializations
        str_src = new string[]
        {
            "SS_MidScreen",
            "WatchParty"
        };
        str_url = CPH.GetGlobalVar<String>("ytNow");
        str_msg = "/me confusedCat The Q-Mander is not watching anything at this moment. BASED";


        //Check Browser Source
        if (CPH.ObsIsSourceVisible(str_src[0], str_src[0]))
        {
            str_msg = "/me The Q-Mander is watching: " + str_url + " FeelsAmazingMan PopcornTime";
        }//if

        CPH.SendMessage(str_msg);
        return true;
    }//Execute()
}//CPHInline