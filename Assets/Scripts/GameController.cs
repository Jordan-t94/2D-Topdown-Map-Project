using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    private Transform _bulletSpawnParent;
    [SerializeField]
    private Texture2D[] _crosshairSprites;

    public Transform BulletSpawnParent
    {
        get
        {
            return _bulletSpawnParent;
        }

        private set
        {
            _bulletSpawnParent = value;
        }
    }

    public Texture2D[] CrosshairSprites
    {
        get
        {
            return _crosshairSprites;
        }

        private set
        {
            _crosshairSprites = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;

        CrosshairSprites = Resources.LoadAll<Texture2D>("crosshairs");
    }
}
