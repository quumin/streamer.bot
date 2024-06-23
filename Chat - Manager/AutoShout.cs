using System;

/*AutoShout Handler
 * 
 *	Run the action that matches the username.
 *  LU: 23-jun-2024
 *  
 */

public class CPHInline
{
	public bool Execute()
	{
		//Declarations
		string usrName;

        //Initializations
        usrName = args["userName"].ToString();

        CPH.RunAction(usrName);
		CPH.DisableAction(usrName);
		return true;
	}//Execute()
}//CPHInline