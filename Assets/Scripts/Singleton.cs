using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{

    public Singleton instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            GameObject.Destroy(this);
        }
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
