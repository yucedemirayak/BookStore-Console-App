using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public abstract class BaseClass
    {
        public const int MIN_ID = 100000;
        public const int MAX_ID = 999999;

        public int Id { get; set; }
        public DateTime UpdatedTime { get; set; }

        public static int GenerateRandomId(int _MinimumValue, int _MaximumValue)
        {
            Random random = new Random();
            int randomValue = random.Next(_MinimumValue, _MaximumValue);
            return randomValue;
        }
        public BaseClass()
        {
            Id = GenerateRandomId(MIN_ID, MAX_ID);
            UpdatedTime = DateTime.Now;
        }        
    }
}
