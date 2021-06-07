using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** Parent class for gameObjects that can only exist one at a time.
 * Only inherit in child scripts attached to root GameObject! DontDestroyOnLoad requirement.
 */

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T scriptInstance;
    public static GameObject gameInstance;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameInstance == null && this.gameObject != gameInstance)
        {
            DontDestroyOnLoad(this.gameObject);
            scriptInstance = this as T;
            gameInstance = this.gameObject;
        }
        else
        {
            this.Destroy();
        }
    }

    // Convenience method
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

}