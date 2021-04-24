using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [Header("Player Effects")]
    [SerializeField] GameObject playerCrashVFX;
    GameObject parentGameObject;


    void Start()
    {
        parentGameObject = GameObject.FindWithTag("Spawn At Runtime");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item") { return; }
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        DisableChildColliders();
        // GetComponent<Rigidbody>().isKinematic = true;
        GameObject explodeVfx = Instantiate(playerCrashVFX, transform.position, Quaternion.identity);
        explodeVfx.transform.parent = parentGameObject.transform;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void DisableChildColliders()
    {
        foreach (Transform childObject in transform)
        {
            if (childObject.name == "Collider")
            {
                // Debug.Log($"Found child named: {childObject.name}");
                foreach (Collider collider in GetComponentsInChildren<Collider>(childObject))
                {
                    // Debug.Log($"Found Child: {collider.name}");
                    // Debug.Log($"Enabled: {collider.enabled}");
                    collider.enabled = false;
                    // Debug.Log($"After run, Enabled: {collider.enabled}");
                }
            }
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
