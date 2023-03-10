using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesCollision : MonoBehaviour
{
    public LevelCreator levelCreator;
    //Checking if the basket are colliding the cubes to increase the score
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cubes") {
            levelCreator.IncreaseScore();
            Destroy(collision.gameObject);
        }
    }
}
