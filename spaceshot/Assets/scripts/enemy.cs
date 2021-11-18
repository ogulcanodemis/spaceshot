using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX; // b�t�n enemylere ayn� particle� vermek i�in
    [SerializeField] GameObject hitVFX; // 
    [SerializeField] int scorePerHit = 15; // skorun ka�ar ka�ar artaca��.
    [SerializeField] int hitpoint = 2; // enemynin healti
    GameObject parentGameObject;  // enemy vfxler clone olarak kald��� i�in olu�turulan game objeckt i�in.
    ScoreBoard scoreBoard;  // scoreBoard scripti

     void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime"); // onejyi tagden bulma  parenti tagden objelere atama
        scoreBoard = FindObjectOfType<ScoreBoard>(); // findobject update'de kullan�lmamal� tek seferliklerde faydal� s�rekli halde bozuluyor
        AddRigidbody();
    }

     void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>(); // rigidbody ekleme
        rb.useGravity = false; // gravity kapatma 
    }

    void OnParticleCollision(GameObject other)
    {
        processHit();
        if (hitpoint<1) // can� bitince enemy silme 
        {
            killEnemy();
        }
    }

     void processHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitpoint--; // enemynin can�n� eksiltme
    }

     void killEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
       // vfx.transform.parent = parent; // oyun �al���rken clone vfxleri empty objeye at�yor. �ok nesneli projelerde i�e yarar
        Destroy(gameObject);
   
    }
}
