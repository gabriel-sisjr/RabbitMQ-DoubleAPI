# RabbitMQ-DoubleAPI

<p align="center">Exemplo voltado ao mundo real para comunicação entre WebApi como serviços utilizando RabbitMQ.</p>

<p align="center">
  <a href="">
      <img src="https://img.shields.io/github/issues/gabriel-sisjr/RabbitMQ-DoubleAPI">
  </a>
  <a href="">
      <img src="https://img.shields.io/github/issues-pr/gabriel-sisjr/RabbitMQ-DoubleAPI">
  </a>
  <a href="">
    <img src="https://img.shields.io/github/stars/gabriel-sisjr/RabbitMQ-DoubleAPI.svg" alt="License">
  </a>
  <a href="">
    <img src="https://img.shields.io/github/license/gabriel-sisjr/RabbitMQ-DoubleAPI" alt="License">
  </a>
</p>

<p align="center">
  <a href="#zap-tech-stack">Tecnologias Utilizadas</a> •
  <a href="#handshake-Contribuição">Contribuição</a> •  
  <a href="#art-about-me">Desenvolvedor</a>
</p>

## :blush: **Motivação**

**Dada a problematica onde precisamos reaproveitar recursos e utiliza-los da melhor forma, busquei mostrar a possibilidade de transformação de 2 WebAPIs em uma arquitetura orientada a eventos. De uma forma simples e dinamica, onde há a possibilidade de escalar um nó a depender do fluxo.**

---

## **Solução**

![Diagram](https://user-images.githubusercontent.com/36143255/171774963-bc35c8f9-5eb5-4de6-9b00-4058e925f8b4.jpg)

### No exemplo adotado, há 4 containers contendo, respectivamente, 2 WebAPIs (C#), 1 Message Broker (RabbitMQ) e 1 Serviço (NodeJS + TS). Todos esses containers sendo criados e geridos via Docker-Compose. Segue abaixo a descrição dos mesmos.

- **PRODUCER**
  - WebAPI responsável por ser o "portão de entrada" da arquitetura como um todo, realizando um tratamento inicial no dado e enviando-o à respectiva fila onde será consumido pelo serviço especifico atrelado. Essa aplicação apenas envia mensagens ao broker.
  - Possui endpoints onde são requisitados normalmente via **HTTP**.
- **CONSUMER**
  - Essa API age também como serviço, ao implementar uma classe **Worker**, esta agindo como **BackgroundService** durante a execução da aplicação. O consumo das mensagens do broker é feita nessa classe e a partir dela, tratamos o dado recebido da forma que a regra de negócio necessitar.
  - Mesmo agindo como um serviço, também dispõe de endpoints **HTTP**, onde podemos realizar funções diferentes das realizadas na _PRODUCER_, diminuindo assim o escopo da aplicação.
- **RABBITMQ**
  - Broker utilizado para ser o gerenciador das mensagens que trafegarão dentro dessa arquitetura.
  - Foram utilizados _ENUMS_ para nomear as filas nas respectivas aplicações, afim de facilitar a identificação das mesmas.
- **EMAILCONSUMER**
  - Serviço criado utilizando **NODEJS + TYPESCRIPT** este serviço agirá como notificador, sendo responsavel por consumir a fila **EMAIL** no **RABBITMQ** e tratar os dados de acordo com a solicitação.

### P.S. Os exemplos aqui demonstrados são de cunho simples, ficticios e direto, afim de mostrar usabilidade num ambiente mais próximo da realidade.

### P.S.2. Não foi implementado a camada com SGBD/Cache/Etc para manter a proposta do exemplo.

---

## :zap: **Tecnologias Utilizadas**

<p align="center">
  <a href="">
      <img src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white">
  </a>
  <a href="">
    <img src="https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white" alt="License">
  </a>
  <a href="">
      <img src="https://img.shields.io/badge/rabbitmq-%23FF6600.svg?&style=for-the-badge&logo=rabbitmq&logoColor=white">
  </a>
  <a href="">
      <img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white">
  </a>
  <a href="">
    <img src="https://img.shields.io/badge/Node.js-43853D?style=for-the-badge&logo=node.js&logoColor=white" alt="License">
  </a>
  <a href="">
    <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" alt="License">
  </a>
</p>

---

## **Instruções de uso**

<br/>

### **Pré-Requisitos**

- Docker
- Docker-compose

<br/>

### **Começando**

#### 1) Setando as configurações do _RABBITMQ_ nos _AppSettings.json_ das WebAPIs

```json
{
  "RabbitMqSettings": {
    "HostName": "Host",
    "Port": "5672",
    "Username": "admin",
    "Password": "admin"
  }
}
```

#### 2) Setando as configurações do **RABBITMQ** e do seu provider de EMAIL no _.env_ do **EmailConsumer**

```sh
# ===== RABBITMQ SETTINGS =====
RABBITMQ_HOST=rabbitmq
RABBITMQ_USER=admin
RABBITMQ_PASS=admin
RABBITMQ_PORT=5672

# ===== EMAIL SETTINGS =====
# Você pode simular em https://mailtrap.io/
EMAIL_HOST=smtp.mailtrap.io
EMAIL_PORT=2525
EMAIL_HOST_USER=user
EMAIL_HOST_PASSWORD=pass
EMAIL_FROM=from-example@email.com
EMAIL_USE_TLS=false
EMAIL_TLS_CIPHERS=SSLv3
EMAIL_TLS_REJECT_UNAUTHORIZED=false
```

#### 3) Para iniciar a infraestrutura, só rodar e esperar a mágica acontecer!

```sh
    docker-compose up --build
```

#### As configurações internas de rede, assim como envs e healths checks já estão devidamente configuradas no compose.

### P.S. Verifique se as portas configuradas no _docker-compose.yml_ estão disponivéis.

---

## :handshake: **Contribuição**

- Sinta-se a vontade para realizar sua contribuição, vamos trocar ideia e discutir soluções! <br/>
- Abra uma ISSUE para que possamos discutir e vincular um possível Pull Request :)

---

## :handshake: **Desenvolvedor**

Gabriel Santana - Dev FullStack - _.NET/NodeJS/AWS_

- Email: <a href="mailto:gabriel.sistemasjr@gmail.com" target="_blank"> gabriel.sistemasjr@gmail.com</a>
- Repo: <a href="https://www.github.com/gabriel-sisjr" target="_blank"> GitHub</a>

---
