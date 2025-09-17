using System;

namespace SistemaBancario
{

    /// Interface que define as operações de transação bancária
    /// Demonstra o conceito de ABSTRAÇÃO

    public interface ITransacao
    {
        bool Sacar(decimal valor);
        bool Depositar(decimal valor);
        bool Transferir(Conta contaDestino, decimal valor);
        void ExibirExtrato();
    }
}
