using System;

/*AdultAdd
 * 
 *  Update the Adulting Message.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        CPH.SetGlobalVar("adultRemind", args["rawInput"].ToString(), true);
        return true;
    }//Execute()
}//CPHInline