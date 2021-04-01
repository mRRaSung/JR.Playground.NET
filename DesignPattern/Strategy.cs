using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Car
    {
        IAttackBehavior _attack;

        public void SetAttack(IAttackBehavior atk)
        {
            _attack = atk;
        }

        public void SpeedUp()
        {
            Console.WriteLine("加速中");
        }

        public void SpeedDown()
        {
            Console.WriteLine("減速中");
        }

        public void UseNOS()
        {
            Console.WriteLine("NOS");
        }

        public void Attatck()
        {
            Console.WriteLine(_attack?.Attake()?? "Attack Fail");
        }
    }

    public class PorscheCar : Car
    {
        public PorscheCar()
        {
            
        }
    }

    public class AmbulanceCar : Car
    {
    }

    public interface IAttackBehavior
    {
        public string Attake();
    }

    public class GunShot : IAttackBehavior
    {
        public string Attake()
        {
            return "Gun Shoot, you already died";
        }
    }

    public class KnifeSplash : IAttackBehavior
    {
        public string Attake()
        {
            return "||||||++++";
        }
    }
}
