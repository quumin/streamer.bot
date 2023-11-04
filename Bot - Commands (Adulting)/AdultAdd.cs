using System;

/*AdultAdd
 * 
 *  Update the Adulting Message.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        CPH.SetGlobalVar("qminAdultRemind", args["rawInput"].ToString(), true);
        return true;
    }//Execute()
}//CPHInline