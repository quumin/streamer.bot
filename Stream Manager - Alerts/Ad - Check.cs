using System;

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string[] str_msg;
		string str_scene;
		int int_msgs;
		bool bool_type;

		//Initializations
		str_msg = new string[]
		{
			"/me ",
            "/me PrimeMe You can also subscribe for free with a Twitch Prime account owoUwu", 
            "/me ContentCheck If you want your clips to be seen in the BRB Screen, then Highlight Q with ALT+X or click the highlight button! peepoChat"
        };
		int_msgs = 1;
		bool_type = Convert.ToBoolean(args["adScheduled"]);

		//If the ad is scheduled...
		if (bool_type)
		{
			//... send the normal message.
			str_msg[0] += "Corpa The Q-mander is running scheduled ads, if you'd like an ad-free viewing experience NOTED  then check out the Subscribe button below. Saved";
		}//if
		else
		{
			//... send the insult message.
			str_msg[0] += "WeirdChamping The Q-mander is now running ads instead of providing content CaughtIn4K , if you'd like an ad-free viewing experience DataFingerbang then check out the Subscribe button below. PogYou";
		}//else

		return true;
	}//Execute()
}//CPHInline