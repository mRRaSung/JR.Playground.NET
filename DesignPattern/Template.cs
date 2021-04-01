using System;

namespace DesignPattern
{
    /// <summary>
    /// 統一執行步驟，提供部分步驟Override，或開放Hook控制步驟
    /// </summary>
    public abstract class Beverage
    {
        public void Prepare()
        {
            Step1();

            Step2();

            Step3();

            if (ExecuteStep4())
                Step4();
        }

        public void Step1() { }

        public void Step2() { }

        public abstract void Step3();

        public abstract void Step4();

        public virtual bool ExecuteStep4() => true;
    }

    public class Coffee : Beverage
    {
        public override void Step3()
        {
            throw new NotImplementedException();
        }

        public override void Step4()
        {
            throw new NotImplementedException();
        }

        public override bool ExecuteStep4()
        {
            return base.ExecuteStep4();
        }
    }
}
