# SimpleDiffie-Helman

Este projeto foi criado com o intuito de demonstrar a troca de chaves utilizando o algoritmo Diffie-Hellman. A chave gerada é utilizada para encriptar um texto usando a cifra de César.

## Parâmetros Padrão

- **G**: 7
- **N**: 23
- **x** (número privado do servidor): 3
- **y** (número privado do cliente): 6

Com esses valores, espera-se que:
- **R1** (calculado pelo servidor) seja 21
- **R2** (calculado pelo cliente) seja 4

### Demonstração da Troca de Valores e Envio da Mensagem

#### Envio do R1
O servidor calcula o valor de R1 e envia para o cliente.
![image](https://github.com/user-attachments/assets/5024dc59-4896-4d42-bf72-0bdb5abec697)

#### Envio do R2
O cliente calcula o valor de R2 e envia para o servidor.
![image](https://github.com/user-attachments/assets/32fd4028-b473-49d4-873f-dca321c1bbd3)

#### Envio da Mensagem Encriptografada
O cliente encripta uma mensagem usando a chave compartilhada e envia para o servidor. O texto original deve ser: "Isso é um teste".
![image](https://github.com/user-attachments/assets/c37cc749-25ce-4b37-9bfe-d9502f9dbc47)

#### Recebimento da Mensagem Formatada
O servidor recebe a mensagem encriptografada, decifra e formata a mensagem para maiúsculas. O texto decifrado deve ser: "ISSO É UM TESTE".
![image](https://github.com/user-attachments/assets/85ffd152-5392-4e02-84d1-acf3ec0f745b)

## Estrutura do Projeto

O projeto foi desenvolvido em duas linguagens: Python e C#. 