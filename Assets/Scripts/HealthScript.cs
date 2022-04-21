using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    private float damageTimer;
    public float damageCooldown = 0.5f;
    public AudioSource audioSource;
    public AudioClip[] punchAudioClips;

    public void TakeDamage(int dmg) {
        if (damageTimer > damageCooldown) {
            damageTimer = 0f;
            //audioSource.PlayOneShot(punchAudioClips[Random.Range(0, punchAudioClips.Length)], 1.0F);
            Debug.Log("dmg taken: " + dmg);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;
    }
}
