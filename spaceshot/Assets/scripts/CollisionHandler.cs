using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // scene de i�lem varsa kullan�l�yor.

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    void OnTriggerEnter(Collider other)
    {
         startCrashSequence(); // crash sekans�
    }

     void startCrashSequence()
    {
        crashVFX.Play(); // particle �al��ma
        GetComponent<PlayerControls>().enabled=false; // kontrol scriptini disable etme
        GetComponent<MeshRenderer>().enabled = false;  // meshrenderer� kapat�yor player g�r�nmez oluyor.
        GetComponent<BoxCollider>().enabled = false;  // BoxCollider kapat�yor player g�r�nmez oluyor.
        Invoke("ReloadLevel", 1f); // level reload� 1sn beklemeli
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // reload level metodu
        SceneManager.LoadScene(currentSceneIndex);
    }
}