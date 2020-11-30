# Resumo
Este projeto foi criado para validar meus conhecimentos e habilidades em .NET Core, Docker e Git.
O sistema consiste em duas APIs desenvolvidas para ajudar usuários no cálculo de seus juros compostos.

# API TaxaJuros
Esta API consiste em um endpoint de verbo GET que retorna a taxa de juros utilizada pela aplicação para execução dos cálculos.
Atualmente o valor é de 1%, sendo assim, a API retorna o valor 0,01 no corpo da sua resposta.

# API CalculaJuros
Esta API consiste em um endpoint de verbo GET que recebe por parâmetro query string o valor inicial e o número de meses a serem utilizados no cálculo. O retorno desta API é o valor final a ser pago aplicando a fórmula "*Valor Final = Valor Inicial * (1 + Taxa de Juros) ^ Tempo*".

Um exemplo de uso seria a utilização dos parâmetros valor inicial e número de meses como 100 e 5, respectivamente, retornando assim o valor final 105,10.

# API ShowMeTheCode
Esta API consiste em um endpoing de verbo GET que retorna a URL do repositório GitHub deste projeto. Serve para facilitar o acesso ao código do projeto e entender melhor a sua implementação.

# Testes unitários
Os testes unitários do projeto foram desenvolvidos utilizando xUnit. Eles servem para validar que as implementações fazem exatamente o que se espera delas.

# Teste integrados
Os testes integrados do projeto foram desenvolvidos utilizando Postman. Por se tratar de uma aplicação de API, entendeu-se que o Postman serviria bem para o propósito de validar se os endpoints respondem o que se espera deles em um cenário que exigia todos os componentes da aplicação funcionais e estáveis.
Localmente os testes podem ser feitos com a utilização do Postman, mas na esteira de build foi necessário implementar o uso do [Newman](https://learning.postman.com/docs/running-collections/using-newman-cli/command-line-integration-with-newman/) para execução da collection via command line.

# Dockerfile
O Dockerfile da aplicação foi criado com base no que já é disponibilizado pelo Visual Studio, com algumas customizações. Uma delas é a utilização de um usuário próprio para criação da imagem, isto facilita a localização de arquivos e também segrega melhor a aplicação dentro do container. Em momentos de depuração via shell diretamente no container esta ação pode ser útil em alguns momentos.
O Dockerfile também teve retirada a instrução de *dotnet build*, em seu lugar foi incluída a instrução *dotnet test*. Desta forma é possível validar tanto o build quanto os testes unitários da aplicação, dando maior segurança para a geração de uma imagem futura.

# Continuous Integration - CI
O processo de CI deste projeto está baseado na branch *main*. Desta forma é necessário solicitar pull-requests (PR) para atualização da branch default. Também foram criadas actions que validam se o código do PR não está infringindo nenhum caso de uso dos testes unitários ou integrados.

# GitHub Actions
Foram criados dois YAMLs de actions para fazer as validações do repositório.

O primeiro é o pr-to-main.yaml que consiste na validação de pull-requests solicitados para a branch main. Os passos feitos pela action são:
- Fazer login no DockerHub;
- Fazer o build da imagem docker com a utilização do Dockerfile contido na branch de source do PR e colocando o sha do commit mais recente desta branch como tag da imagem;
- Executar um container a partir desta imagem, fazendo um port-forwad da porta 8080 do container para a 8088 da máquina em execução no momento;
- Instalar o [Newman](https://learning.postman.com/docs/running-collections/using-newman-cli/command-line-integration-with-newman/) na máquina em execução no momento;
- Executar os testes integrados na máquina local, utilizando a porta 8088 que está redirecionando para a 8080 do container que, por sua vez, aponta para a aplicação FinancialCalculator;

O segundo é o push-to-main.yaml que consiste na validação de pushs feitos na branch main. Os passos feitos pela action são:
- Fazer login no DockerHub;
- Fazer o build da imagem docker com a utilização do Dockerfile contido na branch main e colocando o sha do commit mais recente desta branch como tag da imagem;
- Executar um container a partir desta imagem, fazendo um port-forwad da porta 8080 do container para a 8088 da máquina em execução no momento;
- Instalar o [Newman](https://learning.postman.com/docs/running-collections/using-newman-cli/command-line-integration-with-newman/) na máquina em execução no momento;
- Executar os testes integrados na máquina local, utilizando a porta 8088 que está redirecionando para a 8080 do container que, por sua vez, aponta para a aplicação FinancialCalculator;
- Fazer o push da imagem recém construída e testada para o DockerHub;

# Aprendizados
Com este projeto foi possível revisitar alguns conceitos de Docker que eu, por um pouco de falta de prática, já havia me esquecido. Também foi possível praticar o uso do GitHub, já que este não é muito utilizado profissionalmente nas empresas da minha região, e também aprender a utilizar o GitHub Actions.
O GitHub Actions é recente mas já possui muita documentação e templates. Aconselho a todo desenvolvedor que se aventure nesta área do GitHub pois vale muito a pena.
