using UnityEngine;

public class Object_Actor : MonoBehaviour, IInteractable
{
    protected Transform player;
    protected UI ui;
    
    [SerializeField] private Transform interactObject;
    [SerializeField] private GameObject interactToolTip;
    private bool facingRight = true;

    [Header("Floaty Tooltip")]
    [SerializeField] private float floatSpeed = 8f;
    [SerializeField] private float floatRange = 0.1f;
    private Vector3 startPosition;

    protected virtual void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        startPosition = interactToolTip.transform.position;
        interactToolTip.SetActive(false);
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleNpcFlip();
        HandleToolTipFloat();
    }

    private void HandleToolTipFloat()
    {
        if (interactToolTip.activeSelf)
        {
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
            interactToolTip.transform.position = startPosition + new Vector3(0, yOffset);
        }
    }

    private void HandleNpcFlip()
    {
        if (player == null || interactObject == null)
            return;

        if (interactObject.position.x > player.position.x && facingRight)
        {
            interactObject.transform.Rotate(0, 180,0);
            interactToolTip.transform.Rotate(0, 180, 0);
            facingRight = false;
        }
        else if (interactObject.position.x < player.position.x && facingRight == false)
        {
            interactObject.transform.Rotate(0, 180, 0);
            interactToolTip.transform.Rotate(0, 180, 0);
            facingRight = true;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.transform;
        interactToolTip.SetActive(true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        interactToolTip.SetActive(false);
    }

    public virtual void Interact()
    {

    }
}
