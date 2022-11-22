using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject mainDestructionFX;
    public GameObject secondaryDestructionFX;
    public GameObject tertiaryDestructionFX;

    public static Player instance;
    public float destructionFXRange = .4f;
    [SerializeField] private int health = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        	StartCoroutine(Destruction());
    }

    //'Player's' destruction procedure
    private IEnumerator Destruction()
    {
        Destroy(GetComponent<PlayerShooting>());
        // Destroy(GetComponent<PlayerMoving>());

        Instantiate(mainDestructionFX, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(.6f);
        var randomPos = Random.insideUnitCircle * destructionFXRange;
        Instantiate(secondaryDestructionFX, transform.position + new Vector3(randomPos.x, randomPos.y, 0), Quaternion.identity);

        yield return new WaitForSeconds(.6f);
        randomPos = Random.insideUnitCircle * destructionFXRange;
        Instantiate(tertiaryDestructionFX, transform.position + new Vector3(randomPos.x, randomPos.y, 0), Quaternion.identity);

        FindObjectOfType<LevelController>().PlayerDestroyed();
        Destroy(gameObject);
    }
}
















