using UnityEngine;

public class UI_Options : MonoBehaviour
{
    public void GoMainMenu()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.GoToMainMenu();
    }

    public void GoToLevel1()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.GoToLevel1();
    }
}