using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string str_scene, str_msg;
		
		//Initializations
		str_scene = CPH.ObsGetCurrentScene();
		str_msg = "/me ";
		
		//If the current scene is captue card...
		if (str_scene == "Game_CC")
		{
			//... send "join" message.
			str_msg += "Prayge Wanna join Q? Pog His Friend Code is: SW-8573-1988-4776";
		}//if
		else
		{
			//... send friend code.
			str_msg += "PepeHands Q is not on his Switch Right now, but PauseChamp his Friend Code is: SW-8573-1988-4776.";
		}//else

        //Send message.
        CPH.SendMessage(str_msg);

        return true;
	}//Execute()
}//CPHInline