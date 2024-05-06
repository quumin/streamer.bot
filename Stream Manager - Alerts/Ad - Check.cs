using System;

/*Ad Check
 * 
 *  Manage what type of ads are running.
 *  LU: 06-may-2024
 *  
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string[] str_msg;
        string str_prime;
        int int_msgs;
        bool bool_type;

        //Initializations
        str_msg = new string[3];
        str_prime = "/me PrimeMe You can also subscribe for free with a Twitch Prime account owoUwu";
        int_msgs = 2;
        try
        {
            //	to get adScheduled argument.
            bool_type = Convert.ToBoolean(args["adScheduled"]);
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
            str_msg[0] = "/me Corpa The Q-mander is running scheduled ads, if you'd like an ad-free viewing experience NOTED  then check out the Subscribe button below. Saved";
            str_msg[1] = str_prime;
        }//if
        else
        {
            //... send the insult message.
            str_msg[0] = "/me WeirdChamping The Q-mander is now running ads instead of providing content CaughtIn4K , if you'd like an ad-free viewing experience DataFingerbang then check out the Subscribe button below. PogYou";
            str_msg[1] = str_prime;
            str_msg[2] = "/me ContentCheck If you want your clips to be seen in the BRB Screen, then Highlight Q with ALT+X or click the highlight button! peepoChat";
            int_msgs = 3;
        }//else

        //Send each next line.
        for (int i = 0; i < int_msgs; i++)
        {
            CPH.Wait(500);
            CPH.SendMessage(str_msg[i]);
        }//for

        return true;
    }//Execute()
}//CPHInline