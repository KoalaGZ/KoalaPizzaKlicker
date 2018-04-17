using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryVan : MonoBehaviour {

    public Stapel stapel;

	// Use this for initialization
	void Start () {
		
	}
	
    IEnumerator DeliverDriveby ()
    {
        float duration = 4;

        Vector3 fromPos = transform.position;
        Vector3 toPos = stapel.transform.position;

        for (float t=0; t<duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(fromPos, toPos, t / duration);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1);

        stapel.AddElement();
        toPos = new Vector3(5, 0, 0);
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(fromPos, toPos, t / duration);
            yield return new WaitForEndOfFrame();
        }


        Destroy(gameObject);
    }
	
}
