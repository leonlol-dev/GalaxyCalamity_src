using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeListUI : MonoBehaviour
{
    public int maxMessages = 10;

    //public GameObject chatPanel, textObject;

    [SerializeField]
    public List<Message> messageList = new List<Message>();

    public GameObject chatPanel, textObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    //SendMessageToList("SPACE KEY PRESSED");
        //}
    }

    public void SendMessageToList(string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message(); 

        newMessage.text = text;

        GameObject newText = GameObject.Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }

    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;
    }
}
