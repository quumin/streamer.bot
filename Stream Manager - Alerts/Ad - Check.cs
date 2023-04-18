using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		bool bool_type;

		//Initializations
		bool_type = Convert.ToBoolean(args["adScheduled"]);

		//If the ad is scheduled...
		if (bool_type)
		{
			//... send the normal message.
			CPH.TwitchAnnounce("Corpa The Q-mander is running scheduled ads, if you'd like an ad-free viewing experience NOTED  then check out the Subscribe button below. Saved",
				true, "purple");
		}//if(bool_type)
		else
		{
			//... send the insult message.
			CPH.TwitchAnnounce("WeirdChamping The Q-mander is now running ads instead of providing content CaughtIn4K , if you'd like an ad-free viewing experience DataFingerbang then check out the Subscribe button below. PogYou",
				true, "purple");
			CPH.TwitchAnnounce("ContentCheck If you want your clips to be seen in the BRB Screen, then Highlight Q with ALT+X or click the highlight button! peepoChat",
				true, "purple");
		}//else

		//Send the Prime reminder.
		CPH.TwitchAnnounce("PrimeMe You can also subscribe for free with a Twitch Prime account owoUwu",
			true, "blue");

		return true;
	}
}
