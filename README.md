# VotingSystemProject

投票システムの学習用サンプルです。Blazor Server をフロントエンドに、Application / Domain / Infrastructure のレイヤード構成で
選挙の作成、投票、結果表示、ブロックチェーンによる投票記録の参照ができます。

## 主な構成

- `VotingSystem.Web`: Blazor Server UI
- `VotingSystem.Application`: ユースケースとDTO
- `VotingSystem.Domain`: ドメインモデル
- `VotingSystem.Infrastructure`: データアクセス・サービス実装
- `VotingSystem.Tests`: テストプロジェクト

## 実行方法

```bash
dotnet run --project VotingSystem.Web
```

ローカルで起動後、表示される URL からアクセスしてください。
