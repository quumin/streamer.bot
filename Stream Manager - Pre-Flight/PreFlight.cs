using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/*Pre-Flight Check
 *
 *  Pre-stream sanity checks for OBS. Currently checks audio routing:
 *  capture sources falling back to the "Default" device after a
 *  replug, driver update or Windows default change.
 *
 *  Two methods, bind each with a \"C# Method\" sub-action:
 *    - Execute()      -> Action \"Pre-flight - Run Audio Check\"
 *                        Run this (hotkey/command/Stream Deck) BEFORE going live.
 *    - ExecuteLearn() -> Action \"Pre-flight - Learn Audio Config\"
 *                        Run once, while all OBS audio is set up CORRECTLY.
 *                        Re-run it any time you intentionally change devices.
 *
 *  Wrong device_ids are auto-fixed via SetInputSettings and verified.
 *  Anything that cannot be fixed (device unplugged, OBS unreachable)
 *  is escalated as a log error + chat alert (chat only when live).
 *  LU: 24-nov-2025
 *
 */

public class CPHInline
{
    //Globals used by both methods. Config is persistent, result is not.
    private const string VarConfig = "qminAudioConfig";
    private const string VarResult = "qminPreflightResult";

    public bool Execute()
    {
        //Declarations
        JObject config, resp;
        JProperty prop;
        string name, expDevice, actDevice, fixedList, failedList, cleanList, msgOut;
        int fixedCount, failCount;
        bool isLive;

        //Initializations
        fixedList = "";
        failedList = "";
        cleanList = "";
        fixedCount = 0;
        failCount = 0;
        msgOut = "/me 『PRE-FLIGHT』 ";
        isLive = false;

        //Nothing to check without OBS
        if (!CPH.ObsIsConnected())
        {
            CPH.LogError("『PRE-FLIGHT』: OBS is not connected, audio check aborted!");
            CPH.SetGlobalVar(VarResult, "FAILED|OBS not connected", false);
            return true;
        }//if

        config = ReadConfig();
        if (config == null || config.Count == 0)
        {
            CPH.LogError("『PRE-FLIGHT』: No audio config found! Run \'Pre-flight - Learn Audio Config\' first, while OBS audio is set up correctly.");
            CPH.SetGlobalVar(VarResult, "FAILED|No config", false);
            return true;
        }//if

        //Run above the chat feedback, so fixes happen regardless of anything below.
        isLive = CPH.ObsIsStreaming();

        //Check every learned source
        foreach (JProperty p in config.Properties())
        {
            prop = p;
            name = prop.Name;
            expDevice = (string)prop.Value["device_id"];

            //Fetch current settings
            resp = ObsRequest("GetInputSettings", new JObject { ["inputName"] = name });
            if (!TryGetDevice(resp, out actDevice))
            {
                //Source gone, renamed or unreachable -> cannot auto-fix this.
                failedList += name + ", ";
                failCount++;
                CPH.LogError($"『PRE-FLIGHT』: Could not read \'{name}\' from OBS. Was it renamed or removed?");
                continue;
            }//if

            //Device as expected -> nothing to do.
            if (string.Equals(expDevice, actDevice, StringComparison.OrdinalIgnoreCase))
            {
                cleanList += name + ", ";
                continue;
            }//if

            //Device fell back to a wrong one -> attempt auto-fix.
            CPH.LogInfo($"『PRE-FLIGHT』: \'{name}\' is on \'{actDevice}\', expected \'{expDevice}\'. Attempting auto-fix...");
            if (FixDevice(name, expDevice))
            {
                fixedList += name + ", ";
                fixedCount++;
                CPH.LogInfo($"『PRE-FLIGHT』: \'{name}\' re-routed to \'{expDevice}\' and verified.");
            }
            else
            {
                failedList += name + ", ";
                failCount++;
                CPH.LogError($"『PRE-FLIGHT』: \'{name}\' could NOT be fixed! Target device may be unplugged or renamed. Check OBS manually.");
            }//if
        }//foreach

        //Trim trailing ", " for reporting
        fixedList = TrimList(fixedList);
        failedList = TrimList(failedList);
        cleanList = TrimList(cleanList);

        //Report result
        if (failCount > 0)
        {
            CPH.SetGlobalVar(VarResult, $"FAILED|{failedList}", false);
            CPH.LogWarn($"『PRE-FLIGHT』: {failCount} source(s) unfixable: {failedList}");
            msgOut += $"Serverescrewup: {failCount} audio source(s) could NOT be fixed: {failedList} Check OBS!";
        }
        else if (fixedCount > 0)
        {
            CPH.SetGlobalVar(VarResult, $"FIXED|{fixedList}", false);
            CPH.LogInfo($"『PRE-FLIGHT』: Auto-fixed {fixedCount} source(s): {fixedList}. Clean: {cleanList}");
            msgOut += $"Serverescrewup: caught and re-routed {fixedList}. All good now! DataFingerbang";
        }
        else
        {
            CPH.SetGlobalVar(VarResult, "CLEAN", false);
            CPH.LogInfo($"『PRE-FLIGHT』: All audio sources clean: {cleanList}");
            msgOut += "All audio channels verified Q-Mander. Serverescrewup avoided! DataFingerbang";
        }//if

        //Failures always go to chat (fallback alert); fixes and clean runs only when live.
        if (failCount > 0 || isLive)
        {
            CPH.SendMessage(msgOut);
        }//if

        return true;
    }//Execute()

    public bool ExecuteLearn()
    {
        //Declarations
        JObject config, resp;
        JArray inputs;
        JToken input;
        string name, deviceId;
        List<string> learned;

        //Initializations
        config = new JObject();
        learned = new List<string>();

        if (!CPH.ObsIsConnected())
        {
            CPH.LogError("『PRE-FLIGHT』: OBS is not connected, learning aborted!");
            return true;
        }//if

        //Grab every audio capture source OBS knows about (inputs + outputs + global channels).
        resp = ObsRequest("GetInputList", new JObject());
        if (resp == null || resp["responseData"] == null || resp["responseData"]["inputs"] == null)
        {
            CPH.LogError("『PRE-FLIGHT』: Could not read the input list from OBS!");
            return true;
        }//if

        inputs = (JArray)resp["responseData"]["inputs"];
        foreach (JToken t in inputs)
        {
            input = t;
            if (input["inputKind"] == null || input["inputName"] == null)
            {
                continue;
            }//if

            //wasapi_input_capture = mics, wasapi_output_capture = desktop/global audio.
            if (!((string)input["inputKind"]).Contains("wasapi"))
            {
                continue;
            }//if

            name = (string)input["inputName"];
            resp = ObsRequest("GetInputSettings", new JObject { ["inputName"] = name });
            if (!TryGetDevice(resp, out deviceId))
            {
                CPH.LogWarn($"『PRE-FLIGHT』: Skipped \'{name}\' (no settings readable).");
                continue;
            }//if

            config[name] = new JObject
            {
                ["inputKind"] = (string)input["inputKind"],
                ["device_id"] = deviceId
            };
            learned.Add(name);
        }//foreach

        if (config.Count == 0)
        {
            CPH.LogError("『PRE-FLIGHT』: No audio capture sources found to learn!");
            return true;
        }//if

        //Persist the snapshot
        CPH.SetGlobalVar(VarConfig, config.ToString(Formatting.None), true);
        CPH.LogInfo($"『PRE-FLIGHT』: Learned {learned.Count} audio source(s): {string.Join(", ", learned)}");

        return true;
    }//ExecuteLearn()

    bool FixDevice(string name, string expDevice)
    {
        /*Re-route a source to the expected device and verify the fix took.
		 *
		 */
        //Declarations
        JObject resp, settings;

        //Fetch current settings...
        resp = ObsRequest("GetInputSettings", new JObject { ["inputName"] = name });
        if (!TryGetDevice(resp, out string dummy, out settings))
        {
            return false;
        }//if

        //...swap the device_id in place, keeping any other settings...
        settings["device_id"] = expDevice;
        resp = ObsRequest("SetInputSettings", new JObject
        {
            ["inputName"] = name,
            ["inputSettings"] = settings
        });
        if (resp == null)
        {
            return false;
        }//if

        //...and verify OBS actually took it.
        resp = ObsRequest("GetInputSettings", new JObject { ["inputName"] = name });
        return TryGetDevice(resp, out string afterCheck) &&
               string.Equals(expDevice, afterCheck, StringComparison.OrdinalIgnoreCase);
    }//FixDevice()

    bool TryGetDevice(JObject resp, out string deviceId, out JObject settings)
    {
        /*Extract device_id + inputSettings from a GetInputSettings response.
		 *  Returns false when the request failed or the source does not exist.
		 */
        //Declarations
        JToken status;

        settings = null;
        deviceId = null;

        if (resp == null || resp["requestStatus"] == null || resp["responseData"] == null)
        {
            return false;
        }//if

        status = resp["requestStatus"];
        if (status["result"] == null || !(bool)status["result"])
        {
            return false;
        }//if

        settings = resp["responseData"]["inputSettings"] as JObject;
        if (settings == null || settings["device_id"] == null)
        {
            return false;
        }//if

        deviceId = (string)settings["device_id"];
        return deviceId != null;
    }//TryGetDevice()

    bool TryGetDevice(JObject resp, out string deviceId)
    {
        /*Convenience overload: device_id only.
		 */
        //Declarations
        JObject ignoreSettings;

        return TryGetDevice(resp, out deviceId, out ignoreSettings);
    }//TryGetDevice()

    JObject ObsRequest(string requestType, JObject data)
    {
        /*Send a raw OBS WebSocket v5 request and parse the JSON response.
		 *  Returns null on transport failure or unparseable response.
		 */
        //Declarations
        string resp;

        try
        {
            resp = CPH.ObsSendRaw(requestType, data.ToString(Formatting.None));
        }
        catch (Exception ex)
        {
            CPH.LogError($"『PRE-FLIGHT』: OBS request \'{requestType}\' threw: {ex.Message}");
            return null;
        }//try

        if (string.IsNullOrEmpty(resp))
        {
            CPH.LogError($"『PRE-FLIGHT』: OBS request \'{requestType}\' returned no data.");
            return null;
        }//if

        //Remove these two lines once everything works.
        CPH.LogDebug($"『PRE-FLIGHT』[RAW-req {requestType}]: {data.ToString(Formatting.None)}");
        CPH.LogDebug($"『PRE-FLIGHT』[RAW-res {requestType}]: {resp}");

        try
        {
            JObject parsed;

            parsed = JObject.Parse(resp);

            if (parsed["responseData"] != null)
            {
                //Full obs-websocket v5 envelope, pass it through.
                return parsed;
            }//if

            if (parsed["requestStatus"] != null)
            {
                //Envelope without payload (request error) -> let callers fail on the missing data.
                return parsed;
            }//if

            //Streamer.bot builds that unwrap the response: bare payload -> synthesize the envelope.
            return new JObject
            {
                ["requestStatus"] = new JObject { ["result"] = true },
                ["responseData"] = parsed
            };
        }
        catch (Exception ex)
        {
            CPH.LogError($"『PRE-FLIGHT』: Could not parse response of \'{requestType}\': {ex.Message}");
            return null;
        }//try
    }//ObsRequest()

    JObject ReadConfig()
    {
        /*Load the learned audio config from the persistent global.
		 */
        //Declarations
        string raw;

        raw = CPH.GetGlobalVar<string>(VarConfig, true);
        if (string.IsNullOrEmpty(raw))
        {
            return null;
        }//if

        try
        {
            return JObject.Parse(raw);
        }
        catch (Exception ex)
        {
            CPH.LogError($"『PRE-FLIGHT』: Stored audio config is unreadable: {ex.Message}");
            return null;
        }//try
    }//ReadConfig()

    string TrimList(string s)
    {
        /*Remove the trailing \", \" from an accumulated report list.
		 */
        if (string.IsNullOrEmpty(s))
        {
            return "(none)";
        }//if
        return s.Substring(0, s.Length - 2);
    }//TrimList()
}//CPHInline
