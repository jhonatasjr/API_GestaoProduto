# Gestão de Produtos
API em C# DotNet 5, utilizando o padrão DDD.

## Script de criação do banco

```CREATE DATABASE [API_GestaoProduto]

USE [API_GestaoProduto]

CREATE TABLE [dbo].[Produtos](
	[CodProduto] [int] IDENTITY(1,1) NOT NULL,
	[DsProduto] [varchar](500) NOT NULL,
	[DtFabricacao] [datetime] NULL,
	[DtValidade] [datetime] NULL,
	[CodFornecedor] [varchar](250) NULL,
	[DsFornecedor] [varchar](500) NULL,
	[CNPJ_Fornecedor] [varchar](20) NULL,
	[DtCriacao] [datetime] NOT NULL,
	[DtEdicao] [datetime] NULL,
	[DtExclusao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED 
(
	[CodProduto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```