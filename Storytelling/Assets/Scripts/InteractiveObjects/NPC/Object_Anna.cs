using UnityEngine;

public class Object_Anna : Object_Actor
{
    [Header("Dialogue")]
    [SerializeField] private DialogueLineSO firstDialogueLine;
    [SerializeField] private DialogueLineSO secondDialogueLine;

    public override void Interact()
    {
        DialogueLineSO lineToPlay = null;

        if (GameManager.instance != null && GameManager.instance.milkPicked)
        {
            lineToPlay = secondDialogueLine != null ? secondDialogueLine : firstDialogueLine;
        }
        else
        {
            lineToPlay = firstDialogueLine;
        }

        if (lineToPlay == null)
            return;

        ui.OpenDialogueUI(lineToPlay);
    }
}
