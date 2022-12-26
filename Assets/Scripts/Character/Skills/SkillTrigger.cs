using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkillTrigger : MonoBehaviour, ISkillTrigger, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField]
	private LayerMask _targetlayer;

	private ISkill _skill;
	private Image _skillIcon;
	private Vector3 _startMousePosition;
	private Vector3 _defaultPosition;
	public ICharacter SelfTarget { get; set; }

	public UnityAction<ICharacter> OnHitObject { get; set; }

	private void Awake()
	{
		ComponentCaching();
	}

	private void ComponentCaching()
    {
		_defaultPosition = this.transform.localPosition;
		_skillIcon = this.GetComponent<Image>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (_skill.TargetLayer.value == 0)
			OnHitObject(SelfTarget);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_startMousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(this.transform.position);
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _startMousePosition);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Ray ray = Camera.main.ScreenPointToRay(eventData.position);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100, _skill.TargetLayer))
		{
			HitObject(hit.collider.GetComponent<Character>());
		}
		this.transform.localPosition = _defaultPosition;
	}

	public void SetSkill(ISkill skill)
	{
		_skill = skill;
		this._skillIcon.sprite = skill.IconSprite;
	}

	public void HitObject(ICharacter target)
	{
		OnHitObject(target);
	}

	public GameObject GetGameObject()
    {
		return this.gameObject;
    }
}