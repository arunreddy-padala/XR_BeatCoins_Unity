using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionWithDoor : MonoBehaviour
{
    public AudioClip doorHitSFX;

    GameObject manager; 

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Print the time of when the function is first called.
        Debug.Log("OnTriggerEnter : " + Time.time);

 
        if (manager != null && manager.GetComponent<LevelManager>().IsFinished())
        {
            return;
        }
        if (other.gameObject.CompareTag("Door"))
        {
            Debug.Log("Door!");
            AudioSource.PlayClipAtPoint(doorHitSFX, transform.position);

            manager.GetComponent<LevelManager>().changePoint(-100, other.gameObject.GetInstanceID());
        }
    }
}
