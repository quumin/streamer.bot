using System;
using System.Collections.Generic;
using System.Linq;

/*Qnami Codes
 * 
 *  Play sound effects when the audience triggers the right commands.
 *  LU: 21-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string filePath, mediaOut, commandId, usrName;
        List<string> msgOut;
        int waitTime;
        float vol;

        //Initializations
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot");
        mediaOut = "";
        commandId = args["commandId"].ToString();
        usrName = args["user"].ToString();
        msgOut = new List<string>();
        waitTime = 0;
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //See which reward it is...
        switch (commandId)
        {
            //	Yoshikage Kira
            case "e4a79762-07a3-429a-98f8-48aec6bc5eaf":
                mediaOut = "Jojo_Kira.mp3";
                msgOut.Add("/me \"My name is Yoshikage Kira. I'm 33 years old. My house is in the northeast section of Morioh, where all the villas are, and I am not married. I work as an employee for the Kame Yu department stores, and I get home every day by 8 PM at the latest. I don't smoke, but I occasionally drink. I'm in bed by 11 PM, and make sure I get eight hours of sleep, no matter what.");
                msgOut.Add("/me After having a glass of warm milk and doing about twenty minutes of stretches before going to bed, I usually have no problems sleeping until morning. Just like a baby, I wake up without any fatigue or stress in the morning. I was told there were no issues at my last check-up");
                msgOut.Add("/me I'm trying to explain that I'm a person who wishes to live a very quiet life. I take care not to trouble myself with any enemies, like winning and losing, that would cause me to lose sleep at night. That is how I deal with society, and I know that is what brings me happiness. Although, if I were to fight I wouldn't lose to anyone.\"");
                break;
            //  Bing Chilling
            case "e8fbd54e-2273-4415-bbb2-3cf9bb97876a":
                mediaOut = "BingChilling.mp3";
                msgOut.Add("/me Zǎoshang hǎo zhōngguó xiànzài wǒ yǒu BING CHILLING 🥶🍦 wǒ hěn xǐhuān BING CHILLING 🥶🍦 dànshì sùdù yǔ jīqíng 9 bǐ BING CHILLING 🥶🍦 sùdù yǔ jīqíng sùdù yǔ jīqíng 9 wǒ zuì xǐhuān suǒyǐ…xiànzài shì yīnyuè shíjiān zhǔnbèi 1 2 3 liǎng gè lǐbài yǐhòu sùdù yǔ jīqíng 9 ×3 bùyào wàngjì bùyào cu òguò jìdé qù diànyǐngyuàn kàn sùdù yǔ jīqíng 9 yīn wéi fēicháng hǎo diànyǐng dòngzuò fēicháng hǎo chàbùduō yīyàng BING CHILLING 🥶🍦zàijiàn 🥶🍦");
                break;
            //  KEKW
            case "60cc5a35-5a2c-45a4-8061-5ed4f9969e34":
                mediaOut = "KEKW.mp3";
                break;
            //  Torture Dance
            case "b5000bda-bca9-4770-9937-1e1d0311792c":
                mediaOut = "Jojo_TortureDance.mp3";
                msgOut.Add("/me !showemote jojoPls1");
                msgOut.Add("/me !showemote jojoPls2");
                msgOut.Add("/me !showemote jojoPls3");
                waitTime = 5000;
                break;
            //  BoBW
            case "c7d77eea-2e18-42ff-932c-8007bf4ccc9b":
                mediaOut = "BestOfBothWorlds.mp3";
                msgOut.Add("/me !showemote BOOBEST ");
                msgOut.Add("gachiHYPER gachiGASM gachiBASS");
                break;
            //  Welcome Back!
            case "ef877d16-3b7f-475b-9f38-3293535ed1be":
                mediaOut = "WelcomeBack.mp3";
                msgOut.Add("/me peepoHey Welcome back" + usrName + " gigaQ");
                break;
            //	not made yet...
            default:
                CPH.LogWarn("『SOUNDS』: Something went wrong with \'" + commandId + "\'.");
                return true;
                break;
        }//switch

        //Feedback
        CPH.PlaySound(filePath + mediaOut, vol, false);
        foreach (string s in msgOut)
        {
            CPH.SendMessage(s);
            if (s != msgOut.Last())
            {
                CPH.Wait(waitTime);
            }//if
        }//for each
        return true;
    }//Execute()
}//CPHInline