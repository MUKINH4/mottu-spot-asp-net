# MottuSpot

## Descrição

O objetivo deste projeto é organizar os pátios da empresa Mottu fornecendo rastreamento em tempo real das motos estacionadas. Para isso, propomos instalar um dispositivo de localização em cada moto. Esse dispositivo permite:

- Acionar um alarme sonoro (beep) para facilitar a localização da moto no pátio.
- Acender uma luz indicadora para sinalizar visualmente sua posição.

A API em ASP.NET gerencia as operações de criação, listagem, atualização e remoção de pátios e motos, além de armazenar informações em um banco de dados relacional e acionamento do dispositivo implementadoo na moto.

## Integrantes

- Samuel Heitor – RM 556731
- Lucas Nicolini – RM 557613
- Renan Olivi – RM 557680

## Uso dos Endpoints

Para acessar a API estamos utilizando o Swagger, então basta acessar: https://localhost:7092/swagger e testar os endpoints.

### Endpoints para o pátio
- `POST   api/Patio` – Cria um novo pátio

- `GET    api/Patio`            – Lista pátios

- `GET    api/Patio/{id}`       – Busca pátio por ID

- `PUT    api/Patio/{id}`       – Atualiza pátio

- `DELETE api/Patio/{id}`       – Remove pátio

### Endpoints para a moto
- `POST   api/Moto`             – Registra uma nova 

- `GET    api/Moto`             – Lista motos

- `GET    api/Moto/{id}`        – Busca moto por ID

- `PUT    api/Moto/{id}`        – Atualiza moto

- `DELETE api/Moto/{id}`        – Remove moto

### Endpoints para o dispositivo
- `POST api/Dispositivo` - Adiciona um dispositivo a moto 
- `PUT api/Dispositivo/alarme/{motoId}` - Muda o estado do alarme, ativando ou desativando.

### Passos para Utilizar
1. Clone o repositório:
```bash
   git clone https://github.com/MUKINH4/mottu-spot-asp-net.git
   cd mottu-spot-asp-net
```
2. Configure a string de conexão Oracle:
    - Edite o arquivo `appsettings.json` e ajusta seção `ConnectionStrings:OracleConnection` com seus dados de acesso.
3. Rode as migrations para criar o banco:
```bash
dotnet ef database update
```
4. Execute a aplicação:
```bash
dotnet run
```
5. Acesse a documentação Swagger:
    - Acesse http://localhost:7092/swagger para testar as rotas
