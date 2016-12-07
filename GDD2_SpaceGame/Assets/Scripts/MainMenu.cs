using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

    }

    //
    public void StartButton()
    {
        SceneManager.LoadScene("Scenes/WeaponsTest");
    }

    //
    public void QuitButton()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {


    }
}
