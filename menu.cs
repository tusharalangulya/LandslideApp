/*
Name of the module: Menu
Date on which the module is created: 20/4/18
Author of the module:Manoj Reddy
Modification History: By Bhargav Mallala 21/4/18
                      By Balabolu Tushara Langulya 21/4/18
Synapsis of the module : This module is executed to generate menu options
Functions : in Mainmenu class
				1.	public void RunScenario ()
				2.	public void QuitApp()
			in InGameMenu class
				1.	public void RestartScenario ()
				2.	public void QuitApplication()
Global Variables:No global variables used
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour {

	public void RunScenario ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void QuitApp()
	{
		Application.Quit();
	}
}
public class InGameMenu : MonoBehaviour {

	public void RestartScenario ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void QuitApplication()
	{
		Application.Quit();
	}
}
