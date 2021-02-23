using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumSlots : MonoBehaviour
{
    [SerializeField] private List<Sprite> slotsIcons;
    [SerializeField] private List<SpriteRenderer> slotsItem;
    [SerializeField] private List<GameObject> lines;
    [SerializeField] private float time;
    [SerializeField] private bool isEnable;
    [SerializeField] private Text textField;
    [SerializeField] private Text textTime;
    [SerializeField] private bool isStop;
    [SerializeField] private Text textDate;
    private void Start()
    {
        Time.fixedDeltaTime = 0.3f;
        HideLines();
    }
    private void FixedUpdate()
    {
        Flipping();
    }
    private void Flipping()
    {
        if (isEnable)
        {
            isStop = false;
            slotsItem[3].sprite = slotsItem[4].sprite;//upper item = middle item
            slotsItem[4].sprite = slotsItem[0].sprite;//middle item = down item

            slotsItem[5].sprite = slotsItem[6].sprite;
            slotsItem[6].sprite = slotsItem[1].sprite;

            slotsItem[7].sprite = slotsItem[8].sprite;
            slotsItem[8].sprite = slotsItem[2].sprite;

            slotsItem[0].sprite = slotsIcons[Random.Range(0, slotsIcons.Count)];//and random down item
            slotsItem[1].sprite = slotsIcons[Random.Range(0, slotsIcons.Count)];
            slotsItem[2].sprite = slotsIcons[Random.Range(0, slotsIcons.Count)];
            isStop = true;
        }
    }
    public void FixedTimeUp() { Time.fixedDeltaTime += 0.01f; textTime.text = Time.fixedDeltaTime.ToString(); }
    public void FixedTimeDown() { Time.fixedDeltaTime -= 0.01f; textTime.text = Time.fixedDeltaTime.ToString(); }
    public void EnableOff() { Invoke("DelayNum", time); Invoke("CheckLines", time * 2f); }
    public void EnableOn()
    {
        isEnable = true;
        HideLines();
    }
    public void HideLines()
    {
        lines[0].SetActive(false);
        lines[1].SetActive(false);
        lines[2].SetActive(false); //that faster then use for()
        textField.text = "";
    }
    private void CheckLines()
    {
        HideLines();         //if nothing, for hide lines
        if (slotsItem[0].sprite == slotsItem[1].sprite && slotsItem[1].sprite == slotsItem[2].sprite) { lines[2].SetActive(true); textField.text = "U win!"; }
        if (slotsItem[4].sprite == slotsItem[6].sprite && slotsItem[6].sprite == slotsItem[8].sprite) { lines[1].SetActive(true); textField.text = "U win!"; }
        if (slotsItem[3].sprite == slotsItem[5].sprite && slotsItem[5].sprite == slotsItem[7].sprite) { lines[0].SetActive(true); textField.text = "U win!"; }
    }
    private void DelayNum() { isEnable = false; }
    public void Victory()
    {
        Invoke("DelayNum", time);
        if (isStop) Invoke("Win",time*3f);
        
    }
    private void Win()
    {
        for (int i = 0; i < slotsItem.Count; i++)
        {
            slotsItem[i].sprite = slotsIcons[Random.Range(0, 1)];
        }
        CheckLines();
    }
    [ContextMenu("API Using")]
    public void ShowCurrentTime()
    {
        textDate.text = System.DateTime.Now.ToString();
    }
}
