using UnityEngine;

public class Object_Actor : MonoBehaviour, IInteractable
{
    protected Transform player;
    protected UI ui;

    [SerializeField] protected GameObject interactToolTip;

    [Header("Floaty Tooltip")]
    [SerializeField] private float floatSpeed = 8f;
    [SerializeField] private float floatRange = 0.1f;
    private Vector3 startPosition;

    protected virtual void Awake()
    {
        ui = FindFirstObjectByType<UI>();

        if (interactToolTip != null)
        {
            startPosition = interactToolTip.transform.position;
            interactToolTip.SetActive(false);
        }
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleToolTipFloat();
    }

    private void HandleToolTipFloat()
    {
        if (interactToolTip == null || !interactToolTip.activeSelf)
            return;

        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        interactToolTip.transform.position = startPosition + new Vector3(0, yOffset);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.transform;

        if (interactToolTip != null)
            interactToolTip.SetActive(true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (interactToolTip != null)
            interactToolTip.SetActive(false);
    }

    public virtual void Interact()
    {

    }
}