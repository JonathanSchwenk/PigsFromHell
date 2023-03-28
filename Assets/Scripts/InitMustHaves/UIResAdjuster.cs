using UnityEngine;

public class UIResAdjuster : MonoBehaviour
{

    //[SerializeField] private List<GameObject> UIElements;
    private float scaleValueY;
    private float scaleValueX;
    private float baseScaleY = 750.0f;
    private float baseScaleX = 1334.0f;


    // Start is called before the first frame update
    void Start()
    {

        scaleValueY = Screen.height/baseScaleY;
        scaleValueX = Screen.width/baseScaleX;


        for (int i = 0; i < gameObject.transform.childCount; i++) {
            gameObject.transform.GetChild(i).localScale = new Vector3(scaleValueX, scaleValueY, 1);

            
            gameObject.transform.GetChild(i).position = new Vector3(
                ((gameObject.transform.GetChild(i).transform.position.x - (Screen.width / 2)) * scaleValueX) + (Screen.width / 2),
                ((gameObject.transform.GetChild(i).transform.position.y - (Screen.height / 2)) * scaleValueY) + (Screen.height / 2),
                0
            );

            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
