using UnityEngine;

public class Object_Collectable : MonoBehaviour
{
    private bool playerInRange = false;
    private Player cachedPlayer;

    public Object_InteractionDetect otherScript;
    public Transform focusPoint;

    public bool IsInteractable => true; // 物品永远可交互（你也可改）

    [Header("Collect Flag")]
    public bool picked = false;     // 🔥 捡起后会变 true



    [Header("Collectable Sounds")]
    public AudioSource audioSource;
    public AudioClip[] collectClips;
    public AudioClip fail;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            cachedPlayer = collision.GetComponentInParent<Player>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            if (collision.GetComponentInParent<Player>() == cachedPlayer)
                cachedPlayer = null;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickItem();
        }
    }

    private void PickItem()
    {
        picked = true;  // 🔥 核心功能：捡起后改变变量

        if (otherScript) otherScript.canpress = false;



        PlayRandomCollectSound();



        Destroy(gameObject);
    }


    private Vector3 GetTopOfBounds(Transform t)
    {
        var sr = t.GetComponentInChildren<SpriteRenderer>();
        if (sr)
        {
            var b = sr.bounds;
            return new Vector3(b.center.x, b.max.y, t.position.z);
        }

        var col = t.GetComponentInChildren<Collider2D>();
        if (col)
        {
            var b = col.bounds;
            return new Vector3(b.center.x, b.max.y, t.position.z);
        }

        return t.position + new Vector3(0f, 0.5f, 0f);
    }

    private void PlayRandomCollectSound()
    {
        if (audioSource == null || collectClips == null || collectClips.Length == 0) return;

        int index = Random.Range(0, collectClips.Length);
        float volume = Random.Range(0.9f, 1.0f);
        audioSource.PlayOneShot(collectClips[index], volume);
    }

    private void PlayFailSound()
    {
        if (audioSource == null || fail == null) return;
        audioSource.PlayOneShot(fail, 1f);
    }
}
