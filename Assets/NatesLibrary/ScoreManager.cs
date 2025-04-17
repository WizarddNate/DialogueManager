using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance {get; private set;}
    public float score {get; private set;}
    public float multiplier {get; private set;}
    //public UnityEvent<ScoreInfo> updateScore;

    void Awake()
    {
        //make sure that theres only one score manager object
        if (instance == null)
        {
            instance = this;
        }  
        else
        {
            //called on next score load
            //instance.score = 0;
            Destroy(this);
            return;
        }

    }

    public void AddPoints(float _amount, Vector3? _location = null)
    {
        score += _amount;
        //updateScore.Invoke(new ScoreInfo(score, multiplier, _amount, _location));
    }

    //public class ScoreInfo(float _score, float _multiplier, float _delta, _location = null)
    //{
    //    public float _score;
    //    public float _multiplier;
    //    public float _delta;
    //    public Vector3? _location;
    //}

}
