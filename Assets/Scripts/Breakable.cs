using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breakable : MonoBehaviour
{
    public GameObject smallPieces;
    public GameObject mediumPieces; 
    public GameObject largePieces;
    public float explosionForce = 30f;
    public float explosionRadius = 10f;

    public AudioClip correctClip;
    public AudioClip wrongClip;

    GameObject manager;

    public void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController");
    }

    public void collidesWithController(Vector3 punchDirection, bool colorMatch)
    {
        float magnitude = punchDirection.magnitude;
        if (manager.GetComponent<LevelManager>().IsFinished())
        {
            return;
        }

        int id = gameObject.GetInstanceID();

        if (colorMatch)
        {
            Transform currentShape = gameObject.transform;
            GameObject pieces = null;

            if (magnitude > 2)
            {
                pieces = Instantiate(
                largePieces, currentShape.position, currentShape.rotation);
            }
            else if (magnitude > 1)
            {
                pieces = Instantiate(
               mediumPieces, currentShape.position, currentShape.rotation);
            }
            else if (magnitude > 0.5)
            {
                pieces = Instantiate(
               smallPieces, currentShape.position, currentShape.rotation);
            }

            if (pieces != null)
            {
                Material material = GetComponent<MeshRenderer>().material;

                Rigidbody[] rbPieces = pieces.GetComponentsInChildren<Rigidbody>();

                MeshRenderer[] meshRenderers = pieces.GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer meshRenderer in meshRenderers)
                {
                    meshRenderer.material = material;
                }
                foreach (Rigidbody rb in rbPieces)
                {
                    rb.AddExplosionForce(explosionForce, currentShape.position, explosionRadius);
                    rb.velocity = punchDirection;
                }

                AudioSource.PlayClipAtPoint(correctClip, currentShape.position);
  
                Destroy(gameObject);

                manager.GetComponent<LevelManager>().changePoint(50 + 10 * magnitude, id);
            }


            else
            {
                GetComponent<Rigidbody>().velocity = punchDirection;

               // GetComponent<Rigidbody>().AddForce(transform.forward * 0.5f * magnitude, ForceMode.VelocityChange);
            }

             
        }
        // Color does not match.
        else 
        {
            manager.GetComponent<LevelManager>().changePoint(-50, id);

            AudioSource.PlayClipAtPoint(wrongClip, transform.position);

            Rigidbody rb = GetComponent<Rigidbody>();

            GetComponent<Rigidbody>().velocity = Vector3.forward * (0.5f * magnitude);


            //GetComponent<Rigidbody>().AddForce(transform.forward * 0.5f * magnitude, ForceMode.VelocityChange);

            Invoke("Fly", 0.5f);


        }

    }

    private void Fly()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 1, ForceMode.VelocityChange);
        Invoke("DestroyShape", 1.2f);
    }
    private void DestroyShape()
    {
        Destroy(gameObject);
    }
}
