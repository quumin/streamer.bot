using System;

/*Watch Party Post
 * 
 *  Share what I'm watching.
 *  LU: 21-jun-2024
 * 
 */

//Seems deprecated, but I know I will use it later.

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] obSource;
        string url, msgOut;

        //Initializations
        obSource = new string[]
        {
            "SS_MidScreen",
            "WatchParty"
        };
        url = CPH.GetGlobalVar<String>("qminYouTube");
        msgOut = "/me confusedCat The Q-Mander is not watching anything at this moment. BASED";


        //Check Browser Source
        if (CPH.ObsIsSourceVisible(obSource[0], obSource[1]))
        {
            msgOut = $"/me The Q-Mander is watching: {url} FeelsAmazingMan PopcornTime";
        }//if

        CPH.SendMessage(msgOut);
        return true;
    }//Execute()
}//CPHInline