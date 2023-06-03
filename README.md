# Grim Reaper - 모바일 기반 RPG


## 주의사항
1. Push 이전에 항상 **pull** 할것 있는지 확인하기
2. **Scene 작업** 전에는 항상 Scene 관리자에게 물어보고 작업하기
3. 작업 내용의 **주석**은 최대한 상세히
3. **상속 구조로 이루어진 스크립트**는 기존의 변수명 수정은 최대한 삼가고 불가피하게 작업을 해야하는 경우는 팀원들에게 미리 알리고 주석을 상세히 달기
4. 개발 일정 확인하기 - [개발 일정](https://app.asana.com/0/1204352798021693/list)


---
### V.5.0.01 - GY
> Looting System 개편
  - 다중 몬스터 루팅시 나던 버그 수정 완료
> Ignore Mouse Click 수정
  - Laycast로 인지할수없는 상황에 마우스가 UI 밖으로 나갔을때 클릭이 동작하지않는 현상 수정
  - UI를 닫을때 마우스 클릭을 인지할수없엇던 상황 수정
---
  ## Phase 5 추가 개발

---
### V.4.0.12 - GY
> Light Map
  - Forest , Village , Boss Map Light Map 추가
> 추가개발 일정
  - Phase5
    * 6.4 ~ 6.10 개발 된 요소 중에서 버그가 있는 부분 수정
    * 6.11 ~ 6.24 추가적인 개발 진행 - 6.3 ~ 6.4 에 내용 확정예정
---
### V.4.0.11 - JS
> 영상 촬영
  - 각 Game Scene에서 촬영 후 자막 작업 하는 방식으로 진행
  - Build, Light 등 후처리 관련 부분은 추가 작업 진행하는 것으로 합의

---
### V.4.0.10 - GY
> Scriptmanager
  - JSON File 읽고 텍스트박스에 출력하는 기능 추가
> Shopmanager
  - 구매 버튼 개선 및 재화 시스템과 연동
> Economy
  - 재화 시스템 구현
> EnchantSystem
  - 재화를 사용하여 레벨 증가 시스템 구현
    * 속성 공격력(스킬데미지) 증가 시스템
    * 확률형 시스템으로 제작함
> MiniMap
  - 플레이어 하위 오브젝트로 미니맵카메라 세팅 후 Raw Image 출력으로 구현
> Spawnner
  - 스포너 각각의 개체에 생성가능한 몬스터카운트 지정가능하게 변경
  - 각 스포너의 위치주변에서 몹이 생성되게 적용
---
### V.4.0.09 - JS
> Cut Scene  
  - CutScene Script revised  
> Loading Scene
  - UI 생성 및 Sprite 분류 
> Forest Scene  
  - CutScene01 - Intro Scene
  - CutScene02 - Play-In CutScene
> Build Index 정리  

> 2차 Cam Test 완료  
  - Build 후 라이트 Auto Generate만 해결하면 문제 없을 것으로 보임

---
## Scene Merge & Map Discussion
> Boss Scene
   - 마법진 입체감 향상을 위해 이중 Image로 변경하고 Animation 처리를 통해 생동감을 주는 작업 진행 예정 (수민)  
   - 마법진 블룸처리 (URP와 충돌 문제 해결가능 한지 확인해보고 진행)  
   - 맵 톤 설정  
     * 씬 전환 시 맵 전체적인 분위기 연결을 위해 Light 색 조정 (Tone down)  
       <img src = "https://github.com/kirby1004/AtentsTeamProject/assets/105345909/0b9378d7-3fae-49e8-b0f6-d956dffec242.JPG" width = "300" height ="220"/>
       <img src = "https://github.com/kirby1004/AtentsTeamProject/assets/105345909/f6c9f14e-a5ed-4fb5-89c1-6711a6000a7e.JPG" width = "300" height ="220"/>
       <img src = "https://github.com/kirby1004/AtentsTeamProject/assets/105345909/bec489ed-22dc-456d-b7d3-2f1776c826d5.JPG" width = "300" height ="220"/>

> Forest Scene  
  - 등장 연출 토의  
    * Warp Cut Scene 제작 예정 (진수)
    * 중간 보스 조우 Cut Scene 제작 예정 (진수)  
      + 걸어가다가 서부영화처럼 발부터 올라가면서 캐릭터 전체를 보여주고 다리 너머 기사들이 대화하다가 캐릭터를 발견한 후 캐릭터에게 달려오는 듯하게 연출

> Village Scene  
  - 등장 연출 토의
    * Dummy NPC 생성하고, Nav Mesh Agent 이용해서 정해진 경로를 움직이다가 Player를 만날 시 반대로 도망가는 연출 삽입
    * Village 소환 CutScene 필요시 제작

> Additive Scene
  - Intro, Death, Loading Scene 제작 예정
  - Loading Scene의 경우 각 Map의 스틸 컷을 활용하여 이미지 사용 할 예정 (명오)

> UI  
  - 미니맵 (교원)

---
### V.4.0.08 - JS
> Boss Scene  
  - Boss CutScene 시네머신으로 제작 완료    
      * Boss CutScene01 Encounter 테스트 완료  
         + CutScene01 에서 GameScene으로 바로 넘어가는 방식 // 동기 방식 씬 전환
      * Boss CutScene02 Ending
         + Additive 방식으로 씬 다중 Play 방법으로 씬 전환  
         + CutScene02 재생 시 Transform 넘겨 주는 방법 고려필요
         + CutScene02 재생 이후 본 GameScene으로 넘어왔을 때, 이전 상태 유지하는 방법 고려 필요

> Boss Skill & Effect
  - DevilEye
    * 등장 시 사용되고 공격 기능은 없으므로 Object Pool List로 프리팹 위치 변경 
  - SpitFire  
    * 수민이와 협업 중, 충돌 처리 관련 수정 작업중
  - Bress
    * 추가적인 Boss Skill 제작 구상 중...

> 1st Camera Test  
  - 1차 보스 씬 촬영 영상 Upload
    * https://youtu.be/V-PLCbNNiCU

---
## 1st Camera Test Meeting (05-21, 22)
> UI Bug Corrected  

> 공격력, 방어력 계산식 / 세부 능력치 설계  

> 재화 관련 구조 설계 및 코드 작성 방향 토의  

> 시네머신 연출 관련 토의  

---
### V.4.0.07 - GY
> Forest Scene
  - 포탈 앞 가드몹 2마리 배치 - 몬스터 스텟 조정 필요
> UI
  - Player , Dragon 전용 체력바 추가
    * 게이지로 시각적인 표현을 추가하고 플레이어는 남은체력/최대체력 , 드래곤은 남은 비율 로 추가적인 표기
  - 전반적인 이미지 교체작업
  - 버그수정
    * 특정상황에서 루팅아이템의 상세정보가 출력되지않는현상 - 원인파악은 못하고 고침
    * UI창을 종료버튼으로 닫으면 메뉴바 아이콘들의 클릭이 작동하지않는현상
    * UI창 위에 마우스가 올라와있을때 캐릭터의 공격이 동작하는 현상
---
### V.4.0.06 - JS
> Boss Scene  
  - Boss OnCreate Skill - DevilEye 추가
> Effect
  - URP Setting adopted
---
### V.4.0.05 - JS
> Boss Scene  
  - Game Manager 추가
  - UI Manager 추가 (Event Handler)
  - Skill Manager 추가 - Skill 사용 Test
  - Boss Map 
    * Plane Collider 추가
    * CineMahcine - 추가 예정
---
### V.4.0.04 - GY
> 1. UI  
- LootingSystem 업데이트  
  * LootingWindow 외형구현  
  * LootingItems 드랍테이블 구현 및 확률에 따른 아이템 드랍 구현
  * 다수의 몬스터 처치시 루팅UI는 뜨지만 몬스터가 사라지지않는현상 존재
- BossHpBar 기능 구현 - 외형적인 부분 추가 필요
> 2. Monster
- 몬스터 모델 2종 추가  
  * Knight , Guard 모델 Animatior 추가 , 위치 배치 , 애드 범위 설정
---
###
---
### V.4.0.02 - JS
> Scene 정리  
  - Village 씬 - 수민
  - Forest 씬 - 교원
  - Boss 씬 - 진수

> Warp Point 지정  
  - 씬 전환 담당 => 진수 (~5/19)  
  - 씬 Script 추가 (Intro Scene.cs, Scene Loader.cs)

> Boss Script
  - Boss Attack Pattern Coroutine 반복 호출 에러 수정
  - Boss Attack 각 패턴 별 Attack Delay 수정 가능하게 변경
---
### V.4.0.01 - SM
>  1. Camera
  - SpringArm 수정
    * 시점 변환할 때 이상한 오류 수정.
> 2. Skill
  - SkillManager 수정
    * 스킬매니저가 생성될 때 enum의 수만큼 Dictionary Add해서 쿨타임 관리
> 3. NPC
  - ShopNPC 수정
    * NPC의 PlayerPoint의 로테이션으로 캐릭터가 돌아가지 않는 오류가 생김 수정예정.
    * Trigger Enter, Exit 상태일 때 코드 수정. 
---
## ***Phase4*** 05.15(Mon) ~ 05.30(Tue)
---
### V.3.0.08 - SM
>  1. Player
  - PlayerController 수정
    * Attack할 때 움직일 수 없도록 수정.
    * IinterPlay 인터페이스 상속
      + IinterPlay 인터페이스는 오브젝트와 상호작용할 때 쓰이는 함수, 이벤트를 정의함.
      + Root, Npc, Warp와 상호작용할 예정.
  - Animator 수정.
    * Roll 애니메이션에 Roll State를 추가.
    * Die, Damage 레이어 설정 및 테스트 했음.
> 2. NPC
  - ShopNPC 스크립트 생성
    * ShopNpc 스크립트로 플레이어와 상호작용 테스트할 예정
    * 카메라 이동, UI 함수의 순서를 정하는 구조 고민중..
    * 카메라 이동이 안되는 오류가 있음.. (고칠예정..)
> 3. Layer
  - Invincible 레이어 추가
    * 10번에 무적 레이어 추가, 영어 임의로 넣음.. 수정해주세요.. 감사합니다.
---
### V.3.0.07 - JS
> CharacterMovement_V2.cs  
  - Awake 함수 추가
  - Attack 함수 Player 안쓰면 몬스터로 내리는 거 고려 필요 - with SM 

> AttackPhase.cs  
  - Attack 함수 두 AttackPoint Binding으로 공격 위치 해결  
  - Pattern delay 주기 작업 실패...  
  - AttackPattern 에 ObjectPooling 추가 하는 Idea 구상 완료  

> Monster.cs  
  - Fly 시 Recall 상태로 돌아가지 않도록 Monster에 canFly 변수 추가 및 LostTarget() 업데이트  

---
### V.3.0.06 - SM
>1. Skill 
* Skill Data 스크립트 수정
* Skill 스크립트 수정 (skill 스크립트는 수정할 수도 있음.)
  - ISkill 인터페이스 클래스를 가지고 있고 ISkill안에 use함수를 가지고 있음.
  - Protected로 SkillData와 Transform인 HitPoint를 가지고 있음.
  - 각각의 스킬들은 Skill이랑 ISkill을 상속받으면됨. 
* Skill Manager 수정
  - Dictionary<SkillName,bool>로 스킬 쿨타임을 관리한다. bool값을 이용해서 스킬이 사용될 수 있는 상태인지 확인.
  - ResiterSkill(SkillName name, Transform Point)라는 함수를 이용해서 스킬 이펙트 및 스킬 사용.
    + 스킬 사용 전 딕셔너리의 bool값으로 스킬이 사용될 수 있는 상태인지 확인.
    + GetComponent<ISkill>.use로 스킬 사용하면 된다.
    + 끝에 CoolDown 함수를 실행시켜 스킬 쿨다운.
  - CoolDown이라는 코루틴으로 스킬의 쿨타임을 관리함. 코루틴이 끝나면 딕셔너리의 bool값을 바꿔주어 스킬이 사용될 수 있는 상태로 바꿔줌.
---
### V.3.0.05 - GY
> Looting  
- 루팅창 외형 구현  
- 몬스터 드랍테이블 구현 및 드랍확률 구현
- 플레이어와 죽은몬스터간의 연결작업 필요
- 단일 루팅 및 전체 루팅 기능 구현
> Looting MouseOver
- 마우스오버창 외형 구현
- 루팅될 아이템에 마우스를 올리면 우측 상단에 아이템 정보 출력
- 아이템 루팅시 자동으로 마우스오버창 꺼지도록 설정
---
### V.3.0.04 - SM  
> 1. Player
* ComboAttack 수정
* PlayerController 수정
  - 움직임 처리 및 등등 (오류 있어서 더 수정할 예정)
* Animator, Animation
  - 데미지, 죽는 애니메이션 추가함. 
  - PlayerController을 수정하면서 스킬 애니메이션, 구르기 애니메이션이 이상해짐(수정 예정..)
>2. Skill (구조 확정은 아닌데 일단 올려둠)
* Skill Data 스크립트 생성
  - 스크립터블 오브젝트를 상속받는 SkillData, 스킬 데이터 저장함. 
* Skill 스크립트 생성
  - SkillData와 스킬을 사용할 때 처리할 함수를 Skill 스크립트에서 구현할 예정임. 
  - 각각의 스킬들은 Skill 스크립트를 상속받아 FireBall 같은 스킬 스크립트를 만듦..
* Skill Manager 생성
  - 일단 SkillManager은 instance로 해놓았고 나중에 GameManager에서 접근해서 사용하던지 구조 바꾸면 됨..
  - 지금 구조는 SkillManager가 SkillName이라는 enum을 가지고 있음.
  - ResiterSkill(SkillName name, Transform Point)라는 함수를 이용해서 스킬 이펙트 및 스킬 사용.
    + ObjectPoolingManager에서 name을 string으로 바꿔서 스킬 이펙트를 가져오고 GameObject skilleffect에 저장.
    + skilleffect.GetComponent해서 스킬의 Use함수를 불러와서 스킬을 사용하는 구조를 생각하고 있음.
>3. Camera
- SpringArm 스크립트 생성.
 + 기존에는 카메라를 움직였는데 SpringArm을 움직이는 걸로 함. 
- 시점 변경
  + 캐릭터의 머리를 바라보게 했는데 숄더뷰로 바꿨음.
- toggle, 카메라 대상 전환은 새로 바뀐 카메라에 맞게 수정할 예정..
---
### V.3.0.03 - JS  
> Enemy State Machine 완료 

> Boss  
- Enemy Inherit 구조 사용 가능하게 변경  
- FlyState 구현 완료  

> DragonAI Scene에서 Boss-Player 피격 타격 구현   
---
### V.3.0.02 - GY
> 미사용 프리펩 및 스크립트 정리  
- Slot 통합작업 중 생긴 미사용 프리펩 및 스크립트 정리  
---
### V.3.0.01 - GY
> Slot 통합  
- Inventory , Equipment , Quick , Skill , Soul , Shop Slot 스크립트 및 프리펩 단일객체화  
> Mananger 싱글톤화 및 자동생성  
- UI , Inventory , Equipment , Status , Shop Mananger 로드시 자동 생성 및 참조
---
### V.3.0.00 - JS / Midterm discussion
> Enemy -> Monster 이름 변경  

> Script 정리  
- 쓸모없는 스크립트 정리
- 프로퍼티 공통적으로 쓰는 값들 정리하고 안쓰는 것들은 각자 스크립트에서 오버라이딩

>  SingleTon 구현할 내용 정리  
- GameManager에 들어갈 내용 전체적으로 간추리기

> Skill Manager 구현 방법 토의  

> UI 구현 개념 정리  
---
## ***Phase3*** 05.01(Mon) ~ 05.14(Sun)
---
### V.2.0.11 - JS  
> Enemy State - Battle, Recall 상태 구현  

> Enemy State - Die 상태 구현 예정 (~5/2)  
---
### V.2.0.10 - GY
> Inventory 버그 픽스  
- 장착중인 장비를 아이템이 있는 칸으로 옮겻을때 슬롯에 맞지않는 장비로 이동되는 현상 수정  
ex) 장착중인 무기를 인벤토리의 갑옷이 잇는 칸으로 옮겻을때 갑옷이 무기칸에 장착되는 현상
---
### V.2.0.9 - GY
> ShopManager 구현 
- 상점 외형 , 상점 목록 , 상점 아이템 , 상점 슬롯  
ShopManager , ShopItem, ShopSlot, ItemInfo 스크립트 추가
> ScriptableObject 추가  
- ItemStatus , ShopItemList , PlayerStatus 추가
---
### V.2.0.8 - SM   
> 1. Player
* Player Scene추가.
  - Player 테스트 씬 (플레이어, NPC 테스트 중)
* 플레이어 하위에 CameraPoint, ViewPoint 추가.
  - 빈 오브젝트인 CameraPoint(카메라의 위치), ViewPoint(카메라가 쳐다보는 곳)
* 플레이어 애니메이터, 애니메이션
  - 연속 공격, 움직임 옮겨둠 => 데미지 및 죽는 처리는 옮길 예정.
* NPC와 상호작용 추가.
  - TriggerEnter, Stay, Exit으로 구현. 
  - isNPC로 NPC 범위에 들어와있는지 감지 후 상호작용 키를 누르면 isShop 반응( isShop보다는 isUI나 다른 변수명으로 바꿀 예정)
    + isShop이 반응할 때 게임 매니저에서 NPC의 타입을 체크하고 거기에 알맞는 UI를 실행시킴.
  - isShop이 true가 될 때 NPC와 상호작용하여 UI가 열리는 것이므로 카메라를 NPC의 ViewPoint로 옮겨준다. 
    + 만약 상점과의 스크립트(대화)가 있다면 스크립트 후에 UI가 열리도록 해야함.
* Player 구르기 스킬 및, 다른 스킬 구현할 예정. 
>2. NPC
- NPCProperty 스크립트 생성
  * 열거형 NPCType 선언 => NPC 종류를 구별한다.
  * NPCType의 NpcType을 선언해 프로퍼티로 구현, get으로만 가져올 수 있도록 함.
  * Transform 형식인 PlayerPoint와 ViewPoint를 저장함.(카메라와 플레이어 위치를 저장하는 빈오브젝트.)
- NPCProperty를 상속받는 스크립트를 붙여주고 NPC타입에 맞는 enum을 설정해주면 됨.
>3. Camera
- 카메라가 플레이어가 아닌 다른 시점으로 이동하는 함수와 다시 돌아오는 함수를 구현.
 + 이동하는 함수는 코루틴으로 작성함. 보간으로 이동하며 위치는 NPC에서 받아와 Player에서 GetComponent로 실행함.
- 카메라가 플레이어에 붙어있는지 아닌지를 판별하는 bool isPlayer 추가해서 isPlayer가 false일 땐 마우스 입력에 의해 카메라가 움직이지 않도록 함.
    
---
### V.2.0.7 - JS  
> State - Trace Script 수정  
- Interface 재정의 누락으로 인한 
---
### V.2.0.6 - JS  
> CharacterProperty Script 수정  
- Protected 한정자 일부 Public으로 수정
> State - Idle, Trace Script 구현  
> State - Fly Script 구현  
- Fly Enter 구현, Land 상태 구현 필요
- Fly Animation 
---
### V.2.0.5 - GY
> Inventory 구현  
 - ItemSlot , ItemStatus , Item 추가  
   Item drag & drop , Item Add 구현  
> Equipment 부분 구현
 - EquipmentSlot , EquipmentItem 추가  
 장비칸의 타입에 맞는 장비만 착용되도록 구현  
 장비착용시 착용중인 장비 속성값 추가되도록 설정   
 장비창에서 착용해제할때 착용중인 장비상태 해제되도록 설정 
---
### V.2.0.4 - JS
> CharacterMovementV2 Script 수정  
    - 플레이어의 Attack 함수 가져와서 Collider를 활용하여 Attack성공여부 확인 할 수있게 수정  
  
> Abstract State 스크립트 구현  
  
> StateMachine 및 Enemy Script 구현 예정  
---
### V.2.0.3 - GY
> UI 외형 구현 
- Inventory , Status , Equipment , SoulInchent
> UI 기본 기능 추가
- Drag & Drop 방식으로 이동 기능 추가
- UI Close 구현
- UI Menu 를 통해서 Active 가능하게 구현
---
### V.2.0.2 - JS
> CharacterMovementV2 Script 추가  
- abstract 구조로 변경  
- 플레이어와 에너미 공통적으로 쓰는 메서드들 중 재정의가 필요한 것은 abstract 함수로 정의 후 각각 스크립트에서 재정의 필요  
- 플레이어와 에너미가 공통적으로 쓴느 메서드 중 100% 일치하는 것은 그냥 protected로 선언 후 사용  

> Enemy Script 추가  
- CharacterMovementV2로부터 재정의할 함수 Enemy Script에 구현 필요  
- StateMachine과 스크립트의 분할을 위해 기틀 작업  
- StateMachine, State 구현 예정 (4/19)  
---
### V.2.0.1 - SM
> PlayerAnimEvent Script 추가  
    - 플레이어 애니메이션 이벤트 스크립트

> PlayerMovement Script 추가  
    - 플레이어 움직임 및 공격 함수 구현

> Follow Camera Script 수정  

> ShakeCamera Script 추가  

> CharacterProperty Script 수정  
    - myCamera를 이용해 카메라에 접근할 수 있도록 CharacterProperty에서 myCamera 프로퍼티 추가함.
---
## ***Phase2*** 04.17(Mon) ~ 05.01(Mon)
---
### V.1.0.2 - JS
> GameManager SingleTone 구조 구현  

> ShakeCamera SingleTone 스크립트 작성 및 플레이어 담당 수민에게 전달  

> ObjectPooling source Code 구현  
---
### V.1.0.1 - GY
> 몬스터 스폰상태 추가  

> 몬스터 StateMachine 세분화 작업  
---
### V.1.0.0 - JS
> 개발 버전 업데이트 규칙  

|V.1.0.0  | 1             | 0            |  0           | JS      |
|:-------:|:-------------:|:------------:|:------------:|:-------:|
|   의미  | 개발일정 페이즈 | 씬수정       | 스크립트 작업 |작업자    |

- 개발 일정 페이즈  

|구간   |    Phase1    |     Phase2   |      Phase3   |     Phase4    |
|:----:|:------------:|:------------:|:-------------:|:-------------:|
|기간|03.27 ~ 04.17| 04.18 ~ 05.01 | 05.02 ~ 05.15 | 05.16 ~ 05.26 |
|개요|개발 초기 시스템 설정 및 기반 확립 | 오브젝트간 상호작용 및 상태 머신 구현 | UI 작업 및 세이브 데이터 구현| 추가적인 시스템 구현 및 개발 최적화| 


> 개발 일정 Asana 프로젝트 매니저로 업데이트
- 사용법  
1. 메인 담당자 확인 및 Phase 확인
![ProjectManager01](https://user-images.githubusercontent.com/105345909/230705711-a3b1db32-9918-4815-9d6e-0f82869dbb8b.PNG)
2. 마감일 탭의 시작일을 작업 시작 일로 설정하고 작업 진행  
    - 작업 내용은 Git README.md 파일에 업데이트
    - 상태 업데이트 
3. 완료 시 완료 체크 하고 완료 일에 해당 날짜 기입  
3.1. 미완료 시 예상 완료 시점 기입 및 작업 진행 
4. 간단한 업무 내용 Asana에도 기입 가능  
<img src = "https://user-images.githubusercontent.com/105345909/230705713-2d2a0ddb-fdeb-438e-8949-3ff0a6e94bfb.PNG" width = "180" height ="200"/>
     - 공동 작업이 필요한 경우 공동 작업자 태그를 걸고 사전에 업무 공유하기
     
---
## ***Phase1*** 04.03(Mon) ~ 04.17(Mon)
---
### V.0.0.2

> 공동 작업용 변수명 통일 기준  

- 링크 : [[C#] 변수 명명법](https://jinlee0206.github.io/develop/Naming.html)  

---
### V.0.0.1
> Hierarchical Scripts 추가

> Asset 추가
- Dragon(MainBoss)
- Effect  

_시작지역 맵 일부 추가 - 일부 맵 모델 *.Meta 파일 경로를 못찾는 에러가 존재_

> 개발시작
- Git Project Initialized
---
### V.0.0.0 
> 기획서 제출  
- 장르 : 3D 모바일 RPG 기반 다크 판타지 장르
- 링크 : [Portfolio_design_doc.pdf](https://github.com/kirby1004/AtentsTeamProject/files/11157952/Portfolio_design_doc.pdf)
   

