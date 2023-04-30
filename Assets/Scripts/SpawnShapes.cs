using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShapes : MonoBehaviour
{
    public GameObject shapePrefabRed;
    public GameObject shapePrefabGreen;
    public GameObject door;
    public GameObject manager;
    public float projectSpeed = 4;

    // Start is called before the first frame update 
    void Start()
    {
        
    }

    public void Spawn()
    {
        Vector3 ShapePosition;

        ShapePosition.x = Random.Range(-0.25f, 0.25f);
        ShapePosition.y = Random.Range(0.85f, 1.8f);
        ShapePosition.z = 15f;

        GameObject spawnedShape;
        float val = Random.Range(-1.0f, 1.0f);

        Rigidbody rb;

        if (val > 0.8)
        {
            ShapePosition.x = Random.Range(-0.2f, 0.2f);
            ShapePosition.y = 0;
            ShapePosition.z = 10f;

            spawnedShape = Instantiate(door, ShapePosition, transform.rotation);
            spawnedShape.tag = "Door";

            spawnedShape.transform.localScale = new Vector3(Random.Range(0.4f, 1.0f), 0.85f, 0.6f);
            spawnedShape.SetActive(true);


            //rb = spawnedShape.GetComponentInChildren<Rigidbody>();
        }
        else
        {
            manager.GetComponent<LevelManager>().addMisses();
            if (val > 0)
            {
                spawnedShape = Instantiate(shapePrefabRed, ShapePosition, transform.rotation);
                spawnedShape.tag = "RedShape";
            }
            else
            {
                spawnedShape = Instantiate(shapePrefabGreen, ShapePosition, transform.rotation);
                spawnedShape.tag = "GreenShape";

            }
            rb = spawnedShape.GetComponent<Rigidbody>();

            rb.AddForce(-transform.forward * projectSpeed, ForceMode.VelocityChange);

        }


    }
}
