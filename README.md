## Como rodar o projeto com Docker

1. Entre no diretório do projeto
2. Com terminal aberto, execute "docker build -t eclipse-api ."
3. Execute "docker run --name eclipse-api -p 8080:8080 -d eclipse-api"

## Perguntas que eu faria ao PO

- Por que as prioridades das tarefas não podem ser alteradas?
- Você sabe que sem autenticação, os usuários vão poder criar e editar tarefas de outras pessoas, né?
- Por que os projetos só podem ter 20 tarefas?

## Melhorias do projeto

- Adicionaria autenticação
- Adicionaria autor de comentários
- Ao deletar um projeto, já removeria todas as tarefas
- Adicionaria permissões de edição, se qualquer um pode editar ou somente o autor por exemplo
- Adicionaria a funcionalidade do usuário poder deletar sua própria conta
- Colocaria mais detalhes nos relatórios, como quantidade total de tarefas concluidas, tarefas restantes etc.
