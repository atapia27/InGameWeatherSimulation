using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScore : MonoBehaviour
{
    //private
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void AddToScore(int addAmount)
    {
        score = score + addAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
