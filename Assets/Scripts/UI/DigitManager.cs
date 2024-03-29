﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DigitManager : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _numbers;

        public Sprite GetDigitSprite(float digit)
        {
            var index = (int) digit;
            
            if (index < 0 || index > 9)
            {
                throw new ArgumentException("Index should be between 0 and 9");
            }
            
            return _numbers[index];
        }
    }
}