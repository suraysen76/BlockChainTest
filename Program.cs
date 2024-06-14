using BlockChainTest.Models;
using Newtonsoft.Json;
using System;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace BlockChainTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start!");
            Blockchain aCoin = new Blockchain();
            aCoin.AddBlock(new Block(DateTime.Now, null, "{A sends to B,amount:30}"));
            aCoin.AddBlock(new Block(DateTime.Now, null, "{B sends to C,amount:20}"));
            aCoin.AddBlock(new Block(DateTime.Now, null, "{C sends to D,amount:10}"));
            Console.WriteLine("First:"+ JsonConvert.SerializeObject(aCoin.Chain[1], Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {IsValid(aCoin)}");
            Console.WriteLine($"Update amount to 1000");
            aCoin.Chain[1].Data = "{2.Henry sends to MaHesh,amount:1000}";
            Console.WriteLine("Second:"+ JsonConvert.SerializeObject(aCoin.Chain[1], Formatting.Indented));
            Console.WriteLine($"Is Chain Valid: {IsValid(aCoin)}");
        }
        public static bool IsValid(Blockchain bchain)
        {
            for (int i = 1; i < bchain.Chain.Count; i++)
            {
                Block currentBlock = bchain.Chain[i];
                Block previousBlock = bchain.Chain[i - 1];
                var currentBlockHash = currentBlock.CalculateHash();
                if (currentBlock.Hash != currentBlockHash)
                {
                    Console.WriteLine($"hash is different for index :"+i);
                    Console.WriteLine($"Block Hash :" + currentBlock.Hash);
                    Console.WriteLine($"Calculated Block Hash :" + currentBlock.Hash);
                    return false;
                }
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
    }
}