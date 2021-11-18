using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // scene de iþlem varsa kullanýlýyor.

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    void OnTriggerEnter(Collider other)
    {
         startCrashSequence(); // crash sekansý
    }

     void startCrashSequence()
    {
        crashVFX.Play(); // particle çalýþma
        GetComponent<PlayerControls>().enabled=false; // kontrol scriptini disable etme
        GetComponent<MeshRenderer>().enabled = false;  // meshrendererý kapatýyor player görünmez oluyor.
        GetComponent<BoxCollider>().enabled = false;  // BoxCollider kapatýyor player görünmez oluyor.
        Invoke("ReloadLevel", 1f); // level reloadý 1sn beklemeli
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;  // reload level metodu
        SceneManager.LoadScene(currentSceneIndex);
    }
}