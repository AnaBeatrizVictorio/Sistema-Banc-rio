# Sistema Bancário em C#

Este projeto demonstra os principais conceitos de Programação Orientada a Objetos (POO) em C#, implementando um sistema bancário simples com **Abstração**, **Encapsulamento** e **Herança**.

## Arquitetura do Sistema

### Conceitos de POO Implementados

#### 1. **ABSTRAÇÃO**

- **Interface `ITransacao`**: Define o contrato para operações bancárias (Sacar, Depositar, Transferir, ExibirExtrato)
- **Classe Abstrata `Conta`**: Define o comportamento comum para todos os tipos de conta

#### 2. **ENCAPSULAMENTO**

- **Campos Privados**: Todos os dados sensíveis são protegidos (`private`)
- **Propriedades Públicas**: Acesso controlado através de getters/setters com validação
- **Métodos Protegidos**: Funcionalidades internas não expostas externamente

#### 3. **HERANÇA**

- **`ContaCorrente`**: Herda de `Conta` e implementa lógica específica (limite de crédito, taxa de operação)
- **`ContaPoupanca`**: Herda de `Conta` e implementa lógica específica (rendimento, sem taxa)

## Estrutura do Projeto

```
SistemaBancario/
├── Program.cs           # Programa principal com demonstração
├── ITransacao.cs        # Interface para operações bancárias
├── Cliente.cs           # Classe Cliente com encapsulamento
├── Conta.cs             # Classe abstrata Conta
├── ContaCorrente.cs     # Herança: Conta Corrente
├── ContaPoupanca.cs     # Herança: Conta Poupança
├── Banco.cs             # Classe para gerenciar todas as contas
└── README.md            # Documentação do projeto
```

## 🚀 Funcionalidades

### Cliente

- ✅ Validação de dados (CPF, idade, etc.)
- ✅ Propriedade calculada para idade
- ✅ Encapsulamento completo dos dados pessoais

### Conta (Classe Abstrata)

- ✅ Operações básicas: Sacar, Depositar, Transferir
- ✅ Extrato bancário detalhado
- ✅ Sistema de numeração automática
- ✅ Histórico de movimentações

### Conta Corrente

- ✅ Limite de crédito configurável
- ✅ Taxa de operação mensal (1%)
- ✅ Controle de limite de saque (R$ 5.000)
- ✅ Uso inteligente do limite de crédito

### Conta Poupança

- ✅ Rendimento mensal configurável
- ✅ Sem taxa de operação
- ✅ Limite de saque menor (R$ 1.000)
- ✅ Cálculo de rendimento projetado

### Banco

- ✅ Gerenciamento centralizado de contas
- ✅ Relatórios gerenciais
- ✅ Transferências entre contas
- ✅ Rankings e estatísticas

## Demonstrações de POO

### Polimorfismo

```csharp
List<Conta> todasAsContas = banco.ListarContas();
foreach (Conta conta in todasAsContas)
{
    conta.AplicarTaxaMensal(); // Cada tipo tem sua implementação
    Console.WriteLine(conta.ToString()); // Polimorfismo em ação
}
```

### Interface (Abstração)

```csharp
ITransacao transacao = contaCorrente1;
transacao.Depositar(100m);
transacao.ExibirExtrato();
```

### Herança e Sobrescrita

```csharp
public class ContaCorrente : Conta
{
    public override bool Sacar(decimal valor)
    {
        // Implementação específica para conta corrente
        // Inclui lógica de limite de crédito
    }
}
```

## Como Executar

1. **Pré-requisitos**:

   - .NET 6.0 ou superior
   - Visual Studio ou VS Code

2. **Execução**:

   ```bash
   cd SistemaBancario
   dotnet run
   ```

3. **Compilação**:
   ```bash
   dotnet build
   ```

## Exemplo de Saída

O programa demonstra:

- ✅ Criação de clientes e contas
- ✅ Operações bancárias (depósitos, saques, transferências)
- ✅ Aplicação de rendimentos e taxas
- ✅ Relatórios gerenciais
- ✅ Demonstração de polimorfismo e interfaces
