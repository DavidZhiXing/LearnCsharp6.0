using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    public class StockPortfolio : IEnumerable<Stock>
    {
        private List<Stock> _stocks;

        public StockPortfolio()
        {
            _stocks = new List<Stock>();
        }
        public void Add(string ticker,double gainLoss)
        {
            _stocks.Add(new Stock() { Ticker = ticker, GainLoss = gainLoss });
        }

        public IEnumerable<Stock> GetWorstPerformers(int topNumber) =>
            _stocks.OrderBy((Stock stock) => stock.GainLoss).Take(topNumber);
        public void SellStocks(IEnumerable<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                _stocks.Remove(stock);
            }
        }

        public void PrintPortfolio(string title)
        {
            Console.WriteLine(title);
            _stocks.DisplayStocks();
        }
        public IEnumerator<Stock> GetEnumerator() => _stocks.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public class Stock
    {
        public double GainLoss { get; internal set; }
        public string Ticker { get; internal set; }
    }

    public struct Data
    {
        public Data(int intData=2,float floatData=1.1f,string strData="aaa",
            char charData='A',bool boolData=true)
        {
            IntData = intData;
            FloatData = floatData;
            StrData = strData;
            CharData = charData;
            BoolData = boolData;
        }

        public bool BoolData { get; private set; }
        public char CharData { get; private set; }
        public float FloatData { get; private set; }
        public int IntData { get; private set; }
        public string StrData { get; private set; }

        public override string ToString() =>
            $"{IntData} :: {FloatData} :: {StrData?.ToUpper()} :: {CharData} :: {BoolData}";
    }
}
