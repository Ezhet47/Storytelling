using UnityEngine;

public class Object_Anna : Object_Actor
{
    [Header("Dialogue")]
    [SerializeField] private DialogueLineSO firstDialogueLine;
    [SerializeField] private DialogueLineSO secondDialogueLine;
    [SerializeField] private DialogueLineSO thirdDialogueLine;

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
        if (GameManager.instance == null)
        {
            if (firstDialogueLine != null)
                ui.OpenDialogueUI(firstDialogueLine);
            return;
        }

        var gm = GameManager.instance;
        DialogueLineSO lineToPlay = null;

        switch (gm.annaDialogueStage)
        {
            case 0:
                lineToPlay = firstDialogueLine;
                gm.annaDialogueStage = 1;
                break;

            case 1:
                if (gm.milkPicked && thirdDialogueLine != null)
                {
                    lineToPlay = thirdDialogueLine;
                    gm.annaDialogueStage = 2;
                }
                else
                {
                    lineToPlay = secondDialogueLine != null
                        ? secondDialogueLine
                        : firstDialogueLine;
                }
                break;

            case 2:
            default:
                LockAnna();
                return;
        }

        if (lineToPlay == null)
            return;

        ui.OpenDialogueUI(lineToPlay);

        if (gm.annaDialogueStage == 2)
        {
            LockAnna();
        }
    }

    private void LockAnna()
    {
        if (interactToolTip != null)
            interactToolTip.SetActive(false);

        if (cachedCollider != null)
            cachedCollider.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance != null && GameManager.instance.annaDialogueStage >= 2)
            return;

        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}