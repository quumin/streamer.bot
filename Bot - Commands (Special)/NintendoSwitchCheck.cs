using System;

public class CPHInline
{
	public bool Execute()
	{
		string c_scene = CPH.ObsGetCurrentScene();
		if (c_scene == "Game_CC")
		{
			CPH.TwitchAnnounce("Prayge Wanna join Q? Pog His Friend Code is: SW-8573-1988-4776",
				true, "purple");
		}
		else
		{
			CPH.TwitchAnnounce("PepeHands Q is not on his Switch Right now, but PauseChamp his Friend Code is: SW-8573-1988-4776.",
				true, "blue");
		}
		return true;
	}
}
