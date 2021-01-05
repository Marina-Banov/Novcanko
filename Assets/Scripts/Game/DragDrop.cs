using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

	[SerializeField] private Canvas canvas = null;

	private RectTransform moneyBox, moneyBoxClone;
	private CanvasGroup canvasGroup;

	static public int moneyNumber = 0;
	static public string moneyName;

	public bool droppedOnSlot = false;

	public Vector3 primaryPos;

	private void Start() {
        primaryPos = GetComponent<RectTransform>().localPosition;
    }

    private void Awake() {
        moneyBox = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //defaultPos = GetComponent<RectTransform>().localPosition;
    }

	public void OnBeginDrag(PointerEventData eventData) { 
		canvasGroup.blocksRaycasts = false;
		if (droppedOnSlot) {
			droppedOnSlot = false;
			moneyNumber = getMoneyNumber(moneyName);
			RemovedMoney(moneyNumber);
		}
	}

	public void OnDrag(PointerEventData eventData) {
		moneyNumber = getMoneyNumber(moneyName);
		if (droppedOnSlot) {
			moneyBox.anchoredPosition = primaryPos;
		} else {
			moneyBox.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}
	}
 
	public void OnEndDrag(PointerEventData eventData) {
		canvasGroup.blocksRaycasts = true;
		//int cloneNumber = checkClone(moneyName);
		if (droppedOnSlot) {
			cloneObject();
		} else {
			this.moneyBox.localPosition = primaryPos;
		}
	}

    public void OnPointerDown(PointerEventData eventData) {
    	//Debug.Log("OnPointerDown");
    } 

    public int getMoneyNumber(string MoneyName) {
    	moneyName = GetComponent<Image>().name;
		string moneyNumber_str = moneyName.Substring(2);
		int moneyNumber = int.Parse(moneyNumber_str);
		return moneyNumber;
    }

    public void RemovedMoney(int number) {
    	GiveAmount.givenNumber -= number;
    	moneyBox.gameObject.tag = "Untagged";
    	int cloneCount = checkClone(moneyName);
    	if (cloneCount != 0) {
    		Destroy(moneyBox.gameObject);
    	}
    }

    int checkClone(string objectName) {
    	int cnt = 0;
    	foreach(GameObject gameObj in GameObject.FindObjectsOfType<GameObject>()) {
			//Debug.Log("moneyName: " + moneyName);
			if(gameObj.name == moneyName && gameObj.tag == "Untagged") {
			     cnt++;
			}
		}
		return cnt;
    }

    void cloneObject() {
		moneyBoxClone = Instantiate(moneyBox);
		moneyBoxClone.name = moneyBox.name;
		moneyBoxClone.transform.SetParent(canvas.transform);
		moneyBoxClone.tag = "Untagged";
		moneyBoxClone.GetComponent<DragDrop>().droppedOnSlot = false;
		moneyBoxClone.localPosition = primaryPos;
    }
}
