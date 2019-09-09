using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinimatic : MonoBehaviour {

    public GameObject CinimaticPannel;
    public GameObject[] SceneImages;
    public int currentImage;
    GameObject player;
    TalkBox talkBox;
    public string[] talkText;
    public int ItemNumber;
    public bool hasdoneCinimatic;

    bool inCinimatic;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        talkBox = FindObjectOfType<TalkBox>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Space) && inCinimatic == true)
        {
            switchSceneImage();
        }
	}

    public void StarCinimatic()
    {
        // start music
        player.gameObject.GetComponent<Movement>().CanMove = false;
        CinimaticPannel.gameObject.SetActive(true);
        currentImage = 1;
        for(int i = 1;i < SceneImages.Length; i++)
        {
           if(i == currentImage)
            {
                SceneImages[i].gameObject.SetActive(true);
            }
           else
            {
                SceneImages[i].gameObject.SetActive(false);
            }
        }
        inCinimatic = true;
    }
    public void switchSceneImage()
    {
        currentImage += 1;
        if (currentImage > SceneImages.Length)
        {
            EndCinimatic();
        }
        else
        {
            for (int i = 1; i < SceneImages.Length; i++)
            {
                if (i == currentImage)
                {
                    SceneImages[i].gameObject.SetActive(true);
                }
                else
                {
                    SceneImages[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void EndCinimatic()
    {
        inCinimatic = false;
        hasdoneCinimatic = true;
        CinimaticPannel.gameObject.SetActive(false);
        talkBox.StartDialogue(talkText, ItemNumber, false, 0,false,0,false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (hasdoneCinimatic == false)
            {
                //some effect then
                // start cinimatic images
                StarCinimatic();
            }
        }
    }
}
