using System;

public class CPHInline
{

    public void Init()
    {
        CPH.RunAction("tiny SPOT TO SB - Auto Start Watcher", false);
    }

    public void Dispose()
    {
        return;
    }

    public bool Execute()
    {

        return true;
    }
}