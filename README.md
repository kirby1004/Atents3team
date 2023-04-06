# Grim Reaper - 모바일 기반 RPG


## 주의사항
1. Push 이전에 항상 **pull** 할것 있는지 확인하기
2. **Scene 작업** 전에는 항상 Scene 관리자에게 물어보고 작업하기
3. 작업 내용의 **주석**은 최대한 상세히
3. **상속 구조로 이루어진 스크립트**는 기존의 변수명 수정은 최대한 삼가고 불가피하게 작업을 해야하는 경우는 팀원들에게 미리 알리고 주석을 상세히 달기
4. 개발 일정 확인하기 - To be Continue

---
### 0.0.2
> 변수명 표준화

1. Camel Case를 사용한다. (두번째 단어부터 첫 글자 대문자)
2. 이름은 명사형으로 짓는다.
3. 접두어 + 몸체 + 접미어 형태로 짓는다.
4. 단어의 개수는 4개를 넘지 않는다. (글자 수 최대 20 제한)

참고 : [유니티 - 깔끔하고 보기 좋은 변수명 짓기](https://rito15.github.io/posts/unity-naming-variables-neatly/)
  
> 클래스, 구조체, 열거형, 인터페이스  
1. PascalCasing 을 사용한다. (첫문자 대문자)
2. 명사 또는 명사구 형으로 짓는다
```CS
# 클래스
public class Class { ... }
```

```CS
# 구조체
public struct Nullable<T> where T:struct { ... }
```
```CS
# 열거형
public enum States   
    {
        CREATE,     // 내부 상태는 모두 대문자로 표시 (Optional)
        IDLE,
        ATTACK,
        TRACE,
    }
```
```CS
# 인터페이스
public interface ISessionChannel<TSession> where TSession: ISession // 제너릭 T는 대문자
{
    TSession Session {get;}
}
```
> 타입 멤버명 표준화
- 메서드  
: 메서드 이름은 동사 또는 동사구로 지정. 첫 문자 대문자로 기입

```CS
# 메서드
public class String{
    public int CompareTo(...);      // 동사구
    public string[] Split(...);
    public string Trim();
}
```
- 프로퍼티  
 : 명사구 혹은 형용사 이름으로 지정.
```CS
# 프로퍼티
public enum Color { ... }
public class Control{
    public Color Color { get { ... } set { ... } } // 유형과 동일한 이름의 프로퍼티도 Not bad!
} 
```
- 이벤트 함수  
 : 동사 혹은 동사구 형태로 짓기
```CS
# 대리자
public delegate void ClickedEventHandler(object sender, ClickedEventArgs e);
```

- 필드  
1. static, public, and protected field 에 한하여 PascalCasing 적용 (그러나 유니티 규칙은 camelCase 사용) 
2. Internal, private field의 경우 별다른 가이드라인은 없다. -> camelCase
3. public, protected instance field 또한 별다른 가이드라인은 없다. -> camelCase

```CS
public class ExampleEvents
{
    #region field
    public bool IsValid;    // A public field, these should be used sparingly
    public bool isStarted;  // 유니티에서는 public field varibale도 camelCase 사용   
    public IWorkerQueue WorkerQueue { get; init; }    // An init-only property
    public event Action EventProcessing;    // An event
    
    // Method
    public void StartEventProcessing()
    {
        // Local function
        static int CountQueueItems() => WorkerQueue.Count; // (입력 파라미터) => { 실행 문장 블럭; }
    }
    #endregion
}

public class DataService
{
    private static IWorkerQueue s_workerQueue;  // private static 필드 사용시 s_ 접두어를 붙이기
}

```

> 매개변수 표준화
- 매개변수  
: camelCasing 사용. 매개변수의 의미를 기반으로 이름을 사용하는 것을 고려하기
```CS
#매개변수
public T SomeMethod<T>(int someNumber, bool isValid) { }
```

참고 : [.NET 형식멤버 이름 정리](https://learn.microsoft.com/ko-kr/dotnet/standard/design-guidelines/names-of-type-members)

---
### 0.0.1
> Hierarchical Scripts 추가

> Asset 추가
- Dragon(MainBoss)
- Effect  

_시작지역 맵 일부 추가 - 일부 맵 모델 *.Meta 파일 경로를 못찾는 에러가 존재_

> 개발시작
- Git Project Initialized
---
### 0.0.0 
> 기획서 제출  
- 장르 : 3D 모바일 RPG 기반 다크 판타지 장르
- 링크 : [Portfolio_design_doc.pdf](https://github.com/kirby1004/AtentsTeamProject/files/11157952/Portfolio_design_doc.pdf)
   

