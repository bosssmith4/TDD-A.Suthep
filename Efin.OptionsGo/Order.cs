namespace Efin.OptionsGo
{

    public class Order
    {
        public Order(double price,string symbol,OrderSide side,int contracts,int strikeprice)
        {
            Price= price;
            Symbol= symbol;
            Side= side;
            Contracts= contracts;
            StrikePrice = strikeprice;
        }
        public double Price { get; }
        public string Symbol { get; } = null!;
        public OrderSide Side { get; }
        public int Contracts { get; }
        public int StrikePrice { get; }

        // "LF 950 x 1"
        // Price = 950 , Symbol = $50x99,Side = "Long", Contracts =1
        public static Order FromText(string text,string Symbol)
        {
            try
            {

                var SideType = text.Split(" ")[0].ToUpper();
                Console.WriteLine(SideType);
                double price = 0;
                string symbol = string.Empty;
                var side = new OrderSide();
                int contracts = 0;
                int strikeprice = 0;

                if (SideType == "LF")
                {
                    price = Double.Parse(text.Split(" ")[1].Replace("@", ""));
                    side = OrderSide.Long;
                    contracts = Int32.Parse(text.Split(" ")[3]);
                    symbol = "$50X99";
                }
                else if (SideType == "SF")
                {
                    price = Double.Parse(text.Split(" ")[1].Replace("@", ""));
                    side = OrderSide.Short;
                    contracts = Int32.Parse(text.Split(" ")[3]);
                    symbol = "$50X99";
                }
                else if (SideType == "LC")
                {
                    price = Double.Parse(text.Split(" ")[2].Replace("@", ""));
                    side = OrderSide.Long;
                    contracts = Int32.Parse(text.Split(" ")[4]);
                    symbol = Symbol+ text.Split(" ")[1];
                    strikeprice = Int32.Parse(text.Split(" ")[1]);
                }
                else if (SideType == "SC")
                {
                    price = Double.Parse(text.Split(" ")[2].Replace("@", ""));
                    side = OrderSide.Short;
                    contracts = Int32.Parse(text.Split(" ")[4]);
                    symbol = Symbol + text.Split(" ")[1];
                    strikeprice = Int32.Parse(text.Split(" ")[1]);
                }
                else if (SideType == "LP")
                {
                    price = Double.Parse(text.Split(" ")[2].Replace("@", ""));
                    side = OrderSide.Long;
                    contracts = Int32.Parse(text.Split(" ")[4]);
                    symbol = Symbol + text.Split(" ")[1];
                    strikeprice = Int32.Parse(text.Split(" ")[1]);
                }
                else if (SideType == "SP")
                {
                    price = Double.Parse(text.Split(" ")[2].Replace("@", ""));
                    side = OrderSide.Short;
                    contracts = Int32.Parse(text.Split(" ")[4]);
                    symbol = Symbol + text.Split(" ")[1];
                    strikeprice = Int32.Parse(text.Split(" ")[1]);
                }

                price = Double.Parse(String.Format("{0:0.0}", price));
                //price = Math.Round((Double)price, 1);
                var o = new Order(price, symbol, side, contracts,strikeprice);

                return o;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
    }
}