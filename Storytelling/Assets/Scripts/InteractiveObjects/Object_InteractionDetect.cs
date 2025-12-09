using UnityEngine;

public class Object_InteractionDetect : MonoBehaviour
{
    public GameObject buttonSprite;
    public Transform playerTrans;
    public bool canpress;
    private Object_Collectable current;


    private void Update()
    {
        if (buttonSprite) buttonSprite.SetActive(canpress);
        buttonSprite.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Interactable")) return;

        var col = other.GetComponentInParent<Object_Collectable>();


        bool available = false;
        if (col != null) available |= col.IsInteractable;

        canpress = available;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Interactable")) return;

        if (current && other.GetComponentInParent<Object_Collectable>() == current)
            current = null;

        canpress = false;
    }

    public void ForceHidePrompt()
    {
        canpress = false;
    }
}
