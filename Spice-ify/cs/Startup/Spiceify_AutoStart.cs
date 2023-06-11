using System;

public class CPHInline
{

    public void Init()
    {

    }

    public void Dispose()
    {
        return;
    }

    public bool Execute()
    {
        bool watcher = Convert.ToBoolean(args["watcher"]);
        if (watcher = true)
        {
            CPH.RunAction("tiny SPOT TO SB - [ Now Playing Watcher]", false);
            CPH.Wait(2000);
            CPH.SendMessage("Now Playing Watcher Starting");
        }
        return true;
    }
}
