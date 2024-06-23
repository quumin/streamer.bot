using System;

/*Watch Party
 * 
 *  Upload YT videos as links to Stream.
 *  LU: 21-jun-2024
 * 
 */

//Seems deprecated, but I know I will use it later.

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string rawInput;


        //Initializations
        rawInput = args["rawInput"].ToString();

        //Update Show
        CPH.SetGlobalVar("qminYouTube", rawInput);
        return true;
    }//Execute()
}//CPHInline