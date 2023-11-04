using System;
using System.Collections.Generic;
using System.Linq;

/*Qnami Codes
 * 
 *  Play sound effects when the audience triggers the right commands.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_path, str_media, str_cmd, str_usr;
        List<string> list_msg;
        int int_wait;
        float f_vol;

        //Initializations
        str_path = CPH.GetGlobalVar<string>("qminMediaRoot");
        str_media = "";
        str_cmd = args["commandId"].ToString();
        str_usr = args["user"].ToString();
        list_msg = new List<string>();
        int_wait = 0;
        f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //See which reward it is...
        switch (str_cmd)
        {
            //	Yoshikage Kira
            case "e4a79762-07a3-429a-98f8-48aec6bc5eaf":
                str_media = "Jojo_Kira.mp3";
                list_msg.Add("/me \"My name is Yoshikage Kira. I'm 33 years old. My house is in the northeast section of Morioh, where all the villas are, and I am not married. I work as an employee for the Kame Yu department stores, and I get home every day by 8 PM at the latest. I don't smoke, but I occasionally drink. I'm in bed by 11 PM, and make sure I get eight hours of sleep, no matter what.");
                list_msg.Add("/me After having a glass of warm milk and doing about twenty minutes of stretches before going to bed, I usually have no problems sleeping until morning. Just like a baby, I wake up without any fatigue or stress in the morning. I was told there were no issues at my last check-up");
                list_msg.Add("/me I'm trying to explain that I'm a person who wishes to live a very quiet life. I take care not to trouble myself with any enemies, like winning and losing, that would cause me to lose sleep at night. That is how I deal with society, and I know that is what brings me happiness. Although, if I were to fight I wouldn't lose to anyone.\"");
                break;
            //  Bing Chilling
            case "e8fbd54e-2273-4415-bbb2-3cf9bb97876a":
                str_media = "BingChilling.mp3";
                list_msg.Add("/me Zǎoshang hǎo zhōngguó xiànzài wǒ yǒu BING CHILLING 🥶🍦 wǒ hěn xǐhuān BING CHILLING 🥶🍦 dànshì sùdù yǔ jīqíng 9 bǐ BING CHILLING 🥶🍦 sùdù yǔ jīqíng sùdù yǔ jīqíng 9 wǒ zuì xǐhuān suǒyǐ…xiànzài shì yīnyuè shíjiān zhǔnbèi 1 2 3 liǎng gè lǐbài yǐhòu sùdù yǔ jīqíng 9 ×3 bùyào wàngjì bùyào cu òguò jìdé qù diànyǐngyuàn kàn sùdù yǔ jīqíng 9 yīn wéi fēicháng hǎo diànyǐng dòngzuò fēicháng hǎo chàbùduō yīyàng BING CHILLING 🥶🍦zàijiàn 🥶🍦");
                break;
            //  KEKW
            case "60cc5a35-5a2c-45a4-8061-5ed4f9969e34":
                str_media = "KEKW.mp3";
                break;
            //  Torture Dance
            case "b5000bda-bca9-4770-9937-1e1d0311792c":
                str_media = "Jojo_TortureDance.mp3";
                list_msg.Add("/me !showemote jojoPls1");
                list_msg.Add("/me !showemote jojoPls2");
                list_msg.Add("/me !showemote jojoPls3");
                int_wait = 5000;
                break;
            //  BoBW
            case "c7d77eea-2e18-42ff-932c-8007bf4ccc9b":
                str_media = "BestOfBothWorlds.mp3";
                list_msg.Add("/me !showemote BOOBEST ");
                list_msg.Add("gachiHYPER gachiGASM gachiBASS");
                break;
            //  Welcome Back!
            case "ef877d16-3b7f-475b-9f38-3293535ed1be":
                str_media = "WelcomeBack.mp3";
                list_msg.Add("/me peepoHey Welcome back" + str_usr + " gigaQ");
                break;
            //	not made yet...
            default:
                CPH.LogWarn("『SOUNDS』: Something went wrong with \'" + str_cmd + "\'.");
                return true;
                break;
        }//switch

        //Feedback
        CPH.PlaySound(str_path + str_media, f_vol, false);
        foreach (string s in list_msg)
        {
            CPH.SendMessage(s);
            if (s != list_msg.Last())
            {
                CPH.Wait(int_wait);
            }//if
        }//for each
        return true;
    }//Execute()
}//CPHInline