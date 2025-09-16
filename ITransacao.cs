using System;

namespace SistemaBancario
{
    /// <summary>
    /// Interface que define as operações de transação bancária
    /// Demonstra o conceito de ABSTRAÇÃO
    /// </summary>
    public interface ITransacao
    {
        bool Sacar(decimal valor);
        bool Depositar(decimal valor);
        bool Transferir(Conta contaDestino, decimal valor);
        void ExibirExtrato();
    }
}
