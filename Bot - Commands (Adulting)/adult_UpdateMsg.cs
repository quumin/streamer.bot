using System;

/*Adulting - Update Message
 * 
 *  Update (or clear) the Adulting Message.
 *  LU: 10-oct-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations & Initializations
        string qminAdultRemind = "qminAdultRemind";
        string rawInput = args["rawInput"].ToString();
        string msgOut = "/me dataSMUG ";

        //If the input is empty...
        if (string.IsNullOrEmpty(rawInput))
        {
            //.... then unset the global.
            CPH.UnsetGlobalVar(qminAdultRemind);
            msgOut += "gonna remind you to stay lubricated, sir.";
        }//if
        else
        {
            //... otherwise, update the global with the input.
            CPH.SetGlobalVar(qminAdultRemind, rawInput);
            msgOut += $"will remind you to \"{rawInput}\", sir.";
        }//else

        //Send output message.
        CPH.SendMessage(msgOut);

        return true;
    }//Execute()
}//CPHInline