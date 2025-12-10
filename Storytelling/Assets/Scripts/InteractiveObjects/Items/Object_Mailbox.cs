using UnityEngine;

public class Object_Mailbox : Object_Actor
{
    [Header("Dialogue")]
    [SerializeField] private DialogueLineSO dialogueLine;

    private bool hasInteracted;
    private Collider2D cachedCollider;

    protected override void Awake()
    {
        base.Awake();

        cachedCollider = GetComponent<Collider2D>();
        if (cachedCollider != null)
            cachedCollider.isTrigger = true;
    }

    public override void Interact()
    {
        if (hasInteracted)
            return;

        hasInteracted = true;

        if (dialogueLine != null)
        {
            ui.OpenDialogueUI(dialogueLine);
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.newspaperPicked = true;
        }

        if (interactToolTip != null)
            interactToolTip.SetActive(false);

        if (cachedCollider != null)
            cachedCollider.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasInteracted)
            return;

        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (hasInteracted)
            return;

        base.OnTriggerExit2D(collision);
    }
}
