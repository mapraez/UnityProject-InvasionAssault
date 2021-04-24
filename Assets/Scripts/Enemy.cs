using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosionFX;
    [SerializeField] GameObject enemyHitFX;


    [SerializeField] int scorePerHit = 10;
    [SerializeField] int hitPoints = 10;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("Spawn At Runtime");
        AddRigidBody();
    }

    void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        hitPoints -= 5;
        GameObject hitFx = Instantiate(enemyHitFX, transform.position, Quaternion.identity);
        hitFx.transform.parent = parentGameObject.transform;

        if (hitPoints <= 0)
        {
            killEnemy();
        }
        else
        {
            Debug.Log($"HitPoints: {hitPoints}");
        }
    }

    void killEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        GameObject fx = Instantiate(enemyExplosionFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject);
    }

    // void DisableChildColliders()
    // {
    //     bool hasCollider = GetComponent<Collider>();
    //     if (hasCollider)
    //     {
    //         GetComponent<Collider>().enabled = false;
    //     }
    //     else
    //     {
    //         foreach (Transform childObject in transform)
    //         {
    //             if (childObject.name == "Collider")
    //             {
    //                 // Debug.Log($"Found child named: {childObject.name}");
    //                 foreach (Collider collider in GetComponentsInChildren<Collider>(childObject))
    //                 {
    //                     // Debug.Log($"Found Child: {collider.name}");
    //                     // Debug.Log($"Enabled: {collider.enabled}");
    //                     collider.enabled = false;
    //                     // Debug.Log($"After run, Enabled: {collider.enabled}");
    //                 }
    //             }
    //         }
    //     }
    // }
}
