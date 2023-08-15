# Projeto
TECNOLOGIAS USADAS: Visual Studio Community, C#, EntityFramework, MSTests, SqlSever, MongoDB, Atlas, Azure Cloud, AzureBlobStorage.

- O teste consiste em implementar uma API que é capaz de implementar operações CRUD(Create, Read, Update, Delete). Foi um excelente desafio que vejo como mais uma nova forma de aprendizado.
- 
# Como Acessar / Pontos Importantes
- Primeiro, acesse o link: https://apiprodutosmobilus.azurewebsites.net/swagger/index.html
- Depois selecione quais endpoints desejar. Foram criadas duas rotas de APIs, uma para o SQL com dados relacionais e outra para o MongoDB, assim como descrito nos requisitos deste projeto.
- Observação: No POST e no PUT da API "Produto", remover a linha "id", visto que configurei para que o ID seja incrementado automaticamente a cada nova inserção
- Na Api "ProdutosMongoDB", você pode inserir os dados normalmente com o "id". 
- Foram adcionadas validações para o preço, por isso, não é possível adicionar um preço menor que zero. Também não é possível repetir o id na "ProdutosMongoDB".
- Adicionei summarys em cada método para explicar o funcionamento de cada um

# ARQUITETURA
- O software está hospedado em um APPService na Azure.
- Está fazendo a persistência dos dados SQL também na Azure.
- A persistência dos dados MongoDB estão em cloud disponibilizado pela ferramenta Atlas.<br>
-> Estrutura detalhada a baixo.

![ProdutoAPI](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/0f75333b-57e9-4360-bf00-9a63ba250d43)

# CÓDIGO:



