using System;

/*Ad Check
 * 
 *  Manage what type of ads are running.
 *  LU: 29-mar-2025
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] msgOut;
        string str_prime;
        int int_msgs, adTime, waitTime;
        bool bool_type;

        //Initializations
        msgOut = new string[3];
        str_prime = "/me PrimeMe You can also subscribe for free with a Twitch Prime account owoUwu";
        adTime = 0;
        waitTime = 500;
        int_msgs = 2;
        try
        {
            //	to get adScheduled argument.
            bool_type = Convert.ToBoolean(args["adScheduled"]);
            adTime = Convert.ToInt32("[adLengthMs]");
        }//try
        catch (Exception e)
        {
            //	otherwise debug.
            bool_type = false;
        }//catch

        //If the ad is scheduled...
        if (bool_type)
        {
            //... send the normal message.
            msgOut[0] = "/me Corpa The Q-mander is running scheduled ads, if you'd like an ad-free viewing experience NOTED  then check out the Subscribe button below. Saved";
            msgOut[1] = str_prime;
        }//if
        else
        {
            //... send the insult message.
            msgOut[0] = "/me WeirdChamping The Q-mander is now running ads instead of providing content CaughtIn4K , if you'd like an ad-free viewing experience DataFingerbang then check out the Subscribe button below. PogYou";
            msgOut[1] = str_prime;
            msgOut[2] = "/me ContentCheck If you want your clips to be seen in the BRB Screen, then Highlight Q with ALT+X or click the highlight button! peepoChat";
            int_msgs = 3;
        }//else

        //Send each next line.
        for (int i = 0; i < int_msgs; i++)
        {
            CPH.Wait(waitTime);
            CPH.SendMessage(msgOut[i]);
        }//for

        CPH.Wait(adTime - (int_msgs) * waitTime);
        CPH.SendMessage("Ads are all done Qmander, please resume your humorous escapades Kappa");

        return true;
    }//Execute()
}//CPHInline