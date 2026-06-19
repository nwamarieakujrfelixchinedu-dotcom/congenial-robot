# avatar-call-unity-android

Unity (Android) prototype scaffold for "avatar-call": a mobile app that animates a stylized avatar from live microphone audio and streams the rendered frames via a managed WebRTC SDK (Agora).

This repository contains a minimal Unity project scaffold (scripts + placeholder scene) to get started with the local preview MVP: mic -> viseme -> blendshape animation. It does NOT include any third-party binaries (Agora SDK). See the instructions below to add Agora and build an APK.

Status
- Scaffold added: C# scripts (AudioViseme, AvatarController, AgoraBridge), placeholder scene, README, MIT license.

What I did
- Added a minimal AudioViseme implementation that maps microphone RMS to a mouth blendshape weight.
- Added a simple AvatarController for other animation parameters (eye blink, etc.).
- Added an AgoraBridge placeholder showing how to capture a RenderTexture and where to push frames into Agora (you must adapt to the Agora SDK version you install).

How to use
1) Install Unity 2020 LTS or later and open this project.
2) Import or create a simple avatar with blendshapes (mouth_open at index 0, left/right eye blinks at 1/2). Place it under Assets/Prefabs and instantiate in the Main scene.
3) Attach AudioViseme to the avatar GameObject and set the SkinnedMeshRenderer reference and mouthBlendShapeIndex (usually 0).
4) Configure an Android build target (File > Build Settings > Android) and enable Microphone permission (Unity will request when building/running).
5) Agora: download the latest Agora Unity SDK (see instructions below) and place the package/plugin into Assets/Plugins. Follow Agora docs to enable external video source and push frames from AgoraBridge.

Where to get the Agora Unity SDK
- Official Agora docs & downloads: https://www.agora.io/en/resources/ or https://docs.agora.io/en/
- Place the downloaded Unity package/plugin in Assets/Plugins/ and follow Agora's Unity integration steps.

Notes
- I did NOT include Agora binaries due to licensing.
- This scaffold is intended as a starting point for an Android-first Unity prototype using stylized avatars and a lightweight viseme pipeline.

Next steps (I can do for you)
- Adapt AgoraBridge to the exact Agora Unity API for external video frames and push frames into a channel.
- Add a simple UI for join/leave channel and sensitivity slider.
- Provide a sample avatar prefab or a 2D mouth-sprite fallback.

If you want me to continue, I can adapt the AgoraBridge to a specific Agora SDK version and push the exact integration code (I will not add Agora binaries to the repo).