namespace DesignPattern
{
    /// <summary>
    /// 成品
    /// </summary>
    public class Product
    {
        public string Att1 { get; set; }
        public string Att2 { get; set; }
        public string Att3 { get; set; }
    }

    /// <summary>
    /// 建立者
    /// </summary>
    public interface IBuilder
    {
        public Product Build();
        public Builder SetAttr1(string attr1);
        public Builder SetAttr2(string attr2);
        public Builder SetAttr3(string attr3);
    }

    public class Builder : IBuilder
    {
        private Product prod;

        public Builder()
        {
            prod = new Product();
        }

        public Product Build()
        {
            return prod;
        }
        public Builder SetAttr1(string attr1)
        {
            prod.Att1 = attr1;
            return this;
        }
        public Builder SetAttr2(string attr2)
        {
            prod.Att2 = attr2;
            return this;
        }
        public Builder SetAttr3(string attr3)
        {
            prod.Att3 = attr3;
            return this;
        }
    }

    /// <summary>
    /// 指導者
    /// </summary>
    public class Director
    {
        private IBuilder builder;

        public Director(IBuilder builder)
        {
            this.builder = builder;
        }

        public Product Arrange()
        {
            return builder.SetAttr1("A")
                          .SetAttr2("B")
                          .SetAttr3("C")
                          .Build();
        }

    }



    public class MyStringBuilder
    {
        private string Result { get; set; }

        public MyStringBuilder Append(string message)
        {
            Result += message;
            return this;
        }

        public override string ToString()
        {
            return Result;
        }
    }
}
