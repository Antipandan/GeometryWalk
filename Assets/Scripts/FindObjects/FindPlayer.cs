using System;
using UnityEngine;

public sealed class FindPlayer : AddThingScript<Player>
{
    public event Action<Player> playerFound;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Player foundObject = other.gameObject.GetComponent<Player>();
        if (foundObject == null) return;
        itemFound?.Invoke();
        playerFound?.Invoke(foundObject);
    }
}