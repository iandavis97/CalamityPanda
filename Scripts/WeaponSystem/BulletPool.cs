using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

    public static Dictionary<GameObject, BulletPool> PoolDirectory;
    static BulletPool()
    {
        PoolDirectory = new Dictionary<GameObject, BulletPool>();
    }

    public GameObject Bullet;
    public int InitialSize;
    public bool CanGrow;

    private Stack<GameObject> free;

	// Use this for initialization
	void Awake () {
        PoolDirectory[Bullet] = this;
        free = new Stack<GameObject>();
        for (int i = 0; i < InitialSize; i++)
        {
            GameObject obj = Instantiate(Bullet, transform);
            obj.SetActive(false);
            obj.GetComponent<BulletMovement>().SetPool(this);
            free.Push(obj);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetBullet()
    {
        if(free.Count > 0)
        {
            free.Peek().SetActive(true);
            return free.Pop();
        }
        else if(CanGrow)
        {
            GameObject obj = Instantiate(Bullet, transform);
            obj.SetActive(true);
            obj.GetComponent<BulletMovement>().SetPool(this);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void Free(GameObject bullet)
    {
        free.Push(bullet);
        bullet.SetActive(false);
    }
}
