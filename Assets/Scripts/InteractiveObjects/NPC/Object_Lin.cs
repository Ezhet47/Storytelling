using UnityEngine;

public class Object_Lin : Object_Actor
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

        switch (gm.linDialogueStage)
        {
            case 0:
                lineToPlay = firstDialogueLine;
                gm.linDialogueStage = 1;
                break;

            case 1:
                if (gm.exerciseBookPicked && thirdDialogueLine != null)
                {
                    lineToPlay = thirdDialogueLine;
                    gm.linDialogueStage = 2;
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
                LockLin();
                return;
        }

        if (lineToPlay == null)
            return;

        ui.OpenDialogueUI(lineToPlay);

        if (gm.linDialogueStage == 2)
        {
            LockLin();
        }
    }

    private void LockLin()
    {
        if (interactToolTip != null)
            interactToolTip.SetActive(false);

        if (cachedCollider != null)
            cachedCollider.enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance != null && GameManager.instance.linDialogueStage >= 2)
            return;

        base.OnTriggerEnter2D(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
