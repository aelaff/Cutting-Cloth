using Obi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{


    //List contains all the pixel pictures for the levels
    public List<Texture2D> levelPictures;
    //it is alpha thershold because when we compare by zero all near transparent will not deleted
    int alpha = 40;
    //if you want to change the percentage of collected cubes
    float completionPercentage = .5f;
    //The needed number of cubes to win
    int targetCubes = 0;
    //Assign canvasUI to the canvas game object , which holds all UI elements
    public Transform canvasUI;
    // the score counter
    int score = 0;
    //boolean for take action if the level has been loaded
    bool isLevelLoaded = false;

    // Start is called before the first frame update
    void Start()
    {

       //checking if all levels are played 
        if (PlayerPrefs.GetInt("LevelIndex") >= levelPictures.Count)
        {
            //for rest  level counter
            PlayerPrefs.SetInt("LevelIndex", 0);
            //show all levels completed Panel
            canvasUI.GetChild(5).gameObject.SetActive(true);
            //hide tap to play Panel
            canvasUI.GetChild(6).gameObject.SetActive(false);
        }
        // if not all the level has been completed
        else
        {
            //generate the next level
            GenerateLevel(PlayerPrefs.GetInt("LevelIndex"));

        }
        //intiate the score from the saved score in the playerprefs
        canvasUI.GetChild(2).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Score", 0) + "";

    }
    //for genreating a new level according to its index
    public void GenerateLevel(int levelIndex)
    {
        
        int cubeIndex = 0;

        //Change previous level Text the beginning of level generation
        canvasUI.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>().text = (levelIndex + 1) + "";
        
        //Change next level Text the beginning of level generation
        canvasUI.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>().text = (levelIndex + 2) + "";
        //Rest Slider to 0
        canvasUI.GetChild(1).GetChild(0).GetComponent<Slider>().value = 0;

        //get the currest level image ,it should be in 11*11 diminsion
        Texture2D currentImage = levelPictures[levelIndex];

        // for loops in the width and height numbers for the all pixels of images
        for (int x = 0; x < currentImage.width; x++)
        {
            for (int y = 0; y < currentImage.height; y++)
            {
                //change all the renderer and colliders to true
                transform.GetChild(cubeIndex).GetComponent<Renderer>().enabled = true;
                transform.GetChild(cubeIndex).GetComponent<BoxCollider>().enabled = true;
                
                //get the color from image pixel
                Color32 currentColor = currentImage.GetPixel(x, y);

                //to ignore the alpha pixels
                if (currentColor.a > alpha)
                {
                    // if the pixel are not transparent , it will give the the related cube
                    transform.GetChild(cubeIndex).GetComponent<Renderer>().material.color = currentColor;
                    //increase the target number of cubes 
                    targetCubes++;
                }
                // if the pixel are in transparent form
                else
                {
                    //disable the related cube and it's collider
                    transform.GetChild(cubeIndex).GetComponent<Renderer>().enabled = false;
                    transform.GetChild(cubeIndex).GetComponent<Collider>().enabled = false;
                }
                cubeIndex++;
            }
        }
        // intialize the requested score at the screen's bottom
        canvasUI.GetChild(4).GetChild(0).GetComponent<Text>().text = score + "/" + Convert.ToInt32(targetCubes * completionPercentage);

    }
    //change the level bar UI according to the collecting cubes and the target
    public void ChangeLevelBar(int score)
    {
        canvasUI.GetChild(1).GetChild(0).GetComponent<Slider>().value =(float)score/ (targetCubes* completionPercentage);
      
    }
   
    //it's called at the beginning of genrating such a level
    public void GameReset()
    {
        PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") + 1);
        SceneManager.LoadScene(0);

    }
    //for playing the game again
    public void PlayFromScratch()
    {     
        SceneManager.LoadScene(0);
    }

    //It will be called on the collision of cube and the basket
    public void IncreaseScore()
    {
        //score counter
        score++;
        //changing the value of all score which saved in player prefs
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") +1);
        //changing the text of all score
        canvasUI.GetChild(2).GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("Score") + "";
        //changing target and collected cubes UI
        canvasUI.GetChild(4).GetChild(0).GetComponent<Text>().text = score + "/"+ Convert.ToInt32(targetCubes * completionPercentage);
        // to change the slider value
        ChangeLevelBar(score);
        //checking if the collected cubes more than or equal the target , to rest the game and genrate the next level
     if (score >= targetCubes * completionPercentage && !isLevelLoaded)
        {
            isLevelLoaded = true;
            GameReset();
        }
    }


}
