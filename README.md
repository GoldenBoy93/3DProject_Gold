# 3D 던전 만들기 개인과제

## 필수 기능

### - 기본 이동 및 점프 Input System, Rigidbody ForceMode
플레이어의 이동(WASD), 점프(Space) 등을 설정

![이동점프](https://github.com/user-attachments/assets/4b81f76f-ea02-440e-ae59-f31bd370cfb0)

---

### - 체력바 UI
UI 캔버스에 체력바를 추가하고 플레이어의 체력을 나타내도록 설정. 플레이어의 체력이 변할 때마다 UI 갱신.

(다른 영상자료의 우하단 게이지 참조)

---

### - 동적 환경 조사 Raycast UI
Raycast를 통해 플레이어가 조사하는 오브젝트의 정보를 UI에 표시.
예) 플레이어가 바라보는 오브젝트의 이름, 설명 등을 화면에 표시.

![동적환경조사](https://github.com/user-attachments/assets/72ae896a-4e98-4cde-b5ef-2b536ef56567)

---

### - 점프대 Rigidbody ForceMode
캐릭터가 밟을 때 위로 높이 튀어 오르는 점프대 구현
OnCollisionEnter 트리거를 사용해 캐릭터가 점프대에 닿았을 때 ForceMode.Impulse를 사용해 순간적인 힘을 가함.

![점프대](https://github.com/user-attachments/assets/f81c7fd1-05b4-4c86-98d2-027c3ba9727c)

---

### - 아이템 데이터 ScriptableObject
다양한 아이템 데이터를 ScriptableObject로 정의. 각 아이템의 이름, 설명, 속성 등을 ScriptableObject로 관리

<img width="236" height="310" alt="스크립터블오브젝트" src="https://github.com/user-attachments/assets/004eed24-a66a-4d32-a7d7-90e95071ef0a" />

<img width="211" height="113" alt="스크립터블오브젝트2" src="https://github.com/user-attachments/assets/2af9c26e-1195-4bbf-a6e0-153557eca757" />

(아이템 데이터와 소비아이템 능력 데이터)

---

### - 아이템 사용 Coroutine
특정 아이템 사용 후 효과가 일정 시간 동안 지속되는 시스템 구현
예) 아이템 사용 후 일정 시간 동안 스피드 부스트.

![스피드포션](https://github.com/user-attachments/assets/a1ff87db-348c-4cbd-b31c-c39148083061)

---

## 도전 기능 가이드

### - 추가 UI
점프나 대쉬 등 특정 행동 시 소모되는 스태미나를 표시하는 바 구현
이 외에도 다양한 정보를 표시하는 UI 추가 구현

![스태미너소모](https://github.com/user-attachments/assets/943abb6a-bace-43ce-af5a-845e6cf7843f)

---

### - 3인칭 시점
기존 강의의 1인칭 시점을 3인칭 시점으로 변경하는 연습
3인칭 카메라 시점을 설정하고 플레이어를 따라다니도록 설정

(다른 영상자료 참조)

---

### - 움직이는 플랫폼 구현
시간에 따라 정해진 구역을 움직이는 발판 구현
플레이어가 발판 위에서 이동할 때 자연스럽게 따라가도록 설정

![움직이는발판](https://github.com/user-attachments/assets/64015777-a03b-445e-a456-ebee401bb615)

---

### - 다양한 아이템 구현
추가적으로 아이템을 구현해봅니다.
예) 스피드 부스트(Speed Boost): 플레이어의 이동 속도를 일정 시간 동안 증가시킴. 더블 점프(Double Jump): 일정 시간 동안 두 번 점프할 수 있게 함. 무적(Invincibility): 일정 시간 동안 적의 공격을 받지 않도록 함.

![무적포션](https://github.com/user-attachments/assets/c9c1e09b-f16d-4d12-8748-806a5e8cdd1a)

(무적포션 : 포션 사용 후 체력 감소되는 것은 공복 상태 지속 데미지. 피격 확인은 화면 붉은 점등)

---

### - 상호작용 가능한 오브젝트 표시
상호작용 가능한 오브젝트에 마우스를 올리면 해당 오브젝트에 UI를 표시
예) 문에 마우스를 올리면 'E키를 눌러 열기' 텍스트 표시. 레버(Lever): 'E키를 눌러 당기기' 텍스트 표시. 상자(Box): 'E키를 눌러 열기' 텍스트 표시. 버튼(Button): 'E키를 눌러 누르기' 텍스트 표시.

![상자열기](https://github.com/user-attachments/assets/2802803c-7803-42e6-a8ea-7fd02c953605)

![어디로든문](https://github.com/user-attachments/assets/7c376c98-27d1-4d64-acc7-d7ab79198eee)

---

### - 플랫폼 발사기
캐릭터가 플랫폼 위에 서 있을 때 특정 방향으로 힘을 가해 발사하는 시스템 구현 특정 키를 누르거나 시간이 경과하면 ForceMode를 사용해 발사

![발사대](https://github.com/user-attachments/assets/b3c18dda-e969-4d39-9b12-b9becae540e1)

---

# **Commit Convention**

Feat:	새로운 기능 추가  
Fix:	버그 수정 또는 typo  
Refactor:	리팩토링  
Design:	CSS 등 사용자 UI 디자인 변경  
Comment:	필요한 주석 추가 및 변경  
Style:	코드 포맷팅, 세미콜론 누락, 코드 변경이 없는 경우  
Test:	테스트(테스트 코드 추가, 수정, 삭제, 비즈니스 로직에 변경이 없는 경우)  
Chore:	위에 걸리지 않는 기타 변경사항(빌드 스크립트 수정, assets image, 패키지 매니저 등)  
Init:	프로젝트 초기 생성  
Rename:	파일 혹은 폴더명 수정하거나 옮기는 경우  
Remove:	파일을 삭제하는 작업만 수행하는 경우  
