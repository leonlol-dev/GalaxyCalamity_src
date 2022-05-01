using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCoolDownPickUp : MonoBehaviour
{
    [Header("Movement Variables")]
    public float rotationSpeed = 25f;
    public float vertSpeed = 5f;
    public float height = 0.2f;

    [Header("Teleport cooldown reduction")]
    public float cooldownTime = 0.5f;

    [Header("Objects to set")]
    public AudioSource sound;
    public GameObject model;
    public UpgradeListUI upgradeListUI;

    //Private variables
    private Vector3 pos;
    private bool entered;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.rotation.x, rotationSpeed * Time.deltaTime, transform.rotation.z);

        float newY = Mathf.Sin(Time.time * vertSpeed) * height + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !entered)
        {
            entered = true;
            other.GetComponent<PlayerTeleport>().cooldown -= cooldownTime;
            upgradeListUI.SendMessageToList("Teleport cooldown went down by: " + cooldownTime);
            sound.Play();
            model.SetActive(false);
            Destroy(this.gameObject, 1f);

        }
    }
}
