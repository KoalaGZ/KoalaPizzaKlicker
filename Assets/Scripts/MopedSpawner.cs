using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopedSpawner : MonoBehaviour {

    public static MopedSpawner Instance;

    public GameObject MopedPrefab;

    public Vector3 spawnOffset;

    public Transform koalaMopedsContainer;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        spawnOffset = Vector3.right * GameController.Instance.mopeds * 0.25f;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            Destroy(spriteRenderer);
    }

    public void SpawnMoped()
    {
        spawnOffset = Vector3.right * GameController.Instance.mopeds * 0.25f;
        Instantiate(MopedPrefab, transform.position + spawnOffset, Quaternion.identity, transform);
    }
}
