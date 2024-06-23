using System;

/*Sound Alert
 * 
 *  Find whichever sound they selected and play it.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string filePath, mediaOut, redemptionId, msgOut, usrName, obScene;
        string[] obSource, rewardInfo;
        int waitTime;
        float vol;

        //Initializations
        filePath = CPH.GetGlobalVar<string>("qminMediaRoot") + "Channel Points\\";
        mediaOut = "";
        redemptionId = args["redemptionId"].ToString();
        msgOut = "/me ";
        usrName = args["user"].ToString();
        obScene = "SS_Alerts_Text";
        obSource = new string[] { "Username", "Action" };
        rewardInfo = new string[] { args["rewardId"].ToString(), args["rewardName"].ToString() };
        waitTime = 5000;
        vol = CPH.GetGlobalVar<float>("qminMediaVolume");

        //See which reward it is...
        switch (rewardInfo[0])
        {
            //	Ara Ara
            case "24322dd7-f3c4-4771-8703-9bd32fc32128":
                mediaOut = "Ara Ara";
                msgOut += "WEEBSDETECTED you sicken me @" + usrName + " WTFF";
                break;
            //	Bold Strategy Cotton	
            case "dc30d223-3653-4116-aa58-a849afc00a52":
                mediaOut = "Bold Strat";
                msgOut += "PauseChamp @" + usrName + " KEKWait";
                break;
            //	Emotional Damage
            case "4e2575ff-c878-44e3-b26f-64e5d640ee6b":
                mediaOut = "Emotional Damage";
                msgOut += "society @" + usrName + " Sadge";
                break;
            //	Epic Sax Guy
            case "471a6adc-6ac7-4ba9-930c-e59c4645f1eb":
                mediaOut = "ESG";
                msgOut += "ratJAM @" + usrName + " rainbowPls";
                break;
            //	Fart - Chalupa
            case "9b73418a-28dc-4ec2-b084-e7f6bd2ab96e":
                mediaOut = "Fart - Chalupa";
                msgOut += "NOWAY @" + usrName + " WHAT";
                break;
            //	Fart- Choco Mud
            case "9b79e814-25c7-4b25-9faa-158ae57cc66b":
                mediaOut = "Fart - CM";
                msgOut += "NOPERS @" + usrName + " disGUSTING";
                break;
            //	Fart - Sour Patch Kids
            case "477b2117-3ae4-4d5f-aa45-a10f4c08082d":
                mediaOut = "Fart - SPK";
                msgOut += "Bedge  @" + usrName + " Wokege";
                break;
            //	Fart - Taco Bell
            case "109ed7c2-59d7-4b70-9375-cf7733752875":
                mediaOut = "Fart - TB";
                msgOut += "HARAM @" + usrName + " confusedCat";
                break;
            //	ILU
            case "0f6f4d2b-a7fb-4ffe-ae79-7b7a7bf8282e":
                mediaOut = "ILU";
                msgOut += "widepeepoHappy Than-Q @" + usrName + " quuminL";
                break;
            //	Mario Whomp
            case "7a25ffe0-1451-4d03-88be-06afda29643d":
                mediaOut = "Whomp";
                msgOut += "donowall @" + usrName + " thinkingJojo";
                break;
            //	My Mom!
            case "7372d380-0e8a-4849-892a-1e70c8bbe6ef":
                mediaOut = "My Mom";
                msgOut += "NOOOOOOOOOO @" + usrName + " DataFingerbang";
                break;
            //	Nice Cohg
            case "9a2e6e0e-7bb3-47c8-a97e-3b81f7de1019":
                mediaOut = "NiceCock";
                msgOut += "widepeepoHappy Than-Q @" + usrName + " WorfCUM";
                break;
            //	OOF
            case "5bdd6616-cd77-4753-ab5f-ac1361e839d3":
                mediaOut = "OOF";
                msgOut += "Pepepains @" + usrName + " PepeHands";
                break;
            //	So Anyways…
            case "fccbf9b2-52c8-42cf-8e60-d15e5eb3c450":
                mediaOut = "Blasting";
                msgOut += "PepegaAim fr fr @" + usrName + " PepegaAim";
                break;
            //	The Duke
            case "1d0e8edc-0bbf-4a1d-8288-b5d443f2631d":
                mediaOut = "Bubblegum";
                msgOut += "gigaQ you right @" + usrName + " LETSGO";
                break;
            //	The Hub
            case "891f77db-7505-472d-8f9a-bb08ee4f6b83":
                mediaOut = "Hub";
                msgOut += "Milk Kreygasm @" + usrName + " BOOBEST";
                break;
            //	Victory Screech!
            case "50ae485e-fb67-4a5f-ae84-ec42188fee9a":
                mediaOut = "Victory Screech";
                msgOut += "EZ game @" + usrName + " HYPERCLAP";
                break;
            //	Wehehe
            case "77c45b75-9215-4622-8be8-b4762ff4e3df":
                mediaOut = "Wehehe";
                msgOut += "peepoGiggles @" + usrName + " PepeLaugh";
                break;
            //	Whomp Whomp
            case "1892a852-bba0-4beb-8787-9ac649415b98":
                mediaOut = "Whomp Whomp";
                msgOut += "NOOOO @" + usrName + " OuttaPocket";
                break;
            //	Yamete Kudasai
            case "2b2d1af1-eac4-4751-9e9c-496b0d361109":
                mediaOut = "Yamete";
                msgOut += "weebPeepoSmash @" + usrName + " WEEBSOUT";
                break;
            //	Yes Sirrr
            case "507a97cd-56da-47ef-96a3-3b301769dffc":
                mediaOut = "Yessir";
                msgOut += "NODDERS yes sir @" + usrName + " peepoRiot";
                break;
            //	not made yet...
            default:
                CPH.LogWarn("『SOUNDS』: Something went wrong with \'" + rewardInfo + "\'.");
                CPH.TwitchRedemptionCancel(rewardInfo[0], redemptionId);
                return true;
                break;

        }//switch

        //Feedback
        CPH.PlaySound(filePath + mediaOut + ".mp3", vol, false);
        CPH.SendMessage(msgOut);
        CPH.TwitchRedemptionFulfill(rewardInfo[0], redemptionId);
        CPH.ObsSetGdiText(obScene, obSource[0], usrName);
        CPH.ObsSetGdiText(obScene, obSource[1], rewardInfo[1]);
        CPH.ObsShowSource(obScene, obSource[0]);
        CPH.ObsShowSource(obScene, obSource[1]);
        CPH.Wait(waitTime);
        CPH.ObsHideSource(obScene, obSource[1]);
        CPH.ObsHideSource(obScene, obSource[0]);

        return true;
    }//Execute()
}//CPHInline