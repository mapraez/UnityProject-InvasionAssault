using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject itemGrabVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 40;

    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        killItem();
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void killItem()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GameObject vfx = Instantiate(itemGrabVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(this.gameObject);
    }


}
