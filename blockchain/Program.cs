using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Transactions;

namespace blockchain
{
    public static class CRYPTO
    {
        public static string botyHash(string p_inp)
        {
            throw new NotImplementedException();
        }
    }
    public static class App
    {
        public static void Main(string[] args)
        {
            
        }
        //We need to know whether the chain is still valid or not, so we need a method to check validation.
        //The chain is valid if a block's hash is equal to what its hashing method returns, and a block's 
        //prevHash property should be equal to the previous block's hash.
        public static bool isBlockchainValid(Blockchain p_blockchain)
        {
            // Iterate over the chain, we need to set i to 1 because there are nothing before the genesis block, so we start at the second block.
            for (int blockInd = 1; blockInd < p_blockchain.blocks.Count; blockInd++)
            {
                Block currentBlock = p_blockchain.blocks[blockInd];
                Block prevBlock = p_blockchain.blocks[blockInd - 1];
                // Check validation
                if (currentBlock.hash != currentBlock.calcHash() || prevBlock.hash != currentBlock.prevHash)
                {
                    return false;
                }
            }
            bool isValid = false;
            return isValid;
        }
        public static bool isTransactionValid(Transaction p_transaction) { return false; }
        public static bool isSignatureValid(Transaction p_transaction) { return false; }    //pkey,signature aka prkeyencrypted origijsondata,originaljsondata
    }

    public class Blockchain
    {
        //PROPERTIES
        public List<Block> blocks;
        int miningDifficulty = 1;

        //CONSTRUCTORS
        public Blockchain()
        { //read seq inp file
        }

        //METHODS
        public Block getLastBlock()
        {
            return this.blocks.Last();
        }
        public void addBlock(Block p_block)
        {
            // Since we are adding a new block, prevHash will be the hash of the old latest block
            p_block.prevHash = this.getLastBlock().hash;
            // Since now prevHash has a value, we must reset the block's hash
            p_block.calcHash();
            // Let's find the proper nonce
            p_block.mine(this.miningDifficulty);

            this.blocks.Add(p_block);
        }
    }

    public class Block
    {
        //PROPERTIES
        public int nonce = new Random().Next(int.MinValue, int.MaxValue);
        public string hash = "";
        public string prevHash = "";
        public Transaction transaction;
        public DateTime timeStamp;

        //CONSTRUCTORS
        public Block(string p_prevHash, Transaction p_transaction, DateTime p_timeStamp)
        {
            this.prevHash = p_prevHash;
            this.transaction = p_transaction;
            this.timeStamp = p_timeStamp;
        }

        //METHODS
        public string calcHash()
        {
            string TransactionJsonRepresentation = JsonSerializer.Serialize(this.transaction);
            string hash = CRYPTO.botyHash(TransactionJsonRepresentation + this.prevHash + this.timeStamp + this.nonce);

            return hash;
        }

        public void mine(int p_difficulty)
        {
            //// Basically, it loops until our hash starts with 
            //// the string 0...000 with length of <difficulty>.
            //while (!this.hash.startsWith(Array(difficulty + 1).join("0")))
            //{
            //    // We increases our nonce so that we can get a whole different hash.
            //    this.nonce++;
            //    // Update our new hash with the new nonce value.
            //    this.hash = this.getHash();
            //}
        }

        public class Transaction
        {
            //PROPERTIES
            public int amount;
            public string payer;//public key
            public string payee;//public key
            public string signature;

            //CONSTRUCTORS
            public Transaction(int p_amount, string p_payer, string p_payee)
            {
                this.amount = p_amount;
                this.payer = p_payer;
                this.payee = p_payee;
            }

        }

        public class Wallet // a wrapper for private key and public key
        {
            //PROPERTIES
            public string privateKey;   // unlike sha this is a two way encryption, it can be reversed
            public string publicKey;    // we use the public key to create a cypher text, which can be decrypted using the private key

            //CONSTRUCTORS
            public Wallet() { }

            //METHODS
            public void sign(Transaction p_transaction)
            {
                // creates a signature derived from private key
            }
        }
    }
}