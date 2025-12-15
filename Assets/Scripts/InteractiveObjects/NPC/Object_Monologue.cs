using UnityEngine;

public class Object_Monologue : MonoBehaviour
{
    [Header("Monologue ID")]
    [SerializeField] private string monologueId;

    [Header("Dialogue")]
    [SerializeField] private DialogueLineSO dialogueLine;

    private UI ui;
    private bool triggered;
    private Collider2D cachedCollider;

    private void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        cachedCollider = GetComponent<Collider2D>();
        if (cachedCollider != null)
            cachedCollider.isTrigger = true;

        if (GameManager.instance != null &&
            GameManager.instance.IsMonologueTriggered(monologueId))
        {
            triggered = true;

            if (cachedCollider != null)
                cachedCollider.enabled = false;

            Destroy(this); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered)
            return;
        if (dialogueLine == null)
            return;
        if (!collision.CompareTag("Player"))
            return;

        triggered = true;

        if (GameManager.instance != null)
            GameManager.instance.MarkMonologueTriggered(monologueId);

        if (ui != null)
            ui.OpenDialogueUI(dialogueLine);

        if (cachedCollider != null)
            cachedCollider.enabled = false;

        Destroy(this); 
    }
}