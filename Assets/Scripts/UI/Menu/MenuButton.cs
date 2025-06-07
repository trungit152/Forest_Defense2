using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    
    public void LoadDemo()
    {
        SceneManager.LoadScene("Level5");
        AudioManager.instance.FadeOutMusic("MenuMusic");
    }
}
