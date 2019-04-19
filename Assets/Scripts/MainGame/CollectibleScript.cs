using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CollectibleScript : MonoBehaviour,ISerializable
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] public static int count = 0;
    private AudioSource collectibleSource;
    [SerializeField] private AudioClip collectibleSound;

    public class DataToBeSaved
    {
        public int count;
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveManager.Instance.Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collectibleSource = collision.gameObject.transform.GetChild(3).GetComponent<AudioSource>();
            collectibleSource.PlayOneShot(collectibleSound);
            Destroy(this.gameObject);
            count++;
        }
    }

    public JObject Serialize()
    {
        DataToBeSaved Ds = new DataToBeSaved();
        Ds.count = count;

        string jsonString = JsonUtility.ToJson(Ds);

        return JObject.Parse(jsonString);
    }

    public void DeSerialize(string jsonString)
    {
        DataToBeSaved Ds = new DataToBeSaved();
        JsonUtility.FromJsonOverwrite(jsonString, Ds);
        count = Ds.count;
    }

    public string GetJsonKey()
    {
        return this.name;
    }
}
