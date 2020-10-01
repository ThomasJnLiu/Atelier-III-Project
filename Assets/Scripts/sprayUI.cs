using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sprayUI : MonoBehaviour
{
    public float maxValue;
    public Image fill;
    public GameObject playerController;
    private float currentValue;
    // Start is called before the first frame update
    void Start()
    {
        currentValue = maxValue;
        fill.fillAmount = 1;
        fill = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = (1 - (float)playerController.GetComponent<Paint>().goList.Count / 2000);
        //Debug.LogWarning("goList count: '" + (1-(float)playerController.GetComponent<Paint>().goList.Count/2000));
    }
}
