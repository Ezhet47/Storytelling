using UnityEngine;

public class Entity_AnimationTriggers : MonoBehaviour
{
    private Entity entity;
    private Entity_SFX entitySfx; 

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entitySfx = GetComponentInParent<Entity_SFX>();
    }
    
    public void FootstepTrigger()
    {
        if (entitySfx != null)
            entitySfx.PlayFootstepOnce();
    }
}
