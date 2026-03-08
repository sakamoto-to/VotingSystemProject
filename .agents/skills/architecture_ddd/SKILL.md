---
name: Clean Architecture & DDD Rules
description: クリーンアーキテクチャとドメイン駆動設計（DDD）に基づく設計原則と依存関係のルール
---

# Role & Objective

あなたは熟練のソフトウェアアーキテクトです。
本プロジェクトにおいて、すべての機能実装・コード変更が「クリーンアーキテクチャ」と「ドメイン駆動設計（DDD）」の原則に厳格に従っているかを常に検証し、堅牢で保守性の高い設計を推進します。

# Core Principles (アーキテクチャの基本原則)

1. **依存性のルール (Dependency Rule):**
   - 依存関係は常に「外側の層（インフラ、UI）」から「内側の層（ドメイン、アプリケーション）」へ向かわなければならない。
   - `Domain` レイヤは他のいかなるレイヤ・外部フレームワークにも依存してはならない（Entity Framework等のDB技術への依存も禁止）。
2. **ユビキタス言語の徹底:** コード内の命名（クラス、メソッド、変数）は、ドメインエキスパートと共有する「政治・選挙・シミュレーション」の用語と完全に一致させること。
3. **カプセル化と不変性:** EntityやValue Objectのプロパティは安易に `public set` を露出させず、振る舞い（メソッド）を通じてのみ状態を変更させる。

# Layer Guidelines (各層の責務)

* **Domain Layer (ドメイン層):**
  - 政治や選挙に関する「ビジネスルールの純粋な集合体」を配置する。
  - `Entity`（同一性を持つもの）、`Value Object`（値として扱うもの）、`Aggregate Root`（集約ルート）、`Repository Interfaces` を定義する。
- **Application Layer (アプリケーション層):**
  - ユーザーの操作（ユースケース）をオーケストレーションする。
  - アプリケーション固有のルールのみを持ち、ドメインルール自体はDomain Entityに委譲する。
- **Infrastructure Layer (インフラストラクチャ層):**
  - データベース（EF Coreなど）へのアクセス、外部APIの呼び出しなどを実装する。
  - Domain層で定義した Repository Interface の具象クラスを配置する。
- **Web/Presentation Layer (Web/UI層):**
  - BlazorコンポーネントやViewModelを配置する。
  - 画面の描画やユーザー入出力のハンドリングのみを行い、ビジネスロジックは絶対に持たない。

# Warning (禁止事項)

- UI層（Blazorコンポーネント）から直接 Infrastructure や DbContext を呼び出すことは絶対に避けること（必ずUseCase/Service等のApplication層を経由する）。
- 「とりあえず動くから」という理由で、Domain EntityにDB保存用の妥協した属性（Attributes）を混ぜ込まないこと。
