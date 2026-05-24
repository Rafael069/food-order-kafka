# Food Order Kafka

Sistema de pedidos de comida criado para praticar comunicação assíncrona utilizando Kafka e .NET.

## Arquitetura

Este projeto simula um fluxo básico de pedidos de comida onde uma API publica mensagens no Kafka e um serviço worker consome essas mensagens.

O objetivo é entender como sistemas podem se comunicar através de um **message broker**, em vez de chamar outros serviços diretamente.

## Componentes

### API (.NET)

Responsável por:

- Receber requisições HTTP
- Armazenar pedidos em memória
- Publicar mensagens no Kafka

### Kafka

Funciona como o **broker de mensagens**, responsável por intermediar a comunicação entre os serviços.

### Worker (Consumer)

Escuta o tópico do Kafka e registra no console as mensagens recebidas.

## Endpoints

**POST /api/orders**

Cria um novo pedido e publica uma mensagem no Kafka.

**GET /api/orders**

Retorna todos os pedidos armazenados em memória.

**GET /api/orders/{id}**

Retorna um pedido específico.

**POST /api/orders/{id}/cancel**

Cancela um pedido e publica uma mensagem no Kafka.

## Fluxo da Aplicação

Cliente → API → Kafka → Worker → Log no Console

## Tecnologias Utilizadas

- .NET
- Apache Kafka
- Docker

## Objetivo do Projeto

Este projeto tem como objetivo praticar:

- Comunicação assíncrona
- Integração entre serviços usando Kafka
- Conceitos básicos de producer e consumer
- Arquitetura desacoplada
