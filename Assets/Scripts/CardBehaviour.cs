using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _hitTextObj;
    [SerializeField] private bool _isEnemy;
    private PlayerCardMovement _playerCardMovementScr;
    private Vector3 _normalScale;
    private float _maxCallDistance;
    private int _difficulty;
    private int _hit;

    public int Hit { get { return _hit; } }

    private void Awake()
    {
        _normalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        AddValueToHit();
        var hitTMP = _hitTextObj.GetComponent<TMP_Text>();
        hitTMP.text = _hit.ToString();
        if (_isEnemy)
        {
            hitTMP.color = Color.red;
        }
        else
        {
            hitTMP.color = Color.green;
        }
        transform.DOScale(_normalScale, 0.5f);
    }

    private void AddValueToHit()
    {
        int hit = Random.Range(0, _difficulty + 1);

        if (_isEnemy)
        {
            _hit = -(1+ hit);
        }
        else
        {
            _hit = 1 + hit;
        }
    }

    public void OnPointerClick()
    {
        Vector3 playerPos = _playerCardMovementScr.gameObject.transform.position;

        bool horizontalOrVertical = (playerPos.x == transform.position.x) ^ (playerPos.y == transform.position.y);

        if (horizontalOrVertical & Vector3.Distance(transform.position, playerPos) < _maxCallDistance & _playerCardMovementScr.IsPlayerAlive)
        {
            _playerCardMovementScr.StartMovingTo(transform);
            _playerCardMovementScr.TakeHIt(_hit);
            StartCoroutine(DestroyCartCorutine());
        }
        else
        {
            StartCoroutine(ShakeCard());
        }
    }

    public void GetGameProperties(PlayerCardMovement playerCardMovement, float maxCallDistance, int difficulty)
    {
        _playerCardMovementScr = playerCardMovement;
        _maxCallDistance = maxCallDistance;
        _difficulty = difficulty;
    }

    private IEnumerator ShakeCard()
    {
        transform.DOShakePosition(0.5f , 3 , 20 , 90, true);
        yield return null;
    }

    private IEnumerator DestroyCartCorutine()
    {
        transform.DOScale(Vector3.zero, 0.3f);
        Destroy(gameObject, 0.33f);
        yield return null;
    }

}
