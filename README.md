# CleanArchitecture.WorkerService
Solution template using clean arch for building a .Net 8 Worker Service
Cria��o de novo projeto utilizando a vers�o .net 8, vers�o *Long-Term Support* lan�ada a 14 de novembro de 2023.

# Arquitetura
Sistema monol�tico com uma abordagem a arquitetura de *Clean Architecture* sendo desdobrado na �rea *Core* (inclui a areas de *Domain* e *Application*), *Infraestrutura* (inclui a pr�pria �rea  *Infraestrutura* e a *Persist�ncia*) e por ultimo a �rea de Apresenta��o onde � implementado a parte mais exterior servi�o.

# Tarefas:
- Sistemas de Logs
- Sistemas de Resources
- Sistema de falha em eventos
- Estrutura��o da ORM com seeders para ambiente de desenvolvimento
- Middleware de informa��o de stacktrace para ambiente desenvolvimento
- Configura��o de Gest�o de *packages* e configura��es de projetos centralizada
- RadditMQ:
   - Aprendizagem
   - Configura��o
   - Interliga��o com o Internet Banking
   - Cria��o de eventos de classe 1 e 2 (Queue 1 e 2)
- Integra��o com package Mapping de entidades ( Mapperly )
- Testes unitarios 
   - Sistemas desenvolvidos
   - Valida��o dos eventos classe 1 e 2
   - Valida��o de classes
   - Valida��o de mapper
- Integra��es
   - Chamadas dinamicas com aten��o a integra��o dos sistemas de intera��o (login/start interaction) 
- Orquestra��o
   - Configura��o e adapta��o do .Net Aspire
   - Compatibilidade com docker
- Repositorio
   - Pipelines para deploy nos diversos ambientes
