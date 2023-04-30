using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakableForMenuBall : MonoBehaviour
{
    public GameObject smallPieces;

    public float explosionForce = 30f;
    public float explosionRadius = 10f; 

    public AudioClip audioClip;

    public string sceneName;

    public GameObject gameStats;
    public GameObject FinishMenu;


    public void Hit()
    {
        Debug.Log("Gets here");
        Transform currentShape = gameObject.transform;
        GameObject pieces = Instantiate(
                smallPieces, currentShape.position, currentShape.rotation);

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
        }

        AudioSource.PlayClipAtPoint(audioClip, currentShape.position);

        gameObject.SetActive(false);

        if (gameObject.tag.Equals("GameStats"))
        {
            Invoke("Toggle", 1f);
        }
        else if (gameObject.tag.Equals("BackToFinishMenu"))
        {
            Invoke("Toggle", 1f);
        }
        else
        {
            Invoke("StartForNewScene", 1f);
        }
    }
    void StartForNewScene()
    {
        SceneManager.LoadScene(sceneName);
        Destroy(gameObject);
    }
    void Toggle()
    {
        FinishMenu.SetActive(!FinishMenu.activeSelf);
        gameStats.SetActive(!gameStats.activeSelf);
    }


}
