using System;

public class CPHInline
{
    public bool ProcessMessage()
    {
        // Check for messageText variable.
        if (CPH.TryGetArg("messageText", out string messageText))
        {
            // Split the strings of messageText to handle on various platforms.
            string[] lines = messageText.Split(new[] { "|d|" }, StringSplitOptions.RemoveEmptyEntries);
            // Log # lines.
            CPH.LogInfo($"[Shared Message Handler] Processing {lines.Length} lines.");
            CPH.TryGetArg("commandSource", out string commandSource);
            // Iterate through each line.
            foreach (var line in lines)
            {
                // Check the command source for which platform the message comes from, default to all.
                switch (commandSource)
                {
                    case "twitch":
                        CPH.TwitchAnnounce(line, true, "purple", false);
                        break;
                    case "youtube":
                        CPH.SendYouTubeMessageToLatestMonitored(line, true, false);
                        break;
                    default:
                        CPH.TwitchAnnounce(line, true, "purple", false);
                        CPH.SendYouTubeMessageToLatestMonitored(line, true, false);
                        break;
                } //switch

                CPH.LogVerbose($"[Shared Message Handler] Message is: \"{line}\".");
                CPH.Wait(50);
            } //foreach
        } //if
        else
        {
            CPH.LogError("[Shared Message Handler] No messageText found!");
        } //else

        return true;
    } //ProcessMessage

    public bool Execute()
    {
        return true;
    }
} //CPHInline
