# Projeto / Considerações
**TECNOLOGIAS USADAS:** Visual Studio Community, C#, EntityFramework, MSTests, SqlSever, MongoDB, Atlas, Azure Cloud, AzureBlobStorage.

- O teste consiste em implementar uma API que é capaz de implementar operações CRUD(Create, Read, Update, Delete).

- Agradeço a oportunidade em estar cumprindo com este desafio. Foi uma execelente tarefa, tenho certeza que posso contribuir muito mais trabalhando na fábrica de software juntamente com a execelente equipe da Mobilus.

- Coloco-me à disposição para quaisquer dúvidas em relação ao código. 
  
# Como Acessar / Pontos Importantes
- Primeiro, acesse o link: https://apiprodutosmobilus.azurewebsites.net/swagger/index.html
- Depois selecione quais endpoints desejar. Foram criadas duas rotas de APIs, uma para o SQL com dados relacionais e outra para o MongoDB, assim como descrito nos requisitos deste projeto.
- Observação: No POST e no PUT da API "Produto", remover a linha "id", visto que configurei para que o ID seja incrementado automaticamente a cada nova inserção
- Na Api "ProdutosMongoDB", você pode inserir os dados normalmente com o "id". 
- Foram adcionadas validações para o preço, por isso, não é possível adicionar um preço menor que zero. Também não é possível repetir o id na "ProdutosMongoDB".
- Adicionei summarys em cada método para explicar o funcionamento de cada um
- **TODAS AS REQUISIÇÕES HTTP TESTADAS E FUNCIONAIS**

# Conexão com o Banco de Dados
- Caso deseje baixar a API e utilizar localmente, basta inserir sua ConnectionString no appSettings.Development.json. Por motivos de segurança, removi minhas conexões para não ficar nenhuma autenticação exposta.
- Para banco de dados: "ConexaoMongoDB" e "ConexaoBancoProduto".
- Para blobStorage, considerando que voce já criou uma storage account na Azure, insira sua conexão e o nome do seu container em "BlobConnectionString" e "BlobContainerName". Obs: Não é necessário alterar a disposição das connectionsStrings, visto que já estão funcionais e bem identadas, apenas sera necessária as conexões mesmo.

![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/3ce766a5-d592-4259-971e-67133799f552)

# BlobStorage
"ArquivosProdutoControllers"
- Adicionei uma API BlobStorage que faz as operações parecidas com o CRUD.
- Upload -> Envia o arquivo para o container
- Get -> Baixa o arquivo passando nome dele
- Delete -> Apaga o arquivo, passando o nome dele
- Get -> Lista os arquivos contidos no container
  
- **Voce pode enviar e fazer o download de quantos arquivos voce desejar e de qualquer tipo**
- Para listar os arquivos, vá na última opção Get e verá o retorno dos dados em Json.
- Você terá:<br>
-> "nome" -> O nome do arquivo alocado<br>
-> "tipo" -> Tipo do arquivo que está alocado<br>
-> "uri" -> Endereço do arquivo para ser acessado

  ![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/5388abc0-59b4-49dc-8acc-4efb2ee6bf80)

  
# Arquitetura
- Detalhes:
-> O software está hospedado em um APPService na Azure.<br>
-> Persistência do danco de dados SQL na Azure.<br>
-> Persistência dos dados MongoDB estão em cloud disponibilizado pela ferramenta Atlas.<br>

- Estrutura Geral
  
![ProdutoAPI](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/0f75333b-57e9-4360-bf00-9a63ba250d43)

# Código:
- Eu criei ao todo quatro classes e três são controladoras:<br>

 - ArquivoProdutoController -> Realiza a manipulação dos arquivos que o usuário deseja hospedar
  - ProdutoMController -> Realiza as operações de CRUD no MongoDB
  - ProdutoController -> Realiza as operações de CRUDO no SQL.
  - RegrasController -> Fica com dever de realizar duas importantes validações descritas no projeto, validar o preco do produto para que ele não seja inserido menor que 0 e a operação para multiplicar a qualtidade x valor do endpoint que retonar apenas um tipo de produto do banco.
    
  ![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/76bac583-9248-4fc9-9585-356564f799be)
 
- Foi criadas duas entidades para cada um dos bancos <br>

- Produto -> Representa a tabela do bando de dados do SQL
- ProdutoMongo / ProdutoDatabaseSettings -> Representa a estrutura da tablela do banco de daods MongoDB

![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/d806920f-0752-4f58-866e-32e408d15758)

- ServicesMongoDB -> Também reponsável por fornecer informações de CRUD para o MongoDb.<br>

  ![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/d663a928-6949-4033-9370-43c570b4709d)

- Como de praxe e brando entendimento fiz as conexões através das connectionsStrings, em produção no appSettings.Json e em Desenvolvimento no appSettings.Development.json. <br>

  ![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/13580b2d-e483-4842-bf8c-67f1e8d61b19)

- Na Classe Program, realizei as configurações necessárias para todo o desenvolvimento do código. Tanto para as configurações do SQL quanto do MongoDB.
  
  ![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/95d34546-b5b2-4b45-8e72-f76c3d64b949)

  # Testes

  - Para os testes, adotei uma forma executálos com eficiência e rapidez utilizando o UseInMemoryDatabase(). Deste modo podmeos realizar os testes utilizando um efeito de paginação, onde utilizamos a memória temporaria(RAM) para validar todos os métodos.
 
  - Foram aplicados para todos os endpoints, com os Asserts bem definidos para trabalharem dentro de seu escopo.
    
   ![image](https://github.com/PedroRepos/tech-test-backend-csharp/assets/120064429/8829483c-da85-41cc-ba3d-5a21db302f8e)

  
    

  

