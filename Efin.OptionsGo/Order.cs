namespace Efin.OptionsGo
{

    public class Order
    {
        public Order(double price, string symbol, OrderSide side, int contracts, int strikeprice)
        {
            Price = price;
            Symbol = symbol;
            Side = side;
            Contracts = contracts;
            StrikePrice = strikeprice;
        }
        public double Price { get; }
        public string Symbol { get; } = null!;
        public OrderSide Side { get; }
        public int Contracts { get; }
        public int StrikePrice { get; }

        // "LF 950 x 1"
        // Price = 950 , Symbol = $50x99,Side = "Long", Contracts =1
        public static Order? FromText(string text)
        {
            try
            {
                var data = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var sideType = data[0].ToUpper();
                Console.WriteLine(sideType);
                double price = 0;
                string symbol = "S50X99";
                OrderSide side;
                int contracts = 0;
                int strikePrice = 0; // CamelCase

                if (sideType == "LF")
                {
                    price = double.Parse(data[1].Replace("@", ""));
                    side = OrderSide.Long;
                    contracts = Int32.Parse(data[3]);
                }
                else if (sideType == "SF")
                {
                    price = double.Parse(data[1].Replace("@", ""));
                    side = OrderSide.Short;
                    contracts = Int32.Parse(data[3]);
                }
                else if (sideType == "LC")
                {
                    price = double.Parse(data[2].Replace("@", ""));
                    side = OrderSide.Long;
                    contracts = Int32.Parse(data[4]);
                    strikePrice = Int32.Parse(data[1]);
                    symbol += "C"+ strikePrice;
                }
                else if (sideType == "SC")
                {
                    price = double.Parse(data[2].Replace("@", ""));
                    side = OrderSide.Short;
                    contracts = Int32.Parse(data[4]);
                    strikePrice = Int32.Parse(data[1]);
                    symbol += "C" + strikePrice;
                }
                else if (sideType == "LP")
                {
                    price = double.Parse(data[2].Replace("@", ""));
                    side = OrderSide.Long;
                    contracts = Int32.Parse(data[4]);
                    strikePrice = Int32.Parse(data[1]);
                    symbol += "P" + strikePrice;
                }
                else if (sideType == "SP")
                {
                    price = double.Parse(data[2].Replace("@", ""));
                    side = OrderSide.Short;
                    contracts = Int32.Parse(data[4]);
                    strikePrice = Int32.Parse(data[1]);
                    symbol += "P" + strikePrice;
                }
                else
                {
                    return null;
                }

                //price = double.Parse(String.Format("{0:0.0}", price));
                price = Math.Round((double)price, 1,MidpointRounding.AwayFromZero);
                var o = new Order(price, symbol, side, contracts, strikePrice);

                return o;
            }
            catch 
            {
                return null;
            }

        }
    }
}