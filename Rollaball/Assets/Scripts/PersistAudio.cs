using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance = null;

    public static PersistentAudio Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
