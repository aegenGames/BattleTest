using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BaseSkill : MonoBehaviour, ISkill
{
	[SerializeField]
	protected Sprite _icon;
	[SerializeField]
	protected LayerMask _targetLayer;

	[Header("State effects")]
	[SerializeField]
	[SerializeInterface(typeof(IStateEffect))]
	private List<Object> _effectsPrefab;
	protected List<IStateEffect> _effects => _effectsPrefab.OfType<IStateEffect>().ToList();

	[Header("Visual effects")]
	[SerializeField]
	protected ParticleSystem _particleAttackEffectPrefab;

	protected ParticleSystem _particleAttackEffect;
	public UnityEvent OnSkillFinished { get; } = new UnityEvent();
	public Sprite IconSprite { get => _icon; }
	public LayerMask TargetLayer { get => _targetLayer; }

	public UnityAction<string, bool> AttackAnimationEvent { get; set; }
	public event ISkill.ReturnOnStartPositionHandler ReturnOnStartPositionEvent;
	public event ISkill.MoveToEnemyHandler MoveToEnemyEvent;
	public event ISkill.WaitAnimationStateHandler WaitAnimationStateEvent;

	private void Start()
	{
		if (_particleAttackEffectPrefab)
		{
			_particleAttackEffect = Instantiate(_particleAttackEffectPrefab, this.transform.position, this.transform.rotation, this.transform);
			_particleAttackEffect.Stop();
		}
	}

	public virtual void BreakSkill()
	{
		StopAllCoroutines();
	}

	public virtual void StartSkill(Character target) { }

	protected IEnumerator MoveToEnemy(Transform target, float distanceToEnmy)
	{
		yield return StartCoroutine(MoveToEnemyEvent(target, distanceToEnmy));
	}

	protected IEnumerator ReturnOnStart()
	{
		yield return StartCoroutine(ReturnOnStartPositionEvent());
	}

	protected IEnumerator WaitAnimationState(string nameState, bool value)
	{
		yield return StartCoroutine(WaitAnimationStateEvent(nameState, value));
	}
}