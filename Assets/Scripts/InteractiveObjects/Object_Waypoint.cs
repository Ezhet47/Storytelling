using UnityEngine;

public class Object_Waypoint : MonoBehaviour
{
    [SerializeField] private string transferToScene;
    [SerializeField] private Transform respawnPoint;
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public Vector3 GetRespawnPosition()
    {
        return respawnPoint != null ? respawnPoint.position : transform.position;
    }

    private void OnValidate()
    {
        gameObject.name = "Object_Waypoint - " + transferToScene;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance != null && GameManager.instance.AllNpcDialogueCompleted())
            transferToScene = "Ending";
        

        GameManager.instance.ChangeScene(transferToScene);
    }
}