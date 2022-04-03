using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//닷지-> 'instantiate'(매번 필요시마다 생성, 사용) / 메서드처럼 오브젝트를 실시간 파괴
//-> 성능 많이 요구 ->(for메모리 정리)가비지 컬렉션 유발 쉬움/ 이처럼 오브젝트를 너무 자주 생성, 파괴 -> 프리즈 현상 발생

//but 이번에는 '오브젝트 풀링(Object Pooling)'으로.. 게임 초기에 필요한 만큼 미리 생성
//pool에 쌓아두는 방식임

//발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; //생성할 발판의 원본 프리팹
    public int count = 3; //생성할 발판 수

    public float timeBetSpawnMin = 1.25f;   //다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f;    //다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn;    //다음 배치까지의 시간 간격

    public float yMin = -3.5f;    //배치할 위치의 최소 y값
    public float yMax = 1.5f;    //배치할 위치의 최대 y값
    private float xPos = 20f;    //배치할 위치의 x값

    private GameObject[] platforms;    //미리 생성할 발판들을 보관할 배열
    private int currentIndex = 0;    //사용할 현재 순번의 발판

    //초반에 생성할 발판을 화면 밖에 숨겨둘 위치
    private Vector2 poolPosition = new Vector2(0, -25);
    private float lastSpawnTime;    //마지막 배치 시점

        // 변수를 초기화하고 사용할 발판을 미리 생성
    void Start()
    {
        //count 만큼의 공간을 가지는 새로운 발판 배열 생성
        platforms = new GameObject[count];

        //count 만큼 루프하면서 발판 생성 / i=0(0,1,2 순차적 접근o)
        for (int i = 0; i < count; i++)
        {
            //platformPrefab을 원본으로 새 발판을 poolPosition 위치에 복제 생성
            //생성된 발판을 platform 배열에 할당
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }

        //마지막 배치 시점 초기화
        lastSpawnTime = 0f;
        //다음번 배치까지의 시간 간격을 0으로 초기화
        timeBetSpawn = 0f;
    }

    void Update()
    {
        // * 순서를 돌아가며 주기적으로 발판을 배치
        if (GameManager.instance.isGameover)
        {
            return; //게임오버니까 아래 실행x
        }

        //마지막 배치 시점에서 timeBetSpawn 이상 시간이 흘렀다면
        //업데이트가 실행되자마자 한 번 실행하겠다!
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            //기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastSpawnTime = Time.time; //Time.time 현재 플레이 된 시간

            //다음 배치까지의 시간 간격을 Min,Max 사이에서 랜덤 설정(랜덤하게 가져오기)
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //배치할 위치의 높이를 yMin과 yMax 사이에서 랜덤 설정(랜덤하게 가져오기)
            float yPos = Random.Range(yMin, yMax);

            //사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 즉시 다시 활성화 (OnEnable 메서드 때문에)
            //이때 발판의 platform 컴퍼넌트의 *OnEnable 메서드가 실행됨(플랫폼 스크립트 참조)
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            //현재 순번의 발판을 화면 오른쪽에 재배치
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            //순번 넘기기
            currentIndex++;

            //마지막 순번에 도달했다면 순번을 리셋
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}

