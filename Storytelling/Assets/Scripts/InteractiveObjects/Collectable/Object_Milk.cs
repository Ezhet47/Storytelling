using UnityEngine;

public class Object_Milk : Object_Collectable
{
    protected override void Awake()
    {
        base.Awake();

        if (GameManager.instance != null && GameManager.instance.milkPicked)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnPicked()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.milkPicked = true;
        }

        base.OnPicked();
    }
}