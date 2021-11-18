using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX; // bütün enemylere ayný particleý vermek için
    [SerializeField] GameObject hitVFX; // 
    [SerializeField] int scorePerHit = 15; // skorun kaçar kaçar artacaðý.
    [SerializeField] int hitpoint = 2; // enemynin healti
    GameObject parentGameObject;  // enemy vfxler clone olarak kaldýðý için oluþturulan game objeckt için.
    ScoreBoard scoreBoard;  // scoreBoard scripti

     void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime"); // onejyi tagden bulma  parenti tagden objelere atama
        scoreBoard = FindObjectOfType<ScoreBoard>(); // findobject update'de kullanýlmamalý tek seferliklerde faydalý sürekli halde bozuluyor
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
        if (hitpoint<1) // caný bitince enemy silme 
        {
            killEnemy();
        }
    }

     void processHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitpoint--; // enemynin canýný eksiltme
    }

     void killEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
       // vfx.transform.parent = parent; // oyun çalýþýrken clone vfxleri empty objeye atýyor. çok nesneli projelerde iþe yarar
        Destroy(gameObject);
   
    }
}
