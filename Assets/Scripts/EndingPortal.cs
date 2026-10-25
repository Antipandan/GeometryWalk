using System;
using System.Collections;
using CustomUtility;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class EndingPortal : MonoBehaviour
{
    [SerializeField] private SpriteRenderer objectRenderer;
    [SerializeField] private Collider2D objectCollider;
    [SerializeField] private FindPlayer playerDetectionZone;
    [SerializeField] [Range(10f, 60000f)] [Tooltip("Time meassured in milliseconds")] private float travelTime = 10f;
    [SerializeField] private AudioClip endingSound;
    private float delayTime = 0f;
    private GameObject player;
    private Vector2 playerFoundPosition;
    private static EndingPortal instance;

    public float TravelTime
    {
        get => travelTime;
    }

    public float TravelTimeSeconds
    {
        get => travelTime / 1000f;
    }
    
    private void Awake()
    {
        CheckReferences();
        Singleton();
    }

    private void OnEnable()
    {
        SubscribeEvents();
        if (endingSound != null) delayTime = endingSound.length;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent<Player>(out _)) return;
        if (endingSound == null) return;
        SoundPlayer.CreateInstance().PlaySound(endingSound);
    }

    private IEnumerator LerpPosition()
    {
        float elapsedTime = 0f;
        float travelTimeSeconds = TravelTimeSeconds;
        Vector2 destination = CalculateEndingPosition();
        while (true)
        {
            if (player == null)
            {
                StartCoroutine(DelayEnding());
                yield break;
            }
            float ratio = (elapsedTime / travelTimeSeconds);
            player.transform.position = Vector2.Lerp(playerFoundPosition, destination, ratio);
            elapsedTime += Time.deltaTime;
            if (ratio >= 1f)
            {
                StartCoroutine(DelayEnding());
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator DelayEnding()
    {
        yield return new WaitForSeconds(delayTime);
        EventBus<GameWonEvent>.Raise();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private Vector2 CalculateEndingPosition()
    {
        float spriteWidth = objectRenderer.bounds.size.x;
        Vector2 leftMostPosition = new Vector2(gameObject.transform.position.x - spriteWidth / 2f, gameObject.transform.position.y);
        return leftMostPosition;
    }

    private void OnPlayerFound(Player foundPlayer)
    {
        player = foundPlayer.gameObject;
        foundPlayer.DeactivateAllComponents();
        playerFoundPosition = foundPlayer.transform.position;
        StartCoroutine(LerpPosition());
    }
    

    private void SubscribeEvents()
    {
        if (playerDetectionZone == null) return;
        playerDetectionZone.playerFound -= OnPlayerFound;
        playerDetectionZone.playerFound += OnPlayerFound;
    }

    private void UnsubscribeEvents()
    {
        if (playerDetectionZone == null) return;
        playerDetectionZone.playerFound -= OnPlayerFound;
    }

    private void Singleton()
    {
        if (instance == null) instance = this;
        else
        {
            Logging.LogSingletonError(nameof(EndingPortal));
            Destroy(gameObject);
        }
    }

    private void CheckReferences()
    {
        ReferenceValidator.CheckComponentForNull(ref objectCollider, gameObject, nameof(objectCollider));
        ReferenceValidator.CheckComponentForNull(ref playerDetectionZone, gameObject, nameof(playerDetectionZone));
    }
}