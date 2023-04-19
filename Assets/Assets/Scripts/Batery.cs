using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batery : MonoBehaviour
{

    [SerializeField]
    private float speedRotation;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, this.speedRotation * Time.deltaTime, 0));
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.TakeBatery();
            this.gameObject.SetActive(false);
        }
    }
}
