using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_0 : MonoBehaviour
{
    private MeshRenderer _meashRen;
    public static event Messenger.OnCollision ChengeCol;
    [SerializeField] private Material _collisionMat;
    [SerializeField] private Material _myMat;

    void Start()
    {
        ChengeCol += ChangeColor;
        _meashRen = GetComponent<MeshRenderer>();
       // _collisionMat = _meashRen.material;
    }

    private void OnEnable()
    {
        MooveSpher.CollEnter += ChangeColor;
        MooveSpher.CollExit += ReturnColor;
    }

    public void ChangeColor(string name)
    {
        Vibration.Vibrate(40);
        if (name == this.gameObject.name)
        {
            _meashRen.material = _collisionMat;
        }
    }

    public void ReturnColor(string name)
    {
        if (name == this.gameObject.name)
        {
            _meashRen.material = _myMat;
        }
    }

    private void OnDisable()
    {
        MooveSpher.CollEnter -= ChangeColor;
        MooveSpher.CollExit -= ReturnColor;
    }
}
