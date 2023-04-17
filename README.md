# Grim Reaper - 모바일 기반 RPG


## 주의사항
1. Push 이전에 항상 **pull** 할것 있는지 확인하기
2. **Scene 작업** 전에는 항상 Scene 관리자에게 물어보고 작업하기
3. 작업 내용의 **주석**은 최대한 상세히
3. **상속 구조로 이루어진 스크립트**는 기존의 변수명 수정은 최대한 삼가고 불가피하게 작업을 해야하는 경우는 팀원들에게 미리 알리고 주석을 상세히 달기
4. 개발 일정 확인하기 - [개발 일정](https://app.asana.com/0/1204352798021693/list)
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
   

