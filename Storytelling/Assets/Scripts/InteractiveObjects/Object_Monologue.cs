using UnityEngine;

public class Object_Monologue : Object_Actor
{
    [Header("Dialogue")]
    [SerializeField] private DialogueLineSO firstDialogueLine;

    [Header("Trigger Options")]
    [SerializeField] private bool oneShot = true;
    [SerializeField] private bool disableColliderAfterTrigger = true;

    private bool triggered;
    private Collider2D cachedCollider;

    // 不调用 base.Awake()，避免父类 tooltip 初始化
    protected override void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        cachedCollider = GetComponent<Collider2D>();
        if (cachedCollider != null) cachedCollider.isTrigger = true;
    }

    // 不需要父类的 Update（翻转与浮动提示）
    protected override void Update() { }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered || firstDialogueLine == null) return;

        Player playerComponent = collision.GetComponent<Player>();
        if (playerComponent == null) return;

        player = playerComponent.transform;
        triggered = true;

        ui.OpenDialogueUI(firstDialogueLine);

        if (disableColliderAfterTrigger && cachedCollider != null)
            cachedCollider.enabled = false;

        if (oneShot)
            Destroy(this);
    }

    protected override void OnTriggerExit2D(Collider2D collision) { }

    public override void Interact() { }
}
