using System;
using System.Collections.Generic;

/*Squad Builder
 * 
 *  Build a list of people you're playing with from the Squad Global.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        List<string> list_squad;
        string str_build;
        int int_total;

        //Initializations
        list_squad = CPH.GetGlobalVar<List<string>>("squadCurrent");
        str_build = "/me ";
        int_total = list_squad.Count;

        //If the list has entries...
        if (int_total > 0)
        {
            //... build the message.
            str_build += "hmmMeeting The Q-mander is playing with ";

            //... iterate through the list...
            for (int i = 0; i < int_total; i++)
            {
                //... first/only entry.
                if (i == 0)
                {
                    str_build += list_squad[i] + " ";
                }//if
                 //... last entry.
                else if (i == int_total - 1)
                {
                    str_build += " & " + list_squad[i] + " ";
                }//else if
                 //... subsequent entries.
                else
                {
                    str_build += list_squad[i] + ", ";
                }//else
            }//for
            str_build += "ButtBooty";
        }//if
        else
        {
            str_build += "/me The Q-mander did not add anybody to the squad! LULdata";

        }//else

        //Send message.
        CPH.SendMessage(str_build);

        return true;
    }//Execute()
}//CPHInline