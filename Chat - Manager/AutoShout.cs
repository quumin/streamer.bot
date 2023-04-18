using System;

public class CPHInline
{
	public bool Execute()
	{
		string usr = args["userName"].ToString();
		CPH.RunAction(usr);
		CPH.DisableAction(usr);
		return true;
	}
}
