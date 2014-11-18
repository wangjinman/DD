using UnityEngine;
using System.Collections;

[System.Serializable]
public class ComboData{
	public int count2Next; //多少次后，到下一等级
	public float time;

	public ComboData(int c,float t){
		count2Next = c;
		time = t;
	}
}

public class Player : Character {
	private bool inDoubleJump = false;

	[SerializeField]
	int combo;
	public int Combo {
		get {return combo;}
		set {combo = value;}
	}
	public ComboData[] comboDatas = new ComboData[]{new ComboData(10,3.0f)}; 
	public ComboData CurrentCombo {
		get {return comboDatas[currentComboLevel];}
	}
	int currentComboLevel = 0;
	[SerializeField]
	float comboTimer;

	SkillBase[] skills;
	
	void Awake(){
		skills = GetComponentsInChildren<SkillBase>();
	}

	// Use this for initialization
	void Start () {
		OnInit();
	}

	public void HandleInput(InputData data){
		Vector3 velocity = new Vector3();
		if(controller.isGrounded )
		{
			inDoubleJump = false;
		}

		if (data.move.x != 0.0f){
			velocity.x = data.move.x;
		}
		
		// we can only jump whilst grounded
		if (data.move.y > 0.0f)
		{
			if (controller.isGrounded)
			{
				velocity.y = 1.0f;
				//_animator.Play( Animator.StringToHash( "Jump" ) );
			} else if(!inDoubleJump) {
				velocity.y = 0.8f;
				inDoubleJump = true;
			}
		}

		if (data.useSkill1){
			skills[0].Use(this);
		}

		if (data.useSkill2){
			skills[1].Use(this);
		}

		motor.AddSelfMove(velocity.x,velocity.y);
	}

	void OnEnable(){
		//playerInput.UnlockJump();
		//playerInput.UnlockMove();
	}
	
	// Update is called once per frame
	void Update () {
		Tick();
		//motor.MotorVelocity = velocity;
	}

	protected override void OnInit(){
		Combo = 0;
		base.OnInit();
	}

	protected override void Tick(){
		if (comboTimer > 0.0f)
			comboTimer -= Time.deltaTime;
		else{
			Combo = 0;
		}

		base.Tick();
	}

	public override void OnDie(Character killer){
		Debug.Log("Die");
	}

	public override void OnKill(Character other){
		Debug.Log(combo);
		Combo = Combo + 1;
		if (Combo > CurrentCombo.count2Next){
			MoveToNextComboLevel();
		}
		comboTimer = CurrentCombo.time;
	}

	void MoveToNextComboLevel(){
		currentComboLevel = currentComboLevel + 1;
		currentComboLevel = Mathf.Min(currentComboLevel,comboDatas.Length-1);
	}
}
