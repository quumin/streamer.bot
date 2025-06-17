using System;
using System.IO;
using System.Collections.Generic;

/*Games - Fallout Change Game
 * 
 * Select and change to the game that was chosen on the randomizer.
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        int nextFalloutId = CPH.GetGlobalVar<int>("qminNextFalloutId");
        CPH.SetChannelGameById($"{nextFalloutId}");

        return true;
    }//Execute()
}//CPHInline