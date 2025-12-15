using UnityEngine;

public class UI_Options : MonoBehaviour
{
    public void GoMainMenu()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.GoToMainMenu();
    }

    public void GoToAnnaBack()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.AnnaStageBack();
    }

    public void GoToLinBack()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.LinStageBack();
    }

    public void GoToZhaoBack()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.ZhaoStageBack();
    }
}