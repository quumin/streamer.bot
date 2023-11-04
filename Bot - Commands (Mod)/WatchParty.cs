using System;

/*Watch Party
 * 
 *  Upload YT videos as links to Stream.
 *  LU: 4-nov-2023
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

        //Update Show
        CPH.SetGlobalVar("qminYouTube", str_ri);
        return true;
    }//Execute()
}//CPHInline