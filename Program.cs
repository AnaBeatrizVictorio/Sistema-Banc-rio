using System;

namespace SistemaBancario
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA BANCÁRIO ===\n");

            // Criando o banco
            Banco banco = new Banco("Banco Digital C#");

            // Criando clientes
            Cliente cliente1 = new Cliente("João Silva", "12345678901", new DateTime(1985, 5, 15), "Rua A, 123");
            Cliente cliente2 = new Cliente("Maria Santos", "98765432109", new DateTime(1990, 8, 22), "Rua B, 456");
            Cliente cliente3 = new Cliente("Pedro Oliveira", "11122233344", new DateTime(1978, 12, 3), "Rua C, 789");

            Console.WriteLine("=== CRIANDO CLIENTES ===");
            Console.WriteLine(cliente1.ToString());
            Console.WriteLine("\n" + cliente2.ToString());
            Console.WriteLine("\n" + cliente3.ToString());

            // Criando contas
            Console.WriteLine("\n=== CRIANDO CONTAS ===");
            
            ContaCorrente contaCorrente1 = new ContaCorrente(cliente1, 2000m);
            ContaPoupanca contaPoupanca1 = new ContaPoupanca(cliente1, 0.006m);
            
            ContaCorrente contaCorrente2 = new ContaCorrente(cliente2, 1500m);
            ContaPoupanca contaPoupanca2 = new ContaPoupanca(cliente2, 0.007m);
            
            ContaCorrente contaCorrente3 = new ContaCorrente(cliente3, 3000m);

            // Adicionando contas ao banco
            banco.AdicionarConta(contaCorrente1);
            banco.AdicionarConta(contaPoupanca1);
            banco.AdicionarConta(contaCorrente2);
            banco.AdicionarConta(contaPoupanca2);
            banco.AdicionarConta(contaCorrente3);

            // Operações nas contas
            Console.WriteLine("\n=== REALIZANDO OPERAÇÕES ===");
            
            // Depósitos
            contaCorrente1.Depositar(1500m);
            contaPoupanca1.Depositar(5000m);
            contaCorrente2.Depositar(800m);
            contaPoupanca2.Depositar(3000m);
            contaCorrente3.Depositar(2000m);

            // Saques
            Console.WriteLine("\n--- Tentando saques ---");
            contaCorrente1.Sacar(300m);
            contaPoupanca1.Sacar(200m);
            contaCorrente2.Sacar(1000m); // Vai usar limite de crédito
            contaCorrente2.Sacar(2000m); // Vai exceder limite

            // Transferências
            Console.WriteLine("\n--- Realizando transferências ---");
            banco.RealizarTransferencia(contaCorrente1.NumeroConta, contaPoupanca1.NumeroConta, 500m);
            banco.RealizarTransferencia(contaCorrente3.NumeroConta, contaCorrente2.NumeroConta, 300m);

            // Aplicando rendimento na poupança
            Console.WriteLine("\n--- Aplicando rendimento nas poupanças ---");
            contaPoupanca1.AplicarRendimento();
            contaPoupanca2.AplicarRendimento();

            // Exibindo extratos
            Console.WriteLine("\n=== EXTRATOS DAS CONTAS ===");
            contaCorrente1.ExibirExtrato();
            contaPoupanca1.ExibirExtrato();

            // Relatórios do banco
            banco.ExibirRelatorioGeral();
            banco.ExibirRankingMaioresSaldos(3);

            // Demonstração de polimorfismo
            Console.WriteLine("\n=== POLIMORFISMO ===");
            List<Conta> todasAsContas = banco.ListarContas();
            
            foreach (Conta conta in todasAsContas)
            {
                Console.WriteLine(conta.ToString()); // Cada tipo chama sua própria implementação
                
                // Demonstração de polimorfismo com método virtual
                conta.AplicarTaxaMensal();
                Console.WriteLine($"Saldo após taxa/rendimento: {conta.Saldo:C}\n");
            }

            // Demonstração de interface
            Console.WriteLine("\n=== INTERFACE ===");
            ITransacao transacao = contaCorrente1;
            transacao.Depositar(100m);
            transacao.ExibirExtrato();


            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
