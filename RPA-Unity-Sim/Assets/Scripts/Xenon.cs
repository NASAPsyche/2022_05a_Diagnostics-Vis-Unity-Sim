using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xenon : MonoBehaviour
{
    GameObject RPACenter;

    float YDirection;

    float speed;

    bool hasElectron = false;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        RPACenter = GameObject.FindGameObjectWithTag("RPACenter");
        meshRenderer = this.GetComponent<MeshRenderer>();

        YDirection = Random.Range(-1, 2);
        speed = Random.Range(150f, 200f);

        GetComponent<Rigidbody>().AddForce(new Vector3(1, YDirection).normalized * speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasElectron)
        {
            //float step = 1.5f * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(this.transform.position, RPACenter.transform.position, step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            Destroy(this.gameObject);
        }

        if (collision.transform.tag == "Electron" && !hasElectron)
        {
            Debug.Log("Xenon touching Electron");
            meshRenderer.material.color = Color.magenta;
            hasElectron = true;

            this.gameObject.layer = 9;
            this.gameObject.tag = "ChargedXenon";

            this.GetComponent<MultipleElementHighlight>().UpdateColor();

            GetComponent<Rigidbody>().velocity = Vector3.zero;

            int positionRange = Random.Range((int)RPACenter.transform.position.y - 4, (int)RPACenter.transform.position.y + 4);

            GetComponent<Rigidbody>().AddForce((new Vector3(RPACenter.transform.position.x, positionRange) - new Vector3(this.transform.position.x, this.transform.position.y)) * 8f);
        }

        if(collision.gameObject.layer == 14 && !hasElectron)
            Destroy(this.gameObject);

        if (collision.gameObject.layer == 11)
            Destroy(this.gameObject);

        // made it to collector
        if (collision.gameObject.layer == 16)
            Destroy(this.gameObject);
    }
}
