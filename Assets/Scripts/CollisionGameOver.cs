using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionGameOver : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
