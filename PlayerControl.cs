using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    GameObject _player;
    Transform _playerTransform;

    // Use this for initialization
    void Start()
    {
        this._player = this.gameObject;
        this._playerTransform = this._player.GetComponent<Transform>();
    }

    void Update () {
        this._playerTransform.Translate(this._playerTransform.forward * Time.deltaTime*2);
	}
}
