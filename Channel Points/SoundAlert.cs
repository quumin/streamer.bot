using System;

/*Sound Alert
 * 
 *  Find whichever sound they selected and play it.
 *  LU: 4-nov-2023
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string str_path, str_media, str_redeem, str_msg, str_usr, str_ss;
        string[] str_src, str_reward;
        int int_wait;
        float f_vol;

        //Initializations
        str_path = CPH.GetGlobalVar<string>("qminMediaRoot") + "Channel Points\\";
        str_media = "";
        str_redeem = args["redemptionId"].ToString();
        str_msg = "/me ";
        str_usr = args["user"].ToString();
        str_ss = "SS_Alerts_Text";
        str_src = new string[] { "Username", "Action" };
        str_reward = new string[] { args["rewardId"].ToString(), args["rewardName"].ToString() };
        int_wait = 5000;
        f_vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //See which reward it is...
        switch (str_reward[0])
        {
            //	Ara Ara
            case "24322dd7-f3c4-4771-8703-9bd32fc32128":
                str_media = "Ara Ara";
                str_msg += "WEEBSDETECTED you sicken me @" + str_usr + " WTFF";
                break;
            //	Bold Strategy Cotton	
            case "dc30d223-3653-4116-aa58-a849afc00a52":
                str_media = "Bold Strat";
                str_msg += "PauseChamp @" + str_usr + " KEKWait";
                break;
            //	Emotional Damage
            case "4e2575ff-c878-44e3-b26f-64e5d640ee6b":
                str_media = "Emotional Damage";
                str_msg += "society @" + str_usr + " Sadge";
                break;
            //	Epic Sax Guy
            case "471a6adc-6ac7-4ba9-930c-e59c4645f1eb":
                str_media = "ESG";
                str_msg += "ratJAM @" + str_usr + " rainbowPls";
                break;
            //	Fart - Chalupa
            case "9b73418a-28dc-4ec2-b084-e7f6bd2ab96e":
                str_media = "Fart - Chalupa";
                str_msg += "NOWAY @" + str_usr + " WHAT";
                break;
            //	Fart- Choco Mud
            case "9b79e814-25c7-4b25-9faa-158ae57cc66b":
                str_media = "Fart - CM";
                str_msg += "NOPERS @" + str_usr + " disGUSTING";
                break;
            //	Fart - Sour Patch Kids
            case "477b2117-3ae4-4d5f-aa45-a10f4c08082d":
                str_media = "Fart - SPK";
                str_msg += "Bedge  @" + str_usr + " Wokege";
                break;
            //	Fart - Taco Bell
            case "109ed7c2-59d7-4b70-9375-cf7733752875":
                str_media = "Fart - TB";
                str_msg += "HARAM @" + str_usr + " confusedCat";
                break;
            //	ILU
            case "0f6f4d2b-a7fb-4ffe-ae79-7b7a7bf8282e":
                str_media = "ILU";
                str_msg += "widepeepoHappy Than-Q @" + str_usr + " quuminL";
                break;
            //	Mario Whomp
            case "7a25ffe0-1451-4d03-88be-06afda29643d":
                str_media = "Whomp";
                str_msg += "donowall @" + str_usr + " thinkingJojo";
                break;
            //	My Mom!
            case "7372d380-0e8a-4849-892a-1e70c8bbe6ef":
                str_media = "My Mom";
                str_msg += "NOOOOOOOOOO @" + str_usr + " DataFingerbang";
                break;
            //	Nice Cohg
            case "9a2e6e0e-7bb3-47c8-a97e-3b81f7de1019":
                str_media = "NiceCock";
                str_msg += "widepeepoHappy Than-Q @" + str_usr + " WorfCUM";
                break;
            //	OOF
            case "5bdd6616-cd77-4753-ab5f-ac1361e839d3":
                str_media = "OOF";
                str_msg += "Pepepains @" + str_usr + " PepeHands";
                break;
            //	So Anyways…
            case "fccbf9b2-52c8-42cf-8e60-d15e5eb3c450":
                str_media = "Blasting";
                str_msg += "PepegaAim fr fr @" + str_usr + " PepegaAim";
                break;
            //	The Duke
            case "1d0e8edc-0bbf-4a1d-8288-b5d443f2631d":
                str_media = "Bubblegum";
                str_msg += "gigaQ you right @" + str_usr + " LETSGO";
                break;
            //	The Hub
            case "891f77db-7505-472d-8f9a-bb08ee4f6b83":
                str_media = "Hub";
                str_msg += "Milk Kreygasm @" + str_usr + " BOOBEST";
                break;
            //	Victory Screech!
            case "50ae485e-fb67-4a5f-ae84-ec42188fee9a":
                str_media = "Victory Screech";
                str_msg += "EZ game @" + str_usr + " HYPERCLAP";
                break;
            //	Wehehe
            case "77c45b75-9215-4622-8be8-b4762ff4e3df":
                str_media = "Wehehe";
                str_msg += "peepoGiggles @" + str_usr + " PepeLaugh";
                break;
            //	Whomp Whomp
            case "1892a852-bba0-4beb-8787-9ac649415b98":
                str_media = "Whomp Whomp";
                str_msg += "NOOOO @" + str_usr + " OuttaPocket";
                break;
            //	Yamete Kudasai
            case "2b2d1af1-eac4-4751-9e9c-496b0d361109":
                str_media = "Yamete";
                str_msg += "weebPeepoSmash @" + str_usr + " WEEBSOUT";
                break;
            //	Yes Sirrr
            case "507a97cd-56da-47ef-96a3-3b301769dffc":
                str_media = "Yessir";
                str_msg += "NODDERS yes sir @" + str_usr + " peepoRiot";
                break;
            //	not made yet...
            default:
                CPH.LogWarn("『SOUNDS』: Something went wrong with \'" + str_reward + "\'.");
                CPH.TwitchRedemptionCancel(str_reward[0], str_redeem);
                return true;
                break;

        }//switch

        //Feedback
        CPH.PlaySound(str_path + str_media + ".mp3", f_vol, false);
        CPH.SendMessage(str_msg);
        CPH.TwitchRedemptionFulfill(str_reward[0], str_redeem);
        CPH.ObsSetGdiText(str_ss, str_src[0], str_usr);
        CPH.ObsSetGdiText(str_ss, str_src[1], str_reward[1]);
        CPH.ObsShowSource(str_ss, str_src[0]);
        CPH.ObsShowSource(str_ss, str_src[1]);
        CPH.Wait(int_wait);
        CPH.ObsHideSource(str_ss, str_src[1]);
        CPH.ObsHideSource(str_ss, str_src[0]);

        return true;
    }//Execute()
}//CPHInline