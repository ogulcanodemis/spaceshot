using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timetillDestroy = 3f;
    void Start()
    {
        Destroy(gameObject,timetillDestroy); // clone olan particlellar kendini 3sn sonra yoketksin diye
    }

}
