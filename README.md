# Gricon_Game-s_Code

2024/12/27 に桃山大学テック部主催で開催されたハッカソンで制作した作品 **Gricon** の、Unityゲーム側のプログラムです（2024/12/28 時点）。

本リポジトリには、コントローラから受信したリアルタイムデータをもとに、ゲーム内キャラクターの挙動、UI、カメラなどを制御するスクリプトが含まれています。

---

## 🔗 関連リンク

- 🎮 コントローラ側のプログラム  
  https://github.com/naka6ryo/Gricon_Controller-s_Code/tree/main

- 📄 作品の解説・開発の流れ（Notion）  
  https://flossy-band-678.notion.site/Gricon-Arduino-Unity-16a9ff2ca96c8032b9a5cc3768512383?pvs=4

---
## 🎥 デモ動画

[![Gricon デモ動画](https://img.youtube.com/vi/qLPBSaw1i-E/0.jpg)](https://youtu.be/qLPBSaw1i-E?si=rnXXbKDZcDVdmmQi)

---

## 📁 スクリプト構成と説明

### 📌 GameSystem.cs
- ゲーム全体の状態管理（スタート・ポーズ・終了など）を行う。
- ゲームフローの中心を担うマネージャークラス。

---

### 🎮 Player 関連スクリプト

#### PlayerMove.cs
- コントローラから受け取った姿勢・曲げデータをもとに、プレイヤーキャラの移動を処理。
- モーターの振動情報も処理することで、触覚フィードバックと連携。

#### Get_Information.cs
- シリアル通信で取得したコントローラからのデータを解析し、角度や曲げ値を他スクリプトに渡す。

#### HandleRotateX.cs / HandleRotateYZ.cs
- ピッチ・ロール・ヨーなどの角度情報に基づいて、プレイヤーキャラの回転処理を行う。

#### HandleCollision.cs
- プレイヤーが他オブジェクトと衝突した際の挙動を管理。

#### TimeManager.cs
- ステージごとの制限時間のカウントと管理を行う。

#### WarningManager.cs / WarningDelayManager.cs  
- プレイヤーが操作範囲外に出たときに、画面に警告を表示するシステム。
- `WarningDelayManager`は警告の表示・非表示タイミングを制御。

---

### 🧭 Camera 関連スクリプト

#### CameraController.cs
- プレイヤーの移動や回転に追従するようにカメラを制御。
- ズームイン・アウトなどの補助機能とも連携。

#### MiniMapRotateY.cs
- ミニマップの回転角をプレイヤーの方向に合わせて更新する。

---

### 🖱️ ボタン操作系（Script/Button フォルダ）

| スクリプト名               | 機能概要                     |
|----------------------------|------------------------------|
| iZoomButton.cs             | ズーム系ボタンの共通インターフェース |
| ZoomInButton.cs            | ズームイン実行               |
| ZoomOutButton.cs           | ズームアウト実行             |
| PauseButton.cs             | ポーズ画面の表示             |
| ReStartButton.cs           | ゲームの再スタート処理       |
| StageSelectButton.cs       | ステージ選択画面へ遷移       |
| StageButton.cs             | ステージ開始ボタン処理       |
| ResetCameraPosButton.cs    | カメラ位置の初期化           |
| TitleButton.cs             | タイトル画面へ戻る処理       |

---

### ⏳ Modal / UI 表示系

#### OkCancelDialog.cs
- 確認ダイアログ（OK/キャンセル）処理の基盤クラス。
- シーン遷移やリトライ確認に使用。

#### TimerText.cs
- ゲーム内に表示される残り時間テキストを管理・更新する。

---

## 📂 フォルダ構成（簡略）

Assets/
├─ Script/
│ ├─ GameSystem.cs
│ ├─ Camera/
│ ├─ Player/
│ ├─ Modal/
│ └─ Button/
└─ その他（素材・シーンなど）


---

## 📝 備考

- データ通信は `Get_Information.cs` で受け取り、`PlayerMove.cs` や `CameraController.cs` に反映。
- ゲームロジックの変更やUI処理の拡張も各スクリプトに追記可能。
- 実装はC#、Unityバージョンは2022系を想定。

---

## 🛠 ライセンス

MIT License
