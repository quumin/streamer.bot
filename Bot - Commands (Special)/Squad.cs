using System;
using System.Collections.Generic;

/*Squad Builder
 * 
 *  Build a list of people you're playing with from the Squad Global.
 *  LU: 07-oct-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        List<string> squadCurrent;
        string squadAdd;
        int squadCount;

        //Initializations
        squadCurrent = CPH.GetGlobalVar<List<string>>("qminSquadCurrent");
        squadAdd = "/me ";
        squadCount = squadCurrent.Count;
        CPH.LogInfo($"『SPECIAL』 Squad Count: {squadCount}");

        //If the list has entries...
        if (squadCount > 0)
        {
            //... build the message.
            squadAdd += "hmmMeeting The Q-mander is playing with ";

            //... iterate through the list...
            for (int i = 0; i < squadCount; i++)
            {
                //... first/only entry.
                if (i == 0)
                {
                    squadAdd += $"{squadCurrent[i]}";
                }//if
                 //... last entry.
                else if (i == squadCount - 1)
                {
                    //... oxford comma if 1 > squadCount >= 3.
                    if (squadCount != 2)
                    {
                        squadAdd += ", ";
                    }
                    squadAdd += $" & {squadCurrent[i]}";
                }//else if
                 //... subsequent entries.
                else
                {
                    squadAdd += $", {squadCurrent[i]}";
                }//else
            }//for
            squadAdd += " ButtBooty";
        }//if
        else
        {
            squadAdd += "/me The Q-mander did not add anybody to the squad! LULdata";

        }//else

        //Send message.
        CPH.SendMessage(squadAdd);

        return true;
    }//Execute()
}//CPHInline