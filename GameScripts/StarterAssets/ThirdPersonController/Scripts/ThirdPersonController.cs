using Assets.StarterAssets.ThirdPersonController.Scripts;
using Assets.StarterAssets.ThirdPersonController.Scripts.Player;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        //public float SprintSpeed = 5.335f;
        public float SprintSpeed = 8.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Tooltip("Time required to pass before being able to roll again. Set to 0f to instantly roll again")]
        public float RollTimeout = 0.05f;
        public bool rolled = false;

        [Tooltip("Time required to pass before being able to punch again. Set to 0f to instantly punch again")]
        public float PunchTimeout = 0.08f;
        public bool punched = false;
        public float SprintPunchTimeout = 17.0f;

        [Tooltip("Time required to pass before being able to block again. Set to 0f to instantly block again")]
        public float BlockTimeout = 0.08f;
        public bool blocked = false;

        [Tooltip("Time required to pass before being able to doge again. Set to 0f to instantly doge again")]
        public float DogeTimeout = 0.08f;
        public bool doged = false;

        [Tooltip("Time required to pass before being able to recover again. Set to 0f to instantly recover again")]
        public float RecoverTimeout = 0.08f;
        public bool recovered = false;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;
        private float _rollTimeoutDelta;
        private float _punchTimeoutDelta;
        private float _blockTimeoutDelta;
        private float _dogeTimeoutDelta;
        private float _sprintPunchTimeoutDelta;
        private float _recoverTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDRoll;
        private int _animIDPunch;
        private int _animIDHeavy;
        private int _animIDBlock;
        private int _animIDGuard;
        private int _animIDSprintAttack;
        private int _animIDDoge;
        private int _animIDHit;
        private int _animIDRecover;
        private int _animIDDeath;

        //weapon animation IDs
        private int _unarmed;
        private int _armsword;
        private int _armaxe;
        private int _guard;


        private bool isHeavy = false;
        public bool isSprint = false;

        public BarSlide staminBar;
        public int totalStamin = 100;
        public int currentStamin = 100;
        public int recoverStamin = 20;
        public float deltaStamin = .0f;

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private PlayerInventory _playerInventory;
        private PlayerBasics _playerBasics;

        private const float _threshold = 0.01f;
        private int baseStamin;

        private bool _hasAnimator;

        public int CrystalID;
        public int lastID;
        public bool canTeleport;


        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }


        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            staminBar = FindObjectOfType<BarSlide>();
            staminBar.SetMaxStamina(totalStamin);
        }

        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM 
            _playerInput = GetComponent<PlayerInput>();
            _playerInventory = GetComponent<PlayerInventory>();
            _playerBasics = GetComponent<PlayerBasics>();

            baseStamin = _playerInventory.right_weapon.baseStamin;
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
            _rollTimeoutDelta = 0.0f;
            _punchTimeoutDelta = 0.0f;
            _blockTimeoutDelta = 0.0f;
            _dogeTimeoutDelta = 0.0f;
            _recoverTimeoutDelta = 0.0f;
            punched = false;
            
        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            baseStamin = _playerInventory.right_weapon.baseStamin;
            JumpAndGravity();
            GroundedCheck();
            CheckDeath();
            Doging();
            RollForward();
            CheckHit();
            Guarding();
            Blocking();
            Punch();
            ChangeWeapon();
            WeaponAnimations();
            Recover();
            Move();
            UpdateStamin();

            if (canTeleport)
            {
                teleport(CrystalID);
                canTeleport = false;
            }
        }

        public void SetCanTeleport(int ID)
        {
            CrystalID = ID;
            canTeleport = true;
        }

        public void teleport(int ID)
        {
            switch (ID)
            {
                case 0:
                    _controller.transform.position = new Vector3(342.79f, 21.2f, 73.5f);
                    break;
                case 1:
                    _controller.transform.position = new Vector3(352.3f, 21.01f, 205.7f);
                    break;
                case 2:
                    _controller.transform.position = new Vector3(402f, 0.085f, 247.17f);
                    break;
                case 3:
                    _controller.transform.position = new Vector3(533.96f, 31.086f, 227.18f);
                    break;
                case 4:
                    _controller.transform.position = new Vector3(603.4f, 19.83f, 201.72f);
                    break;
                case 5:
                    _controller.transform.position = new Vector3(688.3f, 19.83f, 256.6f);
                    break;
                case 6:
                    _controller.transform.position = new Vector3(695.29f, 4.97f, 175.49f);
                    break;
                    
            }
        }


        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDRoll = Animator.StringToHash("Roll");
            _animIDPunch = Animator.StringToHash("Punch");
            _animIDHeavy = Animator.StringToHash("Heavy");
            _animIDBlock = Animator.StringToHash("Block");
            _animIDGuard = Animator.StringToHash("Guard");
            _animIDSprintAttack = Animator.StringToHash("SprintAttack");
            _animIDDoge = Animator.StringToHash("Doge");
            _unarmed = Animator.StringToHash("Unarmed");
            _armsword = Animator.StringToHash("ArmSword");
            _armaxe = Animator.StringToHash("ArmAxe");
            _animIDHit = Animator.StringToHash("Hit");
            _animIDRecover = Animator.StringToHash("Recover");
            _guard = Animator.StringToHash("IsGuard");
            _animIDDeath = Animator.StringToHash("Death");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private void UpdateStamin()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Walk Run Blend") && !punched) {
                deltaStamin += Time.deltaTime;
                if(currentStamin < totalStamin && deltaStamin > 0.1f)
                {
                    currentStamin += Mathf.RoundToInt(deltaStamin * recoverStamin);
                    if (currentStamin > totalStamin) currentStamin = totalStamin;
                    staminBar.SetCurrentStamina(currentStamin);
                    deltaStamin = 0;
                }
            }
            else
            {
                deltaStamin = 0;
            }
        }
        private bool a = true;
        private void CheckDeath()
        {
            if (_playerBasics.currentHealth <= 0.1f && a)
            {
                _animator.SetBool(_animIDDeath, true);
                Invoke("Reborn", 3f);
                a = false;
            }
        }

        public void Reborn()
        {
            GameObject.Find("CrystalManager").GetComponent<CrystalData>().tpUI();
            _animator.SetBool(_animIDDeath, false);
            
            teleport(lastID);
            _playerBasics.recoverHealth();
            a = true;
        }
        private void Recover()
        {
            if (_input.recover && _recoverTimeoutDelta <= 0.0f && recovered == false)
            {

                _animator.SetBool(_animIDRecover, true);

                _recoverTimeoutDelta = BlockTimeout;
                recovered = true;
                if (_playerInventory.haveDrink)
                {
                    _playerBasics.recoverHealth();
                    GameObject.Find("MenuJogador").GetComponent<backPack>().hideImage(7);
                }
            }
            else if (recovered == true && _recoverTimeoutDelta <= 0.0f)
            {
                _animator.SetBool(_animIDRecover, false);
                _input.recover = false;
                recovered = false;
            }

            // recover timeout
            if (_recoverTimeoutDelta > 0.0f)
            {
                _recoverTimeoutDelta -= Time.deltaTime;
            }

        }

        private void CheckHit()
        {
            if (_animator.GetBool(_animIDHit) == true)
            {
                _animator.SetBool(_animIDHit, false);
            }
            if (_playerBasics.isHit)
            {
                _animator.SetBool(_animIDHit, true);
                _playerBasics.isHit = false;
            }
           
        }
        private void ChangeWeapon()
        {
            if (_input.changeweapon)
            {
                _playerInventory.changeRightWeapon();
            }
        }
        private void WeaponAnimations()
        {
            if (_playerInventory.cur_right_index == -1)
            {
                _animator.SetBool(_armaxe, false);
                _animator.SetBool(_armsword, false);
                _animator.SetBool(_unarmed, true);
            }
            else if (_playerInventory.right_weapons_slot[_playerInventory.cur_right_index].kind == 2)
            {
                _animator.SetBool(_armaxe, true);
                _animator.SetBool(_armsword, false);
                _animator.SetBool(_unarmed, false);

            }
            else if (_playerInventory.right_weapons_slot[_playerInventory.cur_right_index].kind == 1)
            {
                _animator.SetBool(_armaxe, false);
                _animator.SetBool(_armsword, true);
                _animator.SetBool(_unarmed, false);
            }
          
        }

        private void Move()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("AxeSprint") && _sprintPunchTimeoutDelta<0.0f) return;
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") || _animator.GetCurrentAnimatorStateInfo(0).IsName("AxePunch")) return;
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("HeavySwordStart") || _animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyAxe") || _animator.GetCurrentAnimatorStateInfo(0).IsName("HeavyFist")) return;
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

            if (!_input.guard)
            {
                // set target speed based on move speed, sprint speed and if sprint is pressed
                float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

                // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

                // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
                // if there is no input, set the target speed to 0
                if (_input.move == Vector2.zero) targetSpeed = 0.0f;

                // a reference to the players current horizontal velocity
                float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

                float speedOffset = 0.1f;
                float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

                // accelerate or decelerate to target speed
                if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                    currentHorizontalSpeed > targetSpeed + speedOffset)
                {
                    // creates curved result rather than a linear one giving a more organic speed change
                    // note T in Lerp is clamped, so we don't need to clamp our speed
                    _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                        Time.deltaTime * SpeedChangeRate);

                    // round speed to 3 decimal places
                    _speed = Mathf.Round(_speed * 1000f) / 1000f;
                }
                else
                {
                    _speed = targetSpeed;
                }

                _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
                if (_animationBlend < 0.01f) _animationBlend = 0f;

                // normalise input direction
                Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

                // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
                // if there is a move input rotate player when the player is moving
                if (_input.move != Vector2.zero)
                {
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                      _mainCamera.transform.eulerAngles.y;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }
                transform.position = Vector3.Lerp(transform.position, inputDirection, Time.deltaTime * SpeedChangeRate);

                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

                // move the player
                _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                                 new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetFloat(_animIDSpeed, _animationBlend);
                    _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
                }
            }
        }

        private void Guarding()
        {
            if (_input.guard)
            {
                _animator.SetBool(_animIDGuard, true); 
            }
            else
            {
                _animator.SetBool(_animIDGuard, false);
            }
        }
        private void Blocking()
        {
            if (_input.block && _blockTimeoutDelta <= 0.0f && blocked == false)
            {

                _animator.SetBool(_animIDBlock, true);

                _blockTimeoutDelta = BlockTimeout;
                blocked = true;
            }
            else if (blocked == true && _blockTimeoutDelta <= 0.0f)
            {
                _animator.SetBool(_animIDBlock, false);
                _input.block = false;
                blocked = false;
            }

            // block timeout
            if (_blockTimeoutDelta > 0.0f)
            {
                _blockTimeoutDelta -= Time.deltaTime;
            }
        }

        private void Doging()
        {
            if (_input.doge && _dogeTimeoutDelta <= 0.0f && doged == false)
            {

                _animator.SetBool(_animIDDoge, true);
                if (_input.move != Vector2.zero)
                {
                    Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                      _mainCamera.transform.eulerAngles.y;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }

                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * new Vector3(-1.0f, 0.0f, -1.0f);
                _controller.Move(targetDirection.normalized * (20.0f * Time.deltaTime) +
                                 new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
                _dogeTimeoutDelta = DogeTimeout; 
                doged = true;
            }
            else if (doged == true && _dogeTimeoutDelta <= 0.0f)
            {
                _animator.SetBool(_animIDDoge, false);
                _input.doge = false;
                doged = false;
            }

            // punch timeout
            if (_dogeTimeoutDelta > 0.0f)
            {
                _dogeTimeoutDelta -= Time.deltaTime;
            }
        }

        private void Punch()
        {   
            if (_input.heavy)
            {
                isHeavy = true;
            }
            else
            {
                isHeavy = false;
            }

            if (_input.sprint)
            {
                isSprint = true;
            }
            else
            {
                isSprint= false;
            }

            if (_input.punch && _punchTimeoutDelta <= 0.0f && punched == false)
            {
                var actualStamin = baseStamin;
                if (!isHeavy && !isSprint)
                {

                    _animator.SetBool(_animIDPunch, true);
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Walk Run Blend")) currentStamin = (currentStamin >= 25) ? currentStamin - 25 : 0;
                    staminBar.SetCurrentStamina(currentStamin);
                }
                
                else if (isSprint)
                {
                    actualStamin = 3 * actualStamin / 2;
                    _animator.SetBool(_animIDSprintAttack, true);
                    _sprintPunchTimeoutDelta = 1.7f;
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Walk Run Blend")) currentStamin = (currentStamin >= actualStamin) ? currentStamin - actualStamin : 0;
                    staminBar.SetCurrentStamina(currentStamin);
                }
                else
                {
                    actualStamin *= 2;
                    _animator.SetBool(_animIDHeavy, true);
                    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle Walk Run Blend")) currentStamin = (currentStamin >= actualStamin) ? currentStamin - actualStamin : 0;
                    staminBar.SetCurrentStamina(currentStamin);
                }
                

                _punchTimeoutDelta = PunchTimeout;
                punched = true;
            }
            else if(punched == true && _punchTimeoutDelta <= 0.0f)
            {
                _animator.SetBool(_animIDPunch, false);
                _animator.SetBool(_animIDHeavy, false);
                _animator.SetBool(_animIDSprintAttack, false);
                _input.punch = false;
                punched = false;
            }

            // punch timeout
            if (_punchTimeoutDelta > 0.0f)
            {
                _punchTimeoutDelta -= Time.deltaTime;
            }
            if(_sprintPunchTimeoutDelta > 0.0f)
            {
                _sprintPunchTimeoutDelta -= Time.deltaTime;
            }
        }
        private void RollForward()
        {   
            if (_input.roll && _rollTimeoutDelta <= 0.0f && rolled == false)
            {
                _animator.SetBool(_animIDRoll, true);
                _rollTimeoutDelta = RollTimeout;
                rolled = true;
                //
                Invoke("goforward", 0.3f);
                //
            }
            else if(_rollTimeoutDelta <= 0.0f)
            {
                _animator.SetBool(_animIDRoll, false);
                _input.roll = false;
                rolled = false;
                
            }

            // roll timeout
            if (_rollTimeoutDelta >= 0.0f)
            {
                _rollTimeoutDelta -= Time.deltaTime;
            }

        }
        private void goforward()
        {
            _controller.Move(new Vector3(0f, 0f, 0.4f));
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                    
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }

        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }
    }
}