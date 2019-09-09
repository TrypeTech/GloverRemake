using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleCam : MonoBehaviour {


    // my varables
    public int PlayerCount = 0;
    public int PlayerNumber = 0;
    public bool hasPlayer = false;


    [SerializeField]
    public Transform target;
    [SerializeField]
    public Vector3 defaultDistance = new Vector3(0f, 2f, -10f);
    [SerializeField]
    public float distanceDamp = 10f;
    // public float rotationDamp = 10f;

    public Vector3 velocity = Vector3.one;
    Transform myT;


    // Use this for initialization
    void Start()
    {

        myT = transform;


    }

    private void LateUpdate()
    {
        if (hasPlayer == true)
        {
            SmoothFollow();
        }
    }


    void SmoothFollow()
    {
        Vector3 toPos = target.position + (target.rotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(myT.position, toPos, ref velocity, distanceDamp);
        myT.position = curPos;

        myT.LookAt(target, target.up);
    }

    public void enableCamera(GameObject player)
    {

        // canvasPrefabs to
        // is is player 1 and there is 1 player
        if (PlayerCount == 1 && PlayerNumber == 1)
        {
            target = player.transform;

            gameObject.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
        }

        // if is player 1 and there is 2 players
        if (PlayerCount == 2 && PlayerNumber == 1)
        {
            target = player.transform;

            gameObject.GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 1);
        }

        // if is player 2 and there is 2 player
        if (PlayerCount == 2 && PlayerNumber == 2)
        {
            target = player.transform;

            gameObject.GetComponent<Camera>().rect = new Rect(0, -0.5f, 1, 1);
        }

        // add more where there is a need for them
        hasPlayer = true;

    }

    public void disableCamera()
    {
        gameObject.SetActive(false);
    }

    // shakes camera when hit
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignialPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, orignialPos.z);

            elapsed += Time.deltaTime;
            yield return null;

        }

        transform.localPosition = orignialPos;
    }

}
