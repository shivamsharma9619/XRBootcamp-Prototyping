using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fIRErEACTION : MonoBehaviour
{
  public GameObject FirePS;
  
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blue"))
        {
            FirePS.SetActive(true);
        }
    }
  
}
