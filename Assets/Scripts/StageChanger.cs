using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChanger : MonoBehaviour
{
    Collider collider;

    void Start()
    {
        collider = this.GetComponent<Collider>();
    }

    public void ChangeToTrigger()
    {
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(null);
            //null -> function qui permet de recevoir la salle
        }
    }
}
