# CleanArchitecture.WorkerService
Solution template using clean arch for building a .Net 8 Worker Service
Criação de novo projeto utilizando a versão .net 8, versão *Long-Term Support* lançada a 14 de novembro de 2023.

# Arquitetura
Sistema monolítico com uma abordagem a arquitetura de *Clean Architecture* sendo desdobrado na área *Core* (inclui a areas de *Domain* e *Application*), *Infraestrutura* (inclui a própria área  *Infraestrutura* e a *Persistência*) e por ultimo a área de Apresentação onde é implementado a parte mais exterior serviço.

# Tarefas:
- Sistemas de Logs
- Sistemas de Resources
- Sistema de falha em eventos
- Estruturação da ORM com seeders para ambiente de desenvolvimento
- Middleware de informação de stacktrace para ambiente desenvolvimento
- Configuração de Gestão de *packages* e configurações de projetos centralizada
- RadditMQ:
   - Aprendizagem
   - Configuração
   - Interligação com o Internet Banking
   - Criação de eventos de classe 1 e 2 (Queue 1 e 2)
- Integração com package Mapping de entidades ( Mapperly )
- Testes unitarios 
   - Sistemas desenvolvidos
   - Validação dos eventos classe 1 e 2
   - Validação de classes
   - Validação de mapper
- Integrações
   - Chamadas dinamicas com atenção a integração dos sistemas de interação (login/start interaction) 
- Orquestração
   - Configuração e adaptação do .Net Aspire
   - Compatibilidade com docker
- Repositorio
   - Pipelines para deploy nos diversos ambientes
