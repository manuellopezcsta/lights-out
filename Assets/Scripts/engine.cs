using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class engine : MonoBehaviour
{
    int nrOfLevels = 5;
    public int currentLevel;
    public int nrOfMoves;
    [SerializeField] Animator cameraAnimator;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] AudioSource aSource1;
    [SerializeField] AudioSource aSource2;


    public void init(int number)
    {
        nrOfMoves = number;
        currentLevel = getProgress();
        //Debug.Log(PlayerPrefs.GetInt("1"));
    }

    int getProgress(){
        int progress = 0;
        for(int i=0; i<nrOfLevels + 1; i++)
        {
            // Si la key existe, es xq el player ya la completo y sigue
            if(PlayerPrefs.HasKey(i.ToString())){
                progress++;
            }else {
                // Si no existe es xq esta en ese level , entonces salimos del loop.
                progress++;
                break;
            }
        }
        return progress;
    }

    int getScore(string level){
        return PlayerPrefs.GetInt(level);
    }

    void saveScore(){
        // If the current level has a saved score
        if(PlayerPrefs.HasKey(currentLevel.ToString())){
            if(getScore(currentLevel.ToString()) > nrOfMoves)// CREO Q ES < aca
            {
                PlayerPrefs.SetInt(currentLevel.ToString(), nrOfMoves);
            }  

        } else{
            // Si no exitia el nivel en el score
            PlayerPrefs.SetInt(currentLevel.ToString(), nrOfMoves);
        }
    }

    public void startGame()
    {
        cameraAnimator.SetInteger("moveCamera", 1);
        gameObject.GetComponent<levelHandler>().loadLevel(currentLevel);
    }
    
    public void gameFinished()
    {
        aSource2.Play();
        // Guardamos el score, reseteamos los movimientos y cargamos el siguiente nivel.
        saveScore();
        currentLevel++;
        Debug.Log(currentLevel);
        nrOfMoves = 0;
        // Failsafe x si no hay mas niveles???
        if(currentLevel >= nrOfLevels + 1)
        {
            Debug.Log("Ganaste");
            victoryScreen();
            return;
        }
        Invoke("animationDone", 1f);
        cameraAnimator.SetInteger("moveCamera", 0);
        cameraAnimator.Play("swapLevel");        
    }

    void animationDone()
    {
        //cameraAnimator.SetInteger("moveCamera", 0);
        //cameraAnimator.Play("swapLevel");
        gameObject.GetComponent<levelHandler>().loadLevel(currentLevel);        
    }

    void victoryScreen()
    {
        victoryPanel.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayClick()
    {
        aSource1.Play();
    }
}
