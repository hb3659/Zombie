using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private void Start()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
    {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행
        // 움직임 실행
        Move();
        // 회전 실행
        Rotate();

        // 입력값에 따라 애니메이터의 Move 파라미터값 변경
        playerAnimator.SetFloat("Move", playerInput.move);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move()
    {
        // 상대적으로 이동할 거리 계산
        Vector3 moveDistance =
            playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        // 리지드바디를 이용해 게임 오브젝트 위치 변경
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        // transform.position = transform.position + moveDistance;
        // 같은 방식이지만 사용하지 않는 이유는
        // 트랜스폼의 위칫값을 직접 변경하면 물리 처리를 무시하고
        // 위치를 덮어쓰기 때문
        // 벽을 무시하고 벽 반대쪽으로 이동할 수 있다.
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate()
    {
        // 상대적으로 회전할 수치 계산
        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;
        // 리지드바디를 이용해 게임 오브젝트 회전 변경
        playerRigidbody.rotation =
            playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }
}