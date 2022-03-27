using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerCardMovement : MonoBehaviour
{
    [SerializeField] private GameObject _healthTextObj;
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    private bool _isPlayerAlive;

    public bool IsPlayerAlive { get { return _isPlayerAlive; } }

    public delegate void PlayerCard();
    public static event PlayerCard OnMoved;
    public static event PlayerCard IsDead;

    private void Start()
    {
        _isPlayerAlive = true;
        _healthTextObj.GetComponent<TMP_Text>().text = _health.ToString();
    }

    private void Update()
    {
        _healthTextObj.GetComponent<TMP_Text>().text = _health.ToString();

        if (_health <= 0 & _isPlayerAlive)
        {
            _isPlayerAlive = false;
            IsDead();
        }
    }

    public void StartMovingTo(Transform target)
    {
        StartCoroutine(MoveToCorutine(target));
    }

    public void TakeHIt(int hit)
    {
        _health += hit;
    }

    public IEnumerator MoveToCorutine(Transform target)
    {
        transform.parent = target.parent;
        transform.DOMove(target.parent.position, _speed);
        OnMoved();
        yield return null;
    }
}
