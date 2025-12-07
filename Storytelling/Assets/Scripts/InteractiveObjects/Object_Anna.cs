using UnityEngine;

public class Object_Anna : Object_Actor
{
    [Header("Dialogue")]
    [SerializeField] private DialogueLineSO firstDialogueLine;

    public override void Interact()
    {
        if (firstDialogueLine == null) return;
        ui.OpenDialogueUI(firstDialogueLine);
    }
}
