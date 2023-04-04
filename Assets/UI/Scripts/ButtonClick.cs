using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip, _uncompressClip;
    [SerializeField] private AudioSource _audioSource;

    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
        _audioSource.PlayOneShot(_compressClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
        _audioSource.PlayOneShot(_uncompressClip);
    }
    
    public void WasClicked()
    {
        Debug.Log("Button was clicked");
    }
}
