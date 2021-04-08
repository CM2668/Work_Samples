using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public Button cont, quit;

    void Start()
    {
        cont.onClick.AddListener(Continue);
        quit.onClick.AddListener(Quit);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Continue()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
	}

    void Quit()
	{
        Application.Quit();
	}
}