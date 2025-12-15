using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    private Player player;
    private Entity_Health playerHealth;
    [SerializeField] private RectTransform healthRect;
    [SerializeField] private Slider healthSlider;

    
    private void Start()
    {
        // 玩家血量
        player = FindFirstObjectByType<Player>();
        playerHealth = player.GetComponent<Entity_Health>();
        playerHealth.OnHealthUpdate += UpdatePlayerHealthBar;
        UpdatePlayerHealthBar();
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthUpdate -= UpdatePlayerHealthBar;
    }

    // ===== 玩家血条 =====
    private void UpdatePlayerHealthBar()
    {
        float currentHealth = Mathf.RoundToInt(playerHealth.currentHealth);
        float maxHealth = playerHealth.maxHealth;

        healthSlider.value = currentHealth / maxHealth;
    }
}




