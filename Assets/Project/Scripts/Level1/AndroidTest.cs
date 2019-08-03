using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class AndroidTest : MonoBehaviour,IPointerClickHandler
{
    public GameObject Postprocessing;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Postprocessing.activeInHierarchy)
        {
            Postprocessing.SetActive(false);
        }
        else
        {
            Postprocessing.SetActive(true);
        }
    }
}
