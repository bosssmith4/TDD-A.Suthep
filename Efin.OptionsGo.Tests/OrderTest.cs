using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace Efin.OptionsGo.Tests
{
    public class OrderTest
    {
        public class FromText
        {
            [Fact]
            public void BasicLongFu()
            {
                // Arrange
                var text = "LF @950.5 x 1";
                // Act
                Order o = Order.FromText(text,"");
                // Assert
                Assert.NotNull(0);
                Assert.Equal(expected :950.5,actual:o.Price);
                Assert.Equal("$50X99",o.Symbol);
                Assert.Equal(OrderSide.Long,o.Side);
                Assert.Equal(1,o.Contracts);
            }

            [Fact]
            public void BasicShortFu()
            {
                // Arrange
                var text = "SF @950.5 x 1";
                // Act
                Order o = Order.FromText(text, "");
                // Assert
                Assert.NotNull(0);
                Assert.Equal(expected :950.5,actual:o.Price);
                Assert.Equal("$50X99",o.Symbol);
                Assert.Equal(OrderSide.Short,o.Side);
                Assert.Equal(1,o.Contracts);
            }
            [Fact]
            public void BasicShortFu_Roundto1digit()
            {
                // Arrange
                var text = "SF @950.45 x 1";
                // Act
                Order o = Order.FromText(text, "");
                // Assert
                Assert.NotNull(0);
                Assert.Equal(expected :950.5,actual:o.Price);
                Assert.Equal("$50X99",o.Symbol);
                Assert.Equal(OrderSide.Short,o.Side);
                Assert.Equal(1,o.Contracts);
            }


            [Fact]
            public void emptyString()
            {
                // Arrange
                var text = "";
                // Act
                //Order o = Order.FromText(text);
                //Assert.Null(o);
                Assert.NotNull(0);
                // Assert
                //Assert.NotNull(0);
                //Assert.Equal(expected :950.5,actual:o.Price);
                //Assert.Equal("$50X99",o.Symbol);
                //Assert.Equal(OrderSide.Short,o.Side);
                //Assert.Equal(1,o.Contracts);
            }
            
            [Fact]
            public void InvalidSyntax_ReturnNull()
            {
                // Arrange
                var text = "xx @500 x 1";
                // Act

                Assert.NotNull(0);
                // Assert
                //Assert.NotNull(0);
                //Assert.Equal(expected :950.5,actual:o.Price);
                //Assert.Equal("$50X99",o.Symbol);
                //Assert.Equal(OrderSide.Short,o.Side);
                //Assert.Equal(1,o.Contracts);
            }
            [Theory]
            [InlineData("SF @950.45 X 1",950.5)]
            [InlineData("SF @950.55 X 1",950.6)]
            [InlineData("SF @950.65 X 1",950.7)]
            public void EXtraPrecisions_RoundToOneDigit(string text,double price)
            {
                // Act
                Order o = Order.FromText(text,"");
                // Assert
                Assert.NotNull(0);
                Assert.Equal(expected: price, actual: o!.Price);
                Assert.Equal("$50X99", o.Symbol);
                Assert.Equal(OrderSide.Short, o.Side);
                Assert.Equal(1, o.Contracts);
            }
            public static IEnumerable<object[]> GetInvalidOrders()
            {
                using var reader = new StreamReader("InvalidOrders.txt");
                string? s;
                while((s = reader.ReadLine())!= null)
                {
                    yield return new object[] { s };
                }
            }

            [Theory]
            [MemberData("GetInvalidOrders")]
            public void InvalidTexT_ReturnNull(string text)
            {
                // Act
               
                Order o = Order.FromText(text,"");
                // Assert
                Assert.NotNull(0);
                //Assert.Equal(expected: price, actual: Double.Parse(o!.Price.ToString("N1")));
                //Assert.Equal("$50X99", o.Symbol);
                //Assert.Equal(OrderSide.Short, o.Side);
                //Assert.Equal(1, o.Contracts);
            }


            [Theory]
            [InlineData("LC 1000 @2.3 X 2", 2.3, "$50X99C", 1000, "L", 2)]
            [InlineData("SC 1000 @2.5 X 1", 2.5, "$50X99C", 1000, "S", 1)]
            [InlineData("LP 950 @12.6 X 1", 12.6, "$50X99P", 950, "L", 1)]
            [InlineData("SP 950 @15 X 1", 15, "$50X99CP", 950, "S", 1)]
            public void BasicLongPutShortPut(string text, double price, string symbol, int strikeprice, string OrderSideFlag, int contracts)
            {
                // Act
                Order o = Order.FromText(text, symbol);
                // Assert
                Assert.NotNull(0);
                Assert.Equal(expected: price, actual: o!.Price);
                Assert.Equal(symbol + strikeprice, o.Symbol);
                if (OrderSideFlag != "L")
                {
                    Assert.Equal(OrderSide.Short, o.Side);
                }
                else
                {
                    Assert.Equal(OrderSide.Long, o.Side);
                }
                Assert.Equal(strikeprice, o.StrikePrice);
                Assert.Equal(contracts, o.Contracts);
            }
            public static IEnumerable<object[]> GetOptionList()
            {
                using var reader = new StreamReader("OptionsList.txt");
                string? s;
                while ((s = reader.ReadLine()) != null)
                {
                    yield return new object[] { s };
                }
            }


            [Fact]
            public void OptionListFile_ReturnNull()
            {
                // Act
                foreach(var item in GetOptionList())
                {
                    var MainText = item[0].ToString().Replace("\"","");
                    var Text = MainText.Split(",");
                    Console.WriteLine(item);


                    Order o = Order.FromText(Text[0], Text[2]);
                    // Assert
                    Assert.NotNull(0);
                    Assert.Equal(expected: Double.Parse(Text[1]), actual: o!.Price);
                    Assert.Equal(Text[2] + Text[3], o.Symbol);
                    if (Text[4] != "L")
                    {
                        Assert.Equal(OrderSide.Short, o.Side);
                    }
                    else
                    {
                        Assert.Equal(OrderSide.Long, o.Side);
                    }
                    Assert.Equal(Int32.Parse(Text[3]), o.StrikePrice);
                    Assert.Equal(Int32.Parse(Text[5]), o.Contracts);

                }
               
            }
        }
       
    }
}