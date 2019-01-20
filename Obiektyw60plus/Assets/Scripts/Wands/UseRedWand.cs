using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;


public class UseRedWand : MonoBehaviour {

    
    ParticleSystem particleSystem;
    DistanceGrabbable distanceGrabbable;
    CastingToObject castingToObject;

    bool UsingItem = false;

    // Use this for initialization
    void Start()
    {
        distanceGrabbable = GetComponent<DistanceGrabbable>();
        particleSystem = GetComponent<ParticleSystem>();
        castingToObject = GetComponent<CastingToObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            DrawRay();
            print("B pressed");

        }

        if (distanceGrabbable.isGrabbed)
        {
            DrawRay();
            //use item
            if (!UsingItem)
            {
                castingToObject.IsCasting = true;

                if (OVRInput.GetDown(OVRInput.Button.One))//A key
                {
                    print("using red wand");
                    StartCoroutine(UseWand());
                    particleSystem.Play();
                    UsingItem = true;
                }


            }
        }
        if (UsingItem && !distanceGrabbable.isGrabbed)
        {
            //stop using item
            UsingItem = false;
            castingToObject.IsCasting = false;
            //TODO stop coroutine
            StopCoroutine(UseWand());
            particleSystem.Stop();
            print("stopped using wand");
        }
    }

    private void DrawRay()
    {
        RaycastHit objectHit;

        //realforward = transform.forward * 10000f + rotate90;
       

        // Shoot raycast
        if (Physics.Raycast(transform.position, transform.forward, out objectHit, 50))
        {
            Debug.Log("Raycast hitted to: " + objectHit.collider);
            //targetEnemy = objectHit.collider.gameObject;
        }
    }

    IEnumerator UseWand()
    {
        for (int i = 0; i < 10; i++)
        {
            particleSystem.Emit(1);
            particleSystem.emissionRate = 10f;
            print("coroutine running every second");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
