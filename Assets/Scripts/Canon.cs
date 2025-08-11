using UnityEngine;

public class Canon : MonoBehaviour
{
    public int shootPower; // 발사 파워

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 콜라이더의 리지드바디를 변수로 가져옴
        Rigidbody Rb = other.GetComponent<Rigidbody>();

        // 리지드바디에 에드포스
        Rb.AddForce(new Vector3(1,1,0) * shootPower, ForceMode.Impulse);
    }
}
