# Projeto
TECNOLOGIAS USADAS: Visual Studio Community, C#, EntityFramework, MSTests, SqlSever, MongoDB, Atlas, Azure Cloud, AzureBlobStorage.

- O teste consiste em implementar uma API que é capaz de implementar operações CRUD(Create, Read, Update, Delete).
  
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
- Eu criei ao todo quatro classes e três são controladoras:<br>
  ![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/76bac583-9248-4fc9-9585-356564f799be)

  - ArquivoProdutoController -> Realiza a manipulação dos arquivos que o usuário deseja hospedar
  - ProdutoMController -> Realiza as operações de CRUD no MongoDB
  - ProdutoController -> Realiza as operações de CRUDO no SQL.
  - RegrasController -> Fica com dever de realizar duas importantes validações descritas no projeto, validar o preco do produto para que ele não seja inserido menor que 0 e a operação para multiplicar a qualtidade x valor do endpoint que retonar apenas um tipo de produto do banco.
 
- Foi criadas duas entidades para cada um dos bancos <br>
![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/d806920f-0752-4f58-866e-32e408d15758)

Produto -> Representa a tabela do bando de dados do SQL
ProdutoMongo -> Representa a estrutura da tablela do banco de daods MongoDB






