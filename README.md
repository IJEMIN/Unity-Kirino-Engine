# 유니티 키리노 엔진

키리노 엔진은 유니티에서 동작하는 비주얼 노벨 개발용 프레임워크 입니다.

모듈은 크게 두 부분으로, 스크립트 모듈과 코어 모듈로 나누어져 있습니다.

코어 모듈은 이미지를 띄우거나 대사를 출력하고 명령 스택을 관리하는 등 '동작'부분에 해당합니다. 스크립트 모듈은 그것들을 순서에 따라 어떻게 동작시킬지를 '계획'할 수 있게합니다.

코어 모듈이 하는 일...

- 배경 이미지 출력
- 캐릭터 스프라이트를 출력하고 교체
- 대사 출력과 넘기기
- 음악과 보이스 재생
- 이미지 트랜지션 효과
- etc...

스크립팅 모듈이 하는 일...

- 코어 모듈을 제어
- VNComman 에서 파생된 여러 타입의 명령 제공
- VNCommand 타입의 인스턴스를 명령 스택에 쌓기
- RPY 파일을 파싱하여 스크립트로 부터 VNCommand 오브젝트를 인스턴스화
- 스토리 분기를 추적
- 명령 스택의 커서를 다음으로 옮기기



원래 이 프레임워크는 제가 [유니티로 만든 게임인 츤데레 아가씨](https://play.google.com/store/apps/details?id=com.applemint.deregirl&hl=ko)의 소스코드에 포함되어 있었습니다.

제가 만든 게임 소스코드에서 비주얼 노벨 파트 부분을 따로 때어 라이브러리화 해서 공유하면 도움이 될것이라 생각했거든요.

RPY 스크립트 파싱을 위해서 [VGPrompter](https://github.com/eugeniusfox/vgprompter) 를 사용하고 있습니다.

## 필수 사항

모든 RPY 스크립트 파일은 Assets/RPY 경로에 있어야 합니다.
모든 시리얼라이즈화 된 스크립트 파일은 Assets/StreamingAssets 에 저장되야 합니다.

## 문법 보기

문법은 [이곳](/kirino-syntax.md)에서 확인할 수 있습니다.

- 문법은 공개되지 않은 실험 버전에서 만들어졌습니다. 따라서 문법 파일에 설명하고 있는 사항이 이 저장소의 버전에서는 동작하지 않을 수 있습니다.

## 면책 조항
**이 프로젝트는 완성되지 않았습니다!**

## 질문은?
- i_jemin@hotmail.com
- https://ijemin.com

# Unity Kirino Engine

Kirino Engine is framework for developing Visual Novel features in Unity3D.

Kirino Engine has two parts. Core module and sciprint module.

What core module do...

- display background
- display and swapping charaters sprites
- print dialogues
- play voice and music
- image transition effects
- etc...

What scripting module do...

- Use core module
- Provice variouse command types based on VNCommand
- Stacking VNCommand instance
- Parse RPY scripts to instantiate VNCommand instance
- Tracking story branches
- Move command stack cursor to next


Originally, I made this framework for [my game developed with Unity](https://play.google.com/store/apps/details?id=com.applemint.deregirl&hl=ko). I thought it might be helpful to visual novel developers if I share visual novel parts from the game's source code.

Currenlty it's not fully functional yet because I need to decouple the framework from the game's source code.


Using [VGPrompter](https://github.com/eugeniusfox/vgprompter) for parse RPY.

## Pre-rquired

All rpy scripts file must be stored in Assets/RPY.
All serilized Scripts file must be stored in Assets/StreamingAssets.

## Kirino Engine Syntax

You can check scriping syntax in [here](/kirino-syntax.md).

- Described scripting syntax on that file was created in experimental version of Kirino Engine which is not in public yet. So some syntax may not work in Kirino Engine of this repository.

## Disclaimer
**It's still a work in progress!**

## Any Question?
- i_jemin@hotmail.com
- https://ijemin.com
