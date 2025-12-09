using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Object_Collectable : MonoBehaviour
{
    [Header("Pickup Input")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("Collectable Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectClip;     

    [Header("Floaty Tooltip (same as Object_Actor)")]
    [SerializeField] private GameObject interactToolTip;
    [SerializeField] private float floatSpeed = 8f;
    [SerializeField] private float floatRange = 0.1f;

    private Vector3 startPosition;
    private bool playerInRange;
    private Player cachedPlayer;
    private bool hasBeenPicked; 

    protected virtual void Awake()
    {
        if (interactToolTip != null)
        {
            startPosition = interactToolTip.transform.position;
            interactToolTip.SetActive(false);
        }

        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void Update()
    {
        HandleToolTipFloat();
        HandlePickupInput();
    }

    private void HandleToolTipFloat()
    {
        if (interactToolTip == null || !interactToolTip.activeSelf)
            return;

        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        interactToolTip.transform.position =
            startPosition + new Vector3(0f, yOffset, 0f);
    }

    private void HandlePickupInput()
    {
        if (!playerInRange || hasBeenPicked)
            return;

        if (Input.GetKeyDown(interactKey))
        {
            OnPicked();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenPicked)
            return;

        if (!collision.CompareTag("Player"))
            return;

        playerInRange = true;
        cachedPlayer = collision.GetComponentInParent<Player>();

        if (interactToolTip != null)
            interactToolTip.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (collision.GetComponentInParent<Player>() != cachedPlayer)
            return;

        playerInRange = false;
        cachedPlayer = null;

        if (interactToolTip != null)
            interactToolTip.SetActive(false);
    }

    protected virtual void OnPicked()
    {
        if (hasBeenPicked)
            return;

        hasBeenPicked = true;

        if (interactToolTip != null)
            interactToolTip.SetActive(false);

        PlayCollectSound();

        Destroy(gameObject);
    }

    private void PlayCollectSound()
    {
        if (audioSource == null || collectClip == null)
            return;

        audioSource.PlayOneShot(collectClip);
    }
}