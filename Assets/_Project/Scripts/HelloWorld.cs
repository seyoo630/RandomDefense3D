using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helloworld
{


    class Program : MonoBehaviour
    {
        public void onAwake()
        {
            Car myCar = new Car();
            myCar.Color = "red";
            myCar.model = "Toyota";

            myCar.Drive();

            Console.WriteLine($"The car model is {myCar.model}, and color is {myCar.Color}");
        }
    }

    public class Car
    {
        public string Color;
        public string model;

        public void Drive()
        {
            Console.WriteLine("the car is driving");
        }

    }

}