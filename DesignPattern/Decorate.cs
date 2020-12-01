namespace DesignPattern
{
    public abstract class 飲料
    {
        public abstract string GetDescription();

        public abstract decimal GetCost();
    }

    public class 珍珠奶茶 : 飲料
    {
        public override string GetDescription() => "珍珠奶茶";

        public override decimal GetCost() => 30;
    }

    public abstract class 加料飲料 : 飲料
    {
        protected 飲料 _drink;
    }

    public class 椰果 : 加料飲料
    {
        public 椰果(飲料 drink)
        {
            _drink = drink;
        }

        public override decimal GetCost()
            => _drink.GetCost() + 5;

        public override string GetDescription()
            => _drink.GetDescription() + ", +椰果";
    }

    public class 檸檬 : 加料飲料
    {
        public 檸檬(飲料 drink)
        {
            _drink = drink;
        }

        public override decimal GetCost()
            => _drink.GetCost() + 10;

        public override string GetDescription()
            => _drink.GetDescription() + ", +檸檬";
    }

    public class 粉條 : 加料飲料
    {
        public 粉條(飲料 drink)
        {
            _drink = drink;
        }

        public override decimal GetCost()
            => _drink.GetCost() + 5;

        public override string GetDescription()
            => _drink.GetDescription() + ", +粉條";
    }
}
