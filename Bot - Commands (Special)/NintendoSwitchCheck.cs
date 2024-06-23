using System;

/*Nintendo Switch Check
 * 
 *  Check if the Capture Card scene is active when somebody asks for my friend code.
 *  LU: 21-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string obScene, msgOut;

        //Initializations
        obScene = CPH.ObsGetCurrentScene();
        msgOut = "/me ";

        //If the current scene is captue card...
        if (obScene == "Game_CC")
        {
            //... send "join" message.
            msgOut += "Prayge Wanna join Q? Pog His Friend Code is: SW-8573-1988-4776";
        }//if
        else
        {
            //... send friend code.
            msgOut += "PepeHands Q is not on his Switch Right now, but PauseChamp his Friend Code is: SW-8573-1988-4776";
        }//else

        //Send message.
        CPH.SendMessage(msgOut);

        return true;
    }//Execute()
}//CPHInline