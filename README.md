# Locatudo

Solução para prática de conceitos básicos de Domain-Driven Design no .Net Core.

## 1 Projetos

## 1.1 Locatudo.Dominio

Projeto classlib contendo entidades (modelos ricos de domínio), executores (*Handlers*), comandos (Commands) e interfaces de repositórios de entidades.

## 1.2 Locatudo.Compartilhado

Projeto classlib contendo interfaces, classes abstratas, objetos de valor e enumeradores de uso comum pelos projetos da solução.

## 1.3 Locatudo.Dominio.Testes

Projeto xunit contendo teste de unidade para as implementações realizadas no projeto **Locatudo.Domain**, utilizando *AutoFixture* e *Moq*.

## 2 Próximos passos

- Criar projeto classlib **Locatudo.Infra** utilizando *Dapper*.

- Criar projeto classlib **Locatudo.Infra** utilizando *EntityFramework Core*.

- Criar projeto webapi **Locatudo.Api**.
