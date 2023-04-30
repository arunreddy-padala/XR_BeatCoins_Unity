using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour 
{
    public float points = 0;
    public AudioSource audioSource;

    public TMP_Text score;

    public GameObject canvas;
    public TMP_Text message;

    bool isFinished;

    HashSet<int> seen = new HashSet<int>();

    public TMP_Text gameStats;

    int correctHits = 0;
    int wrongHits = 0;
    int misses = 0;
    int doorHits = 0;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Camera").GetComponent<AudioSource>();
    }
    public bool IsFinished()
    {
        return isFinished;
    }

    public void addMisses()
    {
        misses++;
    }

    public void changePoint(float delta, int id)
    {
        if (seen.Contains(id))
        {
            return;
        }
        seen.Add(id);

        if (delta == -50)
        {
            wrongHits++;
            misses--;
        }
        else if (delta == -100)
        {
            doorHits++;
        }
        else
        {
            correctHits++;
            misses--;
        }

        if (points < 0) return;

        points += delta;

        if (points < 0)
        {
            score.text = "0";

            Invoke("LoseGame", 2);
        }
        else
        {
            score.text = ((int)points).ToString();
        }
    }

    public void LoseGame()
    {

        isFinished = true;

        message.text = "Level Failed";

        audioSource.Stop();

        canvas.SetActive(true);

        gameStats.text = "Number of Correct Hits: " + correctHits + "\n" +
                         "Number of Wrong Hits: " + wrongHits + "\n" +
                         "Number of Misses: " + misses + "\n" +
                         "Number of Door Hits: " + doorHits;

    }
    public void WinGame()
    {
        isFinished = true;

        message.text = "Level Passed";
  
        canvas.SetActive(true);

        gameStats.text = "Number of Correct Hits: " + correctHits + "\n" +
                         "Number of Wrong Hits: " + wrongHits + "\n" +
                         "Number of Misses: " + misses + "\n" +
                         "Number of Correct Door Hits: " + doorHits;

    }
    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && !isFinished)
        {
            Invoke("WinGame", 2);
        }
    }
}
