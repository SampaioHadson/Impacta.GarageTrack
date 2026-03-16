# Impacta GarageTrack

Sistema web para controle de estacionamento, desenvolvido com foco no registro e gerenciamento da entrada de veiculos por meio de uma aplicacao moderna com frontend e backend desacoplados.

## Sobre o projeto

O Impacta GarageTrack foi desenvolvido como aplicacao full stack, dividido em duas camadas principais:

- Frontend: responsavel pela interface com o usuario.
- Backend: responsavel pelas regras de negocio, autenticacao e persistencia de dados.

Essa separacao favorece organizacao, manutencao e evolucao do sistema.

## Funcionalidades implementadas

- Autenticacao de usuario (Login)
- Registro de entrada de veiculos no estacionamento

## Tecnologias utilizadas

### Frontend

- React
- Vite
- TypeScript

### Backend

- .NET 8
- Minimal APIs
- Clean Architecture
- PostgreSQL

## Estrutura do projeto

```text
backend/
	Impacta.GarageTrack.System.Api/
frontend/
	impacta-garagetrack/
```

## Arquitetura

O backend foi estruturado com principios de Arquitetura Limpa (Clean Architecture), promovendo separacao de responsabilidades em camadas como:

- API
- Application
- Domain
- Infrastructure

Beneficios dessa abordagem:

- Melhor organizacao do codigo
- Baixo acoplamento
- Facilidade de manutencao
- Maior facilidade para testes e evolucao

## Como executar o projeto

### Pre-requisitos

- .NET SDK 8
- Node.js (LTS recomendado)
- PostgreSQL
- Git

### 1. Clonar o repositorio

```bash
git clone https://github.com/SampaioHadson/Impacta.GarageTrack.git
cd Impacta.GarageTrack
```

### 2. Executar o backend

```bash
cd backend/Impacta.GarageTrack.System.Api/Impacta.GarageTrack.System.Api
dotnet restore
dotnet run
```

Observacao: configure a conexao com o PostgreSQL no arquivo de configuracao da API e aplique as migracoes, se necessario.

### 3. Executar o frontend

Em outro terminal:

```bash
cd frontend/impacta-garagetrack
npm install
npm run dev
```

### 4. Acessar a aplicacao

Acesse no navegador o endereco informado pelo Vite no terminal (normalmente localhost).

## Board do projeto

Organizacao das tarefas e progresso do desenvolvimento:

- https://github.com/users/SampaioHadson/projects/2

## Repositorio

Codigo-fonte completo:

- https://github.com/SampaioHadson/Impacta.GarageTrack

## Integrante

- Hadson Palauro Sampaio

## Consideracoes finais

O desenvolvimento deste projeto permitiu aplicar na pratica conceitos importantes de engenharia de software e desenvolvimento web moderno, como:

- Integracao entre frontend e backend
- Construcao de APIs REST
- Organizacao de codigo com Arquitetura Limpa
- Persistencia de dados em banco relacional

Este repositorio representa a base inicial do sistema e esta preparado para futuras evolucoes.
