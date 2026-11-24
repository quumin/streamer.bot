using System;

/*Scene Checker
 * 
 *	Check which scene we're currently in and make adjustments.
 *  LU: 23-jun-2024
 * 
 */

public class CPHInline
{
    public bool Execute()
    {
        //Declarations
        string obScene, obSubScene, lastScene;
        bool rollCredits;
        int waitTime, commercialTime;

        //Initializations
        obScene = CPH.ObsGetCurrentScene();
        obSubScene = "SS_MidScreen";
        lastScene = CPH.GetGlobalVar<string>("qminLastScene", false) ?? "";
        rollCredits = false;
        waitTime = 12000;
        commercialTime = 180;
        CPH.LogDebug($"[SCENE] - {obScene}");

        //OBS can refire scene events (transitions, StreamState setting the scene); only act on an actual change.
        if (lastScene == obScene)
        {
            return true;
        }//if
        CPH.SetGlobalVar("qminLastScene", obScene, false);

        switch (obScene)
        {
            //	OVERLAYS

            case "Chat":
                break;
            case "Starter":
            case "Be Right Back":
                //If I'm live...
                if (CPH.ObsIsStreaming())
                {
                    //... run 3 mins of commercial.
                    CPH.TwitchRunCommercial(commercialTime);
                }//if
                break;
            case "Ender":
                rollCredits = true;
                break;
            //	CONTENT
            case "Game_CC":
            case "ScreenShare":
            case "PC_Game":
                CPH.RunAction("Cam Controller");
                break;

            // OTHER
            default:
                return true;
        }//switch

        //Handle Credits
        CPH.ObsSetSourceVisibility(obSubScene, "Credits", rollCredits);

        //If I'm live...
        if (CPH.ObsIsStreaming())
        {
            //... create a marker.
            CPH.CreateStreamMarker($"Scene changed to {obScene}.");
        }//if

        return true;
    }//Execute()
}//CPHInline